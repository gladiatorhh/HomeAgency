using HomeAgency.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeAgency.Web.ViewModels;

public class CreateAmenityViewModel
{
    public Amenity Amenity { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> Villas { get; set; }
}
