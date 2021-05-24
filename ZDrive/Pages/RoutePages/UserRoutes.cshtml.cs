using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ZDrive.Pages.RoutePages
{
    [Authorize(Roles = "Driver")]
    public class UserRoutesModel : PageModel
    {
        private IRouteService RouteService;
        private IStopsService StopService;
        public List<Route> Routes { get; set; }

        public UserRoutesModel(IRouteService rService, IStopsService sService)
        {
            RouteService = rService;
            StopService = sService;
            Routes = new List<Route>();
        }

        public void OnGet(int uid)
        {
            Routes = RouteService.AllRoutes().Where(r => r.UserId == uid).ToList();
            foreach (Route route in Routes)
            {
                route.Stops = StopService.AllStops().Where(s => s.RouteId == route.RouteId).ToList();
            }
        }

        public IActionResult OnPostDelete(int rid, int uid)
        {
            Route r = RouteService.GetRoute(rid);
            RouteService.DeleteRoute(r);
            OnGet(uid);
            return Page();
        }
    }
}
