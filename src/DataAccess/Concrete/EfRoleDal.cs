using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Management;
using DataAccess.Abstract;
using DataAccess.EntityFramework.Contexts;

namespace DataAccess.Concrete;

public class EfRoleDal : EfBaseRepository<Role, BackendDbContext>, IRoleDal;