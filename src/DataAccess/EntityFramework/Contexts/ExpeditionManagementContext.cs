using Core.Entities.Concrete.Base;
using Core.Entities.Concrete.Management;
using Core.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.EntityFramework.Contexts;

public class ExpeditionManagementContext : DbContext
{
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Role>? Role { get; set; }
    public DbSet<User>? User { get; set; }
    public DbSet<UserRole>? UserRole { get; set; }
    public DbSet<MenuRole>? MenuRole { get; set; }
    public DbSet<Notification>? Notification { get; set; }
    public DbSet<SystemParameter>? SystemParameter { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableServiceProviderCaching();
        optionsBuilder.EnableThreadSafetyChecks();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        optionsBuilder.UseNpgsql(ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<string>("Databases:ExpeditionManagementDB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.Id)).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.CreatedDate)).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.IsUpdated)).HasDefaultValue(false);
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.UpdatedDate)).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.IsDeleted)).HasDefaultValue(false);
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.DeletedDate)).HasDefaultValue(DateTime.Now);
        }
    }
}