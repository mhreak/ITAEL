using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

using Web.Model;
using Web.Service.Interface;

namespace Shodamad.Service
{
    public class BankGatewayService : IBankGatewayService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<BankGateway> _model;

        public BankGatewayService(IUnitOfWork database, IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _model = _database.Set<BankGateway>();
        }

        public int Add(BankGatewayViewModel uiModel)
        {
            var dbModel = new BankGateway();
            _mapper.Map(source: uiModel, destination: dbModel);

            _model.Add(dbModel);
            try
            {
                _database.SaveChanges();

                return dbModel.BankGatewayId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(BankGatewayViewModel uiModel)
        {
            var dbModel = _model.SingleOrDefault(x => x.BankGatewayId == uiModel.BankGatewayId);
            _mapper.Map(uiModel, dbModel);
            _model.Attach(dbModel);

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

        public BankGatewayViewModel Get(int id)
        {
            var dbModel = _model.SingleOrDefault(x => x.BankGatewayId == id);
            var uiModel = new BankGatewayViewModel();
            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<BankGatewayViewModel> GetAll(bool? active)
        {
            var dbModelList = new List<BankGateway>();
            if (active != null)
            {
                dbModelList = _model.Where(x => x.Active == (bool)active).ToList();
            }
            else
            {
                dbModelList = _model.ToList();
            }
            List<BankGatewayViewModel> uiModelList = new List<BankGatewayViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
