using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;

namespace Business.Abstract.Identity;

public interface IUserService
{
    IDataResult<List<User>> GetUsers();
    IDataResult<List<Role>> GetRoles(User? entity);
    IDataResult<User?> GetUserById(Guid id);
    IDataResult<User?> GetUserByUsername(string? username);
    IDataResult<User?> GetUserByMailAddress(string? mailAddress);

    IResult Add(User? entity);
    IResult Update(User? entity);
    IResult Delete(User? entity);
}