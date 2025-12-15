using System.Linq.Expressions;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Base;
using Core.Entities.Concrete.Log;
using Core.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Core.DataAccess.EntityFramework.Contexts;

public class BaseDbContext : DbContext
{
    public DbSet<DatabaseAuditLog> DatabaseAuditLog { get; set; }
    public DbSet<TrafficAuditLog> TrafficAuditLog { get; set; }
    public DbSet<ErrorLog> ErrorLog { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableServiceProviderCaching();
        optionsBuilder.EnableThreadSafetyChecks();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        optionsBuilder.UseNpgsql(ConfigurationHelper.GetConfigWithFile("configurationSettings.json").GetValue<string>("Databasing:PostgreSQL"));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) continue;

            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.Id)).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.CreatedDate)).HasDefaultValueSql("now()");
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.IsUpdated)).HasDefaultValue(false);
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.IsDeleted)).HasDefaultValue(false);
        }

        ApplyGlobalFilters(modelBuilder);
    }

    private static void ApplyGlobalFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) continue;

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
            var falseConstant = Expression.Constant(false);
            var comparison = Expression.Equal(property, falseConstant);
            var lambda = Expression.Lambda(comparison, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }

    public override int SaveChanges()
    {
        SetAuditFields();
        LogEntityChanges();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        LogEntityChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.IsUpdated = false;
                    entry.Entity.IsDeleted = false;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.Now;
                    entry.Entity.IsUpdated = true;
                    if (entry.Entity.IsDeleted && entry.Entity.DeletedDate == null)
                        entry.Entity.DeletedDate = DateTime.Now;
                    break;
            }
    }

    private static bool IsAuditEntity(BaseEntity entity)
    {
        return entity is Entities.Concrete.Log.DatabaseAuditLog or Entities.Concrete.Log.TrafficAuditLog or Entities.Concrete.Log.ErrorLog;
    }

    private void LogEntityChanges()
    {
        ChangeTracker.DetectChanges();

        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted && !IsAuditEntity(e.Entity))
            .ToList();

        foreach (var auditLog in entries.Select(CreateAuditLog))
        {
            if (DatabaseAuditLogger.ShouldWriteToDatabase) DatabaseAuditLog.Add(auditLog);
            DatabaseAuditLogger.Log(auditLog);
        }
    }

    private static DatabaseAuditLog CreateAuditLog(EntityEntry<BaseEntity> entry)
    {
        var entityName = entry.Entity.GetType().Name;
        var tableName = entry.Metadata.GetTableName();
        var primaryKey = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString();
        var changeDetails = new List<object>();

        foreach (var prop in entry.Properties)
        {
            var originalValue = prop.OriginalValue?.ToString();
            var currentValue = prop.CurrentValue?.ToString();

            if (entry.State == EntityState.Modified && Equals(originalValue, currentValue)) continue;

            changeDetails.Add(new
            {
                Property = prop.Metadata.Name,
                OldValue = entry.State == EntityState.Added ? null : originalValue,
                NewValue = entry.State == EntityState.Deleted ? null : currentValue
            });
        }

        return new DatabaseAuditLog
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            IsUpdated = false,
            IsDeleted = false,
            TableName = tableName ?? entityName,
            EntityName = entityName,
            PrimaryKey = primaryKey ?? string.Empty,
            Operation = entry.State.ToString(),
            Json = JsonConvert.SerializeObject(changeDetails, Formatting.Indented)
        };
    }
}