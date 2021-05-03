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
    public class AllRoutesModel : PageModel
    {
        private IRouteService RouteService;
        private IStopsService StopService;
        public List<Route> Routes { get; set; }
        [BindProperty(SupportsGet = true)]
        public Stop Filter { get; set; }

        public AllRoutesModel(IRouteService rService, IStopsService sService)
        {
            RouteService = rService;
            StopService = sService;
            Routes = new List<Route>();
        }

        public void OnGet()
        {
            if (Filter.StopAddress != null)
            {
                foreach (Stop stop in StopService.AllStops().ToList().Where(s => s.StopAddress.Equals(Filter.StopAddress)))
                {
                    Routes.Add(RouteService.AllRoutes().ToList().Find(r => r.RouteId == stop.RouteId));
                }
            }
            else
            {
                Routes = RouteService.AllRoutes().ToList();
            }

            foreach (Route route in Routes)
            {
                route.Stops = StopService.AllStops().Where(s => s.RouteId == route.RouteId).ToList();
            }
        }
    }
}
