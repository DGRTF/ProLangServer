namespace Api.Models.ResponseModels.Authorize;

/// <summary>
/// Возвращает успешный ответ авторизации
/// </summary>
public class SucceededAuthorize
{
    /// <summary>
    /// Jwt
    /// </summary>
    /// <value></value>
    public string Token { get; set; }

    /// <summary>
    /// Токен получения новой пары токенов
    /// </summary>
    /// <value></value>
    public string RefreshToken { get; } = string.Empty;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="token">Jwt</param>
    public SucceededAuthorize(string token, string refreshToken)
    {
        Token = token ?? string.Empty;
        RefreshToken = refreshToken ?? string.Empty;
    }
}