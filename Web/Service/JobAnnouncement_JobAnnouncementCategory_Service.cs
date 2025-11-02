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
    public class JobAnnouncement_JobAnnouncementCategory_Service : IJobAnnouncement_JobAnnouncementCategory_Service
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<JobAnnouncement_JobAnnouncementCategory> _table;

        public JobAnnouncement_JobAnnouncementCategory_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<JobAnnouncement_JobAnnouncementCategory>();
        }

        public bool Add(int jobAnnouncementId, int jobAnnouncementCategoryId)
        {
            if (!IsDuplicate(jobAnnouncementId, jobAnnouncementCategoryId))
            {
                var dbModel = new JobAnnouncement_JobAnnouncementCategory();

                dbModel.JobAnnouncementId = jobAnnouncementId;
                dbModel.JobAnnouncementCategoryId = jobAnnouncementCategoryId;
                dbModel.InsertDate = DateTime.Now;

                _table.Add(dbModel);
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
            else
            {
                //return false if record exists in database (duplicate record)
                return false;
            }
        }

        public bool Delete(int jobAnnouncementId, int jobAnnouncementCategoryId)
        {
            if (IsDuplicate(jobAnnouncementId, jobAnnouncementCategoryId))
            {
                var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId &&
                    x.JobAnnouncementCategoryId == jobAnnouncementCategoryId);

                _database.Entry(dbModel).State = EntityState.Deleted;

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
            else
            {
                //return true if record not exists in database
                return true;
            }
        }

        public bool Edit(JobAnnouncement_JobAnnouncementCategory_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == uiModel.JobAnnouncementId &&
                x.JobAnnouncementCategoryId == uiModel.JobAnnouncementCategoryId);

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

        public JobAnnouncement_JobAnnouncementCategory_ViewModel Get(int jobAnnouncementId, int jobAnnouncementCategoryId)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId &&
                x.JobAnnouncementCategoryId == jobAnnouncementCategoryId);
            var uiModel = new JobAnnouncement_JobAnnouncementCategory_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<JobAnnouncement_JobAnnouncementCategory_ViewModel> GetAllByJobAnnouncementCategoryId(int jobAnnouncementCategoryId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementCategoryId == jobAnnouncementCategoryId).ToList();
            var uiModelList = new List<JobAnnouncement_JobAnnouncementCategory_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<JobAnnouncement_JobAnnouncementCategory_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId).ToList();
            var uiModelList = new List<JobAnnouncement_JobAnnouncementCategory_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicate(int jobAnnouncementId, int jobAnnouncementCategoryId)
        {
            return _table.Any(x => x.JobAnnouncementId == jobAnnouncementId && x.JobAnnouncementCategoryId == jobAnnouncementCategoryId);
        }
    }
}
