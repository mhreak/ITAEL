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
    public class Wallet_ReferralCode_CommissionRule_Service : IWallet_ReferralCode_CommissionRule_Service
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<Wallet_Collaborator_CommissionRule> _table;

        public Wallet_ReferralCode_CommissionRule_Service(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<Wallet_Collaborator_CommissionRule>();
        }

        public bool Add(Wallet_ReferralCode_CommissionRule_ViewModel uiModel)
        {
            if (!IsDuplicate(uiModel.WalletId, uiModel.ReferralCodeId, uiModel.CommissionRuleId))
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

        public bool Delete(int walletId, int referralCodeId, int commissionRuleId)
        {
            if (IsDuplicate(walletId, referralCodeId, commissionRuleId))
            {
                var dbModel = _table.SingleOrDefault(x => x.WalletId == walletId && x.ReferralCodeId == referralCodeId && x.CommissionRuleId == commissionRuleId);

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

        public bool Edit(Wallet_ReferralCode_CommissionRule_ViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == uiModel.WalletId && x.ReferralCodeId == uiModel.ReferralCodeId &&
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

        public Wallet_ReferralCode_CommissionRule_ViewModel Get(int walletId, int referralCodeId, int commissionRuleId)
        {
            var dbModel = _table.SingleOrDefault(x => x.WalletId == walletId && x.ReferralCodeId == referralCodeId &&
                x.CommissionRuleId == commissionRuleId);

            var uiModel = new Wallet_ReferralCode_CommissionRule_ViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<Wallet_ReferralCode_CommissionRule_ViewModel> GetAllByReferralCodeId(int referralCodeId)
        {
            var dbModelList = _table.Where(x => x.ReferralCodeId == referralCodeId).ToList();
            var uiModelList = new List<Wallet_ReferralCode_CommissionRule_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Wallet_ReferralCode_CommissionRule_ViewModel> GetAllByWalletId(int walletId)
        {
            var dbModelList = _table.Where(x => x.WalletId == walletId).ToList();
            var uiModelList = new List<Wallet_ReferralCode_CommissionRule_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public IList<Wallet_ReferralCode_CommissionRule_ViewModel> GetAllCommissionRuleId(int commissionRuleId)
        {
            var dbModelList = _table.Where(x => x.CommissionRuleId == commissionRuleId).ToList();
            var uiModelList = new List<Wallet_ReferralCode_CommissionRule_ViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }

        public bool IsDuplicate(int walletId, int referralCodeId, int commissionRuleId)
        {
            return _table.Any(x => x.WalletId == walletId && x.ReferralCodeId == referralCodeId &&
                x.CommissionRuleId == commissionRuleId);
        }
    }
}
