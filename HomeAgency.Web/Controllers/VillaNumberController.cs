using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using HomeAgency.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeAgency.Web.Controllers;

public class VillaNumberController : Controller
{

    private readonly HomeAgencyDbContext _context;

    public VillaNumberController(HomeAgencyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() =>
        View(_context.VillaNumbers.Include(v => v.Villa).ToList());

    public IActionResult Create() =>
        View(new CreateVillaNumberViewModel
        {
            Villas = _context.villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            })
        });

    [HttpPost]
    public IActionResult Create(CreateVillaNumberViewModel obj)
    {
        if (!ModelState.IsValid)
        {
            obj.Villas = _context.villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            return View(obj);
        }

        if (_context.VillaNumbers.Any(v => v.Villa_Number == obj.VillaNumber.Villa_Number))
        {
            obj.Villas = _context.villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });

            TempData["Error"] = "Villa number exists try another one";

            return View(obj);
        }

        _context.VillaNumbers.Add(obj.VillaNumber);
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
