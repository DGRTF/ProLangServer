using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TemplateDataLayer.Models.Authorize;

namespace TemplateDataLayer.Contexts;

/// <summary>
/// Контекст авторизации к базе данных
/// </summary>
public class AuthorizeContext : IdentityDbContext<User, Role, Guid>
{
    public AuthorizeContext(DbContextOptions<AuthorizeContext> options) : base(options)
    {
    }
}