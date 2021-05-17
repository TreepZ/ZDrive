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
    public class AllStopsModel : PageModel
    {
        private IStopsService stopsService;
        public AllStopsModel(IStopsService _stopsService)
        {
            stopsService = _stopsService;
        }
        public IEnumerable<Stop> Stops { get; set; }
        public IActionResult OnGet()
        {
            Stops = stopsService.AllStops();
            return Page();
        }
    }
}