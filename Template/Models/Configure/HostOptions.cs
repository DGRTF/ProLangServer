namespace Template.Models.Configure;

/// <summary>
/// Текущий хост
/// </summary>
public class HostOptions
{
    /// <summary>
    /// Глобальный адрес сервера
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// Текущий порт сервера для доступа извне
    /// </summary>
    public int Port { get; set; } = 443;

    /// <summary>
    /// Доменное имя хоста
    /// </summary>
    /// <value></value>
    public string Uri { get; set; } = string.Empty;
}