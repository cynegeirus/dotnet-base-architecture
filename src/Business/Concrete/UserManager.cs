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
        return new SuccessDataResult<List<User>>(userDal.GetList(x => x.IsDeleted == false).ToList(), Messages.TransactionSuccess);
    }

    public IDataResult<List<Role>> GetRoles(User? entity)
    {
        if (entity == null)
            return new ErrorDataResult<List<Role>>(Messages.RequiredField);

        var roles = userDal.GetRoles(entity);

        return roles.Any() ? new SuccessDataResult<List<Role>>(roles, Messages.TransactionSuccess) : new ErrorDataResult<List<Role>>(Messages.RoleNotFound);
    }

    public IDataResult<User?> GetUserById(Guid id)
    {
        var user = userDal.Get(x => x!.Id == id);
        return user != null ? new SuccessDataResult<User?>(user, Messages.TransactionSuccess) : new ErrorDataResult<User?>(Messages.TransactionError);
    }

    public IDataResult<User?> GetUserByUsername(string? username)
    {
        var user = userDal.Get(x => x!.Username == username);
        return user != null ? new SuccessDataResult<User?>(user, Messages.TransactionSuccess) : new ErrorDataResult<User?>(Messages.TransactionError);
    }

    public IDataResult<User?> GetUserByMailAddress(string? mailAddress)
    {
        var user = userDal.Get(x => x!.MailAddress == mailAddress);
        return user != null ? new SuccessDataResult<User?>(user, Messages.TransactionSuccess) : new ErrorDataResult<User?>(Messages.TransactionError);
    }

    public IResult Add(User? entity)
    {
        if (entity == null)
            return new ErrorResult(Messages.RequiredField);

        var result = userDal.Add(entity);
        return result ? new SuccessResult(Messages.UserAdded) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Update(User? entity)
    {
        if (entity == null)
            return new ErrorResult(Messages.RequiredField);

        var result = userDal.Update(entity);
        return result ? new SuccessResult(Messages.UserUpdated) : new ErrorResult(Messages.TransactionError);
    }

    public IResult Delete(User? entity)
    {
        if (entity == null)
            return new ErrorResult(Messages.RequiredField);

        var result = userDal.Delete(entity);
        return result ? new SuccessResult(Messages.UserDeleted) : new ErrorResult(Messages.TransactionError);
    }
}