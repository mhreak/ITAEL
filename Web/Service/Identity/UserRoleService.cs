using System;

using AutoMapper;
using DbConnection;
using DbEntities.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Model.Identity;
using Web.Service.Identity.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Web.Service.Identity
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly DbSet<CustomUserRole> _table;

        public UserRoleService(
            IUnitOfWork uow,
            IMapper mappingEngine)
        {
            _uow = uow;
            _mapper = mappingEngine;
            _table = _uow.Set<CustomUserRole>();
        }

        public IList<UserRoleViewModel> GetAllByUserId(int userId)
        {
            var dbModelList = _table.Where(x => x.UserId == userId);
            var uiModelList = new List<UserRoleViewModel>();

            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        public IList<UserRoleViewModel> GetAllByRoleId(int roleId)
        {
            var dbModelList = _table.Where(x => x.RoleId == roleId);
            var uiModelList = new List<UserRoleViewModel>();

            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        public bool Add(UserRoleViewModel model)
        {
            var dbModel = _table.FirstOrDefault(x => x.RoleId == model.RoleId && x.UserId == model.UserId);

            if (dbModel != null)
            {
                return false;
            }

            var userRole = new CustomUserRole();
            _mapper.Map(model, userRole);
            _table.Add(userRole);

            try
            {
                _uow.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
