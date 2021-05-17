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
    public class StopInfoModel : PageModel
    {
        private IStopsService stopService;
        public StopInfoModel(IStopsService _stopService)
        {
            stopService = _stopService;
        }
        public Stop Stop { get; private set; }
        public IActionResult OnGet(int stopID)
        {
            Stop = stopService.GetStop(stopID);
            return Page();
        }
    }
}