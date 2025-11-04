using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IApplicantExamAttemptService
    {
        int Add(ApplicantExamAttemptViewModel uiModel);

        bool Edit(ApplicantExamAttemptViewModel uiModel);

        bool Delete(int id);

        ApplicantExamAttemptViewModel Get(int id);

        public IList<ApplicantExamAttemptViewModel> GetAllFiltered(
            string filterApplicantId, string filterExamId,
            string filterApplicantFullName, string filterExamTitle,
            string filterShamsiStartTime, string filterShamsiEndTime,
            string filterFinalScore, string filterStatus,
            int currentPage, int pageSize, out int totalRecord);
    }
}
