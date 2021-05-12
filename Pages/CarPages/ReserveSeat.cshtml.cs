using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Pages.CarPages
{
    public class ReserveSeatModel : PageModel
    {
        private ICarService carService;
        private IRouteService routeService;
        [BindProperty]
        public IEnumerable<Car> Cars { get; set; }
        [BindProperty]
        public Car Car { get; set; }
        public ReserveSeatModel(ICarService service, IRouteService rService)
        {
            carService = service;
            routeService = rService;
        }
        public IActionResult OnGet(int routeId)
        {
            Cars = carService.AllCars().Where(c => c.AvailableSeats >= 0);
            return Page();
        }
        public IActionResult OnPost()
        {
            Car car = carService.GetCar(Car.Licenseplate);
            car.AvailableSeats -= 1;
            carService.UpdateCar(car);
            return RedirectToPage("ReserveSeat");
        }
    }
}
