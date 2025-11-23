using AutoMapper;
using DbConnection;
using DbEntities;
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
    public class StudyField_ExamResource_Service : IStudyField_ExamResource_Service
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<StudyField_ExamResource> _table;

        public StudyField_ExamResource_Service(IUnitOfWork database,
                                               IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<StudyField_ExamResource>();
        }

        public bool Add(int examResourceId, int studyFieldId)
        {
            if (!IsDuplicate(examResourceId, studyFieldId))
            {
                var dbModel = new StudyField_ExamResource();

                dbModel.ExamResourceId = examResourceId;
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

        public bool Edit(StudyField_ExamResource_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == uiModel.ExamResourceId && x.StudyFieldId == uiModel.StudyFieldId);

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

        public bool Delete(int examResourceId, int studyFieldId)
        {
            if (IsDuplicate(examResourceId, studyFieldId))
            {
                var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == examResourceId && x.StudyFieldId == studyFieldId);

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

        public bool IsDuplicate(int examResourceId, int studyFieldId)
        {
            return _table.Any(x => x.ExamResourceId == examResourceId && x.StudyFieldId == studyFieldId);
        }

        public StudyField_ExamResource_ViewModel Get(int examResourceId, int studyFieldId)
        {
            var dbModel = _table.Include(x => x.StudyField)
                                .Include(x => x.ExamResource)
                                .SingleOrDefault(x => x.ExamResourceId == examResourceId && x.StudyFieldId == studyFieldId);
            var uiModel = new StudyField_ExamResource_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<StudyField_ExamResource_ViewModel> GetAllByExamResourceId(int examResourceId)
        {
            var dbModelList = _table.Where(x => x.ExamResourceId == examResourceId)
                                    .Include(x => x.StudyField)
                                    .Include(x => x.ExamResource)
                                    .ToList();
            var uiModelList = new List<StudyField_ExamResource_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<StudyField_ExamResource_ViewModel> GetAllByStudyFieldId(int studyFieldId)
        {
            var dbModelList = _table.Where(x => x.StudyFieldId == studyFieldId)
                                    .Include(x => x.StudyField)
                                    .Include(x => x.ExamResource)
                                    .ToList();
            var uiModelList = new List<StudyField_ExamResource_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<StudyField_ExamResource_ViewModel> GetAllFiltered(string filterStudyFieldId,
                                                                       string filterExamResourceId,
                                                                       string filterInsertDateFrom,
                                                                       string filterInsertDateTo,
                                                                       int currentPage,
                                                                       int pageSize,
                                                                       out int totalRecord)
        {
            string whereStr = "ExamResourceId > 0 AND StudyFieldId > 0";

            if (!string.IsNullOrEmpty(filterStudyFieldId))
            {
                whereStr += " AND StudyFieldId = " + filterStudyFieldId;
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

            var dbModelList = new List<StudyField_ExamResource>();

            dbModelList = _table.Include(x => x.ExamResource).Where(whereStr, insertDateFromMiladi, insertDateToMiladi).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize)
                          .Take(pageSize)
                          .ToList();

            var uiModelList = new List<StudyField_ExamResource_ViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
