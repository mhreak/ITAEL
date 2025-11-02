using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class SkillService : ISkillService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<Skill> _table;

        public SkillService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Skill>();
        }

        public int Add(SkillViewModel uiModel)
        {
            var dbModel = new Skill();
            _mapper.Map(source: uiModel, destination: dbModel);

            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.SkillId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.SkillId == id);

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

        public bool Edit(SkillViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.SkillId == uiModel.SkillId);

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

        public SkillViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.
                Where(x => x.SkillId == id)
                .FirstOrDefault();

            var uiModel = new SkillViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<SkillViewModel> GetAll(bool? active)
        {
            var dbModelList = new List<Skill>();

            if (active != null)
            {
                dbModelList = _table.Where(x => x.Active == (bool)active).ToList();
            }
            else
            {
                dbModelList = _table.ToList();
            }

            var uiModelList = new List<SkillViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<SkillViewModel> GetAllFiltered(
            string filterSkillName, string filterActive, int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "SkillId > 0 ";

            if (!String.IsNullOrEmpty(filterSkillName))
            {
                whereStr += " AND SkillName.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterActive))
            {
                whereStr += " AND Active = " + filterActive;
            }

            var dbModelList = new List<Skill>();

            dbModelList = _table.Where(whereStr, filterSkillName, filterActive).ToList();

            totalRecord = dbModelList.Count();

            var uiModelList = new List<SkillViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicateByName(int? skillId, string skillName)
        {
            if (skillId != null)
            {
                return _table.Any(x => x.SkillId != (int)skillId && x.SkillName == skillName);
            }
            else
            {
                return _table.Any(x => x.SkillName == skillName);
            }
        }
    }
}
