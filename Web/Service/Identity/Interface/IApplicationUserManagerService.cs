using DbEntities.Identity;
using System.Collections.Generic;

using Web.Model;
using Web.Model.Identity;

namespace Web.Service.Identity.Interface
{
    public interface IApplicationUserManagerService
    {
        string GetCurrentUserId();
        ApplicationUserViewModel GetCurrentUser();

        ApplicationUserViewModel Get(int id);

        ApplicationUserViewModel GetByPhoneNumber(string phoneNumber);

        int Add(ApplicationUserViewModel model, string password, string roleName);

        bool Edit(ApplicationUser model);

        bool SetOTP(int userId);
        bool ValidateOTP(int userId, string otp);

        bool SendPassword(int userId, string password);
        ApplicationUserViewModel GetUserByApplicantId(int applicantId);
        IList<ApplicationUserViewModel> GetAllFiltered(
            string filterUserName, string filterName, string filterRoleId, int currentPage, int pageSize,
            string sortField, string sortDirection, bool currentUserIsManager, out int totalRecord);
        ApplicationUser FindById(string userId);
        bool SetPhoneNumberConfirmd(int userId, bool value);
        bool ResetPassword(int userId, string token, string newPassword);
        bool DeleteUser(int id);
    }
}
