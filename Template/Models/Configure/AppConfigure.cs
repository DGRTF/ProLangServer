using UserLogic.Models.JSON;

namespace Template.Models.Configure;

/// <summary>
/// Модель конфигурации всего приложения
/// </summary>
public class AppConfigure
{
    /// <summary>
    /// Конфигурация JWT
    /// </summary>
    public JWTAuthOptions JWTAuthOptions { get; set; } = new JWTAuthOptions();

    /// <summary>
    /// Конфигурация баз данных
    /// </summary>
    public Db Db { get; set; } = new Db();   
    
}