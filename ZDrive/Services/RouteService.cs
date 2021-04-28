using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public void DeleteRoute(Route r)
        {
            throw new NotImplementedException();
        }

        public Route GetRoute(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoute(Route r)
        {
            throw new NotImplementedException();
        }
    }
}
