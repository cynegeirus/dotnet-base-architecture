using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class MenuRoleManager(IMenuRoleDal menuRoleDal) : IMenuRoleService
{
    public IDataResult<MenuRole> Get(Guid id)
    {
        var data = menuRoleDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<MenuRole>(data, Messages.TransactionSuccess)
            : new ErrorDataResult<MenuRole>(Messages.RecordNotFound);
    }

    public IDataResult<List<MenuRole>> GetList()
    {
        return new SuccessDataResult<List<MenuRole>>(menuRoleDal.GetList(), Messages.TransactionSuccess);
    }

    public IResult Add(MenuRole entity)
    {
        var result = menuRoleDal.Add(entity);
        return result ? new SuccessResult(Messages.RecordAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(MenuRole entity)
    {
        var checkRecord = menuRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = menuRoleDal.Update(entity);

        return result ? new SuccessResult(Messages.RecordUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(MenuRole entity)
    {
        var checkRecord = menuRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = menuRoleDal.Delete(checkRecord);

        return result ? new SuccessResult(Messages.RecordDeleted) : new ErrorResult(Messages.TransactionError);
    }
}