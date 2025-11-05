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
    public class ExamResourceOrderService : IExamResourceOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<ExamResourceOrder> _table;

        public ExamResourceOrderService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<ExamResourceOrder>();
        }

        public int Add(ExamResourceOrderViewModel uiModel)
        {
            var dbModel = new ExamResourceOrder();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.ExamResourceOrderId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(ExamResourceOrderViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceOrderId == uiModel.ExamResourceOrderId);

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
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceOrderId == id);

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

        public ExamResourceOrderViewModel Get(int id)
        {
            var dbModel = _table.Include(x => x.Applicant)
                                .Include(x => x.ExamResource)
                                .FirstOrDefault(x => x.ExamResourceOrderId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ExamResourceOrderViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ExamResourceOrderViewModel> GetAllFiltered(string filterApplicantId,
                                                                string filterExamResourceId,
                                                                string filterStatus,
                                                                string filterTotalPriceFrom,
                                                                string filterTotalPriceTo,
                                                                string filterOrderDateFrom,
                                                                string filterOrderDateTo,
                                                                string filterDeliveryDateFrom,
                                                                string filterDeliveryDateTo,
                                                                int currentPage,
                                                                int pageSize,
                                                                out int totalRecord)
        {
            string whereStr = "ExamResourceOrderId > 0 ";

            if (!String.IsNullOrEmpty(filterApplicantId))
            {
                whereStr += " AND ApplicantId = " + filterApplicantId;
            }

            if (!String.IsNullOrEmpty(filterExamResourceId))
            {
                whereStr += " AND ExamResourceId = " + filterExamResourceId;
            }

            if (!String.IsNullOrEmpty(filterStatus))
            {
                whereStr += " AND Status = " + filterStatus;
            }

            if (!string.IsNullOrEmpty(filterTotalPriceFrom))
            {
                whereStr += " AND TotalPrice >= @0";
            }

            if (!string.IsNullOrEmpty(filterTotalPriceTo))
            {
                whereStr += " AND TotalPrice <= @1";
            }

            DateTime? insertOrderDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterOrderDateFrom))
            {
                filterOrderDateFrom =
                    filterOrderDateFrom.Replace("۰", "0")
                                             .Replace("۱", "1")
                                             .Replace("۲", "2")
                                             .Replace("۳", "3")
                                             .Replace("۴", "4")
                                             .Replace("۵", "5")
                                             .Replace("۶", "6")
                                             .Replace("۷", "7")
                                             .Replace("۸", "8")
                                             .Replace("۹", "9");
                PersianDateTime shamsiOrderDateFrom = PersianDateTime.Parse(filterOrderDateFrom);
                insertOrderDateFromMiladi = shamsiOrderDateFrom.ToDateTime();
                whereStr += " AND OrderDate >= @2";
            }

            DateTime? insertOrderDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterOrderDateTo))
            {
                filterOrderDateTo =
                    filterOrderDateTo.Replace("۰", "0")
                                           .Replace("۱", "1")
                                           .Replace("۲", "2")
                                           .Replace("۳", "3")
                                           .Replace("۴", "4")
                                           .Replace("۵", "5")
                                           .Replace("۶", "6")
                                           .Replace("۷", "7")
                                           .Replace("۸", "8")
                                           .Replace("۹", "9");
                PersianDateTime shamsiOrderDateTo = PersianDateTime.Parse(filterOrderDateTo);
                insertOrderDateToMiladi = shamsiOrderDateTo.ToDateTime();

                insertOrderDateToMiladi = insertOrderDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                whereStr += " AND OrderDate <= @3";
            }

            DateTime? insertDeliveryDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterDeliveryDateFrom))
            {
                filterOrderDateFrom =
                    filterOrderDateFrom.Replace("۰", "0")
                                             .Replace("۱", "1")
                                             .Replace("۲", "2")
                                             .Replace("۳", "3")
                                             .Replace("۴", "4")
                                             .Replace("۵", "5")
                                             .Replace("۶", "6")
                                             .Replace("۷", "7")
                                             .Replace("۸", "8")
                                             .Replace("۹", "9");
                PersianDateTime shamsiDeliveryDateFrom = PersianDateTime.Parse(filterDeliveryDateFrom);
                insertDeliveryDateFromMiladi = shamsiDeliveryDateFrom.ToDateTime();
                whereStr += " AND DeliveryDate >= @4";
            }

            DateTime? insertDeliveryDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterDeliveryDateTo))
            {
                filterOrderDateTo =
                    filterOrderDateTo.Replace("۰", "0")
                                           .Replace("۱", "1")
                                           .Replace("۲", "2")
                                           .Replace("۳", "3")
                                           .Replace("۴", "4")
                                           .Replace("۵", "5")
                                           .Replace("۶", "6")
                                           .Replace("۷", "7")
                                           .Replace("۸", "8")
                                           .Replace("۹", "9");
                PersianDateTime shamsiDeliveryDateTo = PersianDateTime.Parse(filterDeliveryDateTo);
                insertDeliveryDateToMiladi = shamsiDeliveryDateTo.ToDateTime();

                insertDeliveryDateToMiladi = insertDeliveryDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                whereStr += " AND DeliveryDate <= @5";
            }

            var dbModelList = new List<ExamResourceOrder>();

            dbModelList = _table.Where(whereStr, filterTotalPriceFrom,
                                       filterTotalPriceTo, insertOrderDateFromMiladi,
                                       insertOrderDateToMiladi, insertDeliveryDateFromMiladi,
                                       insertDeliveryDateToMiladi)
                                .Include(x => x.Applicant)
                                .Include(x => x.ExamResource)
                                .ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                .OrderBy(x => x.OrderDate)
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ExamResourceOrderViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
