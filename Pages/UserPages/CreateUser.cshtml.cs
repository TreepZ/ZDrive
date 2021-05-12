using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Services;
using ZDrive.Interfaces;

namespace ZDrive.Pages.UserPages
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        IUserService service;

        public CreateUserModel(IUserService service)
        {
            this.service = service;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            service.AddUser(User);
            return RedirectToPage("/UserPages/AllUsers");
        }
    }
}
