using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAgency.Domain.Entities;

public class Amenity
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    [ForeignKey(nameof(Villa))]
    public int VillaId { get; set; }
    public Villa Villa { get; set; }
}
