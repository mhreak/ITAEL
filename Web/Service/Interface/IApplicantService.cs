using System.Collections.Generic;
using Web.Model;

namespace Web.Service.Interface
{
    public interface IApplicantService
    {
        int Add(ApplicantViewModel uiModel);

        bool Edit(ApplicantViewModel uiModel);

        bool Delete(int id);

        ApplicantViewModel Get(int id);

        public IList<ApplicantViewModel> GetAllFiltered(
            string filterFirstName, string filterLastName,
            string filterFullName, string filterGender,
            string filterNationalCode, string filterMobile,
            string filterBirthDateFrom, string filterBirthDateTo,
            string filterProvinceId, string filterCityId, string filterStudyFieldId,
            string filterInsertDateFrom, string filterInsertDateTo,
            int currentPage, int pageSize, out int totalRecord);

        bool IsDuplicateByMobile(int? applicantId, string mobile);

        bool IsDuplicateByNationalCode(int? applicantId, string nationalCode);

        bool SetPersonalImageFileName(int applicantId, string fileName);

        bool SetNationalCardFrontFileName(int applicantId, string fileName);

        bool SetNationalCardBackFileName(int applicantId, string fileName);

        bool SetIdentityCertificateFirstPageFileName(int applicantId, string fileName);

        bool SetIdentityCertificateSecondPageFileName(int applicantId, string fileName);

        bool SetEducationalCertificateFileName(int applicantId, string fileName);
    }
}
