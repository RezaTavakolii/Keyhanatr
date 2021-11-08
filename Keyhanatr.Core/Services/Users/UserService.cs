using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Keyhanatr.Core.Security;
using Keyhanatr.Data.Domain.User;
using Keyhanatr.Data.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Services.Users
{
    public class UserService : IUserService
    {
        private KeyhanatrContext _context;
        public UserService(KeyhanatrContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Role);
        }
        public User GetUserById(int userId)
        {
            return _context.Users.Include(u => u.Role).SingleOrDefault(u => u.UserId == userId);
        }
        public void AddUser(User user)
        {
            user.Password = PasswordHelper.HashNewPassword(user.Password);
            user.RegisterDate = DateTime.Now;
            user.ActiveCode = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void EditUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            _context.Remove(user);
            _context.SaveChanges();
        }
        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles;
        }
        public bool RegisterUser(RegisterViewModel register)
        {
            User newUser = new User()
            {
                Mobile = register.Mobile.ToString(),
                UserName = register.UserName.Trim(),
                ActiveCode = Guid.NewGuid().ToString(),
                IsActive = false,
                RoleId = 1,
                RegisterDate = DateTime.Now,
                Password = PasswordHelper.HashNewPassword(register.Password)
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //TODO Send Activation Email

            //string body = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views/EmailTemplates",
            //    "activeuser.html"));
            //body = body.Replace("{username}", newUser.UserName).Replace("{href}",
            //    $"{SiteResources.SiteDomain}/account/activeuser/{newUser.ActiveCode}");
            //_messageSender.SendEmail(newUser.Email, "فعالسازی", body);

            return true;
        }
        public bool IsExistEmail(string mobile)
        {
            return _context.Users.Any(u => u.Mobile == mobile.ToString());
        }
        public bool ActiveUser(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null)
                return false;

            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }
        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.HashNewPassword(login.Password);
            return _context.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == hashPassword);
        }
        public bool IsUserPassword(int userId, string password)
        {
            return _context.Users.Any(u =>
                u.UserId == userId && u.Password == PasswordHelper.HashNewPassword(password));
        }
        public void ChangeUserPassword(int userId, string password)
        {
            var user = _context.Users.Find(userId);
            user.Password = PasswordHelper.HashNewPassword(password);
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}

