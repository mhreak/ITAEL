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
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _uow;
        readonly DbSet<City> _model;

        public CityService(IUnitOfWork uow, IMapper mappingEngine)
        {
            _uow = uow;
            _model = _uow.Set<City>();
            _mapper = mappingEngine;
        }

        public int Add(CityViewModel uiModel)
        {
            var dbModel = new City();
            _mapper.Map(source: uiModel, destination: dbModel);
            _model.Add(dbModel);

            try
            {
                _uow.SaveChanges();

                return dbModel.CityId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _model.SingleOrDefault(x => x.CityId == id);

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
        public bool Edit(CityViewModel uiModel)
        {
            var dbModel = _model.SingleOrDefault(x => x.CityId == uiModel.CityId);
            uiModel.ProvinceId = dbModel.ProvinceId;
            _mapper.Map(uiModel, dbModel);
            _model.Attach(dbModel);
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

        public CityViewModel Get(int id)
        {
            var dbModel = _model.Where(x => x.CityId == id).FirstOrDefault();
            var uiModel = new CityViewModel();
            _mapper.Map(dbModel, uiModel);
            return uiModel;
        }

        public IList<CityViewModel> GetAllByProvinceId(int provinceId)
        {
            var dbModelList = _model.ToList().Where(x => x.ProvinceId == provinceId).OrderBy(x => x.CityName);
            var uiModelList = new List<CityViewModel>();
            _mapper.Map(dbModelList, uiModelList);
            return uiModelList;
        }
    }
}
