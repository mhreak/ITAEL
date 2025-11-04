using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IExamService
    {

        int Add(ExamViewModel uiModel);

        bool Edit(ExamViewModel uiModel);

        bool Delete(int id);

        ExamViewModel Get(int id);

        public IList<ExamViewModel> GetAllFiltered(
            string filterTitle, string filterDescription,
            string filterShamsiStartTime, string filterShamsiEndTime,
            string filterDurationMinutes, string filterRandomizeQuestions,
            string filterRandomizeOptions, string filterAllowNavigateToPreviousQuestion,
            int currentPage, int pageSize, out int totalRecord);
    }
}
