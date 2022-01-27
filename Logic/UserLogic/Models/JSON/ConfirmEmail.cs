namespace UserLogic.Models.JSON
{
    /// <summary>
    /// Представляет настройки для сообщения пользователю 
    /// о подтверждении email
    /// </summary>
    public class ConfirmEmail
    {
        /// <summary>
        /// От кого (здесь не указывается почтовый адрес, это произвольный текст)
        /// </summary>
        /// <value></value>
        public string By { get; set; } = string.Empty;

        /// <summary>
        /// Кому (здесь не указывается почтовый адрес, это произвольный текст)
        /// </summary>
        /// <value></value>
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Тема сообщения
        /// </summary>
        /// <value></value>
        public string Subject { get; set; } = string.Empty;
    }
}