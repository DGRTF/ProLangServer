namespace UserLogic.Models;

/// <summary>
/// Представляет пару токенов аторизации
/// </summary>
public class TokenPairs
{
    /// <summary>
    /// Регулярный токен
    /// </summary>
    /// <value></value>
    public string Token { get; } = string.Empty;

    /// <summary>
    /// Токен получения новой пары токенов
    /// </summary>
    /// <value></value>
    public string RefreshToken { get; } = string.Empty;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="token">Регулярный токен аторизации</param>
    /// <param name="refreshToken">Токен получения новой пары</param>
    public TokenPairs(string token, string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }
}