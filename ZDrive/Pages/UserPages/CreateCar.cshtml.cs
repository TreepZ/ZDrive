using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Services;
using ZDrive.Interfaces;

namespace ZDrive.Pages.UserPages
{
    public class CreateCarModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; }
        ICarService service;

        public CreateCarModel(ICarService service)
        {
            this.service = service;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            service.AddCar(Car);
            return RedirectToPage("/UserPages/AddCarToUser");
        }
    }
}