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
    public class ApplicantExamQuestionAnswerService : IApplicantExamQuestionAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<ApplicantExamQuestionAnswer> _table;

        public ApplicantExamQuestionAnswerService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<ApplicantExamQuestionAnswer>();
        }
        public List<int> Add(ApplicantExamQuestionAnswerViewModel uiModel)
        {
            var dbModel = new ApplicantExamQuestionAnswer();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.InsertDate = DateTime.Now;
            var idList = new List<int>();
            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();
                idList.Add(dbModel.ApplicantExamAttemptId);
                idList.Add(dbModel.ExamQuestionId);
                return idList;
            }
            catch (Exception)
            {
                idList.Add(-1);
                return idList;
            }
        }

        public bool Delete(int applicantExamAttemptId, int examQuestionId)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantExamAttemptId == applicantExamAttemptId 
                                                      && x.ExamQuestionId == examQuestionId);

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

        public bool Edit(ApplicantExamQuestionAnswerViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantExamAttemptId == uiModel.ApplicantExamAttemptId 
                                                      && x.ExamQuestionId == uiModel.ExamQuestionId);

            uiModel.InsertDate = dbModel.InsertDate;

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

        public ApplicantExamQuestionAnswerViewModel Get(int applicantExamAttemptId, int examQuestionId)
        {
            if (applicantExamAttemptId == 0 || examQuestionId == 0) { return null; }

            var dbModel = _table.FirstOrDefault(x => x.ApplicantExamAttemptId == applicantExamAttemptId
                                                     && x.ExamQuestionId == examQuestionId);

            var uiModel = new ApplicantExamQuestionAnswerViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }
    }
}

