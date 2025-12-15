using Core.DataAccess.EntityFramework.Contexts;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Contexts;

public class BackendDbContext : BaseDbContext
{
    public DbSet<Role>? Role { get; set; }
    public DbSet<User>? User { get; set; }
    public DbSet<UserRole>? UserRole { get; set; }
    public DbSet<Notification>? Notification { get; set; }
}