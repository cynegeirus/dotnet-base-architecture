using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Base;

public class BaseEntity : IEntity
{
    [Key] [Column(nameof(Id), Order = 0)] public Guid Id { get; set; }

    [Column(nameof(CreatedDate), Order = 1)]
    public DateTime CreatedDate { get; set; }

    [Column(nameof(CreatedUserId), Order = 2)]
    public Guid? CreatedUserId { get; set; }

    [Column(nameof(IsUpdated), Order = 3)] public bool IsUpdated { get; set; }

    [Column(nameof(UpdatedDate), Order = 4)]
    public DateTime UpdatedDate { get; set; }

    [Column(nameof(UpdatedUserId), Order = 5)]
    public Guid? UpdatedUserId { get; set; }

    [Column(nameof(IsDeleted), Order = 6)] public bool IsDeleted { get; set; }

    [Column(nameof(DeletedDate), Order = 7)]
    public DateTime DeletedDate { get; set; }

    [Column(nameof(DeletedUserId), Order = 8)]
    public Guid? DeletedUserId { get; set; }
}