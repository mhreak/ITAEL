using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using Web.Service.Interface;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class ExamQuestionService : IExamQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<ExamQuestion> _table;

        public ExamQuestionService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<ExamQuestion>();
        }

        public int Add(ExamQuestionViewModel uiModel)
        {
            var dbModel = new ExamQuestion();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.ExamQuestionId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(ExamQuestionViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamQuestionId == uiModel.ExamQuestionId);

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

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamQuestionId == id);

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

        public ExamQuestionViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.Include(x => x.Exam).FirstOrDefault(x => x.ExamQuestionId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ExamQuestionViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ExamQuestionViewModel> GetAllFiltered(string filterText, string filterExamId, string filterExamTitle,
                                                           string filterType, string filterQuestionOrder, 
                                                           int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ExamQuestionId > 0 ";

            if (!String.IsNullOrEmpty(filterText))
            {
                whereStr += " AND Text.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterExamTitle))
            {
                whereStr += " AND Exam.Title.Contains(@1)";
            }

            if (!String.IsNullOrEmpty(filterExamId))
            {
                whereStr += " AND ExamId = " + filterExamId;
            }

            if (!String.IsNullOrEmpty(filterType))
            {
                whereStr += " AND Type = " + filterType;
            }

            if (!String.IsNullOrEmpty(filterExamTitle))
            {
                whereStr += " AND QuestionOrder = " + filterQuestionOrder;
            }

            var dbModelList = new List<ExamQuestion>();

            dbModelList = _table.Where(whereStr, filterText, filterExamTitle).Include(x => x.Exam).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.QuestionOrder)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ExamQuestionViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
