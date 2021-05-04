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
            if (!isPassValid(u.UserPass))
            {
                throw new ArgumentException("Password doesn't meet requirements");
            }
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
            if (!UserEmail.Contains(".edu.easj"))
            {
                throw new ArgumentException("Email must contain .edu.easj");
            }
            else
            {
                return true;
            }
        }
        public static bool isPassValid(String UserPass)
        {
            //Pass length between 6 and 20 characters
            if (!((UserPass.Length >= 6)
                && (UserPass.Length <= 20)))
            {
                return false;
            }
            // to check space
            if (UserPass.Contains(" "))
            {
                return false;
            }
            if (true)
            {
                int count = 0;
                //Digits from 0-9
                for (int i = 0; i <= 9; i++)
                {
                    //Converting int to string
                    String str1 = i.ToString();

                    if (UserPass.Contains(str1))
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
            }
            // Pass MUST contain AT LEAST one of these
            if (!(UserPass.Contains("@") || UserPass.Contains("#")
                || UserPass.Contains("!") || UserPass.Contains("~")
                || UserPass.Contains("$") || UserPass.Contains("%")
                || UserPass.Contains("^") || UserPass.Contains("&")
                || UserPass.Contains("*") || UserPass.Contains("(")
                || UserPass.Contains(")") || UserPass.Contains("-")
                || UserPass.Contains("+") || UserPass.Contains("/")
                || UserPass.Contains(":") || UserPass.Contains(".")
                || UserPass.Contains(", ") || UserPass.Contains("<")
                || UserPass.Contains(">") || UserPass.Contains("?")
                || UserPass.Contains("|")))
            {
                return false;
            }
            if (true)
            {
                int count = 0;

                //Checking for capital letters
                for (int i = 65; i <= 90; i++)
                {
                    // type casting
                    char c = (char)i;
                    String str1 = c.ToString();
                    if (UserPass.Contains(str1))
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
            }

            if (true)
            {
                int count = 0;

                // Checking small letters
                for (int i = 90; i <= 122; i++)
                {
                    // type casting
                    char c = (char)i;
                    String str1 = c.ToString();

                    if (UserPass.Contains(str1))
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
            }

            // if all conditions fails
            return true;
        }

        public void DeleteUser(int id)
        {
            User @user = server.Users.Where(us => us.UserId == id).FirstOrDefault();
            server.Users.Remove(@user);
            server.SaveChanges();
        }

        public void UpdateUser(User u)
        {
            User @user = server.Users.Where(us => us.UserId == u.UserId).FirstOrDefault();
            @user.UserName = u.UserName;
            @user.UserPass = u.UserPass;
            @user.UserEmail = u.UserEmail;
            @user.UserType = u.UserType;
            server.SaveChanges();
        }

    }
}
