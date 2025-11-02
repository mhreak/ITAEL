using AutoMapper;
using DbConnection;
using DbEntities;
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
    public class CommissionRuleService : ICommissionRuleService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<CommissionRule> _table;

        public CommissionRuleService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<CommissionRule>();
        }

        public int Add(CommissionRuleViewModel uiModel)
        {
            var dbModel = new CommissionRule();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.IsDeleted = false;

            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.CommissionRuleId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.CommissionRuleId == id);

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

        public bool Edit(CommissionRuleViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.CommissionRuleId == uiModel.CommissionRuleId);

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

        public CommissionRuleViewModel Get(int id)
        {
            var dbModel = _table
                .Where(x => x.CommissionRuleId == id)
                .FirstOrDefault();

            var uiModel = new CommissionRuleViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<CommissionRuleViewModel> GetAll(bool? active)
        {
            var dbModelList = new List<CommissionRule>();

            if (active != null)
            {
                dbModelList = _table.Where(x => x.Active == (bool)active && !x.IsDeleted).ToList();
            }
            else
            {
                dbModelList = _table.Where(x => !x.IsDeleted).ToList();
            }

            var uiModelList = new List<CommissionRuleViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<CommissionRuleViewModel> GetAllFiltered(
            string filterCommissionBasedOn, string filterCommissionType, string filterActive,
            int currentPage, int pageSize, string sortField,
            string sortDirection, out int totalRecord)
        {
            string whereStr = " IsDeleted = False ";
            currentPage = currentPage - 1;

            if (!String.IsNullOrEmpty(filterCommissionBasedOn))
            {
                whereStr += " AND CommissionBasedOn = " + filterCommissionBasedOn;
            }

            if (!String.IsNullOrEmpty(filterCommissionType))
            {
                whereStr += " AND CommissionType = " + filterCommissionType;
            }

            if (!String.IsNullOrEmpty(filterActive))
            {
                whereStr += " AND Active = " + filterActive;
            }

            var dbModelList = _table.Where(whereStr)
                .OrderBy(sortField + " " + sortDirection)
                .Skip(currentPage * pageSize).Take(pageSize).ToList();

            totalRecord = _table.Where(whereStr).Count();

            var uiModelList = new List<CommissionRuleViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            foreach (var uiModelItem in uiModelList)
            {
                if (uiModelItem.CommissionBasedOn == 1)
                {
                    uiModelItem.Min = uiModelItem.MinimumNumber + " عدد";
                    uiModelItem.Max = uiModelItem.MaximumNumber + " عدد";
                }
                else if (uiModelItem.CommissionBasedOn == 2)
                {
                    uiModelItem.Min = ((long)uiModelItem.MinimumAmount).ToString("N0", new NumberFormatInfo()
                    {
                        NumberGroupSizes = new[] { 3 },
                        NumberGroupSeparator = ","
                    }) + " ریال";

                    uiModelItem.Max = ((long)uiModelItem.MaximumAmount).ToString("N0", new NumberFormatInfo()
                    {
                        NumberGroupSizes = new[] { 3 },
                        NumberGroupSeparator = ","
                    }) + " ریال";
                }
            }

            return uiModelList;
        }
    }
}
