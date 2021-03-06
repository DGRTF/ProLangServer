using Api.Models.RequestModels.Authorize;
using AutoMapper;
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
        CreateMap<ConfirmUserEmailModel, ConfirmUserEmail>().ReverseMap();
        CreateMap<ChangePasswordModel, ChangePassword>().ReverseMap();
    }
}