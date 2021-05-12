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
    public class CreateRouteModel : PageModel
    {
        private IRouteService RouteService;
        private ICarService CarService;
        [BindProperty]
        public Route Route { get; set; }
        public List<Car> Cars { get; set; }

        public CreateRouteModel(IRouteService rService, ICarService cService)
        {
            RouteService = rService;
            CarService = cService;
            Route = new Route();
            Cars = new List<Car>();
        }

        public void OnGet(int uid)
        {
            Route.UserId = uid;
            Cars = CarService.AllCars().Where(car => car.UserId == uid).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RouteService.AddRoute(Route);
            return Redirect($"/RoutePages/UserRoutes?uid={Route.UserId}");
        }

    }
}
