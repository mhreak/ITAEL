using System.Collections.Generic;

using Web.Model;

namespace Web.Service.Interface
{
    public interface IContactMessageService
    {
        int Add(ContactMessageViewModel uiModel);

        bool Delete(int id);

        ContactMessageViewModel Get(int id);

        List<ContactMessageViewModel> GetAll();

    }
}
