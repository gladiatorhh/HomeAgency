using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeAgency.Web.Controllers;

public class VillaNumberController : Controller
{

    private readonly HomeAgencyDbContext _context;

    public VillaNumberController(HomeAgencyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() =>
        View(_context.VillaNumbers.ToList());

    public IActionResult Create()
    {
        ViewBag.Villas = _context.villas.Select(v => new SelectListItem
        {
            Text = v.Name,
            Value = v.Id.ToString()
        });

        return View();
    }

    [HttpPost]
    public IActionResult Create(VillaNumber obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _context.VillaNumbers.Add(obj);
        _context.SaveChanges();

        TempData["OpSuccess"] = "A new villa has been created";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int villaNumberId)
    {
        var villaNumber = _context.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId);

        if (villaNumber == null)
        {
            return NotFound();
        }

        return View(villaNumber);
    }

    [HttpPost]
    public IActionResult Update(VillaNumber obj)
    {

        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _context.VillaNumbers.Update(obj);
        _context.SaveChanges();

        TempData["OpSuccess"] = "Villa has been updated";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int villaNumberId)
    {
        var villaNumber = _context.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId);


        if (villaNumber == null)
        {
            return NotFound();
        }

        return View(villaNumber);
    }

    [HttpPost]
    public IActionResult Delete(VillaNumber obj)
    {
        var villaNumber = _context.VillaNumbers.Find(obj.Villa_Number);

        if (villaNumber == null)
        {
            return NotFound();
        }

        _context.VillaNumbers.Remove(villaNumber);
        _context.SaveChanges();

        TempData["OpSuccess"] = "Villa has been deleted";
        return RedirectToAction(nameof(Index));
    }
}
