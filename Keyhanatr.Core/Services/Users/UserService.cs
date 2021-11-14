using Keyhanatr.Core.Interfaces.Users;
using Keyhanatr.Data.Context;
using Keyhanatr.Core.Security2;
using Keyhanatr.Data.Domain.User;
using Keyhanatr.Data.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keyhanatr.Core.Interfaces.Message;
using Keyhanatr.Core.Senders;
using Keyhanatr.Data.ViewModel.Account;

namespace Keyhanatr.Core.Services.Users
{
    public class UserService : IUserService
    {
        private KeyhanatrContext _context;
        private IMessageSender _messageSender;
        public UserService(KeyhanatrContext context, IMessageSender messageSender)
        {
            _context = context;
            _messageSender = messageSender;
        }

        #region RegisterUser & ActiveUser
        public bool RegisterUser(RegisterViewModel register)
        {
            var rand = new Random().Next(100000,999999);
            User newUser = new User()
            {
                UserName = register.UserName.Trim(),
                Mobile = register.Mobile.ToString(),
                ActiveCode = rand.ToString(),
                IsActive = false,
                RoleId = 1,
                Rate = 1,
                RegisterDate = DateTime.Now,
                Password = PasswordHelper.HashNewPassword(register.Password)
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            MessageSender sms = new MessageSender();
            sms.SendMessage(newUser.Mobile, newUser.ActiveCode, "Verify");
            return true;
        }
        public bool ActiveUser(string activeCode)
        {
            var rand = new Random().Next(100000, 999999);
            var user = _context.Users.FirstOrDefault(u => u.IsActive == false && u.ActiveCode == activeCode);
            if (user != null)
            {
                user.ActiveCode = rand.ToString();
                user.IsActive = true;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsActive(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId && u.IsActive == true);
        }
        public bool IsExistMobile(string mobile)
        {
            return _context.Users.Any(u => u.Mobile == mobile.ToString());
        }
        #endregion

        #region change password
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
        #endregion

        #region Login
        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.HashNewPassword(login.Password);
            return _context.Users.SingleOrDefault(u => u.Mobile == login.Mobile && u.Password == hashPassword);
        }
        #endregion

        #region ForegetPassword
        public bool ResetPassword(string activeCode, string password)
        {
            var rand = new Random().Next(100000, 999999);
            var user = _context.Users.FirstOrDefault(a => a.ActiveCode == activeCode && a.IsActive == true);
            if (user != null)
            {
                user.Password = PasswordHelper.HashNewPassword(password);
                user.ActiveCode = rand.ToString();
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
           
        }
        public User GetMobile(string mobileNumber)
        {
            return _context.Users.FirstOrDefault(e => e.Mobile == mobileNumber);
        }

        public bool SendLink(MobileViewModel sendSms)
        {
            var users = _context.Users.First(m => m.Mobile == sendSms.Mobile);

            MessageSender sms = new MessageSender();
            sms.SendMessage(users.Mobile, users.ActiveCode, "Verify");
            return true;
        }
        #endregion

        #region UserCrud
        public IEnumerable<User> GetAllUser()
        {
            return _context.Users.Include(u => u.Role);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Include(u => u.Role)
                .SingleOrDefault(u => u.UserId == userId);
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
        #endregion

        //public User GetUserById(int userId)
        //{
        //    return _context.Users.FirstOrDefault(u => u.UserId == userId);
        //}
    }
}

