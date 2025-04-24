using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class NotificationManager(INotificationDal notificationDal) : INotificationService
{
    public IDataResult<Notification> Get(Guid id)
    {
        var data = notificationDal.Get(x => x != null && x.Id == id);
        return data != null
            ? new SuccessDataResult<Notification>(new Notification
            {
                Id = data.Id,
                Title = data.Title,
                Content = data.Content
            }, Messages.TransactionSuccess)
            : new ErrorDataResult<Notification>(Messages.RecordNotFound);
    }

    public IDataResult<List<Notification>> GetList()
    {
        return new SuccessDataResult<List<Notification>>(notificationDal.GetList(), Messages.TransactionSuccess);
    }

    public IResult Add(Notification entity)
    {
        var result = notificationDal.Add(entity);
        return result ? new SuccessResult(Messages.RecordAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(Notification entity)
    {
        var checkRecord = notificationDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        checkRecord.Title = entity.Title;
        checkRecord.Content = entity.Content;

        var result = notificationDal.Update(checkRecord);

        return result ? new SuccessResult(Messages.RecordUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(Notification entity)
    {
        var checkRecord = notificationDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(Messages.RecordNotFound);

        var result = notificationDal.Delete(checkRecord);

        return result ? new SuccessResult(Messages.RecordDeleted) : new ErrorResult(Messages.TransactionError);
    }
}