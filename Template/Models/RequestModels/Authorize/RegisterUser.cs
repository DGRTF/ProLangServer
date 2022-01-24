namespace Template.Models.RequestModels.Authorize;

/// <summary>
/// Модель для регистрации пользовател
/// </summary>
public class RegisterUserModel
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