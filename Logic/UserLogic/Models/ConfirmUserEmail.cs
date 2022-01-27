namespace UserLogic.Models;

/// <summary>
/// Модель подтверждения email адреса пользователя
/// </summary>
public class ConfirmUserEmail
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Токен высланный на почтовый ящик для проверки
    /// </summary>
    public string Token { get; set; } = string.Empty;
}