using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Models;

namespace ZDrive.Interfaces
{
    public interface IStopsService
    {
        void AddStop(Stop s);
        IEnumerable<Stop> AllStops();
        void DeleteStop(Stop stop);
        void UpdateStop(Stop s);
        Stop GetStop(int stopId);
    }
}
