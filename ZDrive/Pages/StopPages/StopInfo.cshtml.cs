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
    public class StopInfoModel : PageModel
    {
        private IStopsService stopService;
        public StopInfoModel(IStopsService _stopService)
        {
            stopService = _stopService;
        }
        public Stop Stop { get; private set; }
        public IActionResult OnGet(int stopID, int routeID)
        {
            Stop = stopService.GetStop(stopID);
            return Page();
        }
    }
}