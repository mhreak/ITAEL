using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncementService
    {
        int Add(JobAnnouncementViewModel uiModel);

        bool Edit(JobAnnouncementViewModel uiModel);

        bool Delete(int id);

        JobAnnouncementViewModel Get(int id);

        List<JobAnnouncementViewModel> GetAllLast(int count);

        public IList<JobAnnouncementViewModel> GetAllFiltered(
            string filterTitle, string filterGender, string filterHasEmployementExam,
            string filterPublishDateFrom, string filterPublishDateTo,
            string filterExamDateFrom, string filterExamDateTo,
            string filterCapacityFrom, string filterCapacityTo,
            string filterActive, string filterJobType, string filterJobTime,
            int currentPage, int pageSize, out int totalRecord);
    }
}
