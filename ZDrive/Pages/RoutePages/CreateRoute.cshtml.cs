using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Interfaces;

namespace ZDrive.Pages.RoutePages
{
    public class CreateRouteModel : PageModel
    {
        private IRouteService Service;
        [BindProperty]
        public Route Route { get; set; }

        public CreateRouteModel(IRouteService service, IStopsService stopService)
        {
            Service = service;
            Route = new Route();
        }

        public void OnGet(int uid)
        {
            Route.UserId = uid;
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            Service.AddRoute(Route);
            return Redirect($"/RoutePages/UserRoutes?uid={Route.UserId}");
        }

    }
}
