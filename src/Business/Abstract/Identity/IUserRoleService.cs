using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;

namespace Business.Abstract.Identity;

public interface IUserRoleService
{
    IDataResult<UserRole> Get(Guid id);
    IDataResult<List<UserRole>> GetList();
    IResult Add(UserRole requestDto);
    IResult Update(UserRole requestDto);
    IResult Delete(UserRole requestDto);
}