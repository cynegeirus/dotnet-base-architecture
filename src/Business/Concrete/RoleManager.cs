using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class RoleManager(IRoleDal roleDal) : IRoleService
{
    public IDataResult<Role> Get(Guid id)
    {
        var data = roleDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<Role>(data, Messages.TransactionSuccess)
            : new ErrorDataResult<Role>(Messages.RecordNotFound);
    }

    public IDataResult<List<Role>> GetList()
    {
        return new SuccessDataResult<List<Role>>(roleDal.GetList(), Messages.TransactionSuccess);
    }

    public IResult Add(Role entity)
    {
        var result = roleDal.Add(entity);
        return result ? new SuccessResult(Messages.RecordAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(Role entity)
    {
        var checkRecord = roleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = roleDal.Update(entity);

        return result ? new SuccessResult(Messages.RecordUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(Role entity)
    {
        var checkRecord = roleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = roleDal.Delete(checkRecord);

        return result ? new SuccessResult(Messages.RecordDeleted) : new ErrorResult(Messages.TransactionError);
    }
}