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
    [Authorize(Roles = "Driver, Passenger")]
    public class AllRoutesModel : PageModel
    {
        private IRouteService RouteService;
        private IStopsService StopService;
        private ICarService CarService;
        private IReserveService reserveService;
        private IUserService userService;
        public List<Route> Routes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }
        public AllRoutesModel(IRouteService rService, IStopsService sService, ICarService cService, 
                              IReserveService reService, IUserService userService)
        {
            RouteService = rService;
            StopService = sService;
            CarService = cService;
            reserveService = reService;
            this.userService = userService;
            Routes = new List<Route>();
        }

        public void OnGet()
        {
            if (Filter != null)
            {
                List<Stop> stops = StopService.AllStops().Where(s => s.StopAddress.ToLower().Contains(Filter.ToLower())).ToList();
                Routes = GetRoutesByStops(stops).OrderBy(r => r.RouteId).ToList();
            }
            else
            {
                Routes = RouteService.AllRoutes().OrderBy(r => r.RouteId).ToList();
            }

            foreach (Route route in Routes)
            {
                route.Stops = StopService.AllStops().Where(s => route.RouteId == s.RouteId).ToList();
                route.Car = CarService.AllCars().Where(car => car.Licenseplate == route.CarId).FirstOrDefault();
            }

        }

        public List<Route> GetRoutesByStops(List<Stop> stops)
        {
            List<Route> RouteList = new List<Route>();

            foreach (var stop in stops)
            {
                Route route = RouteService.AllRoutes().ToList().Find(r => r.RouteId == stop.RouteId);
                if (!RouteList.Contains(route))
                {
                    RouteList.Add(route);
                }
            }
            return RouteList;
        }

        public IActionResult OnPost(int rid)
        {
            Route route = RouteService.GetRoute(rid);
            route.Car = CarService.AllCars().ToList().Find(c => c.Licenseplate == route.CarId);
            try
            {
                reserveService.AddReservation(new ReservedSeat() { RouteId = rid, UserId = userService.GetZUserByIdentityID(User.Identity.Name).UserId });
            } catch(Exception e)
            {
                OnGet();
                TempData["ErrorMessage"] = "You already have a seat on this route";
                return Page();
            }
            route.Car.AvailableSeats--;
            CarService.UpdateCar(route.Car);
            OnGet();
            return Page();
        }
    }
}
