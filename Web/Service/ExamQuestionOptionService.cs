using System;
using Web.Model;
using DbEntities;
using AutoMapper;
using System.Linq;
using DbConnection;
using Web.Service.Interface;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Web.Service;

public class ExamQuestionOptionService : IExamQuestionOptionService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _database;
    private readonly DbSet<ExamQuestionOption> _table;
    private readonly IExamQuestionService _examQuestionService;

    public ExamQuestionOptionService(IUnitOfWork database,
                                     IMapper mappingEngine,
                                     IExamQuestionService examQuestionService)
    {
        _database = database;
        _mapper = mappingEngine;
        _examQuestionService = examQuestionService;
        _table = _database.Set<ExamQuestionOption>();
    }

    public int Add(ExamQuestionOptionViewModel uiModel)
    {
        var dbModel = new ExamQuestionOption();
        _mapper.Map(source: uiModel, destination: dbModel);

        _table.Add(dbModel);

        try
        {
            _database.SaveChanges();

            return dbModel.ExamQuestionOptionId;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public bool Edit(ExamQuestionOptionViewModel uiModel)
    {
        var dbModel = _table.SingleOrDefault(x => x.ExamQuestionOptionId == uiModel.ExamQuestionOptionId);

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
        var dbModel = _table.SingleOrDefault(x => x.ExamQuestionOptionId == id);

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

    public ExamQuestionOptionViewModel GetCorrectAnswerByExamQuestionId(int examQuestionId)
    {
        var examQuestionViewModel = _examQuestionService.Get(examQuestionId);

        if (examQuestionViewModel == null)
        {
            return null;
        }

        var dbModel = _table.FirstOrDefault(x => x.ExamQuestionId == examQuestionViewModel.ExamQuestionId && x.IsCorrectAnswer);

        if (dbModel == null)
        {
            return null;
        }

        var uiModel = new ExamQuestionOptionViewModel();
        _mapper.Map(dbModel, uiModel);

        return uiModel;
    }

    public ExamQuestionOptionViewModel Get(int id)
    {
        if (id == 0)
        {
            return null;
        }

        var dbModel = _table.Include(x => x.ExamQuestion).FirstOrDefault(x => x.ExamQuestionOptionId == id);

        if (dbModel == null)
        {
            return null;
        }

        var uiModel = new ExamQuestionOptionViewModel();

        _mapper.Map(dbModel, uiModel);

        return uiModel;
    }

    public List<ExamQuestionOptionViewModel> GetAllByExamQuestionId(int examQuestionId)
    {
        if (examQuestionId == 0)
        {
            return null;
        }

        var dbModel = _table.Include(x => x.ExamQuestion).Where(x => x.ExamQuestionId == examQuestionId).ToList();

        if (dbModel == null)
        {
            return null;
        }

        var uiModel = new List<ExamQuestionOptionViewModel>();

        _mapper.Map(dbModel, uiModel);

        return uiModel;
    }

    public IList<ExamQuestionOptionViewModel> GetAllFiltered(string filterTitle,
                                                             string filterExamQuestionId,
                                                             string filterOrder,
                                                             string filterIsCorrectAnswer,
                                                             int currentPage,
                                                             int pageSize,
                                                             out int totalRecord)
    {
        string whereStr = "ExamQuestionOptionId > 0 ";

        if (!string.IsNullOrEmpty(filterTitle))
        {
            whereStr += " AND Title.Contains(@0)";
        }

        if (!string.IsNullOrEmpty(filterExamQuestionId))
        {
            whereStr += " AND ExamQuestionId = " + filterExamQuestionId;
        }

        if (!string.IsNullOrEmpty(filterOrder))
        {
            whereStr += " AND Order = " + filterOrder;
        }

        if (!string.IsNullOrEmpty(filterIsCorrectAnswer))
        {
            whereStr += " AND IsCorrectAnswer = " + filterIsCorrectAnswer;
        }

        var dbModelList = new List<ExamQuestionOption>();

        dbModelList = _table.Where(whereStr, filterTitle).Include(x => x.ExamQuestion).ToList();

        totalRecord = dbModelList.Count();

        dbModelList = dbModelList
                      .OrderByDescending(x => x.Order)
                      .Skip((currentPage - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();

        var uiModelList = new List<ExamQuestionOptionViewModel>();
        _mapper.Map(dbModelList, uiModelList);

        return uiModelList;
    }
}