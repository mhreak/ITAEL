using AutoMapper;
using DbConnection;
using DbEntities.Identity;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Model.Identity;
using Web.Service.Identity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;

namespace Web.Service.Identity
{
    public class ApplicationUserManagerService : IApplicationUserManagerService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly DbSet<ApplicationUser> _table;
        private ApplicationUser _user;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleManagerService _roleManagerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserManagerService(
            IUnitOfWork uow,
            IMapper mappingEngine,
            //Func<IIdentity> identity,
            IUserRoleService userRoleService,
            IRoleManagerService roleManagerService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _database = uow;
            //_identity = identity;
            _mapper = mappingEngine;
            _table = _database.Set<ApplicationUser>();
            _userRoleService = userRoleService;
            _roleManagerService = roleManagerService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
    }

        public ApplicationUserViewModel GetCurrentUser()
        {
            var dbModel = _user ?? (_user = this.FindById(GetCurrentUserId()));
            var uiModel = new ApplicationUserViewModel();
            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IList<ApplicationUserViewModel> GetAllFiltered(string filterUserName,
            string filterName,
            string filterRoleId, int currentPage, int pageSize,
            string sortField, string sortDirection, bool currentUserIsManager, out int totalRecord)
        {
            string whereStr = " Id > 0";
            currentPage = currentPage - 1;

            if (!String.IsNullOrEmpty(filterUserName))
            {
                whereStr += " AND UserName.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterName))
            {
                whereStr += " AND Name.Contains(@1)";
            }

            var dbModelList1 = _table.Where(whereStr, filterUserName, filterName);
            var dbModelList2 = new List<ApplicationUser>();
            var dbModelList3 = new List<ApplicationUser>();

            if (!String.IsNullOrEmpty(filterRoleId))
            {
                var usersWithRole = _userRoleService.GetAllByRoleId(Convert.ToInt32(filterRoleId));

                dbModelList2 = dbModelList1
                    .Where(x => usersWithRole.Select(ur => ur.UserId).ToList().Contains(x.Id))
                    .OrderBy(sortField + " " + sortDirection)
                    .ToList();
            }
            else
            {
                dbModelList2 = dbModelList1.OrderBy(sortField + " " + sortDirection).ToList();
            }

            if (currentUserIsManager)
            {
                dbModelList3 = dbModelList2;
            }
            else
            {
                var managerUsers = _userRoleService.GetAllByRoleId(1);

                dbModelList3 = dbModelList2
                    .Where(x => !(managerUsers.Select(ur => ur.UserId).ToList().Contains(x.Id)))
                    .ToList();
            }

            var uiModelList = new List<ApplicationUserViewModel>();

            _mapper.Map(dbModelList3.Skip(currentPage * pageSize).Take(pageSize).ToList(), uiModelList);

            for (int i = 0; i < uiModelList.Count; i++)
            {
                string roleName = _roleManagerService.Get(_userRoleService.GetAllByUserId(dbModelList3[i].Id).First().RoleId).Name;

                switch (roleName)
                {
                    case "Applicant":
                        roleName = "داوطلب";
                        break;
                    case "Admin":
                        roleName = "ادمین";
                        break;
                    case "Manager":
                        roleName = "ادمین";
                        break;
                }

                uiModelList[i].RoleName = roleName;
            }

            totalRecord = dbModelList3.Count;
            return uiModelList;
        }

        public ApplicationUser FindById(string userId)
        {
            return _table.FirstOrDefault(x => x.Id == Convert.ToInt32(userId));
        }

        public ApplicationUserViewModel GetUserByApplicantId(int applicantId)
        {
            var dbModelList = _table.Where(x => x.ApplicantId == applicantId).ToList();
            var uiModelList = new List<ApplicationUserViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            if (dbModelList != null && dbModelList.Count == 1)
            {
                return uiModelList[0];
            }
            else
            {
                return null;
            }
        }

        public bool SetOTP(int userId)
        {
            try
            {
                if (_table.Any(x => x.Id == userId))
                {
                    var dbModel = _table.SingleOrDefault(x => x.Id == userId);

                    if ((dbModel.OTPExpirationDate == null)
                        || (dbModel.OTPExpirationDate != null && (DateTime.Compare(DateTime.Now, (DateTime)dbModel.OTPExpirationDate) > 0)))
                    {
                        object syncLock = new object();
                        string otp = "";
                        lock (syncLock)
                        {
                            otp = new Random().Next(10000, 99999).ToString();
                        }

                        if (otp == "") { return false; }

                        dbModel.OTP = otp;
                        dbModel.OTPExpirationDate = DateTime.Now.AddMinutes(5);

                        _table.Attach(dbModel);
                        _database.Entry(dbModel).State = EntityState.Modified;

                        _database.SaveChanges();

                        List<string> reciption = new List<string>();
                        reciption.Add(dbModel.PhoneNumber);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ValidateOTP(int userId, string otp)
        {
            if (_table.Any(x => x.Id == userId))
            {
                var user = _table.SingleOrDefault(x => x.Id == userId);
                if (user.OTP != null && user.OTP != "")
                {
                    if (user.OTPExpirationDate != null)
                    {
                        if (DateTime.Compare(DateTime.Now, (DateTime)user.OTPExpirationDate) <= 0)
                        {
                            if (user.OTP == otp)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool SetPhoneNumberConfirmd(int userId, bool value)
        {
            try
            {
                if (_table.Any(x => x.Id == userId))
                {
                    var dbModel = _table.SingleOrDefault(x => x.Id == userId);
                    dbModel.PhoneNumberConfirmed = value;

                    _table.Attach(dbModel);
                    _database.Entry(dbModel).State = EntityState.Modified;

                    _database.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ResetPassword(int userId, string token, string newPassword)
        {
            try
            {
                var user = _table.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    var result = _userManager.ResetPasswordAsync(user, token, newPassword);

                    return result.Result.Succeeded;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.Id == id);

            _database.Entry(dbModel).State = EntityState.Deleted;

            try
            {
                _database.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ApplicationUserViewModel Get(int id)
        {
            var dbModel = _table.Where(x => x.Id == id).FirstOrDefault();

            if(dbModel == null)
            {
                return null;
            }

            ApplicationUserViewModel uiModel = new ApplicationUserViewModel();

            _mapper.Map(dbModel, uiModel);

            string roleName = _roleManagerService.Get(_userRoleService.GetAllByUserId(id).First().RoleId).Name;

            switch (roleName)
            {
                case "Passenger":
                    roleName = "مسافر";
                    break;
                case "Admin":
                    roleName = "مدیر";
                    break;
                case "Manager":
                    roleName = "مدیر ارشد";
                    break;
            }

            uiModel.RoleName = roleName;

            return uiModel;
        }

        public bool Edit(ApplicationUser model)
        {
            var dbModel = _table.SingleOrDefault(x => x.Id == model.Id);

            model.ApplicantId = dbModel.ApplicantId;
            model.AccessFailedCount = dbModel.AccessFailedCount;
            model.EmailConfirmed = dbModel.EmailConfirmed;
            model.LockoutEnabled = dbModel.LockoutEnabled;
            model.LockoutEnd = dbModel.LockoutEnd;
            model.PasswordHash = dbModel.PasswordHash;
            model.SecurityStamp = dbModel.SecurityStamp;
            model.TwoFactorEnabled = dbModel.TwoFactorEnabled;

            _mapper.Map(model, dbModel);
            _table.Attach(dbModel);
            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
