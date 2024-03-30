using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeAgency.Web.Controllers;

public class VillaController : Controller
{

    private readonly HomeAgencyDbContext _context;

    public VillaController(HomeAgencyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() =>
        View(_context.villas.ToList());

    public IActionResult Create() =>
        View();

    [HttpPost]
    public IActionResult Create(Villa obj)
    {
        if (obj.Name == obj.Description)
        {
            ModelState.AddModelError("", "Villa name shouldn't be the same as description");
            return View(obj);
        }

        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _context.villas.Add(obj);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
