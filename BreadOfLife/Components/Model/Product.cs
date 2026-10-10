using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BreadOfLife.Components.Model
{
    public class Product
    {

        [BsonId]
        [JsonIgnore]
        public ObjectId MongoId { get; set; }

        [BsonElement("Id")]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [Required, MinLength(5), MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MinLength(5), MaxLength(300)]
        public string Ingredients { get; set; }

        [Required]
        [MinLength(3), MaxLength(200)]
        public string Allergens { get; set; }

        [Required]
        [MinLength(3), MaxLength(200)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 9)]
        public int Days { get; set; }

        [Required]
        [MinLength(3), MaxLength(200)]
        public string Baker { get; set; }
    }
}