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
        private IUserService service;

        public UserInfoModel(IUserService service)
        {
            this.service = service;
        }
        public IActionResult OnGet()
        {
            Users = service.AllUsers();
            User = Users.FirstOrDefault();
            return Page();
        }
    }
}
