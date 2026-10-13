using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BreadOfLife.Models
{
    public class Baker
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ZipCode { get; set; }
    }

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string MissionaryName { get; set; }
        public string MissionName { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }
}