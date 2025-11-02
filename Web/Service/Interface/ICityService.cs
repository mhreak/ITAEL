using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface ICityService
    {
        int Add(CityViewModel uiModel);

        bool Edit(CityViewModel uiModel);

        bool Delete(int id);

        IList<CityViewModel> GetAllByProvinceId(int provinceId);

        CityViewModel Get(int id);
    }
}
