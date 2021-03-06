using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Pages.StopPages
{
    [Authorize(Roles = "Driver")]
    public class CreateStopModel : PageModel
    {
        private IStopsService stopService;
        public CreateStopModel(IStopsService _stopService)
        {
            stopService = _stopService;
        }
        [BindProperty]
        public Stop AddStop { get; set; }
        public IActionResult OnGet(int? routeId)
        {
            AddStop = new Stop();
            if (routeId != null)
            {
                AddStop.RouteId = routeId;
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            stopService.AddStop(AddStop);
            if (AddStop.RouteId == null)
            {
                return RedirectToPage("../Index");
            }
            return RedirectToPage("../RoutePages/UpdateRoute", new { rid = AddStop.RouteId });
        }
    }
}