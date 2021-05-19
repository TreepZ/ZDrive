using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Services;
using ZDrive.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ZDrive.Pages.UserPages
{
    [AllowAnonymous]
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public ZUser ZUser { get; set; }
        private IUserService service;
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<IdentityUser> signInManager;
        public CreateUserModel(IUserService service, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            this.service = service;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            IdentityUser user = userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (ZUser.UserType == "Driver")
            {
                var result2 = await userManager.AddToRoleAsync(user, "Driver");
            }
            var result = await userManager.AddToRoleAsync(user, "Passenger");
            ZUser.AspUserId = userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;
            service.AddUser(ZUser);
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToPage("../Index");
        }
    }
}
