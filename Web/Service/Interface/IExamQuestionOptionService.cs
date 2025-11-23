using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IExamQuestionOptionService
    {
        int Add(ExamQuestionOptionViewModel uiModel);

        bool Edit(ExamQuestionOptionViewModel uiModel);

        bool Delete(int id);

        ExamQuestionOptionViewModel GetCorrectAnswerByExamQuestionId(int examQuestionId);

        ExamQuestionOptionViewModel Get(int id);

        public IList<ExamQuestionOptionViewModel> GetAllFiltered(string filterTitle,string filterExamQuestionId,
                                                                 string filterOrder, string filterIsCorrectAnswer,
                                                                 int currentPage, int pageSize, out int totalRecord);
    }
}
