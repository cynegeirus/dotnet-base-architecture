using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Management;
using DataAccess.Abstract;
using DataAccess.EntityFramework.Contexts;

namespace DataAccess.Concrete;

public class EfUserDal : EfBaseRepository<User, ExpeditionManagementContext>, IUserDal
{
    public List<Role> GetRoles(User user)
    {
        using ExpeditionManagementContext context = new();
        var result = context.Role!.Join(context.UserRole!,
            roles => roles.Id,
            userRoles => userRoles.RoleId, (roles, userRoles) => new
            {
                roles,
                userRoles
            }).Where(t => t.userRoles.UserId == user.Id).Select(t => new Role
        {
            Id = t.roles.Id,
            Name = t.roles.Name
        });
        return result.ToList();
    }
}