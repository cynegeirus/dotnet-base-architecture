using Business.Abstract;
using Business.Constants;
using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Concrete;

public class UserManager(IUnitOfWork unitOfWork) : IUserService
{
    public IDataResult<List<User>> GetUsers()
    {
        var users = unitOfWork.Repository<User>().GetList();
        return new SuccessDataResult<List<User>>(users, CustomMessage.TransactionSuccess);
    }

    public IDataResult<List<Role>> GetRoles(User? entity)
    {
        if (entity == null) return new ErrorDataResult<List<Role>>(CustomMessage.RequiredField);
        var roles = unitOfWork.Repository<UserRole>().GetList(x => x.UserId == entity.Id).Select(x => x.Role).ToList();
        return roles.Count != 0 ? new SuccessDataResult<List<Role>>(roles!, CustomMessage.TransactionSuccess) : new ErrorDataResult<List<Role>>(CustomMessage.RoleNotFound);
    }

    public IDataResult<User?> GetUserById(Guid id)
    {
        var user = unitOfWork.Repository<User>().Get(x => x.Id == id);
        return user != null ? new SuccessDataResult<User?>(user, CustomMessage.TransactionSuccess) : new ErrorDataResult<User?>(CustomMessage.RecordNotFound);
    }

    public IDataResult<User?> GetUserByUsername(string? username)
    {
        if (string.IsNullOrEmpty(username)) return new ErrorDataResult<User?>(CustomMessage.RequiredField);
        var user = unitOfWork.Repository<User>().Get(x => x.Username == username);
        return user != null ? new SuccessDataResult<User?>(user, CustomMessage.TransactionSuccess) : new ErrorDataResult<User?>(CustomMessage.RecordNotFound);
    }

    public IDataResult<User?> GetUserByMailAddress(string? mailAddress)
    {
        if (string.IsNullOrEmpty(mailAddress)) return new ErrorDataResult<User?>(CustomMessage.RequiredField);
        var user = unitOfWork.Repository<User>().Get(x => x.MailAddress == mailAddress);
        return user != null ? new SuccessDataResult<User?>(user, CustomMessage.TransactionSuccess) : new ErrorDataResult<User?>(CustomMessage.RecordNotFound);
    }

    public IResult Add(User? entity)
    {
        if (entity == null) return new ErrorResult(CustomMessage.RequiredField);
        unitOfWork.Repository<User>().Add(entity);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.UserAdded) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Update(User? entity)
    {
        if (entity == null) return new ErrorResult(CustomMessage.RequiredField);
        var user = unitOfWork.Repository<User>().Get(x => x.Id == entity.Id);
        if (user == null) return new ErrorResult(CustomMessage.RecordNotFound);
        user.FirstName = entity.FirstName;
        user.LastName = entity.LastName;
        user.Username = entity.Username;
        user.MailAddress = entity.MailAddress;
        unitOfWork.Repository<User>().Update(user);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.UserUpdated) : new ErrorResult(CustomMessage.TransactionError);
    }

    public IResult Delete(User? entity)
    {
        if (entity == null) return new ErrorResult(CustomMessage.RequiredField);
        var user = unitOfWork.Repository<User>().Get(x => x.Id == entity.Id);
        if (user == null) return new ErrorResult(CustomMessage.RecordNotFound);
        unitOfWork.Repository<User>().Delete(user);
        var result = unitOfWork.SaveChanges();
        return result > 0 ? new SuccessResult(CustomMessage.UserDeleted) : new ErrorResult(CustomMessage.TransactionError);
    }
}