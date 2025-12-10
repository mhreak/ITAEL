using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using MD.PersianDateTime;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class ExamResourceService : IExamResourceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<ExamResource> _table;

        public ExamResourceService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<ExamResource>();
        }

        public int Add(ExamResourceViewModel uiModel)
        {
            var dbModel = new ExamResource();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.InsertDate = DateTime.Now;

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.ExamResourceId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(ExamResourceViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == uiModel.ExamResourceId);

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
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == id);

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

        public ExamResourceViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.FirstOrDefault(x => x.ExamResourceId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ExamResourceViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ExamResourceViewModel> GetAll()
        {
            
            var dbModel = _table.ToList();

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new List<ExamResourceViewModel>();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ExamResourceViewModel> GetAllFiltered(string filterResourceName, string filterDescription,
                                                           string filterType, string filterPriceFrom, string filterPriceTo,
                                                           string filterInsertDateFrom, string filterInsertDateTo,
                                                           int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ExamResourceId > 0 ";

            if (!string.IsNullOrEmpty(filterResourceName))
            {
                whereStr += " AND ResourceName.Contains(@0)";
            }

            if (!string.IsNullOrEmpty(filterDescription))
            {
                whereStr += " AND Description.Contains(@1)";
            }

            if (!string.IsNullOrEmpty(filterType))
            {
                whereStr += " AND Type = " + filterType;
            }

            int? priceFrom = null;
            if (!string.IsNullOrEmpty(filterPriceFrom))
            {
                priceFrom = int.Parse(filterPriceFrom);
                whereStr += " AND Price >= @2";
            }

            int? priceTo = null;
            if (!string.IsNullOrEmpty(filterPriceTo))
            {
                priceTo = int.Parse(filterPriceTo);
                whereStr += " AND Price <= @3";
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
                whereStr += " AND InsertDate >= @4";
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

                whereStr += " AND InsertDate <= @5";
            }


            var dbModelList = new List<ExamResource>();

            dbModelList = _table.Where(whereStr, filterResourceName, filterDescription, priceFrom, priceTo,
                                       insertDateFromMiladi, insertDateToMiladi).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ExamResourceViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        public bool SetImageFileName(int examResourceId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == examResourceId);

            dbModel.ImageFileName = fileName;

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
    }
}
