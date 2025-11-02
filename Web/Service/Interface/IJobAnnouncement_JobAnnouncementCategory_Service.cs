using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncement_JobAnnouncementCategory_Service
    {
        bool Add(int jobAnnouncementId, int jobAnnouncementCategoryId);

        bool Edit(JobAnnouncement_JobAnnouncementCategory_ViewModel uiModel);

        bool Delete(int jobAnnouncementId, int jobAnnouncementCategoryId);

        bool IsDuplicate(int jobAnnouncementId, int jobAnnouncementCategoryId);

        JobAnnouncement_JobAnnouncementCategory_ViewModel Get(int jobAnnouncementId, int jobAnnouncementCategoryId);

        IList<JobAnnouncement_JobAnnouncementCategory_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId);

        IList<JobAnnouncement_JobAnnouncementCategory_ViewModel> GetAllByJobAnnouncementCategoryId(int jobAnnouncementCategoryId);
    }
}
