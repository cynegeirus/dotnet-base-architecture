using Business.Abstract.Identity;
using Business.Constants;
using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;
using DataAccess.Abstract.Identity;

namespace Business.Concrete.Identity;

public class UserRoleManager(IUserRoleDal userRoleDal) : IUserRoleService
{
    public IDataResult<UserRole> Get(Guid id)
    {
        var data = userRoleDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<UserRole>(data, CustomMessage.TransactionSuccess)
            : new ErrorDataResult<UserRole>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<UserRole>> GetList()
    {
        return new SuccessDataResult<List<UserRole>>(userRoleDal.GetList(), CustomMessage.TransactionSuccess);
    }

    public IResult Add(UserRole entity)
    {
        var result = userRoleDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(UserRole entity)
    {
        var checkRecord = userRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = userRoleDal.Update(entity);

        return result ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(UserRole entity)
    {
        var checkRecord = userRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = userRoleDal.Delete(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}