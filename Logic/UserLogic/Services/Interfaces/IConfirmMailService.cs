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
    /// <param name="email">Email пользователя</param>
    /// <returns></returns>
    public Task<bool> SendMessage(string uri, string email);
}