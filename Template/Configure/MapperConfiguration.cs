using AutoMapper;
using Template.Models.RequestModels.Authorize;
using UserLogic.Models;

namespace Template.Configure;

/// <summary>
/// Конфигурация AutoMapper
/// </summary>
public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CreateMap<RegisterUserModel, RegisterUser>().ReverseMap();
        CreateMap<LoginUserModel, LoginUser>().ReverseMap();
    }
}