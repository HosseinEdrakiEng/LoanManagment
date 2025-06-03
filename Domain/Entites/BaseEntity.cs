using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; } = false;

}
