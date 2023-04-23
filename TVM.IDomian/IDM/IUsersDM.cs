using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVM.DTO.Users;
using TVM.Model.CM;

namespace TVM.IDomian.IDM
{
    public interface IUsersDM
    {
        string Login(string userName, string password, out string message);

        List<UsersDTO> GetUsersList(Request_Users dto, out int count);
        bool AddUsers(UsersModel model);

        bool UpUsers(UsersModel model);

        bool DeUsers(int id);
    }
}
