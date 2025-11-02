using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface ISkillService
    {
        int Add(SkillViewModel uiModel);

        bool Edit(SkillViewModel uiModel);

        bool Delete(int id);

        SkillViewModel Get(int id);

        public IList<SkillViewModel> GetAll(bool? active);

        public IList<SkillViewModel> GetAllFiltered(
            string filterSkillName, string filterActive,
            int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByName(int? skillId, string skillName);
    }
}
