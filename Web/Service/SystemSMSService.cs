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
    public class SystemSMSService : ISystemSMSService
    {
        readonly IMapper _mapper;
        private readonly IFarazSMSService _farazSMSService;
        readonly IUnitOfWork _database;
        readonly DbSet<SystemSMS> _table;

        public SystemSMSService(IUnitOfWork database, IMapper mappingEngine, IFarazSMSService farazSMSService)
        {
            _database = database;
            _mapper = mappingEngine;
            _farazSMSService = farazSMSService;
            _table = _database.Set<SystemSMS>();
        }

        public int Add(SystemSMSViewModel uiModel)
        {
            var dbModel = new SystemSMS();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.SystemSMSId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.SystemSMSId == id);

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

        public bool Edit(SystemSMSViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.SystemSMSId == uiModel.SystemSMSId);

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

        public SystemSMSViewModel Get(int id)
        {
            var dbModel = _table.Where(x => x.SystemSMSId == id).FirstOrDefault();

            var uiModel = new SystemSMSViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public KeyValuePair<bool, string> SendSMS(List<string> receipts, string message)
        {
            return new KeyValuePair<bool, string>(_farazSMSService.SendSMS(receipts, message), "");
        }

        public IList<SystemSMSViewModel> GetAllFiltered(
            string filterMobile, string filterSMSType,
            string filterSendDateFrom, string filterSendDateTo,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = " SystemSMSId > 0 ";
            currentPage = currentPage - 1;

            if (!String.IsNullOrEmpty(filterMobile))
            {
                whereStr += " AND Mobile.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterSMSType))
            {
                whereStr += " AND SMSType = " + filterSMSType;
            }

            DateTime? sendDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterSendDateFrom))
            {
                filterSendDateFrom =
                    filterSendDateFrom.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime fromDateShamsi = PersianDateTime.Parse(filterSendDateFrom);
                sendDateFromMiladi = fromDateShamsi.ToDateTime();
                whereStr += " And SendDate >= @1";
            }

            DateTime? sendDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterSendDateTo))
            {
                filterSendDateTo =
                    filterSendDateTo.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime toDateShamsi = PersianDateTime.Parse(filterSendDateTo);
                sendDateToMiladi = toDateShamsi.ToDateTime();

                sendDateToMiladi = sendDateToMiladi.Value.AddHours(23);
                sendDateToMiladi = sendDateToMiladi.Value.AddMinutes(59);
                sendDateToMiladi = sendDateToMiladi.Value.AddSeconds(59);

                whereStr += " And SendDate <= @2";
            }

            var dbModelList = _table
                .Where(whereStr, filterMobile, sendDateFromMiladi, sendDateToMiladi)
                .OrderByDescending(x => x.SendDate)
                .ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList.Skip(currentPage * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<SystemSMSViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            PersianCalendar pc = new PersianCalendar();
            foreach (var uiModelItem in uiModelList)
            {
                uiModelItem.ShamsiSendDate = 
                    uiModelItem.SendDate.Hour.ToString() + ":" +
                    uiModelItem.SendDate.Minute.ToString() + " " +
                    pc.GetYear(uiModelItem.SendDate) + "/" +
                    (pc.GetMonth(uiModelItem.SendDate) < 10 ? "0" +
                    pc.GetMonth(uiModelItem.SendDate).ToString() :
                    pc.GetMonth(uiModelItem.SendDate).ToString()) +
                    "/" + (pc.GetDayOfMonth(uiModelItem.SendDate) < 10 ? "0"
                    + pc.GetDayOfMonth(uiModelItem.SendDate).ToString()
                    : pc.GetDayOfMonth(uiModelItem.SendDate).ToString());
            }


            return uiModelList;
        }
    }
}
