using HomeAgency.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HomeAgency.Web.ViewModels;

public class HomeVM
{
    public IEnumerable<Villa>? VillaList { get; set; }
    public DateOnly CheckInDate { get; set; }
    public DateOnly? CheckOutDate { get; set; }
    public int? Nights { get; set; }
}
