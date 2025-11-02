using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class ProvinceService : IProvinceService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<Province> _table;

        public ProvinceService(IUnitOfWork uow, IMapper mappingEngine)
        {
            _database = uow;
            _table = _database.Set<Province>();
            _mapper = mappingEngine;
        }

        public int Add(ProvinceViewModel uiModel)
        {
            var dbModel = new Province();
            _mapper.Map(source: uiModel, destination: dbModel);
            _table.Add(dbModel);

            try
            {
                _database.SaveChanges();

                return dbModel.ProvinceId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Edit(ProvinceViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ProvinceId == uiModel.ProvinceId);
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

        public ProvinceViewModel Get(int id)
        {
            var dbModel = _table.Where(x => x.ProvinceId == id).FirstOrDefault();
            var uiModel = new ProvinceViewModel();
            _mapper.Map(dbModel, uiModel);
            return uiModel;
        }

        public IList<ProvinceViewModel> GetAll()
        {
            var dbModelList = _table
                .Include(x => x.CityList)
                .ToList()
                .OrderBy(x => x.ProvinceName);
            var uiModelList = new List<ProvinceViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }

        public bool IsDuplicate(int id, string name)
        {
            if (id != 0)
            {
                return _table.Any(x => x.ProvinceId != id && x.ProvinceName == name);
            }
            else
            {
                return _table.Any(x => x.ProvinceName == name);
            }
        }
    }
}
