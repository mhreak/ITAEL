using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IJobAnnouncement_Skill_Service
    {
        bool Add(int jobAnnouncementId, int skillId);

        bool Edit(JobAnnouncement_Skill_ViewModel uiModel);

        bool Delete(int jobAnnouncementId, int skillId);

        bool IsDuplicate(int jobAnnouncementId, int skillId);

        JobAnnouncement_Skill_ViewModel Get(int jobAnnouncementId, int skillId);

        IList<JobAnnouncement_Skill_ViewModel> GetAllByJobAnnouncementId(int jobAnnouncementId);

        IList<JobAnnouncement_Skill_ViewModel> GetAllBySkillId(int skillId);
    }
}
