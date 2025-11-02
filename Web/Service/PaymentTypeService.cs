using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class PaymentTypeService : IPaymentTypeService
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _database;
        readonly DbSet<PaymentType> _table;

        public PaymentTypeService(
            IUnitOfWork database,
            IMapper mappingEngine)
        {
            _database = database;
            _mapper = mappingEngine;
            _table = _database.Set<PaymentType>();
        }

        public PaymentTypeViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table.
                Where(x => x.PaymentTypeId == id)
                .FirstOrDefault();

            var uiModel = new PaymentTypeViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<PaymentTypeViewModel> GetAll()
        {
            var dbModelList = _table.ToList();
            var uiModelList = new List<PaymentTypeViewModel>();

            _mapper.Map(dbModelList, uiModelList);

            return uiModelList;
        }
    }
}
