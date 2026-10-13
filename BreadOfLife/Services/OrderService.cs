using BreadOfLife.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BreadOfLife.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrderService(IMongoDatabase database)
        {
            _ordersCollection = database.GetCollection<Order>("order");
        }

        public async Task PlaceOrderAsync(Order order)
        {
            await _ordersCollection.InsertOneAsync(order);
        }
    }
}