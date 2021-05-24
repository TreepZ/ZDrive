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
        public Car Car { get; private set; }
        ICarService service;

        public DeleteCarModel(ICarService service)
        {
            this.service = service;
        }
        public void OnGet(string cid)
        {
            Car = service.AllCars().Where(c => c.Licenseplate == cid).FirstOrDefault();
        }
        public IActionResult OnPost(string cid)
        {
            int returnID = service.GetCar(cid).UserId;
            service.DeleteCar(cid);
            return RedirectToPage("/CarPages/UserCars", new { uid = returnID });
        }
    }
}