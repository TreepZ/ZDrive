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

namespace ZDrive.Pages.UserPages
{
    public class UserInfoModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<ReservedSeat> ReservedSeats { get; set; }
        private IUserService service;
        private IReserveService reserveService;
        public UserInfoModel(IUserService service, IReserveService rService)
        {
            this.service = service;
            reserveService = rService;
        }
        public IActionResult OnGet()
        {
            Users = service.AllUsers();
            User = Users.FirstOrDefault();
            ReservedSeats = reserveService.GetReservedSeats();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (User.UserType == "Driver")
            {
                User.UserType = "Passenger";
            }
            else
            {
                User.UserType = "Driver";
            }
            service.UpdateUser(User);
            return Page();
        }
        public IActionResult OnPostDelete()
        {
            service.DeleteUser(User.UserId);
            //LOGOUT HERE
            return Page();
        }
    }
}
