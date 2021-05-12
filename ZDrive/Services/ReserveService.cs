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
            context.ReservedSeats.Add(seat);
            context.SaveChanges();
        }

        public IEnumerable<ReservedSeat> GetReservedSeats()
        {
            return context.ReservedSeats.Include(r => r.Route);
        }
    }
}