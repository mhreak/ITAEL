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
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<Company> _table;

        public CompanyService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Company>();
        }

        public int Add(CompanyViewModel uiModel)
        {
            var dbModel = new Company();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.InsertDate = DateTime.Now;
            dbModel.IsDeleted = false;

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.CompanyId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.CompanyId == id);

            dbModel.IsDeleted = true;

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

        public bool Edit(CompanyViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.CompanyId == uiModel.CompanyId);

            uiModel.InsertDate = dbModel.InsertDate;
            uiModel.IsDeleted = dbModel.IsDeleted;

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

        public CompanyViewModel Get(int id)
        {
            var dbModel = _table.FirstOrDefault(x => x.CompanyId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new CompanyViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<CompanyViewModel> GetAll()
        {
            var dbModelList = _table.ToList();
            var uiModelList = new List<CompanyViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<CompanyViewModel> GetAllFiltered(
            string filterCompanyName,
            string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "IsDeleted != True ";

            if (!String.IsNullOrEmpty(filterCompanyName))
            {
                whereStr += " AND CompanyName.Contains(@0)";
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

            var dbModelList = new List<Company>();

            dbModelList = _table.Where(whereStr, filterCompanyName,
                insertDateFromMiladi, insertDateToMiladi).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                .OrderBy(x => x.CompanyName)
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<CompanyViewModel>();
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

        public bool IsDuplicateByCompanyName(int? companyId, string companyName)
        {
            if (companyId != null)
            {
                return _table.Any(x => x.CompanyId != (int)companyId && x.CompanyName == companyName);
            }
            else
            {
                return _table.Any(x => x.CompanyName == companyName);
            }
        }
    }
}
