using System.Security.Claims;

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
    /// <returns>Jwt</returns>
    public string GetJwt(IReadOnlyList<Claim> claims);
}