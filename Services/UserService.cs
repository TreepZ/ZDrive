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
        private ZDriveIdentityDbContext server;
        public UserService(ZDriveIdentityDbContext context)
        {
            server = context;
        }
     
        public IEnumerable<User> AllUsers()
        {
            return server.Users;
        }

        public void AddUser(User u)
        {
            if (!isEmailValid(u.UserEmail))
            {
                throw new ArgumentException("Email Invalid");
            }
            else
            {
                server.Users.Add(u);
                server.SaveChanges();
            }
        }
        public static bool isEmailValid(String UserEmail)
        {
            if (!UserEmail.Contains("edu.easj"))
            {
                throw new ArgumentException("Email must contain edu.easj");
            }
            else
            {
                return true;
            }
        }

        public void DeleteUser(int id)
        {
            User @user = server.Users.Where(us => us.UserId == id).FirstOrDefault();
            server.Users.Remove(@user);
            server.SaveChanges();
        }

        public void UpdateUser(User u)
        {
            if (!isEmailValid(u.UserEmail))
            {
                throw new ArgumentException("Email Invalid");
            }
            else { 
            User @user = server.Users.Where(us => us.UserId == u.UserId).FirstOrDefault();
            @user.UserName = u.UserName;
            @user.UserEmail = u.UserEmail;
            @user.UserType = u.UserType;
            server.SaveChanges();
            }
        }

    }
}
