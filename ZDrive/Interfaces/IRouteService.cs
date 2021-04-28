using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Models;

namespace ZDrive.Interfaces
{
    public interface IRouteService
    {
        void AddRoute(Route r);
        void DeleteRoute(Route r);
        //Emiiil, Ohhhh Emil!
        IEnumerable<Route> AllRoutes();
        void UpdateRoute(Route r);
        Route GetRoute(int id);
    }
}
