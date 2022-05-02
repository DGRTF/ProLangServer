using System.Security.Claims;
using UserLogic.Models;

namespace UserLogic.Services.Interfaces;

/// <summary>
/// Сервис получения JWT
/// </summary>
public interface IJwtGenerator
{
    /// <summary>
    /// Создает jwt из списка утверждений
    /// </summary>
    /// <param name="claims">Коллекция представлений</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Jwt</returns>
    public TokenPairs GetJwt(IReadOnlyList<Claim> claims, Guid userId);
}