using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using MD.PersianDateTime;
using System.Globalization;
using Web.Service.Interface;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class ApplicantExamAttemptService : IApplicantExamAttemptService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<ApplicantExamAttempt> _table;
        public ApplicantExamAttemptService(IUnitOfWork database, IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<ApplicantExamAttempt>();
        }

        public int Add(ApplicantExamAttemptViewModel uiModel)
        {
            var dbModel = new ApplicantExamAttempt();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.StartTime = DateTime.Now;

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.ApplicantExamAttemptId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantExamAttemptId == id);

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

        public bool Edit(ApplicantExamAttemptViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantExamAttemptId == uiModel.ApplicantExamAttemptId);

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

        public ApplicantExamAttemptViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.FirstOrDefault(x => x.ApplicantExamAttemptId == id);

            var uiModel = new ApplicantExamAttemptViewModel();

            _mapper.Map(dbModel, uiModel);

            PersianCalendar pc = new PersianCalendar();

            return uiModel;
        }

        public IList<ApplicantExamAttemptViewModel> GetAllFiltered(
            string filterApplicantId, string filterExamId,
            string filterApplicantFullName, string filterExamTitle,
            string filterShamsiStartTime, string filterShamsiEndTime,
            string filterFinalScore, string filterStatus,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ApplicantExamAttemptId > 0 ";

            if (!string.IsNullOrEmpty(filterApplicantId))
            {
                whereStr += " AND ApplicantId = " + filterApplicantId;
            }

            if (!string.IsNullOrEmpty(filterExamId))
            {
                whereStr += " AND ExamId = " + filterExamId;
            }

            if (!string.IsNullOrEmpty(filterApplicantFullName))
            {
                whereStr += " AND Applicant.FirstName.Contains(@0)";
            }

            if (!string.IsNullOrEmpty(filterExamTitle))
            {
                whereStr += " AND Exam.Title.Contains(@1)";
            }

            DateTime? insertDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterShamsiStartTime))
            {
                filterShamsiStartTime =
                    filterShamsiStartTime.Replace("۰", "0")
                                         .Replace("۱", "1")
                                         .Replace("۲", "2")
                                         .Replace("۳", "3")
                                         .Replace("۴", "4")
                                         .Replace("۵", "5")
                                         .Replace("۶", "6")
                                         .Replace("۷", "7")
                                         .Replace("۸", "8")
                                         .Replace("۹", "9");
                PersianDateTime shamsiInsertDateFrom = PersianDateTime.Parse(filterShamsiStartTime);
                insertDateFromMiladi = shamsiInsertDateFrom.ToDateTime();
                whereStr += " AND StartTime >= @2";
            }

            DateTime? insertDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterShamsiEndTime))
            {
                filterShamsiEndTime =
                    filterShamsiEndTime.Replace("۰", "0")
                                       .Replace("۱", "1")
                                       .Replace("۲", "2")
                                       .Replace("۳", "3")
                                       .Replace("۴", "4")
                                       .Replace("۵", "5")
                                       .Replace("۶", "6")
                                       .Replace("۷", "7")
                                       .Replace("۸", "8")
                                       .Replace("۹", "9");
                PersianDateTime shamsiInsertDateTo = PersianDateTime.Parse(filterShamsiEndTime);
                insertDateToMiladi = shamsiInsertDateTo.ToDateTime();

                insertDateToMiladi = insertDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                whereStr += " AND EndTime <= @3";
            }

            if (!string.IsNullOrEmpty(filterFinalScore))
            {
                whereStr += " AND FinalScore = " + filterFinalScore;
            }

            if (!string.IsNullOrEmpty(filterStatus))
            {
                whereStr += " AND Status = " + filterStatus;
            }

            var dbModelList = new List<ApplicantExamAttempt>();

            dbModelList = _table.Where(whereStr, filterApplicantFullName, filterExamTitle,
                                       filterShamsiStartTime, filterShamsiEndTime).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.StartTime)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ApplicantExamAttemptViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
