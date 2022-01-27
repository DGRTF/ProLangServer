namespace UserLogic.Models.JSON;

/// <summary>
/// Представляет настройки Email для сервера
/// </summary>
public class EmailOptions
{
    /// <summary>
    /// Почтовый ящик с которого сервер отправляет сообщения по-умолчанию
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль от почтового ящика по-умолчанию
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Подтверждение email
    /// </summary>
    public ConfirmEmail ConfirmEmail { get; set; } = new ConfirmEmail();

    /// <summary>
    /// Настройки для подключения к серверу электронной почты
    /// </summary>
    public EmailHostOptions Host { get; set; } = new EmailHostOptions();
}