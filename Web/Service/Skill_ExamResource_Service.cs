using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using System.Linq;
using DbConnection;
using Web.Service.Interface;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class Skill_ExamResource_Service : ISkill_ExamResource_Service
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<Skill_ExamResource> _table;

        public Skill_ExamResource_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Skill_ExamResource>();
        }

        public bool Add(int examResourceId, int skillId)
        {
            if (!IsDuplicate(examResourceId, skillId))
            {
                var dbModel = new Skill_ExamResource();

                dbModel.ExamResourceId = examResourceId;
                dbModel.SkillId = skillId;
                dbModel.InsertDate = DateTime.Now;

                _table.Add(dbModel);
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
            else
            {
                //return false if record exists in database (duplicate record)
                return false;
            }
        }

        public bool Edit(Skill_ExamResource_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == uiModel.ExamResourceId && x.SkillId == uiModel.SkillId);

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

        public bool Delete(int examResourceId, int skillId)
        {
            if (IsDuplicate(examResourceId, skillId))
            {
                var dbModel = _table.SingleOrDefault(x => x.ExamResourceId == examResourceId && x.SkillId == skillId);

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
            else
            {
                //return true if record not exists in database
                return true;
            }
        }

        public bool IsDuplicate(int examResourceId, int skillId)
        {
            return _table.Any(x => x.ExamResourceId == examResourceId && x.SkillId == skillId);
        }

        public Skill_ExamResource_ViewModel Get(int examResourceId, int skillId)
        {
            var dbModel = _table.Include(x => x.Skill)
                                .Include(x => x.ExamResource)
                                .SingleOrDefault(x => x.ExamResourceId == examResourceId && x.SkillId == skillId);
            var uiModel = new Skill_ExamResource_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<Skill_ExamResource_ViewModel> GetAllByExamResourceId(int examResourceId)
        {
            var dbModelList = _table.Where(x => x.ExamResourceId == examResourceId)
                                    .Include(x => x.Skill)
                                    .Include(x => x.ExamResource)
                                    .ToList();
            var uiModelList = new List<Skill_ExamResource_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Skill_ExamResource_ViewModel> GetAllBySkillId(int skillId)
        {
            var dbModelList = _table.Where(x => x.SkillId == skillId)
                                    .Include(x => x.Skill)
                                    .Include(x => x.ExamResource)
                                    .ToList();
            var uiModelList = new List<Skill_ExamResource_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
