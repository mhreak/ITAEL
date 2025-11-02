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
    public class Applicant_JobAnnouncement_Service: IApplicant_JobAnnouncement_Service
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<Applicant_JobAnnouncement> _table;

        public Applicant_JobAnnouncement_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Applicant_JobAnnouncement>();
        }

        public bool Add(int applicantId, int jobAnnouncementId)
        {
            if (!IsDuplicate(applicantId, jobAnnouncementId))
            {
                var dbModel = new Applicant_JobAnnouncement();

                dbModel.ApplicantId = applicantId;
                dbModel.JobAnnouncementId = jobAnnouncementId;
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

        public bool Delete(int applicantId, int jobAnnouncementId)
        {
            if (IsDuplicate(applicantId, jobAnnouncementId))
            {
                var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId && x.JobAnnouncementId == jobAnnouncementId);

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

        public bool Edit(Applicant_JobAnnouncement_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == uiModel.ApplicantId && x.JobAnnouncementId == uiModel.JobAnnouncementId);

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

        public Applicant_JobAnnouncement_ViewModel Get(int applicantId, int jobAnnouncementId)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId && x.JobAnnouncementId == jobAnnouncementId);
            var uiModel = new Applicant_JobAnnouncement_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<Applicant_JobAnnouncement_ViewModel> GetAllByApplicantId(int applicantId)
        {
            var dbModelList = _table.Where(x => x.ApplicantId == applicantId)
                .Include(x => x.JobAnnouncement)
                .ToList();
            var uiModelList = new List<Applicant_JobAnnouncement_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Applicant_JobAnnouncement_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId)
                .Include(x => x.Applicant)
                .ToList();
            var uiModelList = new List<Applicant_JobAnnouncement_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicate(int applicantId, int jobAnnouncementId)
        {
            return _table.Any(x => x.ApplicantId == applicantId && x.JobAnnouncementId == jobAnnouncementId);
        }
    }
}
