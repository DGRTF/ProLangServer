namespace Api.Models.Configure
{
    /// <summary>
    /// Модель конфигурации PostgreSql
    /// </summary>
    public class PostgreSql
    {
        /// <summary>
        /// Строка подключение
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}