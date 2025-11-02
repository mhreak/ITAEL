using DbEntities.Identity;
using Microsoft.AspNetCore.Identity;
using Web.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Service.Identity.Interface
{
    public interface IRoleManagerService
    {
        RoleViewModel Get(int roleId);
        IList<RoleViewModel> GetAll();
        int Add(RoleViewModel uiModel);
        bool Edit(RoleViewModel uiModel);
        bool IsDuplicate(int? id, string name);
        bool Delete(int id);
    }
}
