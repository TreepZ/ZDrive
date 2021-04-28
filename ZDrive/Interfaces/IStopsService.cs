using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Models;
//You suck
namespace ZDrive.Interfaces
{
    public interface IStopsService
    {
        void AddStop(Stop s);
        void DeleteStop(Stop s);
        void UpdateStop(Stop s);
        Stop GetStop(int id);
    }
}
