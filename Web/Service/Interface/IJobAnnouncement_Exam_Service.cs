using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncement_Exam_Service
    {
        bool Add(JobAnnouncement_Exam_ViewModel uiModel);

        bool Edit(JobAnnouncement_Exam_ViewModel uiModel);

        bool Delete(int jobAnnouncementId, int examId);

        bool IsDuplicate(int jobAnnouncementId, int examId);

        JobAnnouncement_Exam_ViewModel Get(int jobAnnouncementId, int examId);

        IList<JobAnnouncement_Exam_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId);

        IList<JobAnnouncement_Exam_ViewModel> GetAllByExamId(int examId);

        IList<JobAnnouncement_Exam_ViewModel> GetAllFiltered(string filterJobAnnouncementId,
                                                             string filterExamId,
                                                             string filterExamTitle,
                                                             string filterStartTimeFrom,
                                                             string filterStartTimeTo,
                                                             string filterEndTimeFrom,
                                                             string filterEndTimeTo,
                                                             string filterInsertDateFrom,
                                                             string filterInsertDateTo,
                                                             string filterDurationMinutes,
                                                             string filterRandomizeQuestions,
                                                             string filterRandomizeOptions,
                                                             string filterAllowNavigateToPreviousQuestion,
                                                             int currentPage,
                                                             int pageSize,
                                                             out int totalRecord);
    }
}
