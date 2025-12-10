using System.Collections.Generic;
using Web.Model.Identity;

namespace Web.Service.Identity.Interface
{
    public interface IUserRoleService
    {
        IList<UserRoleViewModel> GetAllByUserId(int userId);
        IList<UserRoleViewModel> GetAllByRoleId(int roleId);
        bool Add(UserRoleViewModel model);
    }
}
