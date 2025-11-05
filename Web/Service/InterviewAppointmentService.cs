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
    public class InterviewAppointmentService : IInterviewAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<InterviewAppointment> _table;

        public InterviewAppointmentService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<InterviewAppointment>();
        }

        public int Add(InterviewAppointmentViewModel uiModel)
        {
            var dbModel = new InterviewAppointment();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.InsertDate = DateTime.Now;

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.InterviewAppointmentId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(InterviewAppointmentViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.InterviewAppointmentId == uiModel.InterviewAppointmentId);

            _mapper.Map(uiModel, dbModel);

            dbModel.InsertDate = DateTime.Now;

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
            var dbModel = _table.SingleOrDefault(x => x.InterviewAppointmentId == id);

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

        public InterviewAppointmentViewModel Get(int id)
        {
            var dbModel = _table.Include(x => x.Applicant)
                                .Include(x => x.JobAnnouncement)
                                .FirstOrDefault(x => x.InterviewAppointmentId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new InterviewAppointmentViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<InterviewAppointmentViewModel> GetAllFiltered(string filterJobAnnouncementId, string filterApplicantId, 
                                                                   string filterStatus, string filterInsertDateFrom, string filterInsertDateTo, 
                                                                   int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "InterviewAppointmentId > 0 ";

            if (!String.IsNullOrEmpty(filterJobAnnouncementId))
            {
                whereStr += " AND JobAnnouncementId = " + filterJobAnnouncementId;
            }

            if (!String.IsNullOrEmpty(filterApplicantId))
            {
                whereStr += " AND ApplicantId = " + filterApplicantId;
            }

            if (!String.IsNullOrEmpty(filterStatus))
            {
                whereStr += " AND Status = " + filterStatus;
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
                PersianDateTime shamsiOrderDateFrom = PersianDateTime.Parse(filterInsertDateFrom);
                insertDateFromMiladi = shamsiOrderDateFrom.ToDateTime();
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
                PersianDateTime shamsiOrderDateTo = PersianDateTime.Parse(filterInsertDateTo);
                insertDateToMiladi = shamsiOrderDateTo.ToDateTime();

                insertDateToMiladi = insertDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                whereStr += " AND InsertDate <= @1";
            }

            var dbModelList = new List<InterviewAppointment>();

            dbModelList = _table.Where(whereStr, insertDateFromMiladi, insertDateToMiladi)
                                .Include(x => x.Applicant)
                                .Include(x => x.JobAnnouncement)
                                .ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                          .OrderBy(x => x.InsertDate)
                          .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<InterviewAppointmentViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
