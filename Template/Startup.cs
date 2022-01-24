using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Template.Configure;
using Template.Models.Configure;
using TemplateDataLayer.Contexts;
using TemplateDataLayer.Models.Authorize;

namespace Template
{
    public class Startup
    {
        private ILifetimeScope _autofacContainer;
        private AppConfigure _appConfigureModel = new AppConfigure();

        public Startup(IConfiguration config)
        {
            _appConfigureModel = config.Get<AppConfigure>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddDbContext<AuthorizeContext>(x =>
                x.UseNpgsql(_appConfigureModel.Db.PostgreSql.ConnectionString));

            services.AddIdentity<User, Role>(x =>
                {
                    x.Password.RequiredLength = 6;
                    x.Password.RequiredUniqueChars = 1;
                    x.Password.RequireUppercase = true;
                    x.Password.RequireLowercase = true;
                    x.Password.RequireDigit = true;
                    x.Password.RequireNonAlphanumeric = true;
                    x.User.AllowedUserNameCharacters = string.Empty;
                    x.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AuthorizeContext>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddControllers();
            services.AddSwaggerDocument();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
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
