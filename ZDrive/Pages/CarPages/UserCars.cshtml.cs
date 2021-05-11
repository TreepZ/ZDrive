using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Interfaces;

namespace ZDrive.Pages.CarPages
{
    public class UserCarsModel : PageModel
    {
        private ICarService Service;
        private IUserService UService;
        public List<Car> Cars { get; private set; }
        public User User { get; private set; }

        public UserCarsModel(ICarService service, IUserService uservice)
        {
            Service = service;
            UService = uservice;
            User = new User();
        }

        public void OnGet(int uid)
        {
            Cars = Service.AllCars().Where(c => c.UserId == uid).ToList();
            User = UService.AllUsers().ToList().Find(u => u.UserId == uid);
        }
    }
}
