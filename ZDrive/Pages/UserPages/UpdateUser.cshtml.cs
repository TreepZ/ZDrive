using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Pages.UserPages
{
    [Authorize(Roles = "Driver, Passenger")]
    public class UpdateUserModel : PageModel
    {
        IUserService service;
        [BindProperty]
        public ZUser User { get; set; }
        public UpdateUserModel(IUserService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? id = 1)
        {
            if (id != null)
            {
                foreach (var u in service.AllUsers())
                {
                    if (u.UserId == id)
                        User = u;
                }
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            service.UpdateUser(User);
            return RedirectToPage("/UserPages/AllUsers");
        }
    }
}
