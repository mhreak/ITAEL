using Web.Model;

namespace Web.Service.Interface
{
    public interface ICollaboratorService
    {


        int Add(CollaboratorViewModel uiModel);

        bool Edit(CollaboratorViewModel uiModel);

        bool Delete(int id);
    }
}
