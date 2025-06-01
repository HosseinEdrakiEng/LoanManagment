using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreateDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }

}
