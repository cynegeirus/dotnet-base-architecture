using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Identity;
using DataAccess.Abstract.Identity;
using DataAccess.EntityFramework.Contexts;

namespace DataAccess.Concrete.Identity;

public class EfUserDal : EfBaseRepository<User, BackendDbContext>, IUserDal
{
    public List<Role> GetRoles(User user)
    {
        using BackendDbContext context = new();
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