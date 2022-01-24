using Autofac;
using Autofac.Builder;
using UserLogic.ExternalInterfaces;
using UserLogic.Services;
using UserLogic.Services.Interfaces;
using TemplateDataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using TemplateDataLayer.Models.Authorize;
using Template.Models.Configure;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace Template.Configure;

public class AutofacConfiguration
{
    private readonly ContainerBuilder _container;
    private readonly AppConfigure _configure;

    public AutofacConfiguration(
        ContainerBuilder container,
        AppConfigure configuration)
    {
        _container = container;
        _configure = configuration;
    }

    public void Configure()
    {
        RegisterSingleInstance<AuthorizeService, IAuthorizeService>();
        RegisterSingleInstance<AuthorizeRepository, IAuthorizeRepository>();
        _container.Register(x => new JWTGeneration(_configure.JWTAuthOptions)).As<IJwtGenerator>();
        _container.RegisterAutoMapper(typeof(Program).Assembly);
    }

    private IRegistrationBuilder<TInstance, Autofac.Builder.ConcreteReflectionActivatorData, Autofac.Builder.SingleRegistrationStyle> RegisterSingleInstance<TInstance, TService>()
        where TInstance : notnull
        where TService : notnull
    {
        return Register<TInstance, TService>().SingleInstance();
    }

    private IRegistrationBuilder<TInstance, Autofac.Builder.ConcreteReflectionActivatorData, Autofac.Builder.SingleRegistrationStyle> Register<TInstance, TService>()
        where TInstance : notnull
        where TService : notnull
    {
        return _container.RegisterType<TInstance>().As<TService>();
    }
}