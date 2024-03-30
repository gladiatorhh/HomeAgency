using System.ComponentModel.DataAnnotations;

namespace HomeAgency.Domain.Entities;

public class Villa
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    [Range(1,10000)]
    public int Sqft { get; set; }

    [Range(1, 10)]
    public int Occupancy { get; set; }

    [Display(Name="Image url")]
    public string? ImageUrl { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
