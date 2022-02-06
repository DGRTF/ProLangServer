using Api.Controllers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Template.Configure;
using Template.Models;
using TemplateDataLayer.Contexts;
using TemplateDataLayer.Models.Authorize;
using static System.Net.Mime.MediaTypeNames;

[assembly: ApiController]
namespace Template
{
    public class Startup
    {
        private ILifetimeScope _autofacContainer = null!;
        private AppConfigure _appConfigureModel = new AppConfigure();

        public Startup(IConfiguration config)
        {
            _appConfigureModel = config.Get<AppConfigure>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AuthorizeController>());

            services.AddDbContext<AuthorizeContext>(x =>
                x.UseNpgsql(_appConfigureModel.Db.PostgreSql.ConnectionString));

            services.AddIdentity<User, Role>(x =>
                {
                    x.Password.RequiredLength = 8;
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireDigit = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.User.AllowedUserNameCharacters = string.Empty;
                    x.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AuthorizeContext>()
                .AddDefaultTokenProviders();

            // services.AddAuthorization(options =>
            // {
            // options.FallbackPolicy = new AuthorizationPolicyBuilder()
            // .RequireAuthenticatedUser()
            // .Build();
            // });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = _appConfigureModel.JWTAuthOptions.Issuer,
                        ValidAudience = _appConfigureModel.JWTAuthOptions.Audience,
                        IssuerSigningKey = _appConfigureModel.JWTAuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddControllers(x => x.Filters.Add(new ProducesAttribute("application/json")));
            services.AddSwaggerDocument();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    context.Response.ContentType = Text.Plain;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is BadHttpRequestException)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error.Message ?? string.Empty);

                        return;
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(string.Empty);
                });
            });

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            new AutofacConfiguration(builder, _appConfigureModel).Configure();
        }

        public void ConfigureAutofac(IApplicationBuilder app)
        {
            _autofacContainer = app.ApplicationServices.GetAutofacRoot();
        }
    }
}
