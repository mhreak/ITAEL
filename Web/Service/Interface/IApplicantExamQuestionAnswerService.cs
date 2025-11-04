using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IApplicantExamQuestionAnswerService
    {
        List<int> Add(ApplicantExamQuestionAnswerViewModel uiModel);

        bool Edit(ApplicantExamQuestionAnswerViewModel uiModel);

        bool Delete(int applicantExamAttemptId, int examQuestionId);

        ApplicantExamQuestionAnswerViewModel Get(int applicantExamAttemptId, int examQuestionId);
    }
}
