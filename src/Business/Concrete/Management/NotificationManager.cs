using Business.Abstract.Management;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract.Management;

namespace Business.Concrete.Management;

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
            }, CustomMessage.TransactionSuccess)
            : new ErrorDataResult<Notification>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<Notification>> GetList()
    {
        return new SuccessDataResult<List<Notification>>(notificationDal.GetList(), CustomMessage.TransactionSuccess);
    }

    public IResult Add(Notification entity)
    {
        var result = notificationDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(Notification entity)
    {
        var checkRecord = notificationDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        checkRecord.Title = entity.Title;
        checkRecord.Content = entity.Content;

        var result = notificationDal.Update(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(Notification entity)
    {
        var checkRecord = notificationDal.Get(x => x != null && x.Id == entity.Id);
        if (checkRecord == null)
            return new ErrorResult(CustomMessage.RecordNotFound);

        var result = notificationDal.Delete(checkRecord);

        return result ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}