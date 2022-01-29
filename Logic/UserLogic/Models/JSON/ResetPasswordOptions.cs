namespace UserLogic.Models.JSON
{
    /// <summary>
    /// Представляет настройки отправки нового пароля по электронной почте
    /// </summary>
    public class ResetPasswordOptions
    {
        /// <summary>
        /// Тема сообщения
        /// </summary>
        /// <value></value>
        public string Subject { get; set; } = string.Empty;
    }
}