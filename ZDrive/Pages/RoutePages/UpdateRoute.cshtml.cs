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
    [Authorize(Roles = "Passenger")]
    public class UpdateRouteModel : PageModel
    {
        private IRouteService Service;
        private IStopsService StopService;
        private IUserService UserService;
        public Route Route { get; set; }
        [BindProperty]
        public Stop Stop { get; set; }
        public bool IsRouteOwner { get; private set; }

        public UpdateRouteModel(IRouteService service, IStopsService stopService, IUserService userservice)
        {
            Service = service;
            StopService = stopService;
            UserService = userservice;
            Route = new Route();
            Stop = new Stop();
        }

        public void OnGet(int rid)
        {
            Stop.RouteId = rid;
            Route = Service.GetRoute(rid);
            Route.Stops = StopService.AllStops().Where(s => s.RouteId == rid).ToList();
            if(Route.UserId == GetUserID())
            {
                IsRouteOwner = true;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet((int)Stop.RouteId);
                return Page();
            }
            StopService.AddStop(Stop);

            return RedirectToPage("/RoutePages/UpdateRoute", new { rid = Stop.RouteId });
        }

        public int GetUserID()
        {
            return UserService.GetZUserByIdentityID(User.Identity.Name).UserId;
        }
    }
}
