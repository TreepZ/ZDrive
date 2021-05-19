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
    public class UpdateCarModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; }
        ICarService service;

        public UpdateCarModel(ICarService service)
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
                return Page();
            }

            try
            {
                service.UpdateCar(Car);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "License plate already registered");
                return Page();
            }
            return RedirectToPage("/CarPages/UserCars", new { uid = Car.UserId });
        }
    }
}