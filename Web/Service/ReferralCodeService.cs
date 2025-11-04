//using AutoMapper;
//using DbConnection;
//using DbEntities;
//using MD.PersianDateTime;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Linq.Dynamic.Core;
//using Web.Model;
//using Web.Service.Interface;

//namespace Web.Service
//{
//    public class ReferralCodeService : IReferralCodeService
//    {
//        readonly IMapper _mapper;
//        readonly IUnitOfWork _database;
//        readonly DbSet<Collaborator> _table;

//        public ReferralCodeService(
//            IUnitOfWork database,
//            IMapper mappingEngine)
//        {
//            _database = database;
//            _mapper = mappingEngine;
//            _table = _database.Set<Collaborator>();
//        }

//        public int Add(ReferralCodeViewModel uiModel)
//        {
//            var dbModel = new Collaborator();
//            _mapper.Map(source: uiModel, destination: dbModel);

//            dbModel.ReferralCodeName =
//                dbModel.ReferralCodeName
//                .Replace("۰", "0")
//                .Replace("۱", "1")
//                .Replace("۲", "2")
//                .Replace("۳", "3")
//                .Replace("۴", "4")
//                .Replace("۵", "5")
//                .Replace("۶", "6")
//                .Replace("۷", "7")
//                .Replace("۸", "8")
//                .Replace("۹", "9");

//            dbModel.RefCode =
//                dbModel.RefCode
//                .Replace("۰", "0")
//                .Replace("۱", "1")
//                .Replace("۲", "2")
//                .Replace("۳", "3")
//                .Replace("۴", "4")
//                .Replace("۵", "5")
//                .Replace("۶", "6")
//                .Replace("۷", "7")
//                .Replace("۸", "8")
//                .Replace("۹", "9");

//            dbModel.InsertDate = DateTime.Now;

//            _table.Add(dbModel);

//            try
//            {
//                _database.SaveChanges();

//                return dbModel.ReferralCodeId;
//            }
//            catch (Exception)
//            {
//                return -1;
//            }
//        }
            
//        public bool Delete(int id)
//        {
//            var dbModel = _table.SingleOrDefault(x => x.ReferralCodeId == id);

//            _database.Entry(dbModel).State = EntityState.Deleted;

//            try
//            {
//                _database.SaveChanges();

//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public bool Edit(ReferralCodeViewModel uiModel)
//        {
//            var dbModel = _table.SingleOrDefault(x => x.ReferralCodeId == uiModel.ReferralCodeId);

//            uiModel.ReferralCodeName =
//                uiModel.ReferralCodeName
//                .Replace("۰", "0")
//                .Replace("۱", "1")
//                .Replace("۲", "2")
//                .Replace("۳", "3")
//                .Replace("۴", "4")
//                .Replace("۵", "5")
//                .Replace("۶", "6")
//                .Replace("۷", "7")
//                .Replace("۸", "8")
//                .Replace("۹", "9");

//            uiModel.RefCode =
//                uiModel.RefCode
//                .Replace("۰", "0")
//                .Replace("۱", "1")
//                .Replace("۲", "2")
//                .Replace("۳", "3")
//                .Replace("۴", "4")
//                .Replace("۵", "5")
//                .Replace("۶", "6")
//                .Replace("۷", "7")
//                .Replace("۸", "8")
//                .Replace("۹", "9");

//            uiModel.InsertDate = dbModel.InsertDate;

//            _mapper.Map(uiModel, dbModel);
//            _table.Attach(dbModel);

//            _database.Entry(dbModel).State = EntityState.Modified;

//            try
//            {
//                _database.SaveChanges();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public ReferralCodeViewModel Get(int id)
//        {
//            if (id == 0) { return null; }

//            var dbModel = _table.
//                Where(x => x.ReferralCodeId == id)
//                .FirstOrDefault();

//            var uiModel = new ReferralCodeViewModel();

//            _mapper.Map(dbModel, uiModel);

//            PersianCalendar pc = new PersianCalendar();

//            uiModel.ShamsiInsertDate = pc.GetYear(uiModel.InsertDate).ToString("0000/") +
//                pc.GetMonth(uiModel.InsertDate).ToString("00/") +
//                pc.GetDayOfMonth(uiModel.InsertDate).ToString("00");

//            return uiModel;
//        }

//        public ReferralCodeViewModel Get(string refCode)
//        {
//            if (string.IsNullOrEmpty(refCode)) { return null; }

//            var dbModel = _table.
//                Where(x => x.RefCode == refCode)
//                .FirstOrDefault();

//            var uiModel = new ReferralCodeViewModel();

//            _mapper.Map(dbModel, uiModel);

//            PersianCalendar pc = new PersianCalendar();

//            uiModel.ShamsiInsertDate = pc.GetYear(uiModel.InsertDate).ToString("0000/") +
//                pc.GetMonth(uiModel.InsertDate).ToString("00/") +
//                pc.GetDayOfMonth(uiModel.InsertDate).ToString("00");

//            return uiModel;
//        }

//        public IList<ReferralCodeViewModel> GetAllFiltered(
//            string filterReferralCodeName, string filterRefCode,
//            string filterActive,
//            string filterInsertDateFrom, string filterInsertDateTo,
//            int currentPage, int pageSize, out int totalRecord)
//        {
//            string whereStr = "ReferralCodeId > 0 ";

//            if (!String.IsNullOrEmpty(filterReferralCodeName))
//            {
//                whereStr += " AND ReferralCodeName.Contains(@0)";
//            }

//            if (!String.IsNullOrEmpty(filterRefCode))
//            {
//                whereStr += " AND RefCode.Contains(@1)";
//            }

//            if (!String.IsNullOrEmpty(filterActive))
//            {
//                whereStr += " AND Active = " + filterActive;
//            }

//            DateTime? insertDateFromMiladi = null;
//            if (!string.IsNullOrEmpty(filterInsertDateFrom))
//            {
//                filterInsertDateFrom =
//                    filterInsertDateFrom.Replace("۰", "0")
//                        .Replace("۱", "1")
//                        .Replace("۲", "2")
//                        .Replace("۳", "3")
//                        .Replace("۴", "4")
//                        .Replace("۵", "5")
//                        .Replace("۶", "6")
//                        .Replace("۷", "7")
//                        .Replace("۸", "8")
//                        .Replace("۹", "9");
//                PersianDateTime shamsiInsertDateFrom = PersianDateTime.Parse(filterInsertDateFrom);
//                insertDateFromMiladi = shamsiInsertDateFrom.ToDateTime();
//                whereStr += " AND InsertDate >= @2";
//            }

//            DateTime? insertDateToMiladi = null;
//            if (!string.IsNullOrEmpty(filterInsertDateTo))
//            {
//                filterInsertDateTo =
//                    filterInsertDateTo.Replace("۰", "0")
//                        .Replace("۱", "1")
//                        .Replace("۲", "2")
//                        .Replace("۳", "3")
//                        .Replace("۴", "4")
//                        .Replace("۵", "5")
//                        .Replace("۶", "6")
//                        .Replace("۷", "7")
//                        .Replace("۸", "8")
//                        .Replace("۹", "9");
//                PersianDateTime shamsiInsertDateTo = PersianDateTime.Parse(filterInsertDateTo);
//                insertDateToMiladi = shamsiInsertDateTo.ToDateTime();

//                insertDateToMiladi = insertDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

//                whereStr += " AND InsertDate <= @3";
//            }

//            var dbModelList = new List<ReferralCode>();

//            dbModelList = _table.Where(whereStr, filterReferralCodeName, filterRefCode,
//                insertDateFromMiladi, insertDateToMiladi).ToList();

//            totalRecord = dbModelList.Count();

//            dbModelList = dbModelList
//                .OrderByDescending(x => x.InsertDate)
//                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

//            var uiModelList = new List<ReferralCodeViewModel>();
//            _mapper.Map(dbModelList, uiModelList);

//            PersianCalendar pc = new PersianCalendar();
//            foreach (var uiModelItem in uiModelList)
//            {
//                uiModelItem.ShamsiInsertDate = pc.GetYear(uiModelItem.InsertDate).ToString("0000/") +
//                    pc.GetMonth(uiModelItem.InsertDate).ToString("00/") +
//                    pc.GetDayOfMonth(uiModelItem.InsertDate).ToString("00");
//            }

//            return uiModelList;
//        }

//        public bool IsDuplicateByRefCode(int? referralCodeId, string refCode)
//        {
//            if (referralCodeId != null)
//            {
//                return _table.Any(x => x.ReferralCodeId != (int)referralCodeId && x.RefCode == refCode);
//            }
//            else
//            {
//                return _table.Any(x => x.RefCode == refCode);
//            }
//        }
//    }
//}
