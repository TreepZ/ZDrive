using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Services
{
    public class CarService : ICarService
    {
        private ZdriveContext server;
        public CarService(ZdriveContext context)
        {
            server = context;
        }
        
        public IEnumerable<Car> AllCars()
        {
            return server.Cars;
        }
        public void AddCar(Car c)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(Car c)
        {
            throw new NotImplementedException();
        }

        public Car GetCar(int id)
        {
            return server.Cars.Where(c => c.UserId == id).FirstOrDefault();
        }

        public void UpdateCar(Car c)
        {
            Car car = server.Cars.Where(ca => ca.UserId == c.UserId).FirstOrDefault();
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
