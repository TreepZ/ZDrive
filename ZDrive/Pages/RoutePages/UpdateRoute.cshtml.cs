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
    public class UpdateRouteModel : PageModel
    {
        private IRouteService Service;
        private IStopsService StopService;
        public Route Route { get; set; }
        [BindProperty]
        public Stop Stop { get; set; }

        public UpdateRouteModel(IRouteService service, IStopsService stopService)
        {
            Service = service;
            StopService = stopService;
            Route = new Route();
            Stop = new Stop();
        }

        public void OnGet(int rid)
        {
            Stop.RouteId = rid;
            Route = Service.AllRoutes().ToList().Find(r => r.RouteId == rid);
            Route.Stops = StopService.AllStops().Where(s => s.RouteId == rid).ToList();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            StopService.AddStop(Stop);

            return Redirect($"/RoutePages/UpdateRoute?rid={Stop.RouteId}");
        }
    }
}
