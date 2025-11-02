using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IProvinceService
    {
        int Add(ProvinceViewModel uiModel);

        bool Edit(ProvinceViewModel uiModel);

        IList<ProvinceViewModel> GetAll();

        ProvinceViewModel Get(int id);

        bool IsDuplicate(int id, string name);
    }
}
