using Core.Entities.Concrete.Management;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IUserRoleService
{
    IDataResult<UserRole> Get(Guid id);
    IDataResult<List<UserRole>> GetList();
    IResult Add(UserRole requestDto);
    IResult Update(UserRole requestDto);
    IResult Delete(UserRole requestDto);
}