using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserLogic.Models.JSON;

/// <summary>
/// Настройки JWT авторизации
/// </summary>
public class JWTAuthOptions
{
    /// <summary>
    /// Ключ шифрования
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Время жизни токена
    /// </summary>
    public int LifeTime { get; set; }

    /// <summary>
    /// Время жизни токена для получения новой пары
    /// </summary>
    public int RefreshLifeTime { get; set; }

    /// <summary>
    /// Издатель токена
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Потребитель токена
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Класс шифрования токена
    /// </summary>
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}