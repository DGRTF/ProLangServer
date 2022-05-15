namespace UserLogic.Models;

/// <summary>
/// 
/// </summary>
public static class Constants
{
    /// <summary>
    /// Константа представления утверждения об истечении срока давности регулярного токена
    /// </summary>
    public static string RegularTokenExpired => "regularTokenExpired".Normalize();

    /// <summary>
    /// Константа представления утверждения об истечении срока давности регулярного токена
    /// </summary>
    public static string SessionId => "SessionId".Normalize();
}