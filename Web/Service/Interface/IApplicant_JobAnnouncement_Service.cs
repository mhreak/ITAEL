using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IApplicant_JobAnnouncement_Service
    {
        bool Add(int applicantId, int jobAnnouncementId);

        bool Edit(Applicant_JobAnnouncement_ViewModel uiModel);

        bool Delete(int applicantId, int jobAnnouncementId);

        bool IsDuplicate(int applicantId, int jobAnnouncementId);

        Applicant_JobAnnouncement_ViewModel Get(int applicantId, int jobAnnouncementId);

        IList<Applicant_JobAnnouncement_ViewModel> GetAllByApplicantId(int applicantId);

        IList<Applicant_JobAnnouncement_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId);
    }
}
