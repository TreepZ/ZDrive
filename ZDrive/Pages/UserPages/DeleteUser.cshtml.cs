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
    public class DeleteUserModel : PageModel
    {
        IUserService service;

        [BindProperty]
        public User User { get; set; }

        public DeleteUserModel(IUserService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? id)
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

        public IActionResult OnPost(int id)
        {
            service.DeleteUser(id);

            return RedirectToPage("/UserPages/AllUsers");
        }


    }
}