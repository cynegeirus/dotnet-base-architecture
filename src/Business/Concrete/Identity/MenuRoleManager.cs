using Business.Abstract.Identity;
using Business.Constants;
using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;
using DataAccess.Abstract.Identity;

namespace Business.Concrete.Identity;

public class MenuRoleManager(IMenuRoleDal menuRoleDal) : IMenuRoleService
{
    public IDataResult<MenuRole> Get(Guid id)
    {
        var data = menuRoleDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<MenuRole>(data, CustomMessage.TransactionSuccess)
            : new ErrorDataResult<MenuRole>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<MenuRole>> GetList()
    {
        return new SuccessDataResult<List<MenuRole>>(menuRoleDal.GetList(), CustomMessage.TransactionSuccess);
    }

    public IResult Add(MenuRole entity)
    {
        var result = menuRoleDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(MenuRole entity)
    {
        var checkRecord = menuRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = menuRoleDal.Update(entity);

        return result ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(MenuRole entity)
    {
        var checkRecord = menuRoleDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = menuRoleDal.Delete(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}