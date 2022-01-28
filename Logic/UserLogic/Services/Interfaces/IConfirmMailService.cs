namespace UserLogic.Services.Interfaces;

/// <summary>
/// Отправка токена подтверждения на почту
/// </summary>
public interface IConfirmMailService
{
    /// <summary>
    /// Отправляет сообщение
    /// </summary>
    /// <param name="uri">Uri для перехода по ссылке подтверждения</param>
    /// <param name="email">Электронная почта, на которую отправить ссылку</param>
    /// <returns>Результат отправки</returns>
    public Task SendMessage(string uri, string email);

    /// <summary>
    /// Отправляет сообщение
    /// </summary>
    /// <param name="password">Новый пароль</param>
    /// <param name="email">Электронная почта, на которую отправить пароль</param>
    /// <returns>Результат отправки</returns>
    public Task SendNewPassword(string password, string email);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="link">Ссылка для подтверждения сброса пароля</param>
    /// <param name="email">Электронная почта, на которую отправить </param>
    Task SendChangePasswordLink(string link, string email);
}