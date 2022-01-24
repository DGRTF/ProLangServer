using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Template.Models.Configure;
using TemplateDataLayer.Contexts;

namespace Template.Configure;

public class MigrationContextFactory : IDesignTimeDbContextFactory<AuthorizeContext>
{
    public AuthorizeContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AuthorizeContext>();
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.Development.json");

        IConfigurationRoot config = builder.Build();
        string connectionString = config.Get<AppConfigure>().Db.PostgreSql.ConnectionString;
        optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(AuthorizeContext).Assembly.GetName().FullName));

        return new AuthorizeContext(optionsBuilder.Options);
    }
    
}