using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using HomeAgency.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeAgency.Web.Controllers;

public class VillaNumberController : Controller
{

    private readonly IUnitOfWork _unitOfWork;

    public VillaNumberController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index() =>
        View(_unitOfWork.VillaNumber.GetAll(includeProperties: "Villa"));

    public IActionResult Create() =>
        View(new CreateVillaNumberViewModel
        {
            Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
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
            obj.Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            return View(obj);
        }

        if (_unitOfWork.VillaNumber.Any(v => v.Villa_Number == obj.VillaNumber.Villa_Number))
        {
            obj.Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });

            TempData["Error"] = "Villa number exists try another one";

            return View(obj);
        }

        _unitOfWork.VillaNumber.Add(obj.VillaNumber);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "A new villa has been created";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int villaNumberId)
    {
        var villaNumber = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == villaNumberId);

        if (villaNumber == null)
        {
            return NotFound();
        }

        return View(new CreateVillaNumberViewModel
        {
            VillaNumber = villaNumber,
            Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            })
        });
    }

    [HttpPost]
    public IActionResult Update(CreateVillaNumberViewModel villaNumberViewModel)
    {

        if (!ModelState.IsValid)
        {
            villaNumberViewModel.Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            return View(villaNumberViewModel);
        }

        _unitOfWork.VillaNumber.Update(villaNumberViewModel.VillaNumber);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "Villa has been updated";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int villaNumberId)
    {
        var villaNumber = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == villaNumberId);


        if (villaNumber == null)
        {
            return NotFound();
        }

        return View((new CreateVillaNumberViewModel
        {
            VillaNumber = villaNumber,
            Villas = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            })
        }));
    }

    [HttpPost]
    public IActionResult Delete(CreateVillaNumberViewModel villaNumberViewModel)
    {
        var villaNumber = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == villaNumberViewModel.VillaNumber.Villa_Number);

        if (villaNumber == null)
        {
            return NotFound();
        }

        _unitOfWork.VillaNumber.Remove(villaNumber);
        _unitOfWork.Save();

        TempData["OpSuccess"] = "Villa has been deleted";
        return RedirectToAction(nameof(Index));
    }
}
