using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.DataAccess.EntityFramework.Abstract;
using Core.DataAccess.EntityFramework.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.EntityFramework.Contexts;
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BackendDbContext>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork<BackendDbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<AccountManager>().As<IAccountService>().InstancePerLifetimeScope();
        builder.RegisterType<NotificationManager>().As<INotificationService>().InstancePerLifetimeScope();
        builder.RegisterType<RoleManager>().As<IRoleService>().InstancePerLifetimeScope();
        builder.RegisterType<UserRoleManager>().As<IUserRoleService>().InstancePerLifetimeScope();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions { Selector = new AspectInterceptorSelector() }).InstancePerLifetimeScope();
    }
}