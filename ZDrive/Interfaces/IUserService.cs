using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Models;

namespace ZDrive.Interfaces
{
    public interface IUserService
    {
        void AddUser(ZUser u);
        IEnumerable<ZUser> AllUsers();
        void DeleteUser(int id);
        void UpdateUser(ZUser u);
    }
}
