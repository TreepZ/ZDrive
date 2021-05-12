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
        void AddUser(User u);
        IEnumerable<User> AllUsers();
        void DeleteUser(int id);
        void UpdateUser(User u);
    }
}
