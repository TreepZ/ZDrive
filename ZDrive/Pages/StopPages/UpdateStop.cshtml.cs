using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Models;
using ZDrive.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ZDrive.Pages.StopPages
{
    [Authorize(Roles = "Driver")]
    public class UpdateStopModel : PageModel
    {
        private IStopsService StopService;
        [BindProperty]
        public Stop Stop { get; set; }

        public UpdateStopModel(IStopsService Service)
        {
            StopService = Service;
        }

        public void OnGet(int id)
        {
            Stop = StopService.GetStop(id);
        }

        public IActionResult OnPost()
        {
            StopService.UpdateStop(Stop);
            return RedirectToPage("/RoutePages/UpdateRoute", new { rid = Stop.RouteId });
        }
    }
}
