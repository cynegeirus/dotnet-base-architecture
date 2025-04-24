using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserRoleManager(IUserRoleDal userRoleDal) : IUserRoleService
{
    public IDataResult<UserRole> Get(Guid id)
    {
        var data = userRoleDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<UserRole>(data, Messages.TransactionSuccess)
            : new ErrorDataResult<UserRole>(Messages.RecordNotFound);
    }

    public IDataResult<List<UserRole>> GetList()
    {
        return new SuccessDataResult<List<UserRole>>(userRoleDal.GetList(), Messages.TransactionSuccess);
    }

    public IResult Add(UserRole entity)
    {
        var result = userRoleDal.Add(entity);
        return result ? new SuccessResult(Messages.RecordAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(UserRole entity)
    {
        var checkRecord = userRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = userRoleDal.Update(entity);

        return result ? new SuccessResult(Messages.RecordUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(UserRole entity)
    {
        var checkRecord = userRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = userRoleDal.Delete(checkRecord);

        return result ? new SuccessResult(Messages.RecordDeleted) : new ErrorResult(Messages.TransactionError);
    }
}