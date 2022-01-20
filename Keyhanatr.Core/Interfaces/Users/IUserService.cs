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
        #region Register & Login
        bool IsExistMobile(string Mobile);
        bool RegisterUser(RegisterViewModel register);
        bool ActiveUser(string activeCode);
        User LoginUser(LoginViewModel login);
        #endregion

        #region Change & ForgetPassword
        bool IsUserPassword(int userId, string password);
        void ChangeUserPassword(int userId, string password);
        bool IsActive(int userId);
        bool ResetPassword(string activeCode,string password);
        User GetMobile(string mobileNumber);
        bool SendLink(MobileViewModel sendSms);
        #endregion

        #region Admin User CRUD 
        User GetUserById(int userId);
        int GetUserIdByUserName(string userName);
        IEnumerable<User> GetAllUser();
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int userId);
        IEnumerable<Role> GetAllRoles();
        #endregion
    }
}
