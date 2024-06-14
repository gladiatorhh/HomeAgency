using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Application.Common.Utility;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Common.Impelementations;
using HomeAgency.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeAgency.Web.Controllers;

[Authorize(Roles = SD.Role_Admin)]
public class AmenityController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AmenityController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index() =>
        View(_unitOfWork.Amenity.GetAll(includeProperties: "Villa"));

    public IActionResult Create()
        => View(new CreateAmenityViewModel
        {
            Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            })
        });

    [HttpPost]
    public IActionResult Create(CreateAmenityViewModel obj)
    {
        ModelState.Remove("Amenity.Villa");
        if (!ModelState.IsValid)
        {
            obj.Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            return View(obj);
        }

        _unitOfWork.Amenity.Add(obj.Amenity);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "A new Amenity has been created";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int amenityId)
    {
        var Amenity = _unitOfWork.Amenity.Get(x => x.Id == amenityId);

        if (Amenity == null)
        {
            return NotFound();
        }

        return View(new CreateAmenityViewModel
        {
            Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }),
            Amenity = Amenity
        });
    }

    [HttpPost]
    public IActionResult Update(CreateAmenityViewModel obj)
    {
        ModelState.Remove("Amenity.Villa");
        if (!ModelState.IsValid)
        {
            obj.Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            return View(obj);
        }

        _unitOfWork.Amenity.Update(obj.Amenity);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "Amenity has been updated";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int amenityId)
    {
        var amenity = _unitOfWork.Amenity.Get(x => x.Id == amenityId);

        if (amenity == null)
        {
            return NotFound();
        }

        return View(new CreateAmenityViewModel
        {
            Amenity = amenity,
            Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            })
        });
    }

    [HttpPost]
    public IActionResult Delete(CreateAmenityViewModel obj)
    {
        var amenity = _unitOfWork.Amenity.Get(v => v.Id == obj.Amenity.Id);

        if (amenity == null)
        {
            obj.Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            return NotFound();
        }

        _unitOfWork.Amenity.Remove(amenity);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "Amenity has been deleted";
        return RedirectToAction(nameof(Index));
    }
}
