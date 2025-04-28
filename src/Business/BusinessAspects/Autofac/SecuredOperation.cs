using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac;

public class SecuredOperation(string roles) : MethodInterception
{
    private readonly IHttpContextAccessor? _httpContextAccessor = ServiceTool.ServiceProvider!.GetService<IHttpContextAccessor>();
    private readonly string[] _roles = roles.Split(',');

    protected override void OnBefore(IInvocation invocation)
    {
        var roleClaims = _httpContextAccessor?.HttpContext.User.ClaimRoles();
        if (_roles.Any(role => roleClaims!.Contains(role))) return;
        throw new Exception(CustomMessage.AuthorizationDenied);
    }
}