using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Management;
using DataAccess.Abstract;
using DataAccess.EntityFramework.Contexts;

namespace DataAccess.Concrete;

public class EfSystemParameterDal : EfBaseRepository<SystemParameter, BackendDbContext>, ISystemParameterDal;