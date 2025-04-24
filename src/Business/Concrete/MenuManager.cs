using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class MenuManager(IMenuDal menuDal) : IMenuService
{
    public IDataResult<Menu> Get(Guid id)
    {
        var data = menuDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<Menu>(data, Messages.TransactionSuccess)
            : new ErrorDataResult<Menu>(Messages.RecordNotFound);
    }

    public IDataResult<List<Menu>> GetList()
    {
        return new SuccessDataResult<List<Menu>>(menuDal.GetList(), Messages.TransactionSuccess);
    }

    public IResult Add(Menu entity)
    {
        var result = menuDal.Add(entity);
        return result ? new SuccessResult(Messages.RecordAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(Menu entity)
    {
        var checkRecord = menuDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = menuDal.Update(entity);

        return result ? new SuccessResult(Messages.RecordUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(Menu entity)
    {
        var checkRecord = menuDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = menuDal.Delete(checkRecord);

        return result ? new SuccessResult(Messages.RecordDeleted) : new ErrorResult(Messages.TransactionError);
    }
}