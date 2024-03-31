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

    public IActionResult Update(int villaId)
    {
        var villa = _context.villas.FirstOrDefault(x => x.Id == villaId);

        if (villa == null)
        {
            return NotFound();
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Update(Villa obj)
    {

        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _context.villas.Update(obj);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int villaId)
    {
        var villa = _context.villas.FirstOrDefault(x => x.Id == villaId);

        if (villa == null)
        {
            return NotFound();
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Delete(Villa obj)
    {
        var villa = _context.villas.Find(obj.Id);

        if (villa == null)
        {
            return NotFound();
        }

        _context.villas.Remove(villa);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
