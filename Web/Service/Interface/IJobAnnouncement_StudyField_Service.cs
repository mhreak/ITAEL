using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncement_StudyField_Service
    {
        bool Add(int jobAnnouncementId, int studyFieldId);

        bool Edit(JobAnnouncement_StudyField_ViewModel uiModel);

        bool Delete(int jobAnnouncementId, int studyFieldId);

        bool IsDuplicate(int jobAnnouncementId, int studyFieldId);

        JobAnnouncement_StudyField_ViewModel Get(int jobAnnouncementId, int studyFieldId);

        IList<JobAnnouncement_StudyField_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId);

        IList<JobAnnouncement_StudyField_ViewModel> GetAllByStudyFieldId(int studyFieldId);
    }
}
