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
    public class StopService : IStopsService
    {
        private ZDriveIdentityDbContext server;
        public StopService(ZDriveIdentityDbContext context)
        {
            server = context;
        }
        public void AddStop(Stop s)
        {
            server.Stops.Add(s);
            server.SaveChanges();
        }

        public IEnumerable<Stop> AllStops()
        {
            return server.Stops.OrderBy(s => s.StopTimestamp);
        }

        public void DeleteStop(Stop stop)
        {
            server.Stops.Remove(stop);
            server.SaveChanges();
        }

        public Stop GetStop(int stopId)
        {
            return server.Stops
                .Include(s => s.Route)
                .ThenInclude(r => r.User)
                .Where(s => s.StopId == stopId).FirstOrDefault();
                //.ThenInclude(u => u.Cars.Where(c => c.UserId == u.UserId))
        }

        public void UpdateStop(Stop s)
        {
            server.Stops.Update(s);
            server.SaveChanges();
        }
    }
}
