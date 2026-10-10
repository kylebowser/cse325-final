using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BreadOfLife.Components.Model;

namespace BreadOfLife.Components.Controllers;

[Route("product")]
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
}