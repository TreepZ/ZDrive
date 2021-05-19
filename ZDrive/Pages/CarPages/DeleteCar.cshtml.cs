using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Services;
using ZDrive.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ZDrive.Pages.CarPages
{
    [Authorize(Roles = "Driver")]
    public class DeleteCarModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; }
        ICarService service;

        public DeleteCarModel(ICarService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? userID)
        {
            if (userID != null)
            {
                Car = service.AllCars().Where(c => c.UserID = userID);
            }
            //Car.UserId = userID;
            return Page();
        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return RedirectToPage("/CarPages/UserCars", new { uid = Car.UserId });
            }

            service.DeleteCar(Car);
            return RedirectToPage("/CarPages/UserCars", new { uid = Car.UserId });
        }
    }
}