using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Application.Common.Utility;
using HomeAgency.Domain.Entities;
using HomeAgency.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace HomeAgency.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IActionResult Login(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        LoginVM loginVM = new()
        {
            ReturnUrl = returnUrl,
        };

        return View(loginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("","Invalid credentials");
            return View(loginVM);
        }

        if (!string.IsNullOrEmpty(loginVM.ReturnUrl))
        {
            return LocalRedirect(loginVM.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    public IActionResult Register(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
        }

        RegisterVM registerVM = new()
        {
            RolesList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }),
            ReturnUrl = returnUrl
        };

        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
        {
            registerVM = new()
            {
                RolesList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
            };

            return View(registerVM);
        }

        ApplicationUser user = new()
        {
            Name = registerVM.Name,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
            NormalizedEmail = registerVM.Email.ToUpper(),
            EmailConfirmed = true,
            UserName = registerVM.Email,
            CreatedAt = DateTime.UtcNow,
        };

        var resutl = await _userManager.CreateAsync(user, registerVM.Password);


        if (resutl.Succeeded)
        {
            if (!string.IsNullOrEmpty(registerVM.UserRole))
            {
                await _userManager.AddToRoleAsync(user, registerVM.UserRole);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            }
        }
        else
        {
            foreach (var error in resutl.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            registerVM = new()
            {
                RolesList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
            };

            return View(registerVM);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        if (!string.IsNullOrEmpty(registerVM.ReturnUrl))
        {
            return LocalRedirect(registerVM.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }


    public IActionResult AccessDenied()
    {
        return View();
    }
}
