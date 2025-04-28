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
            ? new SuccessDataResult<Menu>(data, CustomMessage.TransactionSuccess)
            : new ErrorDataResult<Menu>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<Menu>> GetList()
    {
        return new SuccessDataResult<List<Menu>>(menuDal.GetList(), CustomMessage.TransactionSuccess);
    }

    public IResult Add(Menu entity)
    {
        var result = menuDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(Menu entity)
    {
        var checkRecord = menuDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = menuDal.Update(entity);

        return result ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(Menu entity)
    {
        var checkRecord = menuDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = menuDal.Delete(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}