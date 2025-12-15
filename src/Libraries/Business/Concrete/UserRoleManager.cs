using Business.Abstract;
using Business.Constants;
using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Concrete;

public class UserRoleManager(IUnitOfWork unitOfWork) : IUserRoleService
{
    public IDataResult<UserRole> Get(Guid id)
    {
        var userRole = unitOfWork.Repository<UserRole>().Get(x => x.Id == id);
        return userRole != null ? new SuccessDataResult<UserRole>(userRole, CustomMessage.TransactionSuccess) : new ErrorDataResult<UserRole>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<UserRole>> GetList()
    {
        var list = unitOfWork.Repository<UserRole>().GetList();
        return new SuccessDataResult<List<UserRole>>(list, CustomMessage.TransactionSuccess);
    }

    public IResult Add(UserRole entity)
    {
        unitOfWork.Repository<UserRole>().Add(entity);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(UserRole entity)
    {
        var userRole = unitOfWork.Repository<UserRole>().Get(x => x.Id == entity.Id);
        if (userRole == null) return new ErrorResult(CustomMessage.RecordNotFound);
        userRole.UserId = entity.UserId;
        userRole.RoleId = entity.RoleId;
        unitOfWork.Repository<UserRole>().Update(userRole);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(UserRole entity)
    {
        var userRole = unitOfWork.Repository<UserRole>().Get(x => x.Id == entity.Id);
        if (userRole == null) return new ErrorResult(CustomMessage.RecordNotFound);
        unitOfWork.Repository<UserRole>().Delete(userRole);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}