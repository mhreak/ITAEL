using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Web.Model;
using Web.Service.Interface;
using System.Linq;

namespace Web.Service
{
    public class SettingService : ISettingService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _uow;
        readonly DbSet<Setting> _table;

        public SettingService(IUnitOfWork uow, IMapper mappingEngine)
        {
            _uow = uow;
            _table = _uow.Set<Setting>();
            _mapper = mappingEngine;
        }

        public int Add(SettingViewModel uiModel)
        {
            var dbModel = new Setting();
            _mapper.Map(source: uiModel, destination: dbModel);
            _table.Add(dbModel);

            try
            {
                _uow.SaveChanges();

                return dbModel.SettingId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(SettingViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.SettingId == uiModel.SettingId);
            uiModel.SettingId = dbModel.SettingId;
            _mapper.Map(uiModel, dbModel);
            _table.Attach(dbModel);
            _uow.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _uow.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.SettingId == id);
            _uow.Entry(dbModel).State = EntityState.Deleted;

            try
            {
                _uow.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string Get(string settingKey)
        {
            var dbModel = _table.Where(x => x.SettingKey == settingKey).FirstOrDefault();
            var uiModel = new SettingViewModel();
            _mapper.Map(dbModel, uiModel);
            return uiModel.SettingValue;
        }

        public IList<SettingViewModel> GetAll()
        {
            var dbModelList = _table.ToList();
            var uiModelList = new List<SettingViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        public string GetValueByKey(string settingKey)
        {
            return _table.Where(x => x.SettingKey == settingKey).FirstOrDefault().SettingValue;
        }

        public bool SetSettingValue(string key, string value)
        {
            var dbModel = _table.SingleOrDefault(x => x.SettingKey == key);
            if (dbModel != null)
            {
                dbModel.SettingValue = value;
                _table.Attach(dbModel);
                _uow.Entry(dbModel).State = EntityState.Modified;

                try
                {
                    _uow.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
