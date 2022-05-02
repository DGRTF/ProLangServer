namespace UserLogic.Models;

/// <summary>
/// 
/// </summary>
public static class Constants
{
    /// <summary>
    /// Константа представления утверждения об истечении идентификатора пользователя
    /// /// </summary>
    public static string ClaimUserIdType => "userId".Normalize();

    /// <summary>
    /// Константа представления утверждения об истечении срока давности регулярного токена
    /// </summary>
    public static string RegularTokenExpired => "regularTokenExpired".Normalize();
}