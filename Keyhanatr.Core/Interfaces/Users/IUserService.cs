using Keyhanatr.Data.Domain.User;
using Keyhanatr.Data.ViewModel.Account;
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
        bool IsExistMobile(string Mobile);

        bool RegisterUser(RegisterViewModel register);
        bool ActiveUser(string activeCode);
        User LoginUser(LoginViewModel login);

        bool IsUserPassword(int userId, string password);
        void ChangeUserPassword(int userId, string password);
        bool IsActive(int userId);

        bool ResetPassword(string activeCode,string password);
        User GetMobile(string mobileNumber);
        bool SendLink(MobileViewModel sendSms);
        
    }
}
