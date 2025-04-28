using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Identity;
using DataAccess.Abstract.Identity;
using DataAccess.EntityFramework.Contexts;

namespace DataAccess.Concrete.Identity;

public class EfUserRoleDal : EfBaseRepository<UserRole, BackendDbContext>, IUserRoleDal;