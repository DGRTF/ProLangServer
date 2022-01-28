using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TemplateDataLayer.Models.Authorize;

/// <summary>
/// Представляет роль пользователя
/// </summary>
public class Role : IdentityRole<Guid>
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public Role() : base()
    {
        ConcurrencyStamp = string.Empty;
        NormalizedName = string.Empty;
        Name = string.Empty;
    }

    public Role(string name) : base(name ?? string.Empty)
    {
        NormalizedName = name ?? string.Empty;
    }

}