using Api.Models.Configure;
using UserLogic.Models.JSON;

namespace Template.Models;

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

    /// <summary>
    /// Настройки почтового сервера и связанного
    /// с почтой функционала
    /// </summary>
    public EmailOptions Email { get; set; } = new EmailOptions();

    /// <summary>
    /// Конфигурация текущего хоста
    /// </summary>
    public Api.Models.Configure.HostOptions Host { get; set; } = new Api.Models.Configure.HostOptions();
}