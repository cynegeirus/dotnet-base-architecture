using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class SystemParameterManager(ISystemParameterDal systemParameterDal) : ISystemParameterService
{
    public IDataResult<SystemParameter> Get(Guid id)
    {
        var data = systemParameterDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<SystemParameter>(data, Messages.TransactionSuccess)
            : new ErrorDataResult<SystemParameter>(Messages.RecordNotFound);
    }

    public IDataResult<List<SystemParameter>> GetList()
    {
        return new SuccessDataResult<List<SystemParameter>>(systemParameterDal.GetList(), Messages.TransactionSuccess);
    }

    public IResult Add(SystemParameter entity)
    {
        var result = systemParameterDal.Add(entity);
        return result ? new SuccessResult(Messages.RecordAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(SystemParameter entity)
    {
        var checkRecord = systemParameterDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = systemParameterDal.Update(entity);

        return result ? new SuccessResult(Messages.RecordUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(SystemParameter entity)
    {
        var checkRecord = systemParameterDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = systemParameterDal.Delete(checkRecord);

        return result ? new SuccessResult(Messages.RecordDeleted) : new ErrorResult(Messages.TransactionError);
    }
}