using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EfUserDal>().As<IUserDal>();
        builder.RegisterType<EfMenuDal>().As<IMenuDal>();
        builder.RegisterType<EfRoleDal>().As<IRoleDal>();
        builder.RegisterType<EfMenuRoleDal>().As<IMenuRoleDal>();
        builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();
        builder.RegisterType<EfNotificationDal>().As<INotificationDal>();
        builder.RegisterType<EfSystemParameterDal>().As<ISystemParameterDal>();

        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<AccountManager>().As<IAccountService>();
        builder.RegisterType<NotificationManager>().As<INotificationService>();
        builder.RegisterType<MenuManager>().As<IMenuService>();
        builder.RegisterType<RoleManager>().As<IRoleService>();
        builder.RegisterType<MenuRoleManager>().As<IMenuRoleService>();
        builder.RegisterType<UserRoleManager>().As<IUserRoleService>();
        builder.RegisterType<SystemParameterManager>().As<ISystemParameterService>();

        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions { Selector = new AspectInterceptorSelector() }).SingleInstance();
    }
}