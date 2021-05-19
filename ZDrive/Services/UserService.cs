using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZDrive.Interfaces;
using ZDrive.Models;

namespace ZDrive.Services
{
    public class UserService : IUserService
    {
        private ZDriveIdentityDbContext server;
        private UserManager<IdentityUser> userManager;
        public UserService(ZDriveIdentityDbContext context, UserManager<IdentityUser> userManager)
        {
            server = context;
            this.userManager = userManager;
        }

        public IEnumerable<ZUser> AllUsers()
        {
            return server.ZUsers;
        }

        public void AddUser(ZUser u)
        {
            if (!isEmailValid(u.UserEmail))
            {
                throw new ArgumentException("Email Invalid");
            }
            else
            {
                server.ZUsers.Add(u);
                server.SaveChanges();
            }
        }
        public static bool isEmailValid(String UserEmail)
        {
            //if (!UserEmail.Contains("edu.easj"))
            //{
            //    throw new ArgumentException("Email must contain edu.easj");
            //}
            //else
            //{
            //    return true;
            //}

            return UserEmail.Contains("edu.easj");
        }

        public void DeleteUser(int id)
        {
            ZUser @user = server.ZUsers.Where(us => us.UserId == id).FirstOrDefault();
            server.ZUsers.Remove(@user);
            server.SaveChanges();
        }

        public void UpdateUser(ZUser u)
        {
            if (!isEmailValid(u.UserEmail))
            {
                throw new ArgumentException("Email Invalid");
            }
            else
            {
                ZUser @user = server.ZUsers.Where(us => us.UserId == u.UserId).FirstOrDefault();
                @user.UserName = u.UserName;
                @user.UserEmail = u.UserEmail;
                @user.UserType = u.UserType;
                server.SaveChanges();
            }
        }

        public ZUser GetZUserByIdentityID(string IdentityUserName)
        {
            IdentityUser Iuser = server.Users.Where(user => user.Email == IdentityUserName).FirstOrDefault();
            return server.ZUsers.Where(user => user.AspUserId == Iuser.Id).FirstOrDefault();
        }
    }
}
