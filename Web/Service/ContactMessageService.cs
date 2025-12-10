using System;
using Web.Model;
using DbEntities;
using AutoMapper;
using System.Linq;
using DbConnection;
using Web.Service.Interface;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _uow;
        readonly DbSet<ContactMessage> _model;

        public ContactMessageService(IUnitOfWork uow, IMapper mappingEngine)
        {
            _uow = uow;
            _model = _uow.Set<ContactMessage>();
            _mapper = mappingEngine;
        }

        public int Add(ContactMessageViewModel uiModel)
        {
            var dbModel = new ContactMessage();
            _mapper.Map(source: uiModel, destination: dbModel);
            _model.Add(dbModel);

            try
            {
                _uow.SaveChanges();

                return dbModel.ContactMessageId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _model.SingleOrDefault(x => x.ContactMessageId == id);

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

        public ContactMessageViewModel Get(int id)
        {
            var dbModel = _model.FirstOrDefault(x => x.ContactMessageId == id);

            if (dbModel == null)
            {
                return null;
            }

            var uiModel = new ContactMessageViewModel();
            _mapper.Map(dbModel, uiModel);
            return uiModel;
        }

        public List<ContactMessageViewModel> GetAll()
        {
            var dbModel = _model.ToList();
            var uiModel = new List<ContactMessageViewModel>();
            _mapper.Map(dbModel, uiModel);
            return uiModel;
        }

    }
}
