using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class JobAnnouncementCategoryService : IJobAnnouncementCategoryService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<JobAnnouncementCategory> _table;
        readonly IJobAnnouncement_JobAnnouncementCategory_Service _ja_jac_service;

        public JobAnnouncementCategoryService(
            IUnitOfWork database,
            IMapper mappingEngine,
            IJobAnnouncement_JobAnnouncementCategory_Service ja_jac_service)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<JobAnnouncementCategory>();
            _ja_jac_service = ja_jac_service;
        }

        public int Add(JobAnnouncementCategoryViewModel uiModel)
        {
            var dbModel = new JobAnnouncementCategory();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.JobAnnouncementCategoryId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementCategoryId == id);

            _database.Entry(dbModel).State = EntityState.Deleted;

            var ja_jac_list = _ja_jac_service.GetAllByJobAnnouncementCategoryId(id);
            if(ja_jac_list != null && ja_jac_list.Any())
            {
                foreach(var ja_jac in ja_jac_list)
                {
                    _ja_jac_service.Delete(ja_jac.JobAnnouncementId, id);
                }
            }

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Edit(JobAnnouncementCategoryViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementCategoryId == uiModel.JobAnnouncementCategoryId);

            _mapper.Map(uiModel, dbModel);
            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public JobAnnouncementCategoryViewModel Get(int id)
        {
            var dbModel = _table
                .Where(x => x.JobAnnouncementCategoryId == id)
                .FirstOrDefault();

            var uiModel = new JobAnnouncementCategoryViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<JobAnnouncementCategoryViewModel> GetAll(bool? active)
        {
            var dbModelList = new List<JobAnnouncementCategory>();

            if (active != null)
            {
                dbModelList = _table.Where(x => x.Active == (bool)active).ToList();
            }
            else
            {
                dbModelList = _table.ToList();
            }

            var uiModelList = new List<JobAnnouncementCategoryViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<JobAnnouncementCategoryViewModel> GetAllFiltered(
            string filterCategoryName, string filterActive, int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = " JobAnnouncementCategoryId > 0 ";
            currentPage = currentPage - 1;

            if (!String.IsNullOrEmpty(filterCategoryName))
            {
                whereStr += " AND CategoryName.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterActive))
            {
                whereStr += " AND Active = " + filterActive;
            }

            var dbModelList = _table.Where(whereStr, filterCategoryName)
                .OrderBy(x => x.CategoryName)
                .Skip(currentPage * pageSize).Take(pageSize).ToList();

            totalRecord = _table.Where(whereStr, filterCategoryName).Count();

            var uiModelList = new List<JobAnnouncementCategoryViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicateByName(int? categoryId, string categoryName)
        {
            if (categoryId != null)
            {
                return _table.Any(x => x.JobAnnouncementCategoryId != (int)categoryId && x.CategoryName == categoryName);
            }
            else
            {
                return _table.Any(x => x.CategoryName == categoryName);
            }
        }
    }
}
