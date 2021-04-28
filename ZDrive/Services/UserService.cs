﻿using System;
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

        public void DeleteUser(int id)
        {
            User @user = server.Users.Where(us => us.UserId == id).FirstOrDefault();
            server.Users.Remove(@user);
            server.SaveChanges();
        }

        public void Update(User u)
        {
            throw new NotImplementedException();
        }
    }
}
