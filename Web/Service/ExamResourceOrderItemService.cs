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

namespace Web.Service
{
    public class ExamResourceOrderItemService : IExamResourceOrderItemService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<ExamResourceOrderItem> _table;

        public ExamResourceOrderItemService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<ExamResourceOrderItem>();
        }

        public int Add(ExamResourceOrderItemViewModel uiModel)
        {
            var dbModel = new ExamResourceOrderItem();
            _mapper.Map(uiModel, dbModel);
            dbModel.InsertDate = DateTime.Now;

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.ExamResourceOrderItemId;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public bool Delete(int examResourceOrderItemId)
        {
                var dbModel = _table.SingleOrDefault(x => x.ExamResourceOrderItemId == examResourceOrderItemId);

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

        public bool Edit(ExamResourceOrderItemViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceOrderId == uiModel.ExamResourceOrderItemId);

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

        public ExamResourceOrderItemViewModel Get(int examResourceOrderItemId)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceOrderItemId == examResourceOrderItemId);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ExamResourceOrderItemViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public ExamResourceOrderItemViewModel Get(int examResourceOrderId, int examResourceOrderItemId)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceOrderId == examResourceOrderId && x.ExamResourceOrderItemId == examResourceOrderItemId);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ExamResourceOrderItemViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ExamResourceOrderItemViewModel> GetAllByExamResourceId(int examResourceId)
        {
            var dbModelList = _table.Where(x => x.ExamResourceId == examResourceId)
                .Include(x => x.ExamResource)
                .Include(x => x.ExamResourceOrder)
                .ToList();
            var uiModelList = new List<ExamResourceOrderItemViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<ExamResourceOrderItemViewModel> GetAllByExamResourceOrderId(int examResourceOrderId)
        {
            var dbModelList = _table.Where(x => x.ExamResourceOrderId == examResourceOrderId)
                                    .Include(x => x.ExamResource)
                                    .Include(x => x.ExamResourceOrder)
                .ToList();
            var uiModelList = new List<ExamResourceOrderItemViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<ExamResourceOrderItemViewModel> GetAllFiltered(string filterExamResourceId, string filterExamResourceOrderId,
                                                                    string filterInsertDateFrom, string filterInsertDateTo,
                                                                    int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ExamResourceOrderItemId > 0";

            if (!String.IsNullOrEmpty(filterExamResourceId))
            {
                whereStr += " AND ExamResourceId = " + filterExamResourceId;
            }

            if (!String.IsNullOrEmpty(filterExamResourceOrderId))
            {
                whereStr += " AND ExamResourceOrderId = " + filterExamResourceId;
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

            var dbModelList = new List<ExamResourceOrderItem>();

            dbModelList = _table.Where(whereStr, insertDateFromMiladi, insertDateToMiladi)
                                .Include(x => x.ExamResource)
                                .Include(x => x.ExamResourceOrder)
                                .ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderByDescending(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ExamResourceOrderItemViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
