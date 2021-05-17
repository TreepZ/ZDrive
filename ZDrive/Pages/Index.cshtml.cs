using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZDrive.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private RoleManager<IdentityRole> roleManager;
        public IndexModel(ILogger<IndexModel> logger, RoleManager<IdentityRole> _roleManager)
        {
            _logger = logger;
            roleManager = _roleManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await seedRolesAsync();
            return RedirectToPage("RoutePages/AllRoutes");
        }
        private async Task seedRolesAsync()
        {
            if (!roleManager.RoleExistsAsync("Driver").Result)
            {
                var r1 = await roleManager.CreateAsync(new IdentityRole("Driver"));
            }
            if (!roleManager.RoleExistsAsync("Passenger").Result)
            {
                var r2 = await roleManager.CreateAsync(new IdentityRole("Passenger"));
            }
        }
    }
}
