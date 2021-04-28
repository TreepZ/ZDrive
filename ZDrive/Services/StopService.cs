using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void DeleteStop(Stop s)
        {
            throw new NotImplementedException();
        }

        public Stop GetStop(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStop(Stop s)
        {
            throw new NotImplementedException();
        }
    }
}
