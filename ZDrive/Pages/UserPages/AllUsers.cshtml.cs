using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Pages.UserPages
{
    public class AllUsersModel : PageModel
    {
        private IUserService Service;
        public List<User> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public User Filter { get; set; }

        public AllUsersModel(IUserService service)
        {
            Service = service;
            Users = new List<User>();
        }

        public void OnGet()
        {
            if (Filter.UserType != null)
            {
                Users = Service.AllUsers().Where(u => u.UserType == Filter.UserType).ToList();
            }
            else
                Users = Service.AllUsers().ToList();

        }
        public IActionResult OnPostDelete(int userID)
        {
            Service.DeleteUser(userID);

            return RedirectToPage("/UserPages/AllUsers");
        }
    }
}
