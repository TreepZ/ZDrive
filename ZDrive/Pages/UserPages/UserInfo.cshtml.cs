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
using Microsoft.AspNetCore.Identity;

namespace ZDrive.Pages.UserPages
{
    [Authorize(Roles = "Driver, Passenger")]
    public class UserInfoModel : PageModel
    {
        private UserManager<IdentityUser> userManager;
        [BindProperty]
        public ZUser User { get; set; }
        public IEnumerable<ZUser> Users { get; set; }
        public IEnumerable<ReservedSeat> ReservedSeats { get; set; }
        private IUserService service;
        private IReserveService reserveService;
        public UserInfoModel(IUserService service, IReserveService rService, UserManager<IdentityUser> userManager)
        {
            this.service = service;
            reserveService = rService;
            this.userManager = userManager;
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
