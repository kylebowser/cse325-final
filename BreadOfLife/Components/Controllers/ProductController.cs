using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BreadOfLife.Components.Model;
using MongoDB.Bson;

namespace BreadOfLife.Components.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : Controller
{
    private Product product = new();
    private readonly IMongoCollection<Product> _productCollection;

    public ProductsController(IMongoDatabase db)
    {
        _productCollection = db.GetCollection<Product>("product");
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return await _productCollection.Find(_ => true).ToListAsync();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string searchString)
    {
        // If the search is empty, just return all products
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return Ok(await _productCollection.Find(_ => true).ToListAsync());
        }

        // Create a case-insensitive regex for the search string
        var searchRegex = new BsonRegularExpression(searchString, "i");

        // Search where Name OR Baker matches the regex (single database trip, no duplicates)
        var filter = Builders<Product>.Filter.Regex(p => p.Name, searchRegex) |
                     Builders<Product>.Filter.Regex(p => p.Baker, searchRegex);

        var products = await _productCollection.Find(filter).ToListAsync();
        
        return Ok(products);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var product = await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product product)
    {
        await _productCollection.InsertOneAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.Id);
    }
}