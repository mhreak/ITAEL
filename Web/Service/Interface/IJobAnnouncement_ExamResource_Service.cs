using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncement_ExamResource_Service
    {
        bool Add(int jobAnnouncementId, int examResourceId);

        bool Edit(JobAnnouncement_ExamResource_ViewModel uiModel);

        bool Delete(int jobAnnouncementId, int examResourceId);

        bool IsDuplicate(int jobAnnouncementId, int examResourceId);

        JobAnnouncement_ExamResource_ViewModel Get(int jobAnnouncementId, int examResourceId);

        IList<JobAnnouncement_ExamResource_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId);

        IList<JobAnnouncement_ExamResource_ViewModel> GetAllByExamResourceId(int examResourceId);

        IList<JobAnnouncement_ExamResource_ViewModel> GetAllFiltered(string filterJobAnnouncementId,
                                                                     string filterExamResourceId,
                                                                     string filterInsertDateFrom,
                                                                     string filterInsertDate,
                                                                     int currentPage,
                                                                     int pageSize,
                                                                     out int totalRecord);
    }
}
