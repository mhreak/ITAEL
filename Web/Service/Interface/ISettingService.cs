using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface ISettingService
    {
        int Add(SettingViewModel uiModel);
        bool Edit(SettingViewModel uiModel);
        bool SetSettingValue(string key, string value);
        bool Delete(int id);
        IList<SettingViewModel> GetAll();
        string Get(string settingKey);
        string GetValueByKey(string settingKey);
    }
}
