using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using Web.Model.Identity;
using MD.PersianDateTime;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Web.Service.Interface;

namespace Web.Service
{
    public class OnlineTransactionService : BaseService, IOnlineTransactionService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _uow;
        readonly DbSet<OnlineTransaction> _table;

        public OnlineTransactionService(IUnitOfWork uow, IMapper mappingEngine)
        {
            _uow = uow;
            _table = _uow.Set<OnlineTransaction>();
            _mapper = mappingEngine;
        }

        public long Add(OnlineTransactionViewModel uiModel)
        {
            var dbModel = new OnlineTransaction();
            _mapper.Map(source: uiModel, destination: dbModel);
            _table.Add(dbModel);
            try
            {
                _uow.SaveChanges();
                return dbModel.OnlineTransactionId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public OnlineTransactionViewModel Get(long id)
        {
            if (_table.Any(x => x.OnlineTransactionId == id))
            {
                var dbModel = _table.Where(m => m.OnlineTransactionId == id)
                    .Include(x => x.ExamResourceOrder.Applicant)
                    .Include(x => x.BankGateway)
                    .FirstOrDefault();
                var uiModel = new OnlineTransactionViewModel();
                _mapper.Map(source: dbModel, destination: uiModel);
                return uiModel;
            }
            else
            {
                return null;
            }
        }

        public IList<OnlineTransactionViewModel> GetAllByApplicantId(int applicantId, short? state)
        {
            if (state != null)
            {
                var dbModelList = _table.Where(x => x.ExamResourceOrder.ApplicantId == applicantId && x.State == state)
                    .Include(x => x.ExamResourceOrder.Applicant)
                    .Include(x => x.BankGateway)
                    .ToList();
                List<OnlineTransactionViewModel> uiModelList = new List<OnlineTransactionViewModel>();
                _mapper.Map(dbModelList, uiModelList);
                return uiModelList;
            }
            else
            {
                var dbModelList = _table.Where(x => x.ExamResourceOrder.ApplicantId == applicantId)
                                        .Include(x => x.ExamResourceOrder.Applicant)
                    .Include(x => x.BankGateway)
                    .ToList();
                List<OnlineTransactionViewModel> uiModelList = new List<OnlineTransactionViewModel>();
                _mapper.Map(dbModelList, uiModelList);
                return uiModelList;
            }
        }

        //public IList<OnlineTransactionViewModel> GetAllByExamResourceId(int examResourceId, short? state)
        //{
        //    if (state != null)
        //    {
        //        var dbModelList = _table
        //            .Where(x => x.ExamResourceOrder. == examResourceId && x.State == state)
        //            .Include(x => x.ExamResourceOrder.Applicant)
        //            .Include(x => x.BankGateway)
        //            .ToList();
        //        List<OnlineTransactionViewModel> uiModelList = new List<OnlineTransactionViewModel>();
        //        _mapper.Map(dbModelList, uiModelList);
        //        return uiModelList;
        //    }
        //    else
        //    {
        //        var dbModelList = _table
        //            .Where(x => x.ExamResourceOrder.ExamResourceId == examResourceId)
        //            .Include(x => x.ExamResourceOrder.Applicant)
        //            .Include(x => x.BankGateway)
        //            .ToList();
        //        List<OnlineTransactionViewModel> uiModelList = new List<OnlineTransactionViewModel>();
        //        _mapper.Map(dbModelList, uiModelList);
        //        return uiModelList;
        //    }
        //}

        public IList<OnlineTransactionViewModel> GetAllFiltered(string filterApplicantId,
            string filterExamResourceId,
            string filterAmountFrom, string filterAmountTo,
            string filterDateFrom, string filterDateTo, string filterState,
            int currentPage, int pageSize, string sortField, string sortDirection, out int totalRecord)
        {
            if (!IsValidProperty<OnlineTransaction>(sortField))
                throw new ArgumentException($"Invalid sort field: {sortField} And IsDeleted = false");

            sortDirection = sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";

            string whereStr = "OnlineTransactionId > 0";
            // Initialize parameters list and parameter index
            var parameters = new List<object>();
            int parameterIndex = 0;

            // Filter by PassengerId
            if (!String.IsNullOrEmpty(filterApplicantId))
            {
                whereStr += $" And ApplicantId = @{parameterIndex}";
                parameters.Add(filterApplicantId);
                parameterIndex++;
            }

            // Filter by ShuttleServiceId
            if (!String.IsNullOrEmpty(filterExamResourceId))
            {
                whereStr += $" And ExamResourceId = @{parameterIndex}";
                parameters.Add(filterExamResourceId);
                parameterIndex++;
            }

            // Filter by AmountFrom
            if (!String.IsNullOrEmpty(filterAmountFrom))
            {
                whereStr += $" And Amount >= @{parameterIndex}";
                parameters.Add(filterAmountFrom);
                parameterIndex++;
            }

            // Filter by AmountTo
            if (!String.IsNullOrEmpty(filterAmountTo))
            {
                whereStr += $" And Amount <= @{parameterIndex}";
                parameters.Add(filterAmountTo);
                parameterIndex++;
            }

            // Convert and filter by InsertDate (DateFrom)
            DateTime? dateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterDateFrom))
            {
                filterDateFrom = filterDateFrom.Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9");

                PersianDateTime fromDateShamsi = PersianDateTime.Parse(filterDateFrom);
                dateFromMiladi = fromDateShamsi.ToDateTime();
                whereStr += $" And InsertDate >= @{parameterIndex}";
                parameters.Add(dateFromMiladi);
                parameterIndex++;
            }

            // Convert and filter by InsertDate (DateTo)
            DateTime? dateToMiladi = null;
            if (!string.IsNullOrEmpty(filterDateTo))
            {
                filterDateTo = filterDateTo.Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9");

                PersianDateTime toDateShamsi = PersianDateTime.Parse(filterDateTo);
                dateToMiladi = toDateShamsi.ToDateTime();
                whereStr += $" And InsertDate <= @{parameterIndex}";
                parameters.Add(dateToMiladi);
                parameterIndex++;
            }

            // Filter by State
            if (!String.IsNullOrEmpty(filterState))
            {
                whereStr += $" And State = @{parameterIndex}";
                parameters.Add(filterState);
                parameterIndex++;
            }

            // Adjust pageSize if necessary
            pageSize = pageSize > 1000 ? 1000 : pageSize;

            // Query the database using the built whereStr and parameters
            var dbModelList = _table.Where(whereStr, parameters.ToArray())
                .Include(x => x.ExamResourceOrder.Applicant)
                .Include(x => x.BankGateway)
                .OrderBy(sortField + " " + sortDirection)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            totalRecord = _table.Where(whereStr, parameters.ToArray()).Count();

            var uiModelList = new List<OnlineTransactionViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        //public long GetSumByExamResourceId(int examResourceId, short? state)
        //{
        //    if (state != null)
        //    {
        //        return _table.Where(x => x.ExamResourceOrder.ExamResourceId == examResourceId && x.State == state).Sum(x => x.Amount);
        //    }
        //    else
        //    {
        //        return _table.Where(x => x.ExamResourceOrder.ExamResourceId == examResourceId).Sum(x => x.Amount);
        //    }
        //}

        public bool SetReferenceNumber(long transactionId, string refNumber)
        {
            if (_table.Any(x => x.OnlineTransactionId == transactionId))
            {
                var dbModel = _table.SingleOrDefault(x => x.OnlineTransactionId == transactionId);

                dbModel.ReferenceNumber = refNumber;

                _table.Attach(dbModel);
                _uow.Entry(dbModel).State = EntityState.Modified;

                try
                {
                    _uow.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool SetState(long transactionId, short state, DateTime? verifyDate)
        {
            if (_table.Any(x => x.OnlineTransactionId == transactionId))
            {
                var dbModel = _table.SingleOrDefault(x => x.OnlineTransactionId == transactionId);

                dbModel.State = state;
                if (state == 1 && (verifyDate != null))
                {
                    dbModel.VerifyDate = verifyDate;
                }
                else
                {
                    dbModel.VerifyDate = null;
                }

                _table.Attach(dbModel);
                _uow.Entry(dbModel).State = EntityState.Modified;

                try
                {
                    _uow.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
