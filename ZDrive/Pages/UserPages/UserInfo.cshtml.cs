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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public ZUser ZUser { get; set; }
        public IEnumerable<ReservedSeat> ReservedSeats { get; set; }
        private IUserService service;
        private IReserveService reserveService;
        public UserInfoModel(IUserService service, IReserveService rService,
                                UserManager<IdentityUser> usermanager,SignInManager<IdentityUser> signInManager)
        {
            this.service = service;
            reserveService = rService;
            _userManager = usermanager;
            _signInManager = signInManager;
        }
        public IActionResult OnGet(string uid)
        {
            ZUser = service.AllUsers().Where(user => user.AspUserId == uid).FirstOrDefault();
            ReservedSeats = reserveService.GetReservedSeats();
            return Page();
        }
        public IActionResult OnPost()
        {
            ZUser = service.AllUsers().FirstOrDefault();
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
        public async Task<IActionResult> OnPostDelete()
        {
            service.DeleteUser(ZUser.UserId);

            IdentityUser user = await _userManager.GetUserAsync(User);
            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Account/LogIn");
        }
    }
}
