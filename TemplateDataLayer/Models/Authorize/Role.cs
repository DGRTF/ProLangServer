using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TemplateDataLayer.Models.Authorize;

/// <summary>
/// Представляет роль пользователя
/// </summary>
[Table("role", Schema = "auth")]
[Index("Id")]
public class Role : IdentityRole<Guid>
{
    /// <summary>
    /// Пользователи
    /// </summary>
    ICollection<User> Users { get; set; } = new List<User>();

    public Role()
    {
        
    }

    public Role(string name) : base(name ?? string.Empty)
    {
        NormalizedName = name ?? string.Empty;
    }

}