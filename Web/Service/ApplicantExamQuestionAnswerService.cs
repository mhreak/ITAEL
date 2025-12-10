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
    public class ApplicantExamQuestionAnswerService : IApplicantExamQuestionAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IApplicantService _applicantService;
        private readonly IApplicantExamAttemptService _applicantExamAttemptService;
        private readonly IExamQuestionService _examQuestionService;
        private readonly IExamService _examService;
        private readonly IUnitOfWork _database;
        private readonly DbSet<ApplicantExamQuestionAnswer> _table;

        public ApplicantExamQuestionAnswerService(
            IUnitOfWork database,
            IMapper mappingEngine,
            IApplicantService applicantService,
            IApplicantExamAttemptService applicantExamAttemptService,
            IExamQuestionService examQuestionService,
            IExamService examService)
        {
            _database = database;
            _mapper = mappingEngine;
            _applicantService = applicantService;
            _applicantExamAttemptService = applicantExamAttemptService;
            _examQuestionService = examQuestionService;
            _examService = examService;
            _table = _database.Set<ApplicantExamQuestionAnswer>();
        }
        public List<int> Add(ApplicantExamQuestionAnswerViewModel uiModel)
        {
            var dbModel = new ApplicantExamQuestionAnswer();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.InsertDate = DateTime.Now;
            var idList = new List<int>();
            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();
                idList.Add(dbModel.ApplicantExamAttemptId);
                idList.Add(dbModel.ExamQuestionId);
                return idList;
            }
            catch (Exception)
            {
                idList.Add(-1);
                return idList;
            }
        }

        public bool Delete(int applicantExamAttemptId, int examQuestionId)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantExamAttemptId == applicantExamAttemptId 
                                                      && x.ExamQuestionId == examQuestionId);

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

        public bool Edit(ApplicantExamQuestionAnswerViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantExamAttemptId == uiModel.ApplicantExamAttemptId 
                                                      && x.ExamQuestionId == uiModel.ExamQuestionId);

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

        public bool CalculateGradeAfterExam(int examId, int applicantId)
        {
            var examViewModel = _examService.Get(examId);

            if (examViewModel == null)
            {
                return false;
            }
            var applicantViewModel = _applicantService.Get(applicantId);

            if (applicantViewModel == null)
            {
                return false;
            }

            var attemptViewModel = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantViewModel.ApplicantId, examViewModel.ExamId);

            if (attemptViewModel == null)
            {
                return false;
            }

            //محاسبه نمره هایی که داوطلب از سوالات تستی درآورده
            double applicantGrade = 0;
            var examQuestionViewModelList = _examQuestionService.GetAllByExamId(examViewModel.ExamId);
            foreach (var examQuestionViewModel in examQuestionViewModelList)
            {
                if (examQuestionViewModel.Type == 1)
                {
                    var applicantExamQuestionAnswerViewModel = Get(attemptViewModel.ApplicantExamAttemptId, examQuestionViewModel.ExamQuestionId);
                    var examQuestionOptionViewModel = examQuestionViewModel.ExamQuestionOptionViewModelList.FirstOrDefault(x => x.IsCorrectAnswer);

                    if (examQuestionOptionViewModel.ExamQuestionOptionId == applicantExamQuestionAnswerViewModel.ExamQuestionOptionId)
                    {
                        applicantExamQuestionAnswerViewModel.Grade = examQuestionViewModel.Grade;
                    }
                    else
                    {
                        applicantExamQuestionAnswerViewModel.Grade = 0;
                    }

                    bool isEdited = Edit(applicantExamQuestionAnswerViewModel);

                    if (!isEdited)
                    {
                        return false;
                    }

                    applicantGrade += applicantExamQuestionAnswerViewModel.Grade;
                }
            }

            attemptViewModel.FinalScore = (decimal)applicantGrade;
            double totalGradeOfExam = 0;

            //اصلا اگر سوال تشریحی وجود نداشت همینجا نمره کلی محسابه میشود
            if (examQuestionViewModelList.All(x => x.Type != 2))
            {
                foreach (var examQuestionViewModel in examQuestionViewModelList)
                {
                    totalGradeOfExam += examQuestionViewModel.Grade;
                }

                if (totalGradeOfExam / 2 <= applicantGrade)
                {
                    attemptViewModel.Status = 1;
                }
                else
                {
                    attemptViewModel.Status = 2;
                }
            }
            else
            {
                attemptViewModel.Status = 4;
            }

            bool isEdit = _applicantExamAttemptService.Edit(attemptViewModel);

            if (!isEdit)
            {
                return false;
            }

            return true;
        }

        public ApplicantExamQuestionAnswerViewModel Get(int applicantExamAttemptId, int examQuestionId)
        {
            if (applicantExamAttemptId == 0 || examQuestionId == 0) { return null; }

            var dbModel = _table.FirstOrDefault(x => x.ApplicantExamAttemptId == applicantExamAttemptId
                                                     && x.ExamQuestionId == examQuestionId);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ApplicantExamQuestionAnswerViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public List<ApplicantExamQuestionAnswerViewModel> GetAllFiltered(string filterApplicantExamAttemptId ,string filterInsertDateFrom, string filterInsertDateTo,
                                                                         int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ApplicantExamAttemptId > 0 AND ExamQuestionId > 0";

            if (string.IsNullOrEmpty(filterApplicantExamAttemptId))
            {
                whereStr += " AND ApplicantExamAttemptId = " + filterApplicantExamAttemptId;
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

            var dbModelList = new List<ApplicantExamQuestionAnswer>();

            dbModelList = _table.Where(whereStr, insertDateFromMiladi, insertDateToMiladi)
                                .Include(x => x.ExamQuestion)
                                .Include(x => x.ExamQuestionOption)
                                .Include(x => x.ApplicantExamAttempt)
                                .ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ApplicantExamQuestionAnswerViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}

