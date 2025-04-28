using Business.Abstract.Identity;
using Business.Constants;
using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;
using DataAccess.Abstract.Identity;

namespace Business.Concrete.Identity;

public class RoleManager(IRoleDal roleDal) : IRoleService
{
    public IDataResult<Role> Get(Guid id)
    {
        var data = roleDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<Role>(data, CustomMessage.TransactionSuccess)
            : new ErrorDataResult<Role>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<Role>> GetList()
    {
        return new SuccessDataResult<List<Role>>(roleDal.GetList(), CustomMessage.TransactionSuccess);
    }

    public IResult Add(Role entity)
    {
        var result = roleDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(Role entity)
    {
        var checkRecord = roleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = roleDal.Update(entity);

        return result ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(Role entity)
    {
        var checkRecord = roleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = roleDal.Delete(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}