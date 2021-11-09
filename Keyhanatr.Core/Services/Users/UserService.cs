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
using Keyhanatr.Core.Interfaces.Message;
using Keyhanatr.Core.Senders;

namespace Keyhanatr.Core.Services.Users
{
    public class UserService : IUserService
    {
        private KeyhanatrContext _context;
        private IMessageSender _messageSender;
        public UserService(KeyhanatrContext context, IMessageSender messageSender)
        {
            _context = context;
            _messageSender= messageSender;
        }
        
        public bool IsExistMobile(string mobile)
        {
            return _context.Users.Any(u => u.Mobile == mobile.ToString());
        }
        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.HashNewPassword(login.Password);
            return _context.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == hashPassword);
        }
        public bool RegisterUser(RegisterViewModel register)
        {
            User newUser = new User()
            {
                UserName = register.UserName.Trim(),
                Mobile = register.Mobile.ToString(),
                //ActiveCode = Guid.NewGuid().ToString(),
                ActiveCode= "12345",
                IsActive = false,
                RoleId = 1,
                Rate=1,
                RegisterDate = DateTime.Now,
                Password = PasswordHelper.HashNewPassword(register.Password)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            MessageSender sms =new MessageSender();
            sms.SendMessage(newUser.Mobile, "ثبت نام شما در سایت کیهان عطر انجام شد، کد فعالسازی : " + newUser.ActiveCode);
            //string body = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views/EmailTemplates",
            //    "activeuser.html"));
            //body = body.Replace("{username}", newUser.UserName).Replace("{href}",
            //    $"{SiteResources.SiteDomain}/account/activeuser/{newUser.ActiveCode}");
            //_messageSender.SendEmail(newUser.Email, "فعالسازی", body);
            return true;
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
        public bool IsActive(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId && u.IsActive == true);
        }

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

        //#region ForegetPassword
        //public bool ForgetPassword(ForgetPasswordViewModel forgetpass)
        //{
        //    var useractive = _context.Users.Where(a => a.Email == forgetpass.Email.FixEmail()).
        //        Select(a => new { a.Email }).First().ToString();

        //    var users = _context.Users.First(m => m.Email == forgetpass.Email.FixEmail());
        //    users.Password = PasswordHelper.HashNewPassword(forgetpass.Password);
        //    _context.Update(users);
        //    _context.SaveChanges();
        //    return true;
        //}
        //public User GetEmail(EmailViewModel getemail)
        //{
        //    return _context.Users.SingleOrDefault(e => e.Email == getemail.Email.FixEmail());
        //}

        //public bool SendLink(EmailViewModel sendemail)
        //{
        //    var users = _context.Users.First(m => m.Email == sendemail.Email.FixEmail());
        //    //TODO Send Activation Email
        //    string body = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views/EmailTemplates",
        //        "recoverypassword.html"));
        //    body = body.Replace("{username}", users.UserName).Replace("{href}",
        //        $"{SiteResources.SiteDomain}/account/activepass/{users.ActiveCode}");
        //    _messageSender.SendEmail(users.Email = sendemail.Email.FixEmail(), "فعالسازی", body);
        //    return true;
        //}

        //public string GetUserActiveCode(string email)
        //{
        //    try
        //    {
        //        return _context.Users.FirstOrDefault(s => s.Email == email).ActiveCode;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
        //public string GetEmailforpass(string activecode)
        //{
        //    try
        //    {
        //        return _context.Users.FirstOrDefault(f => f.ActiveCode == activecode).Email;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
        //#endregion
        
    }
}

