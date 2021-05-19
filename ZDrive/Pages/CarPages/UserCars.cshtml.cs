using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ZDrive.Pages.CarPages
{
    [Authorize(Roles = "Driver")]
    public class UserCarsModel : PageModel
    {
        private ICarService Service;
        private IUserService UService;
        public List<Car> Cars { get; private set; }
        public ZUser ZUser { get; private set; }

        public UserCarsModel(ICarService service, IUserService uservice)
        {
            Service = service;
            UService = uservice;
            ZUser = new ZUser();
        }

        public void OnGet(int uid)
        {
            Cars = Service.AllCars().Where(c => c.UserId == uid).ToList();
            ZUser = UService.AllUsers().ToList().Find(u => u.UserId == uid);
        }
    }
}
