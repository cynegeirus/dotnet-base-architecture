using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Management;
using DataAccess.Abstract.Management;
using DataAccess.EntityFramework.Contexts;

namespace DataAccess.Concrete.Management;

public class EfSystemParameterDal : EfBaseRepository<SystemParameter, BackendDbContext>, ISystemParameterDal;