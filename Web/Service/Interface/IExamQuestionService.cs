using Web.Model;
using System.Collections.Generic;

namespace Web.Service.Interface
{
    public interface IExamQuestionService
    {
        int Add(ExamQuestionViewModel uiModel);

        bool Edit(ExamQuestionViewModel uiModel);

        bool Delete(int id);

        ExamQuestionViewModel Get(int id);

        public IList<ExamQuestionViewModel> GetAllFiltered(string filterText, string filterExamId, string filterExamTitle,
                                                           string filterType, string filterQuestionOrder,
                                                           int currentPage, int pageSize, out int totalRecord);

        IList<ExamQuestionViewModel> GetAllByExamId(int examId);

    }
}
