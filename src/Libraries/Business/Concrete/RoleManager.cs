using Business.Abstract;
using Business.Constants;
using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Concrete;

public class RoleManager(IUnitOfWork unitOfWork) : IRoleService
{
    public IDataResult<Role> Get(Guid id)
    {
        var role = unitOfWork.Repository<Role>().Get(x => x.Id == id);
        return role != null ? new SuccessDataResult<Role>(role, CustomMessage.TransactionSuccess) : new ErrorDataResult<Role>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<Role>> GetList()
    {
        var list = unitOfWork.Repository<Role>().GetList();
        return new SuccessDataResult<List<Role>>(list, CustomMessage.TransactionSuccess);
    }

    public IResult Add(Role entity)
    {
        unitOfWork.Repository<Role>().Add(entity);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(Role entity)
    {
        var role = unitOfWork.Repository<Role>().Get(x => x.Id == entity.Id);
        if (role == null) return new ErrorResult(CustomMessage.RecordNotFound);
        role.Name = entity.Name;
        unitOfWork.Repository<Role>().Update(role);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(Role entity)
    {
        var role = unitOfWork.Repository<Role>().Get(x => x.Id == entity.Id);
        if (role == null) return new ErrorResult(CustomMessage.RecordNotFound);
        unitOfWork.Repository<Role>().Delete(role);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}