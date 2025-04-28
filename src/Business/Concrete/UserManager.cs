using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete.Management;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserManager(IUserDal userDal) : IUserService
{
    public IDataResult<List<User>> GetUsers()
    {
        return new SuccessDataResult<List<User>>(userDal.GetList(x => x.IsDeleted == false).ToList(), CustomMessage.TransactionSuccess);
    }

    public IDataResult<List<Role>> GetRoles(User? entity)
    {
        if (entity == null)
            return new ErrorDataResult<List<Role>>(CustomMessage.RequiredField);

        var roles = userDal.GetRoles(entity);

        return roles.Any() ? new SuccessDataResult<List<Role>>(roles, CustomMessage.TransactionSuccess) : new ErrorDataResult<List<Role>>(CustomMessage.RoleNotFound);
    }

    public IDataResult<User?> GetUserById(Guid id)
    {
        var user = userDal.Get(x => x!.Id == id);
        return user != null ? new SuccessDataResult<User?>(user, CustomMessage.TransactionSuccess) : new ErrorDataResult<User?>(CustomMessage.TransactionError);
    }

    public IDataResult<User?> GetUserByUsername(string? username)
    {
        var user = userDal.Get(x => x!.Username == username);
        return user != null ? new SuccessDataResult<User?>(user, CustomMessage.TransactionSuccess) : new ErrorDataResult<User?>(CustomMessage.TransactionError);
    }

    public IDataResult<User?> GetUserByMailAddress(string? mailAddress)
    {
        var user = userDal.Get(x => x!.MailAddress == mailAddress);
        return user != null ? new SuccessDataResult<User?>(user, CustomMessage.TransactionSuccess) : new ErrorDataResult<User?>(CustomMessage.TransactionError);
    }

    public IResult Add(User? entity)
    {
        if (entity == null)
            return new ErrorResult(CustomMessage.RequiredField);

        var result = userDal.Add(entity);
        return result ? new SuccessResult(CustomMessage.UserAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(User? entity)
    {
        if (entity == null)
            return new ErrorResult(CustomMessage.RequiredField);

        var result = userDal.Update(entity);
        return result ? new SuccessResult(CustomMessage.UserUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(User? entity)
    {
        if (entity == null)
            return new ErrorResult(CustomMessage.RequiredField);

        var result = userDal.Delete(entity);
        return result ? new SuccessResult(CustomMessage.UserDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}