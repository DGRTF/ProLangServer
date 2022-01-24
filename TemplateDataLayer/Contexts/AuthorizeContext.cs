using Microsoft.EntityFrameworkCore;
using TemplateDataLayer.Models.Authorize;

namespace TemplateDataLayer.Contexts;

/// <summary>
/// Контекст авторизации к бд
/// </summary>
public class AuthorizeContext : DbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Роли
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    public AuthorizeContext(DbContextOptions<AuthorizeContext> options) : base(options)
    {
    }
}