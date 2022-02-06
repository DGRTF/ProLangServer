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
    /// Конструктор
    /// </summary>
    /// <param name="token">Jwt</param>
    public SucceededAuthorize(string token)
    {
        Token = token ?? string.Empty;
    }
}