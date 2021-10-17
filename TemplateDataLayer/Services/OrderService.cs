using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;
using TemplateDataLayer.Models;
using TemplateDataLayer.Models.JSON;
using TemplateDataLayer.Models.ResponseModels;

namespace TemplateDataLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(ITemplateMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public async Task<FindOrdersResponseModel> GetOrders(int skip, int take)
        {
            var orders = await _orders.AsQueryable()
             .Skip(skip)
             .Take(take)
             .ToListAsync();

            var count = await _orders
                .AsQueryable()
                .CountAsync();

            return new FindOrdersResponseModel(orders, count);
        }

        public async Task<FindOrdersResponseModel> FindOrders(string customer, int skip, int take)
        {
            var orders = await _orders.AsQueryable()
             .Where(order => order.Customer == customer)
             .Skip(skip)
             .Take(take)
             .ToListAsync();

            var count = await _orders
                .AsQueryable()
                .CountAsync(order => order.Customer == customer);

            return new FindOrdersResponseModel(orders, count);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _orders.InsertOneAsync(order);

            return order;
        }

        public async Task DeleteOreder(Guid id) =>
           await _orders.DeleteOneAsync(order => order.Id == id);
    }
}
