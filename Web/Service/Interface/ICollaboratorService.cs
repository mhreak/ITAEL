using Web.Model;
using System.Collections.Generic;

namespace Web.Service.Interface
{
    public interface ICollaboratorService
    {
        int Add(CollaboratorViewModel uiModel);

        bool Edit(CollaboratorViewModel uiModel);

        bool Delete(int id);

        CollaboratorViewModel Get(int id);

        CollaboratorViewModel Get(string referralCode);

        public IList<CollaboratorViewModel> GetAllFiltered(
            string filterReferralCodeName, string filterActive,
           int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByReferralCode(int? id, string referralCode);
    }
}
