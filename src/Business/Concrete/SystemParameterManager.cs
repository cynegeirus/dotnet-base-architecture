using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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
            ? new SuccessDataResult<SystemParameter>(data, CustomMessage.TransactionSuccess)
            : new ErrorDataResult<SystemParameter>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<SystemParameter>> GetList()
    {
        return new SuccessDataResult<List<SystemParameter>>(systemParameterDal.GetList(), CustomMessage.TransactionSuccess);
    }

    public IResult Add(SystemParameter entity)
    {
        var result = systemParameterDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(SystemParameter entity)
    {
        var checkRecord = systemParameterDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = systemParameterDal.Update(entity);

        return result ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(SystemParameter entity)
    {
        var checkRecord = systemParameterDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = systemParameterDal.Delete(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}