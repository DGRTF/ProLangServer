namespace Template.Models.RequestModels.Authorize;

/// <summary>
/// Представляет модель для получения токена
/// </summary>
public class LoginUserModel
{
    /// <summary>
    /// User email
    /// </summary>
    /// <value></value>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User password
    /// </summary>
    /// <value></value>
    public string Password { get; set; } = string.Empty;
}