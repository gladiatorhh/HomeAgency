using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAgency.Domain.Entities;

public class VillaNumber
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Villa number")]
    public int Villa_Number { get; set; }

    public string? Specialities { get; set; }

    [ForeignKey(nameof(Villa))]
    public int VillaId { get; set; }
    public Villa? Villa { get; set; }
}
