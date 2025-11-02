using AutoMapper;
using DbConnection;
using DbEntities;
using MD.PersianDateTime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Model;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Service
{
    public class JobAnnouncement_StudyField_Service : IJobAnnouncement_StudyField_Service
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<JobAnnouncement_StudyField> _table;

        public JobAnnouncement_StudyField_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<JobAnnouncement_StudyField>();
        }

        public bool Add(int jobAnnouncementId, int studyFieldId)
        {
            if (!IsDuplicate(jobAnnouncementId, studyFieldId))
            {
                var dbModel = new JobAnnouncement_StudyField();

                dbModel.JobAnnouncementId = jobAnnouncementId;
                dbModel.StudyFieldId = studyFieldId;
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

        public bool Delete(int jobAnnouncementId, int studyFieldId)
        {
            if (IsDuplicate(jobAnnouncementId, studyFieldId))
            {
                var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.StudyFieldId == studyFieldId);

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

        public bool Edit(JobAnnouncement_StudyField_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == uiModel.JobAnnouncementId && x.StudyFieldId == uiModel.StudyFieldId);

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

        public JobAnnouncement_StudyField_ViewModel Get(int jobAnnouncementId, int studyFieldId)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.StudyFieldId == studyFieldId);
            var uiModel = new JobAnnouncement_StudyField_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<JobAnnouncement_StudyField_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId)
                .Include(x => x.StudyField)
                .ToList();
            var uiModelList = new List<JobAnnouncement_StudyField_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<JobAnnouncement_StudyField_ViewModel> GetAllByStudyFieldId(int studyFieldId)
        {
            var dbModelList = _table.Where(x => x.StudyFieldId == studyFieldId)
                .Include(x => x.JobAnnouncement)
                .ToList();
            var uiModelList = new List<JobAnnouncement_StudyField_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicate(int jobAnnouncementId, int studyFieldId)
        {
            return _table.Any(x => x.JobAnnouncementId == jobAnnouncementId && x.StudyFieldId == studyFieldId);
        }
    }
}
