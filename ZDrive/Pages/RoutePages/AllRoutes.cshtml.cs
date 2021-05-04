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
                List<Stop> stops = StopService.AllStops().Where(s => s.StopAddress.ToLower().Contains(Filter.StopAddress.ToLower())).ToList();

                foreach(var stop in stops)
                {
                    Route route = RouteService.AllRoutes().ToList().Find(r => r.Stops.Contains(stop));
                    if (!Routes.Contains(route))
                    {
                        Routes.Add(route);
                    }
                }
            }
            else
            {
                Routes = RouteService.AllRoutes().ToList();
            }

            foreach (Route route in Routes)
            {
                route.Stops = StopService.AllStops().Where(s => route.RouteId == s.RouteId).ToList();
            }
        }

    }
}
