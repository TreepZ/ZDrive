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
    public class ReserveService : IReserveService
    {
        private ZDriveIdentityDbContext context;
        public ReserveService(ZDriveIdentityDbContext _context)
        {
            context = _context;
        }

        public void AddReservation(ReservedSeat seat)
        {
            if(context.ReservedSeats.Contains(seat))
            {
                throw new Exception("This user already has a seat reserved for this route");
            }
            else 
                context.ReservedSeats.Add(seat);
                context.SaveChanges();
        }

        public IEnumerable<ReservedSeat> GetReservedSeats()
        {
            return context.ReservedSeats.Include(r => r.Route).Include(r => r.User);
        }

        public void RemovePassenger(int rid, int uid)
        {
            ReservedSeat instance = GetReservedSeats().Where(r => r.RouteId == rid && r.UserId == uid).FirstOrDefault();
            context.ReservedSeats.Remove(instance);

            Car car = context.Cars.Where(c => c.Routes.FirstOrDefault().RouteId == rid).FirstOrDefault();
            car.AvailableSeats++;
            context.Cars.Update(car);

            context.SaveChanges();
        }
    }
}