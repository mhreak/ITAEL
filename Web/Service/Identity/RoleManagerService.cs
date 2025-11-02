using AutoMapper;
using DbConnection;
using DbEntities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Model.Identity;
using Web.Service.Identity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Service.Identity
{
    public class RoleManagerService : IRoleManagerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly DbSet<CustomRole> _table;

        public RoleManagerService(
            IUnitOfWork uow,
            IMapper mappingEngine)
        {
            _uow = uow;
            //_identity = identity;
            _mapper = mappingEngine;
            _table = _uow.Set<CustomRole>();
        }

        public int Add(RoleViewModel uiModel)
        {
            var dbModel = new CustomRole();
            _mapper.Map(source: uiModel, destination: dbModel);
            _table.Add(dbModel);

            try
            {
                _uow.SaveChanges();
                return dbModel.Id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var entity = _table.Find(id);
            _uow.Entry(entity).State = EntityState.Deleted;

            try
            {
                _uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Edit(RoleViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.Id == uiModel.Id);
            _mapper.Map(uiModel, dbModel);
            _table.Attach(dbModel);
            _uow.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public RoleViewModel Get(int roleId)
        {
            if(_table.Any(x => x.Id == roleId))
            {
                var dbModel = _table.SingleOrDefault(x => x.Id == roleId);

                var uiModel = new RoleViewModel();

                _mapper.Map(dbModel, uiModel);

                return uiModel;
            }
            else
            {
                return null;
            }
        }

        public IList<RoleViewModel> GetAll()
        {
            var dbModelList = _table.ToList();
            var uiModelList = new List<RoleViewModel>();

            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        public bool IsDuplicate(int? id, string name)
        {
            if(id != null)
            {
                return _table.Any(x => x.Id != (int)id && x.Name == name);
            }
            else
            {
                return _table.Any(x => x.Name == name);
            }
        }
    }
}
