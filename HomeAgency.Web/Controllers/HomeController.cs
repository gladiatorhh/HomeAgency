using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Web.Models;
using HomeAgency.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeAgency.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() =>
            View(new HomeVM
            {
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity")
            });

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
