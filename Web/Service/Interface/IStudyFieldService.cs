using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IStudyFieldService
    {
        int Add(StudyFieldViewModel uiModel);

        bool Edit(StudyFieldViewModel uiModel);

        bool Delete(int id);

        StudyFieldViewModel Get(int id);

        public IList<StudyFieldViewModel> GetAll(bool? active);

        public IList<StudyFieldViewModel> GetAllFiltered(
            string filterStudyFieldName, string filterActive,
            int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByName(int? studyFieldId, string studyFieldName);
    }
}
