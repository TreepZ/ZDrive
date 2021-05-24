using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Services
{
    public class CarService : ICarService
    {
        private ZDriveIdentityDbContext server;
        public CarService(ZDriveIdentityDbContext context)
        {
            server = context;
        }

        public IEnumerable<Car> AllCars()
        {
            return server.Cars;
        }
        public void AddCar(Car c)
        {
            try
            {
                server.Cars.Add(c);
            }
            catch (DbUpdateException dbex)
            {
                throw dbex;
            }
            server.SaveChanges();
        }

        public void DeleteCar(string cid)
        {
            Car car = GetCar(cid);
            car.Routes = server.Routes.Where(r => r.CarId == cid).ToList();
            if (car.Routes.Count > 0)
            {
                Route route = server.Routes.Where(r => r.RouteId == car.Routes.FirstOrDefault().RouteId).FirstOrDefault();
                server.Routes.Remove(route);
            }

            server.Cars.Remove(car);
            server.SaveChanges();
        }

        public Car GetCar(string id)
        {
            return server.Cars.Where(c => c.Licenseplate.Equals(id)).FirstOrDefault();
        }

        public void UpdateCar(Car c)
        {
            Car car = server.Cars.Where(ca => ca.Licenseplate == c.Licenseplate).FirstOrDefault();
            car.AvailableSeats = c.AvailableSeats;
            car.Licenseplate = c.Licenseplate;
            car.NumberOfSeats = c.NumberOfSeats;
            car.SizeOfCar = c.SizeOfCar;
            car.User = c.User;
            car.UserId = c.UserId;
            server.SaveChanges();
        }
    }
}
