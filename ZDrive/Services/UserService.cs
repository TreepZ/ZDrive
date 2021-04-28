using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Services
{
    public class UserService : IUserService
    {
        private ZdriveContext server;
        public UserService(ZdriveContext context)
        {
            server = context;
        }
     
        public IEnumerable<User> AllUsers()
        {
            return server.Users;
        }

        public void AddUser(User u)
        {
            server.Users.Add(u);
            server.SaveChanges();
        }

        public void DeleteUser(User u)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User u)
        {
            throw new NotImplementedException();
        }
    }
}
