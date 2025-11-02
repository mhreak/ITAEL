using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncementCategoryService
    {
        int Add(JobAnnouncementCategoryViewModel uiModel);

        bool Edit(JobAnnouncementCategoryViewModel uiModel);

        bool Delete(int id);

        JobAnnouncementCategoryViewModel Get(int id);

        public IList<JobAnnouncementCategoryViewModel> GetAll(bool? active);

        public IList<JobAnnouncementCategoryViewModel> GetAllFiltered(
            string filterCategoryName, string filterActive,
            int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByName(int? categoryId, string categoryName);
    }
}
