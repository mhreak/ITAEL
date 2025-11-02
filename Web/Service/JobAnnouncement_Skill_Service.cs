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
    public class JobAnnouncement_Skill_Service : IJobAnnouncement_Skill_Service
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<JobAnnouncement_Skill> _table;

        public JobAnnouncement_Skill_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<JobAnnouncement_Skill>();
        }

        public bool Add(int jobAnnouncementId, int skillId)
        {
            if (!IsDuplicate(jobAnnouncementId, skillId))
            {
                var dbModel = new JobAnnouncement_Skill();

                dbModel.JobAnnouncementId = jobAnnouncementId;
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

        public bool Delete(int jobAnnouncementId, int skillId)
        {
            if (IsDuplicate(jobAnnouncementId, skillId))
            {
                var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.SkillId == skillId);

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

        public bool Edit(JobAnnouncement_Skill_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == uiModel.JobAnnouncementId && x.SkillId == uiModel.SkillId);

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

        public JobAnnouncement_Skill_ViewModel Get(int jobAnnouncementId, int skillId)
        {
            var dbModel = _table.SingleOrDefault(x => x.JobAnnouncementId == jobAnnouncementId && x.SkillId == skillId);
            var uiModel = new JobAnnouncement_Skill_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<JobAnnouncement_Skill_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId)
        {
            var dbModelList = _table.Where(x => x.JobAnnouncementId == jobAnnouncementId)
                .Include(x => x.Skill)
                .ToList();
            var uiModelList = new List<JobAnnouncement_Skill_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<JobAnnouncement_Skill_ViewModel> GetAllBySkillId(int skillId)
        {
            var dbModelList = _table.Where(x => x.SkillId == skillId).ToList();
            var uiModelList = new List<JobAnnouncement_Skill_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicate(int jobAnnouncementId, int skillId)
        {
            return _table.Any(x => x.JobAnnouncementId == jobAnnouncementId && x.SkillId == skillId);
        }
    }
}
