using AutoMapper;
using DbConnection;
using DbEntities;
using Kendo.Mvc.UI;
using MD.PersianDateTime;
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

        public bool Add(Applicant_JobAnnouncement_ViewModel model)
        {
            if (!IsDuplicate(model.ApplicantId, model.JobAnnouncementId))
            {
                var dbModel = new Applicant_JobAnnouncement();

                _mapper.Map(model, dbModel);
                dbModel.InsertDate = DateTime.Now;;
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
            var oldInsertDate = dbModel.InsertDate;
            _mapper.Map(uiModel, dbModel);
            dbModel.InsertDate = oldInsertDate;
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
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId && x.JobAnnouncementId == jobAnnouncementId && x.JobAnnouncement.Active == true && x.JobAnnouncement.IsDeleted == false);
            var uiModel = new Applicant_JobAnnouncement_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<Applicant_JobAnnouncement_ViewModel> GetAllByApplicantId(int applicantId)
        {
            var dbModelList = _table.Where(x => x.ApplicantId == applicantId && x.JobAnnouncement.Active == true && x.JobAnnouncement.IsDeleted == false)
                .Include(x => x.JobAnnouncement)
                .ToList();
            var uiModelList = new List<Applicant_JobAnnouncement_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Applicant_JobAnnouncement_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId && x.JobAnnouncement.Active == true && x.JobAnnouncement.IsDeleted == false)
                .Include(x => x.Applicant)
                .ToList();
            var uiModelList = new List<Applicant_JobAnnouncement_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Applicant_JobAnnouncement_ViewModel> GetAllFiltered(string filterJobAnnouncementId, string filterApplicantId, 
                                                                         string filterInsertDateFrom, string filterInsertDateTo,
                                                                         int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "JobAnnouncementId > 0 AND ApplicantId > 0";

            if (!String.IsNullOrEmpty(filterJobAnnouncementId))
            {
                whereStr += " AND JobAnnouncementId = " + filterJobAnnouncementId;
            }

            if (!String.IsNullOrEmpty(filterApplicantId))
            {
                whereStr += " AND ApplicantId = " + filterApplicantId;
            }

            DateTime? insertDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterInsertDateFrom))
            {
                filterInsertDateFrom =
                    filterInsertDateFrom.Replace("۰", "0")
                                        .Replace("۱", "1")
                                        .Replace("۲", "2")
                                        .Replace("۳", "3")
                                        .Replace("۴", "4")
                                        .Replace("۵", "5")
                                        .Replace("۶", "6")
                                        .Replace("۷", "7")
                                        .Replace("۸", "8")
                                        .Replace("۹", "9");
                PersianDateTime shamsiInsertDateFrom = PersianDateTime.Parse(filterInsertDateFrom);
                insertDateFromMiladi = shamsiInsertDateFrom.ToDateTime();
                whereStr += " AND InsertDate >= @0";
            }

            DateTime? insertDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterInsertDateTo))
            {
                filterInsertDateTo =
                    filterInsertDateTo.Replace("۰", "0")
                                      .Replace("۱", "1")
                                      .Replace("۲", "2")
                                      .Replace("۳", "3")
                                      .Replace("۴", "4")
                                      .Replace("۵", "5")
                                      .Replace("۶", "6")
                                      .Replace("۷", "7")
                                      .Replace("۸", "8")
                                      .Replace("۹", "9");
                PersianDateTime shamsiInsertDateTo = PersianDateTime.Parse(filterInsertDateTo);
                insertDateToMiladi = shamsiInsertDateTo.ToDateTime();

                insertDateToMiladi = insertDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                whereStr += " AND InsertDate <= @1";
            }

            var dbModelList = new List<Applicant_JobAnnouncement>();

            dbModelList = _table.Where(x => x.JobAnnouncement.Active == true && x.JobAnnouncement.IsDeleted == false).Where(whereStr, insertDateFromMiladi, insertDateToMiladi)
                .Include(x => x.Applicant)
                .Include(x => x.JobAnnouncement)
                .ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

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
