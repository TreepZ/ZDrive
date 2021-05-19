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
        public ZUser ZUser { get; set; }
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
            ZUser = Users.FirstOrDefault();
            ReservedSeats = reserveService.GetReservedSeats();
            return Page();
        }
        public IActionResult OnPost()
        {
            ZUser = Users.FirstOrDefault();
            if (ZUser.UserType == "Driver")
            {
                ZUser.UserType = "Passenger";
            }
            else
            {
                ZUser.UserType = "Driver";
            }
            service.UpdateUser(ZUser);
            return Page();
        }
        public IActionResult OnPostDelete()
        {
            ZUser = Users.FirstOrDefault();
            service.DeleteUser(ZUser.UserId);
            //LOGOUT HERE
            return Page();
        }
    }
}
