using Web.Model;
using System.Collections.Generic;

namespace Web.Service.Interface
{
    public interface ISkill_ExamResource_Service
    {
        bool Add(int examResourceId, int skillId);

        bool Edit(Skill_ExamResource_ViewModel uiModel);

        bool Delete(int examResourceId, int skillId);

        bool IsDuplicate(int examResourceId, int skillId);

        Skill_ExamResource_ViewModel Get(int examResourceId, int skillId);

        IList<Skill_ExamResource_ViewModel> GetAllByExamResourceId(int examResourceId);

        IList<Skill_ExamResource_ViewModel> GetAllBySkillId(int skillId);
    }
}
