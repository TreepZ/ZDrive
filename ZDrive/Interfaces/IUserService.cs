using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Models;
//Fuck Ya'all
namespace ZDrive.Interfaces
{
    public interface IUserService
    {
        void AddUser(User u);
        IEnumerable<User> AllUsers();
        void DeleteUser(User u);
        void Update(User u);
        User GetUser(int id);
    }
}
