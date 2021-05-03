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
        private ZdriveContext server;
        public StopService(ZdriveContext context)
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
            return server.Stops;
        }

        public void DeleteStop(Stop s)
        {
            throw new NotImplementedException();
        }

        public Stop GetStop(int stopId)
        {
            return server.Stops
                .Include(s => s.Route)
                .ThenInclude(r => r.User)
                .Where(s => s.StopId == stopId).FirstOrDefault();
                //.ThenInclude(u => u.Cars.Where(c => c.UserId == u.UserId))
            //throw new NotImplementedException();
        }

        public void UpdateStop(Stop s)
        {
            throw new NotImplementedException();
        }
    }
}
