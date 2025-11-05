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
    public class Wallet_Collaborator_CommissionRule_Service : IWallet_Collaborator_CommissionRule_Service
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly DbSet<Wallet_Collaborator_CommissionRule> _table;

        public Wallet_Collaborator_CommissionRule_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Wallet_Collaborator_CommissionRule>();
        }

        public bool Add(Wallet_Collaborator_CommissionRule_ViewModel uiModel)
        {
            if (!IsDuplicate(uiModel.WalletId, uiModel.CollaboratorId, uiModel.CommissionRuleId))
            {
                var dbModel = new Wallet_Collaborator_CommissionRule();

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

        public bool Delete(int walletId, int collaboratorId, int commissionRuleId)
        {
            if (IsDuplicate(walletId, collaboratorId, commissionRuleId))
            {
                var dbModel = _table.SingleOrDefault(x => x.WalletId == walletId && x.CollaboratorId == collaboratorId && 
                                                          x.CommissionRuleId == commissionRuleId);

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

        public bool Edit(Wallet_Collaborator_CommissionRule_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == uiModel.WalletId && x.CollaboratorId == uiModel.CollaboratorId &&
                x.CommissionRuleId == uiModel.CommissionRuleId);

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

        public Wallet_Collaborator_CommissionRule_ViewModel Get(int walletId, int collaboratorId, int commissionRuleId)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == walletId && x.CollaboratorId == collaboratorId &&
                x.CommissionRuleId == commissionRuleId);

            var uiModel = new Wallet_Collaborator_CommissionRule_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<Wallet_Collaborator_CommissionRule_ViewModel> GetAllByCollaboratorId(int collaboratorId)
        {
            var dbModelList = _table.Where(x => x.CollaboratorId == collaboratorId).ToList();
            var uiModelList = new List<Wallet_Collaborator_CommissionRule_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Wallet_Collaborator_CommissionRule_ViewModel> GetAllByWalletId(int walletId)
        {
            var dbModelList = _table.Where(x => x.WalletId == walletId).ToList();
            var uiModelList = new List<Wallet_Collaborator_CommissionRule_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Wallet_Collaborator_CommissionRule_ViewModel> GetAllCommissionRuleId(int commissionRuleId)
        {
            var dbModelList = _table.Where(x => x.CommissionRuleId == commissionRuleId).ToList();
            var uiModelList = new List<Wallet_Collaborator_CommissionRule_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicate(int walletId, int collaboratorId, int commissionRuleId)
        {
            return _table.Any(x => x.WalletId == walletId && x.CollaboratorId == collaboratorId &&
                x.CommissionRuleId == commissionRuleId);
        }
    }
}
