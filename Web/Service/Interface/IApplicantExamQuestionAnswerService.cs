using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IApplicantExamQuestionAnswerService
    {
        List<int> Add(ApplicantExamQuestionAnswerViewModel uiModel);

        bool Edit(ApplicantExamQuestionAnswerViewModel uiModel);

        bool Delete(int applicantExamAttemptId, int examQuestionId);

        bool CalculateGradeAfterExam(int examId, int applicantId);

        ApplicantExamQuestionAnswerViewModel Get(int applicantExamAttemptId, int examQuestionId);

        List<ApplicantExamQuestionAnswerViewModel> GetAllFiltered(string filterApplicantExamAttemptId,
                                                                  string filterInsertDateFrom,
                                                                  string filterInsertDateTo,
                                                                  int currentPage,
                                                                  int pageSize,
                                                                  out int totalRecord);
    }
}
