using Core.DataAccess;
using Core.Entities.Concrete.Identity;

namespace DataAccess.Abstract.Identity;

public interface IUserDal : IBaseRepository<User>
{
    List<Role> GetRoles(User user);
}