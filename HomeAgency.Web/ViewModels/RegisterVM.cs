﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HomeAgency.Web.ViewModels;

public class RegisterVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    public string? ReturnUrl { get; set; }

    public string UserRole { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> RolesList { get; set; }
}
