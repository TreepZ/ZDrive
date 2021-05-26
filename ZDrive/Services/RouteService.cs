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
    public class RouteService : IRouteService
    {
        private ZDriveIdentityDbContext server;
        public RouteService(ZDriveIdentityDbContext context)
        {
            server = context;
        }
        public void AddRoute(Route r)
        {
            server.Routes.Add(r);

            server.SaveChanges();
        }

        public IEnumerable<Route> AllRoutes()
        {
            return server.Routes.Include(r => r.User).Include(r => r.Car);
        }

        public void DeleteRoute(Route r)
        {
            r.ReservedSeats = server.ReservedSeats.Where(seat => seat.RouteId == r.RouteId).ToList();

            if (r.ReservedSeats.Count > 0)
            {
                foreach (var seat in r.ReservedSeats)
                {
                    server.ReservedSeats.Remove(seat);
                }
            }

            server.Routes.Remove(r);
            server.SaveChanges();
        }

        public Route GetRoute(int id)
        {
            return server.Routes.Include(r => r.User).Include(r => r.Car).Where(r => r.RouteId == id).FirstOrDefault();
        }

        public void UpdateRoute(Route r)
        {
            Route route = AllRoutes().ToList().Find(route => route.RouteId == r.RouteId);
            route.CarId = r.CarId;
            route.UserId = r.UserId;
            server.SaveChanges();
        }
    }
}
