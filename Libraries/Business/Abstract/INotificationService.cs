using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface INotificationService
{
    IDataResult<Notification> Get(Guid id);
    IDataResult<List<Notification>> GetList();
    IResult Add(Notification entity);
    IResult Update(Notification entity);
    IResult Delete(Notification entity);
}