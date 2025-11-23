using AutoMapper;
using DbConnection;
using DbEntities;
using Kendo.Mvc.Infrastructure.Implementation;
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

        public bool Delete(int jobAnnouncementId, int examResourceId)
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

        public IList<JobAnnouncement_ExamResource_ViewModel> GetAllFiltered(string filterJobAnnouncementId,
                                                                            string filterExamResourceId,
                                                                            string filterInsertDateFrom,
                                                                            string filterInsertDateTo,
                                                                            int currentPage,
                                                                            int pageSize,
                                                                            out int totalRecord)
        {
            string whereStr = "ExamResourceId > 0 AND JobAnnouncementId > 0";

            if (!string.IsNullOrEmpty(filterJobAnnouncementId))
            {
                whereStr += " AND JobAnnouncementId = " + filterJobAnnouncementId;
            }

            if (!string.IsNullOrEmpty(filterExamResourceId))
            {
                whereStr += " AND ExamResourceId = " + filterExamResourceId;
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

            var dbModelList = new List<JobAnnouncement_ExamResource>();

            dbModelList = _table.Include(x => x.ExamResource).Where(whereStr, insertDateFromMiladi, insertDateToMiladi).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<JobAnnouncement_ExamResource_ViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
