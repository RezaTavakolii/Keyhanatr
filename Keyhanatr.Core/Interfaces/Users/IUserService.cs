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

        #region Admin

        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int userId);

        IEnumerable<Role> GetAllRoles();

        #endregion
        bool RegisterUser(RegisterViewModel register);
        bool IsExistEmail(string email);
        bool ActiveUser(string activeCode);
        User LoginUser(LoginViewModel login);

        bool IsUserPassword(int userId, string password);
        void ChangeUserPassword(int userId, string password);
    }
}
