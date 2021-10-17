using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Template.Models.RequestModels;
using TemplateDataLayer.Models;
using TemplateDataLayer.Models.ResponseModels;
using TemplateDataLayer.Services;

namespace Template.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<FindOrdersResponseModel> Orders([FromQuery] GetOrdersModel model)
        {
            return await _orderService.GetOrders(model.Skip, model.Take);
        }

        [HttpPost]
        public async Task<Order> CreateOrder([FromBody] CreateOrderModel model)
        {
            var order = _mapper.Map<Order>(model);

            return await _orderService.CreateOrder(order);
        }

        [HttpDelete]
        public async Task DeleteOrder([FromQuery] Guid id)
        {
            await _orderService.DeleteOreder(id);
        }

        [HttpGet]
        public async Task<FindOrdersResponseModel> FindOrders([FromQuery] FindOrdersModel model)
        {
            return await _orderService.FindOrders(model.Customer, model.Skip, model.Take);
        }
    }
}
