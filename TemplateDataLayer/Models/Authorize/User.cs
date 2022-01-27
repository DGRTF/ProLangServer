using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TemplateDataLayer.Models.Authorize;

/// <summary>
/// Представляет пользователя
/// </summary>
[Table("user", Schema = "auth")]
[Index("Id", IsUnique = true)]
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public Role Role { get; set; }

    public User()
    {
        NormalizedUserName = string.Empty;
        PhoneNumber = string.Empty;
        ConcurrencyStamp = string.Empty;
        SecurityStamp = string.Empty;
        UserName = string.Empty;
        Email = string.Empty;
        Role = new Role();
    }

    public User(Role role, string userName, string email) : this()
    {
        Role = role;
        UserName = userName;
        Email = email;
    }
}