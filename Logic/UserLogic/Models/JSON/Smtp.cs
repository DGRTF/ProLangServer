using System.Net;

namespace UserLogic.Models.JSON
{
    /// <summary>
    /// Настройки smtp
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// Порт почтового сервера
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// IP адрес почтового сервера
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}