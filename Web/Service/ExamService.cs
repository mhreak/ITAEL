using System;
using Web.Model;
using DbEntities;
using AutoMapper;
using System.Linq;
using DbConnection;
using MD.PersianDateTime;
using Web.Service.Interface;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class ExamService : IExamService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<Exam> _table;

        public ExamService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Exam>();
        }
        public int Add(ExamViewModel uiModel)
        {
            var dbModel = new Exam();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.ExamId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(ExamViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamId == uiModel.ExamId);

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
            var dbModel = _table.SingleOrDefault(x => x.ExamId == id);

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

        public ExamViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.FirstOrDefault(x => x.ExamId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ExamViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ExamViewModel> GetAllFiltered(string filterTitle, string filterDescription,
                                                   int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ExamId > 0 ";

            if (!string.IsNullOrEmpty(filterTitle))
            {
                whereStr += " AND Title.Contains(@0)";
            }

            if (!string.IsNullOrEmpty(filterDescription))
            {
                whereStr += " AND Description.Contains(@1)";
            }


            var dbModelList = new List<Exam>();

            dbModelList = _table.Where(whereStr, filterTitle, filterDescription).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.Title)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ExamViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
