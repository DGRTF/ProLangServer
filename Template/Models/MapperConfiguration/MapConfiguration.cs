using AutoMapper;
using Template.Models.RequestModels;
using TemplateDataLayer.Models;

namespace Template.Models.MapperConfiguration
{
    public class MapConfiguration : Profile
    {
        public MapConfiguration()
        {
            CreateMap<CreateOrderModel, Order>();
        }
    }
}
