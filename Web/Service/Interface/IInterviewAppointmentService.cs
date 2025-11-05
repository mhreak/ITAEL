using DbEntities;
using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IInterviewAppointmentService
    {
        int Add(InterviewAppointmentViewModel uiModel);

        bool Edit(InterviewAppointmentViewModel uiModel);

        bool Delete(int id);

        InterviewAppointmentViewModel Get(int id);

        IList<InterviewAppointmentViewModel> GetAllFiltered(string filterJobAnnouncementId,
                                                            string filterApplicantId,
                                                            string filterStatus,
                                                            string filterInsertDateFrom,
                                                            string filterInsertDateTo,
                                                            int currentPage,
                                                            int pageSize,
                                                            out int totalRecord);
    }
}
