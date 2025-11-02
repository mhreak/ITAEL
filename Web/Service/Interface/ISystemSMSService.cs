using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface ISystemSMSService
    {
        int Add(SystemSMSViewModel uiModel);

        bool Edit(SystemSMSViewModel uiModel);

        bool Delete(int id);

        public IList<SystemSMSViewModel> GetAllFiltered(
            string filterMobile, string filterSMSType,
            string filterSendDateFrom, string filterSendDateTo,
            int currentPage, int pageSize, out int totalRecord);

        SystemSMSViewModel Get(int id);
    }
}
