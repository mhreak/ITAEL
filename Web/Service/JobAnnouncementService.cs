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
using Web.Service.Interface;

namespace Web.Service
{
    public class JobAnnouncementService : IJobAnnouncementService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<JobAnnouncement> _table;

        public JobAnnouncementService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<JobAnnouncement>();
        }

        public int Add(JobAnnouncementViewModel uiModel)
        {
            var dbModel = new JobAnnouncement();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.JobAnnouncementId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == id);

            dbModel.IsDeleted = true;

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

        public bool Edit(JobAnnouncementViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == uiModel.JobAnnouncementId);

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

        public JobAnnouncementViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table
                          .Where(x => x.IsDeleted == false && x.Active)
                          .Include(x => x.City)
                          .Include(x => x.Company)
                .FirstOrDefault(x => x.JobAnnouncementId == id);

            var uiModel = new JobAnnouncementViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public List<JobAnnouncementViewModel> GetAllLast(int count)
        {

            var dbModelList = _table.Where(x => x.IsDeleted == false && x.Active).OrderByDescending(x => x.InsertDate)
                                .Take(count)
                                .ToList();

            var uiModelList = new List<JobAnnouncementViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<JobAnnouncementViewModel> GetAllFiltered(
            string filterTitle, string filterGender,
            string filterHasEmployementExam,
            string filterPublishDateFrom, string filterPublishDateTo,
            string filterExamDateFrom, string filterExamDateTo,
            string filterCapacityFrom, string filterCapacityTo,
            string filterActive, string filterJobType, string filterJobTime,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "JobAnnouncementId > 0 AND IsDeleted = false AND Active = true";

            if (!String.IsNullOrEmpty(filterTitle))
            {
                whereStr += " AND Title.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterGender))
            {
                whereStr += " AND Gender = " + filterGender;
            }

            if (!String.IsNullOrEmpty(filterHasEmployementExam))
            {
                whereStr += " AND HasEmployementExam = " + filterHasEmployementExam;
            }

            if (!String.IsNullOrEmpty(filterGender))
            {
                whereStr += " AND Gender = " + filterGender.ToLower();
            }

            DateTime? publishDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterPublishDateFrom))
            {
                filterPublishDateFrom =
                    filterPublishDateFrom.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiPublishDateFrom = PersianDateTime.Parse(filterPublishDateFrom);
                publishDateFromMiladi = shamsiPublishDateFrom.ToDateTime();
                whereStr += " AND PublishDate >= @1";
            }

            DateTime? publishDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterPublishDateTo))
            {
                filterPublishDateTo =
                    filterPublishDateTo.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiPublishDateTo = PersianDateTime.Parse(filterPublishDateTo);
                publishDateToMiladi = shamsiPublishDateTo.ToDateTime();
                whereStr += " AND PublishDate <= @2";
            }

            DateTime? examDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterExamDateFrom))
            {
                filterExamDateFrom =
                    filterExamDateFrom.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiExamDateFrom = PersianDateTime.Parse(filterExamDateFrom);
                examDateFromMiladi = shamsiExamDateFrom.ToDateTime();
                whereStr += " AND ExamDate >= @3";
            }

            DateTime? examDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterExamDateTo))
            {
                filterExamDateTo =
                    filterExamDateTo.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiExamDateTo = PersianDateTime.Parse(filterExamDateTo);
                examDateFromMiladi = shamsiExamDateTo.ToDateTime();
                whereStr += " AND ExamDate <= @4";
            }

            if (!String.IsNullOrEmpty(filterCapacityFrom))
            {
                whereStr += " AND Capacity != null AND Capacity >= " + filterCapacityFrom.ToLower();
            }

            if (!String.IsNullOrEmpty(filterCapacityTo))
            {
                whereStr += " AND Capacity != null AND Capacity <= " + filterCapacityTo.ToLower();
            }

            if (!String.IsNullOrEmpty(filterActive))
            {
                whereStr += " AND Active = " + filterActive.ToLower();
            }

            if (!String.IsNullOrEmpty(filterJobType))
            {
                whereStr += " AND JobType = " + filterJobType.ToLower();
            }

            if (!String.IsNullOrEmpty(filterJobTime))
            {
                whereStr += " AND JobTimeType = " + filterJobTime.ToLower();
            }

            var dbModelList = new List<JobAnnouncement>();

            dbModelList = _table
                          .Where(x => x.IsDeleted == false && x.Active)
                          .Include(x => x.City)
                          .Include(x => x.Company)
                          .Where(whereStr, filterTitle, filterPublishDateFrom, filterPublishDateTo,
                filterExamDateFrom, filterExamDateTo).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                .OrderByDescending(x => x.PublishDate)
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<JobAnnouncementViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
