using AutoMapper;
using DbConnection;
using DbEntities;
using MD.PersianDateTime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class WalletCommissionService : IWalletCommissionService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<WalletCommission> _table;

        public WalletCommissionService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<WalletCommission>();
        }

        public int Add(WalletCommissionViewModel uiModel)
        {
            var dbModel = new WalletCommission();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.WalletCommissionId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == id);

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

        public bool Edit(WalletCommissionViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == uiModel.WalletId);

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

        public WalletCommissionViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.
                Where(x => x.WalletCommissionId == id)
                .FirstOrDefault();

            var uiModel = new WalletCommissionViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<WalletCommissionViewModel> GetAllFiltered(
            string filterWalletId,
            string filterCommissionFrom, string filterCommissionTo,
            string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "WalletCommissionId > 0 ";

            if (!String.IsNullOrEmpty(filterWalletId))
            {
                whereStr += " AND WalletId = " + filterWalletId;
            }

            if (!String.IsNullOrEmpty(filterCommissionFrom))
            {
                whereStr += " AND Commission >= " + filterCommissionFrom;
            }

            if (!String.IsNullOrEmpty(filterCommissionTo))
            {
                whereStr += " AND Commission <= " + filterCommissionTo;
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
                whereStr += " AND InsertDate <= @1";
            }

            var dbModelList = new List<WalletCommission>();

            dbModelList = _table.Where(whereStr, filterInsertDateFrom, filterInsertDateTo).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                .OrderByDescending(x => x.InsertDate)
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<WalletCommissionViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            PersianCalendar pc = new PersianCalendar();

            foreach (var uiModelItem in uiModelList)
            {
                uiModelItem.ShamsiInsertDate = pc.GetYear(uiModelItem.InsertDate).ToString("0000/") +
                    pc.GetMonth(uiModelItem.InsertDate).ToString("00/") +
                    pc.GetDayOfMonth(uiModelItem.InsertDate).ToString("00");
            }

            return uiModelList;
        }
    }
}
