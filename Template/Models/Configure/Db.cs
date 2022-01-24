namespace Template.Models.Configure
{
    /// <summary>
    /// Модель конфигурации подключения к базам данных
    /// </summary>
    public class Db
    {
        /// <summary>
        /// Конфигурация PostgreSql
        /// </summary>
        public PostgreSql PostgreSql { get; set; } = new PostgreSql();
    }
}