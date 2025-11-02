using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Areas.Admin.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class StudyFieldService : IStudyFieldService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<StudyField> _table;

        public StudyFieldService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<StudyField>();
        }

        public int Add(StudyFieldViewModel uiModel)
        {
            var dbModel = new StudyField();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.StudyFieldId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.StudyFieldId == id);

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

        public bool Edit(StudyFieldViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.StudyFieldId == uiModel.StudyFieldId);

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

        public StudyFieldViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.
                Where(x => x.StudyFieldId == id)
                .FirstOrDefault();

            var uiModel = new StudyFieldViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<StudyFieldViewModel> GetAll(bool? active)
        {
            var dbModelList = new List<StudyField>();

            if(active != null)
            {
                dbModelList = _table.Where(x => x.Active == (bool)active).ToList();
            }
            else
            {
                dbModelList = _table.ToList();
            }

            var uiModelList = new List<StudyFieldViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<StudyFieldViewModel> GetAllFiltered(
            string filterStudyFieldName, string filterActive,
            int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "StudyFieldId > 0 ";

            if (!String.IsNullOrEmpty(filterStudyFieldName))
            {
                whereStr += " AND StudyFieldName.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterActive))
            {
                whereStr += " AND Active = " + filterActive;
            }

            var dbModelList = new List<StudyField>();

            dbModelList = _table.Where(whereStr, filterStudyFieldName, filterActive).ToList();

            totalRecord = dbModelList.Count();

            var uiModelList = new List<StudyFieldViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicateByName(int? studyFieldId, string studyFieldName)
        {
            if (studyFieldId != null)
            {
                return _table.Any(x => x.StudyFieldId != (int)studyFieldId && x.StudyFieldName == studyFieldName);
            }
            else
            {
                return _table.Any(x => x.StudyFieldName == studyFieldName);
            }
        }
    }
}
