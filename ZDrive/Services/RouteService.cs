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
        private ZdriveContext server;
        public RouteService(ZdriveContext context)
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
            return server.Routes;
        }

        public void DeleteRoute(Route r)
        {
            throw new NotImplementedException();
        }

        public Route GetRoute(int id)
        {
            return server.Routes.Include(r => r.User).Where(r => r.RouteId == id).FirstOrDefault();
        }

        public void UpdateRoute(Route r)
        {
            throw new NotImplementedException();
        }
    }
}
