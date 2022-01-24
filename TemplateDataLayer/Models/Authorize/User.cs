using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TemplateDataLayer.Models.Authorize;

/// <summary>
/// Представляет пользователя
/// </summary>
[Table("user", Schema = "auth")]
[Index("Id")]
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public Role Role { get; set; }

    public User()
    {
        
    }

    public User(Role role, string userName, string email)
    {
        Role = role;
        UserName = userName;
        Email = email;
        NormalizedUserName = string.Empty;
        PhoneNumber = string.Empty;
        ConcurrencyStamp = string.Empty;
        SecurityStamp = string.Empty;
    }
}