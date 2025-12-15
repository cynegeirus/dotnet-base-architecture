using Business.Abstract;
using Business.Constants;
using Core.DataAccess.EntityFramework.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Concrete;

public class NotificationManager(IUnitOfWork unitOfWork) : INotificationService
{
    public IDataResult<Notification> Get(Guid id)
    {
        var notification = unitOfWork.Repository<Notification>().Get(x => x.Id == id);
        return notification != null ? new SuccessDataResult<Notification>(notification, CustomMessage.TransactionSuccess) : new ErrorDataResult<Notification>(CustomMessage.RecordNotFound);
    }

    public IDataResult<List<Notification>> GetList()
    {
        var list = unitOfWork.Repository<Notification>().GetList();
        return new SuccessDataResult<List<Notification>>(list, CustomMessage.TransactionSuccess);
    }

    public IResult Add(Notification entity)
    {
        unitOfWork.Repository<Notification>().Add(entity);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(Notification entity)
    {
        var notification = unitOfWork.Repository<Notification>().Get(x => x.Id == entity.Id);
        if (notification == null) return new ErrorResult(CustomMessage.RecordNotFound);
        notification.Title = entity.Title;
        notification.Content = entity.Content;
        unitOfWork.Repository<Notification>().Update(notification);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(Notification entity)
    {
        var notification = unitOfWork.Repository<Notification>().Get(x => x.Id == entity.Id);
        if (notification == null) return new ErrorResult(CustomMessage.RecordNotFound);
        unitOfWork.Repository<Notification>().Delete(notification);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.RecordDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}