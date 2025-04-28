using Core.Entities.Concrete.Management;
using Core.Utilities.Results;

namespace Business.Abstract.Management;

public interface INotificationService
{
    IDataResult<Notification> Get(Guid id);
    IDataResult<List<Notification>> GetList();
    IResult Add(Notification entity);
    IResult Update(Notification entity);
    IResult Delete(Notification entity);
}