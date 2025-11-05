using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using MD.PersianDateTime;
using Web.Service.Interface;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service;

public class JobAnnouncement_Exam_Service : IJobAnnouncement_Exam_Service
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _database;
    private readonly DbSet<JobAnnouncement_Exam> _table;

    public JobAnnouncement_Exam_Service(
        IUnitOfWork database,
        IMapper mappingEngine)
    {
        _database = database;
        _mapper = mappingEngine;
        _table = _database.Set<JobAnnouncement_Exam>();
    }

    public bool Add(int jobAnnouncementId, int examId)
    {
        if (!IsDuplicate(jobAnnouncementId, examId))
        {
            var dbModel = new JobAnnouncement_Exam();

            dbModel.JobAnnouncementId = jobAnnouncementId;
            dbModel.ExamId = examId;
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

    public bool Edit(JobAnnouncement_Exam_ViewModel uiModel)
    {
        var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == uiModel.JobAnnouncementId && x.ExamId == uiModel.ExamId);

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

    public bool Delete(int jobAnnouncementId, int examId)
    {
        if (IsDuplicate(jobAnnouncementId, examId))
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.ExamId == examId);

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

    public bool IsDuplicate(int jobAnnouncementId, int examId)
    {
        return _table.Any(x => x.JobAnnouncementId == jobAnnouncementId && x.ExamId == examId);
    }

    public JobAnnouncement_Exam_ViewModel Get(int jobAnnouncementId, int examId)
    {
        var dbModel = _table.Include(x => x.Exam)
                            .Include(x => x.JobAnnouncement)
                            .SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.ExamId == examId);
        var uiModel = new JobAnnouncement_Exam_ViewModel();

        _mapper.Map(dbModel, uiModel);

        return uiModel;
    }

    public IList<JobAnnouncement_Exam_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
    {
        var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId)
                                .Include(x => x.Exam)
                                .Include(x => x.JobAnnouncement)
                                .ToList();
        var uiModelList = new List<JobAnnouncement_Exam_ViewModel>();

        _mapper.Map(dbModelList, uiModelList);

        return uiModelList;
    }

    public IList<JobAnnouncement_Exam_ViewModel> GetAllByExamId(int examId)
    {
        var dbModelList = _table.Where(x => x.ExamId == examId)
                                .Include(x => x.Exam)
                                .Include(x => x.JobAnnouncement)
                                .ToList();
        var uiModelList = new List<JobAnnouncement_Exam_ViewModel>();

        _mapper.Map(dbModelList, uiModelList);

        return uiModelList;
    }

    public IList<JobAnnouncement_Exam_ViewModel> GetAllFiltered(string filterJobAnnouncementId, string filterExamId,
                                                                string filterStartTimeFrom, string filterStartTimeTo,
                                                                string filterEndTimeFrom, string filterEndTimeTo,
                                                                string filterDurationMinutes, string filterRandomizeQuestions,
                                                                string filterRandomizeOptions, string filterAllowNavigateToPreviousQuestion,
                                                                int currentPage, int pageSize, out int totalRecord)
    {
        string whereStr = "JobAnnouncementId > 0 And ExamId > 0 ";

        if (!string.IsNullOrEmpty(filterJobAnnouncementId))
        {
            whereStr += " AND JobAnnouncementId = " + filterJobAnnouncementId;
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

        if (!string.IsNullOrEmpty(filterDurationMinutes))
        {
            whereStr += " AND DurationMinutes = " + filterDurationMinutes;
        }

        if (!string.IsNullOrEmpty(filterRandomizeQuestions))
        {
            whereStr += " AND RandomizeQuestions = " + filterRandomizeQuestions;
        }

        if (!string.IsNullOrEmpty(filterRandomizeOptions))
        {
            whereStr += " AND RandomizeOptions = " + filterRandomizeOptions;
        }

        if (!string.IsNullOrEmpty(filterAllowNavigateToPreviousQuestion))
        {
            whereStr += " AND AllowNavigateToPreviousQuestion = " + filterAllowNavigateToPreviousQuestion;
        }

        var dbModelList = new List<JobAnnouncement_Exam>();

        dbModelList = _table.Where(whereStr, startTimeFromMiladi, startTimeToMiladi,
                                   endTimeFromMiladi, endTimeToMiladi)
                            .Include(x => x.Exam)
                            .Include(x => x.JobAnnouncement)
                            .ToList();

        totalRecord = dbModelList.Count();

        dbModelList = dbModelList
                      .OrderByDescending(x => x.InsertDate)
                      .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        var uiModelList = new List<JobAnnouncement_Exam_ViewModel>();
        _mapper.Map(dbModelList, uiModelList);
        return uiModelList;
    }
}