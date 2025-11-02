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
    public class WalletService : IWalletService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<Wallet> _table;

        public WalletService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Wallet>();
        }

        public int Add(WalletViewModel uiModel)
        {
            var dbModel = new Wallet();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.WalletName =
                dbModel.WalletName.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9");

            dbModel.InsertDate = DateTime.Now;

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.WalletId;
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

        public bool Edit(WalletViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == uiModel.WalletId);

            uiModel.WalletName =
                    uiModel.WalletName.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");

            uiModel.InsertDate = dbModel.InsertDate;

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

        public WalletViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.
                Where(x => x.WalletId == id)
                .FirstOrDefault();

            var uiModel = new WalletViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<WalletViewModel> GetAll()
        {
            var dbModelList = _table.OrderBy(x => x.WalletName).ToList();
            var uiModelList = new List<WalletViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<WalletViewModel> GetAllFiltered(
            string filterWalletName,
            string filterActive, string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "WalletId > 0 ";

            if (!String.IsNullOrEmpty(filterWalletName))
            {
                filterWalletName =
                    filterWalletName.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");

                whereStr += " AND WalletName.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterActive))
            {
                whereStr += " AND Active = " + filterActive;
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
                whereStr += " AND InsertDate >= @1";
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

                whereStr += " AND InsertDate <= @2";
            }

            var dbModelList = new List<Wallet>();

            dbModelList = _table.Where(whereStr, filterWalletName, insertDateFromMiladi, insertDateToMiladi).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                .OrderBy(x => x.WalletName)
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<WalletViewModel>();
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

        public bool IsDuplicateByWalletName(int? walletId, string walletName)
        {
            if (walletId != null)
            {
                return _table.Any(x => x.WalletId != (int)walletId && x.WalletName == walletName);
            }
            else
            {
                return _table.Any(x => x.WalletName == walletName);
            }
        }
    }
}
