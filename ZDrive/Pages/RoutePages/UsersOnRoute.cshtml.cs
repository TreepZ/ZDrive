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
    public class UsersOnRouteModel : PageModel
    {
        private IRouteService RouteService;
        private IStopsService StopService;
        private IUserService UserService;
        public List<Route> Routes { get; set; }
        public int UserID { get; set; }

        public UsersOnRouteModel(IRouteService rService, IStopsService sService, IUserService uService)
        {
            UserService = uService;
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
            return RedirectToPage("User");
        }
    }
}
