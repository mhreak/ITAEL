using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using System.Globalization;
using Web.Service.Interface;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service;

public class CollaboratorService : ICollaboratorService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _database;
    private readonly DbSet<Collaborator> _table;

    public CollaboratorService(
        IUnitOfWork database,
        IMapper mappingEngine)
    {
        _database = database;
        _mapper = mappingEngine;
        _table = _database.Set<Collaborator>();
    }

    public int Add(CollaboratorViewModel uiModel)
    {
        var dbModel = new Collaborator();
        _mapper.Map(source: uiModel, destination: dbModel);

        dbModel.ReferralCode =
            dbModel.ReferralCode
                   .Replace("۰", "0")
                   .Replace("۱", "1")
                   .Replace("۲", "2")
                   .Replace("۳", "3")
                   .Replace("۴", "4")
                   .Replace("۵", "5")
                   .Replace("۶", "6")
                   .Replace("۷", "7")
                   .Replace("۸", "8")
                   .Replace("۹", "9");

        //dbModel.InsertDate = DateTime.Now;

        _table.Add(dbModel);

        try
        {
            _database.SaveChanges();

            return dbModel.CollaboratorId;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public bool Delete(int id)
    {
        var dbModel = _table.SingleOrDefault(x => x.CollaboratorId == id);

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

    public bool Edit(CollaboratorViewModel uiModel)
    {
        var dbModel = _table.SingleOrDefault(x => x.CollaboratorId == uiModel.CollaboratorId);

        uiModel.ReferralCode =
            uiModel.ReferralCode
                   .Replace("۰", "0")
                   .Replace("۱", "1")
                   .Replace("۲", "2")
                   .Replace("۳", "3")
                   .Replace("۴", "4")
                   .Replace("۵", "5")
                   .Replace("۶", "6")
                   .Replace("۷", "7")
                   .Replace("۸", "8")
                   .Replace("۹", "9");

        //uiModel.InsertDate = dbModel.InsertDate;

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

    public CollaboratorViewModel Get(int id)
    {
        if (id == 0) { return null; }

        var dbModel = _table.FirstOrDefault(x => x.CollaboratorId == id);

        if (dbModel == null)
        {
            return null;
        }

        var uiModel = new CollaboratorViewModel();

        _mapper.Map(dbModel, uiModel);

        return uiModel;
    }

    public CollaboratorViewModel Get(string referralCode)
    {
        if (string.IsNullOrEmpty(referralCode)) { return null; }

        var dbModel = _table.FirstOrDefault(x => x.ReferralCode == referralCode);

        var uiModel = new CollaboratorViewModel();

        _mapper.Map(dbModel, uiModel);

        return uiModel;
    }

    public IList<CollaboratorViewModel> GetAllFiltered(
        string filterReferralCodeName, string filterActive,
        int currentPage, int pageSize, out int totalRecord)
    {
        string whereStr = "ReferralCodeId > 0 ";

        if (!String.IsNullOrEmpty(filterReferralCodeName))
        {
            whereStr += " AND ReferralCodeName.Contains(@0)";
        }

        if (!String.IsNullOrEmpty(filterActive))
        {
            whereStr += " AND Active = " + filterActive;
        }

        //DateTime? insertDateFromMiladi = null;
        //if (!string.IsNullOrEmpty(filterInsertDateFrom))
        //{
        //    filterInsertDateFrom =
        //        filterInsertDateFrom.Replace("۰", "0")
        //            .Replace("۱", "1")
        //            .Replace("۲", "2")
        //            .Replace("۳", "3")
        //            .Replace("۴", "4")
        //            .Replace("۵", "5")
        //            .Replace("۶", "6")
        //            .Replace("۷", "7")
        //            .Replace("۸", "8")
        //            .Replace("۹", "9");
        //    PersianDateTime shamsiInsertDateFrom = PersianDateTime.Parse(filterInsertDateFrom);
        //    insertDateFromMiladi = shamsiInsertDateFrom.ToDateTime();
        //    whereStr += " AND InsertDate >= @2";
        //}

        //DateTime? insertDateToMiladi = null;
        //if (!string.IsNullOrEmpty(filterInsertDateTo))
        //{
        //    filterInsertDateTo =
        //        filterInsertDateTo.Replace("۰", "0")
        //            .Replace("۱", "1")
        //            .Replace("۲", "2")
        //            .Replace("۳", "3")
        //            .Replace("۴", "4")
        //            .Replace("۵", "5")
        //            .Replace("۶", "6")
        //            .Replace("۷", "7")
        //            .Replace("۸", "8")
        //            .Replace("۹", "9");
        //    PersianDateTime shamsiInsertDateTo = PersianDateTime.Parse(filterInsertDateTo);
        //    insertDateToMiladi = shamsiInsertDateTo.ToDateTime();

        //    insertDateToMiladi = insertDateToMiladi.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

        //    whereStr += " AND InsertDate <= @3";
        //}

        var dbModelList = new List<Collaborator>();

        dbModelList = _table.Where(whereStr, filterReferralCodeName).ToList();

        totalRecord = dbModelList.Count();

        dbModelList = dbModelList
                      //.OrderByDescending(x => x.InsertDate)
                      .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        var uiModelList = new List<CollaboratorViewModel>();
        _mapper.Map(dbModelList, uiModelList);

        //PersianCalendar pc = new PersianCalendar();
        //foreach (var uiModelItem in uiModelList)
        //{
        //    uiModelItem.ShamsiInsertDate = pc.GetYear(uiModelItem.InsertDate).ToString("0000/") +
        //        pc.GetMonth(uiModelItem.InsertDate).ToString("00/") +
        //        pc.GetDayOfMonth(uiModelItem.InsertDate).ToString("00");
        //}

        return uiModelList;
    }

    public bool IsDuplicateByReferralCode(int? id, string referralCode)
    {
        if (id != null)
        {
            return _table.Any(x => x.CollaboratorId != (int)id && x.ReferralCode == referralCode);
        }
        else
        {
            return _table.Any(x => x.ReferralCode == referralCode);
        }
    }
}