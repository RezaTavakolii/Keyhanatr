using Keyhanatr.Data.Domain.User;
using Keyhanatr.Data.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyhanatr.Core.Interfaces.Users
{
    public interface IUserService
    {

        User GetUserById(int userId);

        bool RegisterUser(RegisterViewModel register);
        bool IsExistMobile(string Mobile);
        bool ActiveUser(string activeCode);
        User LoginUser(LoginViewModel login);
        bool IsUserPassword(int userId, string password);
        void ChangeUserPassword(int userId, string password);
        bool IsActive(int userId);


        //bool ForgetPassword(ForgetPasswordViewModel forgetpass);
        //User GetEmail(EmailViewModel getemail);
        //bool SendLink(EmailViewModel sendemail);
        //string GetEmailforpass(string activecode);
        //string GetUserActiveCode(string email);
    }
}
