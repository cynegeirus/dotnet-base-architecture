﻿using Core.DataAccess;
using Core.Entities.Concrete.Management;

namespace DataAccess.Abstract;

public interface IUserDal : IBaseRepository<User>
{
    List<Role> GetRoles(User user);
}