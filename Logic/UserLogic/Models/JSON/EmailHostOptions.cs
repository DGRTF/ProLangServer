namespace UserLogic.Models.JSON
{
    /// <summary>
    /// Настройки для подключения к серверу электронной почты
    /// </summary>
    public class EmailHostOptions
    {
        /// <summary>
        /// Настройки smtp
        /// </summary>
        public SmtpOptions Smtp { get; set; } = new SmtpOptions();
    }
}