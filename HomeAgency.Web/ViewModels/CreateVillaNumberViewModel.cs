using HomeAgency.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeAgency.Web.ViewModels;

public class CreateVillaNumberViewModel
{
    public VillaNumber VillaNumber { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> Villas { get; set; }
}
