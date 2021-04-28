using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Pages.StopPages
{
    public class CreateStopModel : PageModel
    {
        private IStopsService stopService;
        public CreateStopModel(IStopsService _stopService)
        {
            stopService = _stopService;
        }
        [BindProperty]
        public Stop AddStop { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            AddStop.StopTimestamp = DateTime.Now;
            stopService.AddStop(AddStop);
            return RedirectToPage("../Index");
        }
    }
}