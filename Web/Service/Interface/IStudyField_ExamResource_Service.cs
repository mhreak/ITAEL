using Web.Model;
using System.Collections.Generic;

namespace Web.Service.Interface
{
    public interface IStudyField_ExamResource_Service
    {
        bool Add(int examResourceId, int studyFieldId);

        bool Edit(StudyField_ExamResource_ViewModel uiModel);

        bool Delete(int examResourceId, int studyFieldId);

        bool IsDuplicate(int examResourceId, int studyFieldId);

        StudyField_ExamResource_ViewModel Get(int examResourceId, int studyFieldId);

        IList<StudyField_ExamResource_ViewModel> GetAllByExamResourceId(int examResourceId);

        IList<StudyField_ExamResource_ViewModel> GetAllByStudyFieldId(int studyFieldId);

        IList<StudyField_ExamResource_ViewModel> GetAllFiltered(string filterStudyFieldId,
                                                                string filterExamResourceId,
                                                                string filterInsertDateFrom,
                                                                string filterInsertDateTo,
                                                                int currentPage,
                                                                int pageSize,
                                                                out int totalRecord);
    }
}
