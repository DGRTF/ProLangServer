using System;
using System.Threading.Tasks;
using TemplateDataLayer.Models;
using TemplateDataLayer.Models.ResponseModels;

namespace TemplateDataLayer.Services
{
    public interface IOrderService
    {
        public Task<FindOrdersResponseModel> GetOrders(int skip, int take);

        public Task<FindOrdersResponseModel> FindOrders(string customer, int skip, int take);

        public Task<Order> CreateOrder(Order order);

        public Task DeleteOreder(Guid id);
    }
}
