using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Interfaces;

namespace ZDrive.Pages.StopPages
{
    public class DeleteRouteStopModel : PageModel
    {
        private IStopsService StopService;
        [BindProperty]
        public Stop RouteStop { get; set; }
        private int RouteID;

        public DeleteRouteStopModel(IStopsService service)
        {
            StopService = service;
            RouteStop = new Stop();
        }

        public void OnGet(int id)
        {
            RouteStop = StopService.GetStop(id);
        }

        public IActionResult OnPost()
        {
            RouteID = (int)RouteStop.RouteId;
            StopService.DeleteStop(RouteStop);

            return RedirectToPage("/RoutePages/UpdateRoute", new { rid = RouteID});
        }
    }
}
