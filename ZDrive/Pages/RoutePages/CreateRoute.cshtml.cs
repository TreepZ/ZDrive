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
    [Authorize(Roles = "Driver")]
    public class CreateRouteModel : PageModel
    {
        private IRouteService RouteService;
        private ICarService CarService;
        private IUserService UserService;
        [BindProperty]
        public Route Route { get; set; }
        public List<Car> Cars { get; set; }

        public CreateRouteModel(IRouteService rService, ICarService cService, IUserService userService)
        {
            UserService = userService;
            RouteService = rService;
            CarService = cService;
            Route = new Route();
            Cars = new List<Car>();
        }

        public void OnGet()
        {
            int uid = UserService.AllUsers().Where(u => u.UserEmail == User.Identity.Name).FirstOrDefault().UserId;
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
            return RedirectToPage("/RoutePages/UserRoutes", new { uid = Route.UserId});
        }

    }
}
