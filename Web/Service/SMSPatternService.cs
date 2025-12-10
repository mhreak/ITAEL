using AutoMapper;
using DbConnection;
using DbEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.Model;
using Web.Service.Interface;

namespace Web.Service
{
    public class SMSPatternService : ISMSPatternService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _uow;
        readonly DbSet<SMSPattern> _table;

        public SMSPatternService(IUnitOfWork uow, IMapper mappingEngine)
        {
            _uow = uow;
            _table = _uow.Set<SMSPattern>();
            _mapper = mappingEngine;
        }

        public SMSPatternViewModel Get(int id)
        {
            var dbModel = _table.Where(x => x.SMSPatternId == id).FirstOrDefault();

            var uiModel = new SMSPatternViewModel();
            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public SMSPatternViewModel GetByTemplateType(int id)
        {
            var dbModel = _table.FirstOrDefault(x => x.TemplateType == id);

            var uiModel = new SMSPatternViewModel();
            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }
    }
}
