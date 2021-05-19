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
        private IUserService UserService;
        private IReserveService ReserveService;
        public Route Route { get; set; }
        public List<ZUser> ZUsers { get; set; }

        public UsersOnRouteModel(IRouteService rService, IUserService uService, IReserveService ReserveService)
        {
            UserService = uService;
            RouteService = rService;
            this.ReserveService = ReserveService;
            ZUsers = new List<ZUser>();
        }

        public void OnGet(int rid)
        {
            Route = RouteService.AllRoutes().Where(r => r.RouteId == rid).FirstOrDefault();
            IEnumerable<ReservedSeat> reservations = ReserveService.GetReservedSeats().Where(r => r.RouteId == rid);
            foreach (var r in reservations)
            {
                ZUsers.Add(r.User);
            }
            /* foreach (Route route in Routes)
            {
                route.Stops = StopService.AllStops().Where(s => s.RouteId == route.RouteId).ToList();
            } */
        }

        public IActionResult OnPostDelete(int rid, int uid)
        {
            Route r = RouteService.GetRoute(rid);
            RouteService.DeleteRoute(r);
            return RedirectToPage("User");
        }
    }
}
