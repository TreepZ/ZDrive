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
    [Authorize(Roles = "Passenger")]
    public class AllUsersModel : PageModel
    {
        private IUserService Service;
        public List<ZUser> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public ZUser Filter { get; set; }
        public AllUsersModel(IUserService service)
        {
            Service = service;
            Users = new List<ZUser>();
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
        public IActionResult OnPostDelete()
        {
            Service.DeleteUser(Filter.UserId);

            return RedirectToPage("/UserPages/AllUsers");
        }
    }
}
