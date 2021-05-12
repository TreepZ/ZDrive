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
    public class AddCarToUserModel : PageModel
    {
        private ICarService carService;
        [BindProperty]
        public IEnumerable<Car> Cars { get; set; }
        [BindProperty]
        public Car Car { get; set; }
        public AddCarToUserModel(ICarService service)
        {
            carService = service;
        }
        public IActionResult OnGet()
        {
            Cars = carService.AllCars();
            return Page();
        }
        public IActionResult OnPost()
        {
            Car car = carService.GetCar(Car.Licenseplate);
            car.UserId = 2;
            carService.UpdateCar(car);
            return RedirectToPage("AddCarToUser");
        }
    }
}
