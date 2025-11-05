using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using Web.Service.Interface;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class JobAnnouncement_ExamResource_Service : IJobAnnouncement_ExamResource_Service
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<JobAnnouncement_ExamResource> _table;

        public JobAnnouncement_ExamResource_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<JobAnnouncement_ExamResource>();
        }

        public bool Add(int jobAnnouncementId, int examResourceId)
        {
            if (!IsDuplicate(jobAnnouncementId, examResourceId))
            {
                var dbModel = new JobAnnouncement_ExamResource();

                dbModel.JobAnnouncementId = jobAnnouncementId;
                dbModel.ExamResourceId = examResourceId;
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

        public bool Edit(JobAnnouncement_ExamResource_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == uiModel.JobAnnouncementId && x.ExamResourceId == uiModel.ExamResourceId);

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

        public bool Delete(int jobAnnouncementId, int examResourceId)
        {
            if (IsDuplicate(jobAnnouncementId, examResourceId))
            {
                var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.ExamResourceId == examResourceId);

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

        public bool IsDuplicate(int jobAnnouncementId, int examResourceId)
        {
            return _table.Any(x => x.JobAnnouncementId == jobAnnouncementId && x.ExamResourceId == examResourceId);
        }

        public JobAnnouncement_ExamResource_ViewModel Get(int jobAnnouncementId, int examResourceId)
        {
            var dbModel = _table.Include(x => x.ExamResource)
                                .Include(x => x.JobAnnouncement)
                                .SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.ExamResourceId == examResourceId);
            var uiModel = new JobAnnouncement_ExamResource_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<JobAnnouncement_ExamResource_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId)
                                    .Include(x => x.ExamResource)
                                    .Include(x => x.JobAnnouncement)
                                    .ToList();
            var uiModelList = new List<JobAnnouncement_ExamResource_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<JobAnnouncement_ExamResource_ViewModel> GetAllByExamResourceId(int examResourceId)
        {
            var dbModelList = _table.Where(x => x.ExamResourceId == examResourceId)
                                    .Include(x => x.ExamResource)
                                    .Include(x => x.JobAnnouncement)
                                    .ToList();
            var uiModelList = new List<JobAnnouncement_ExamResource_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
