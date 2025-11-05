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

            return uiModel;
        }

        public IList<ApplicantExamAttemptViewModel> GetAllFiltered(
            string filterApplicantId, string filterExamId,
            string filterStartTimeFrom, string filterStartTimeTo,
            string filterEndTimeFrom, string filterEndTimeTo,
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


            DateTime? startTimeFromMiladi = null;
            if (!string.IsNullOrEmpty(filterStartTimeFrom))
            {
                filterStartTimeFrom =
                    filterStartTimeFrom.Replace("۰", "0")
                                       .Replace("۱", "1")
                                       .Replace("۲", "2")
                                       .Replace("۳", "3")
                                       .Replace("۴", "4")
                                       .Replace("۵", "5")
                                       .Replace("۶", "6")
                                       .Replace("۷", "7")
                                       .Replace("۸", "8")
                                       .Replace("۹", "9");
                PersianDateTime shamsiStartTimeFrom = PersianDateTime.Parse(filterStartTimeFrom);
                startTimeFromMiladi = shamsiStartTimeFrom.ToDateTime();
                whereStr += " AND StartTime >= @0";
            }

            DateTime? startTimeToMiladi = null;
            if (!string.IsNullOrEmpty(filterStartTimeTo))
            {
                filterStartTimeTo =
                    filterStartTimeTo.Replace("۰", "0")
                                     .Replace("۱", "1")
                                     .Replace("۲", "2")
                                     .Replace("۳", "3")
                                     .Replace("۴", "4")
                                     .Replace("۵", "5")
                                     .Replace("۶", "6")
                                     .Replace("۷", "7")
                                     .Replace("۸", "8")
                                     .Replace("۹", "9");
                PersianDateTime shamsiStartTimeTo = PersianDateTime.Parse(filterStartTimeTo);
                startTimeToMiladi = shamsiStartTimeTo.ToDateTime();

                startTimeToMiladi = startTimeToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                whereStr += " AND StartTime <= @1";
            }

            DateTime? endTimeFromMiladi = null;
            if (!string.IsNullOrEmpty(filterEndTimeFrom))
            {
                filterStartTimeFrom =
                    filterStartTimeFrom.Replace("۰", "0")
                                       .Replace("۱", "1")
                                       .Replace("۲", "2")
                                       .Replace("۳", "3")
                                       .Replace("۴", "4")
                                       .Replace("۵", "5")
                                       .Replace("۶", "6")
                                       .Replace("۷", "7")
                                       .Replace("۸", "8")
                                       .Replace("۹", "9");
                PersianDateTime shamsiInsertDateFrom = PersianDateTime.Parse(filterStartTimeFrom);
                endTimeFromMiladi = shamsiInsertDateFrom.ToDateTime();
                whereStr += " AND EndTime >= @2";
            }

            DateTime? endTimeToMiladi = null;
            if (!string.IsNullOrEmpty(filterEndTimeTo))
            {
                filterEndTimeTo =
                    filterEndTimeTo.Replace("۰", "0")
                                   .Replace("۱", "1")
                                   .Replace("۲", "2")
                                   .Replace("۳", "3")
                                   .Replace("۴", "4")
                                   .Replace("۵", "5")
                                   .Replace("۶", "6")
                                   .Replace("۷", "7")
                                   .Replace("۸", "8")
                                   .Replace("۹", "9");
                PersianDateTime shamsiInsertDateTo = PersianDateTime.Parse(filterStartTimeTo);
                endTimeToMiladi = shamsiInsertDateTo.ToDateTime();

                endTimeToMiladi = endTimeToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

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

            dbModelList = _table.Where(whereStr, startTimeFromMiladi, startTimeToMiladi,
                                       endTimeFromMiladi, endTimeToMiladi).ToList();

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
