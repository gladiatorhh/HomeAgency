using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeAgency.Web.Controllers;

public class VillaController : Controller
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index() =>
        View(_unitOfWork.Villa.GetAll());

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

        if (obj.Image is not null)
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "VillaImages", imageName);

            using var stream = new FileStream(path, FileMode.Create);
            obj.Image.CopyTo(stream);

            obj.ImageUrl = "/images/VillaImages/" + imageName;
        }
        else
        {
            obj.ImageUrl = "/images/VillaImages/default.png";
        }

        _unitOfWork.Villa.Add(obj);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "A new villa has been created";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int villaId)
    {
        var villa = _unitOfWork.Villa.Get(x => x.Id == villaId);

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

        _unitOfWork.Villa.Update(obj);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "Villa has been updated";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int villaId)
    {
        var villa = _unitOfWork.Villa.Get(x => x.Id == villaId);

        if (villa == null)
        {
            return NotFound();
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Delete(Villa obj)
    {
        var villa = _unitOfWork.Villa.Get(v => v.Id == obj.Id);

        if (villa == null)
        {
            return NotFound();
        }

        _unitOfWork.Villa.Remove(villa);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "Villa has been deleted";
        return RedirectToAction(nameof(Index));
    }
}
