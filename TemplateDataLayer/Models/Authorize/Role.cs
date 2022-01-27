using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TemplateDataLayer.Models.Authorize;

/// <summary>
/// Представляет роль пользователя
/// </summary>
[Table("role", Schema = "auth")]
public class Role : IdentityRole<Guid>
{
    /// <summary>
    /// Пользователи
    /// </summary>
    ICollection<User> Users { get; set; } = new List<User>();

    public Role() : base()
    {
        ConcurrencyStamp = string.Empty;
        NormalizedName = string.Empty;
    }

    public Role(string name) : base(name ?? string.Empty)
    {
        NormalizedName = name ?? string.Empty;
    }

}