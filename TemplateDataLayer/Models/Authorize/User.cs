using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TemplateDataLayer.Models.Authorize;

/// <summary>
/// Представляет пользователя
/// </summary>
[Index("NormalizedEmail", IsUnique = true)]
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Утверждения о пользователе
    /// </summary>
    /// <value></value>
    public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

    /// <summary>
    /// Логины пользователя
    /// </summary>
    /// <value></value>
    public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }

    /// <summary>
    /// Токены пользователя
    /// </summary>
    /// <value></value>
    public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public virtual ICollection<Role> Roles { get; set; }

    public User(): base()
    {
        Claims = new List<IdentityUserClaim<Guid>>();
        Logins = new List<IdentityUserLogin<Guid>>();
        Tokens = new List<IdentityUserToken<Guid>>();
        Roles = new List<Role>();
    }

    public User(Role role, string userName, string email) : this()
    {
        Roles.Add(role);
        UserName = userName;
        Email = email;
    }
}