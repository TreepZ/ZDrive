using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Models;

namespace ZDrive.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> AllCars();
        void AddCar(Car c);
        void DeleteCar(string cid);
        void UpdateCar(Car c);
        Car GetCar(string id);
    }
}
