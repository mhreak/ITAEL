using AutoMapper;
using DbConnection;
using DbEntities;
using MD.PersianDateTime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using Web.Model;
using Web.Model.Identity;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Service
{
    public class ApplicantService : IApplicantService
    {
        readonly IMapper _mapper;
        private readonly ICityService _cityService;
        readonly IUnitOfWork _database;
        readonly DbSet<Applicant> _table;
        readonly IUserRoleService _userRoleService;
        readonly IApplicationUserManagerService _userManagerService;
        readonly IApplicant_JobAnnouncement_Service _applicant_JobAnnouncement_Service;

        public ApplicantService(
            IUnitOfWork database,
            IMapper mappingEngine,
            ICityService cityService,
            IUserRoleService userRoleService,
            IApplicationUserManagerService userManagerService,
            IApplicant_JobAnnouncement_Service applicant_JobAnnouncement_Service)
        {
            _database = database;
            _mapper = mappingEngine;
            _cityService = cityService;
            _userRoleService = userRoleService;
            _table = _database.Set<Applicant>();
            _userManagerService = userManagerService;
            _applicant_JobAnnouncement_Service = applicant_JobAnnouncement_Service;
        }

        public int Add(ApplicantViewModel uiModel, string password)
        {
            var dbModel = new Applicant();
            _mapper.Map(source: uiModel, destination: dbModel);

            dbModel.InsertDate = DateTime.Now;
            dbModel.PersonalImageFileName = null;
            dbModel.NationalCardFrontFileName = null;
            dbModel.NationalCardBackFileName = null;
            dbModel.IdentityCertificateFirstPageFileName = null;
            dbModel.IdentityCertificateSecondPageFileName = null;
            dbModel.EducationalCertificateFileName = null;


            _table.Add(dbModel);
            try
            {
                _database.SaveChanges();
                //var user = _userManagerService.GetUserByApplicantId(dbModel.ApplicantId);

                //if (user == null)
                //{
                //    var applicationUserViewModel = new ApplicationUserViewModel()
                //                                   {
                //                                       Name = dbModel.FirstName + " " + dbModel.LastName,
                //                                       PhoneNumber = dbModel.Mobile,
                //                                       ApplicantId = dbModel.ApplicantId,
                //                                       AccountState = 1,
                //                                       CompanyName = "",
                //                                       RoleName = "",
                //                                       UserName = ""
                //                                   };
                //    int userId = _userManagerService.Add(applicationUserViewModel, password, "Applicant");

                //    if (userId == -1)
                //    {
                //        return -1;
                //    }
                //}

                return dbModel.ApplicantId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == id);

            _database.Entry(dbModel).State = EntityState.Deleted;

            try
            {
                _database.SaveChanges();

                var applicant_jobAnnouncement_list = _applicant_JobAnnouncement_Service.GetAllByApplicantId(id);
                if (applicant_jobAnnouncement_list != null && applicant_jobAnnouncement_list.Any())
                {
                    foreach (var a_ja in applicant_jobAnnouncement_list)
                    {
                        _applicant_JobAnnouncement_Service.Delete(id, a_ja.JobAnnouncementId);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Edit(ApplicantViewModel uiModel)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == uiModel.ApplicantId);

            uiModel.InsertDate = dbModel.InsertDate;
            uiModel.PersonalImageFileName = dbModel.PersonalImageFileName;
            uiModel.NationalCardFrontFileName = dbModel.NationalCardFrontFileName;
            uiModel.NationalCardBackFileName = dbModel.NationalCardBackFileName;
            uiModel.IdentityCertificateFirstPageFileName = dbModel.IdentityCertificateFirstPageFileName;
            uiModel.IdentityCertificateSecondPageFileName = dbModel.IdentityCertificateSecondPageFileName;
            uiModel.EducationalCertificateFileName = dbModel.EducationalCertificateFileName;

            _mapper.Map(uiModel, dbModel);
            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ApplicantViewModel Get(int id)
        {
            if (id == 0) { return null; }

            var dbModel = _table
                          .Include(x => x.City)
                          .Include(x => x.StudyField)
                          .FirstOrDefault(x => x.ApplicantId == id);

            var uiModel = new ApplicantViewModel();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ApplicantViewModel> GetAll()
        {

            var dbModel = _table.ToList();

            var uiModel = new List<ApplicantViewModel>();

            _mapper.Map(dbModel, uiModel);

            return uiModel;
        }

        public IList<ApplicantViewModel> GetAllFiltered(
            string filterFirstName, string filterLastName,
            string filterFullName, string filterGender, string filterNationalCode,
            string filterMobile, string filterBirthDateFrom, string filterBirthDateTo,
            string filterProvinceId, string filterCityId, string filterStudyFieldId,
            string filterInsertDateFrom, string filterInsertDateTo, int currentPage, int pageSize, out int totalRecord)
        {
            string whereStr = "ApplicantId > 0 ";

            if (!String.IsNullOrEmpty(filterFirstName))
            {
                whereStr += " AND FirstName.Contains(@0)";
            }

            if (!String.IsNullOrEmpty(filterLastName))
            {
                whereStr += " AND LastName.Contains(@1)";
            }

            if (!String.IsNullOrEmpty(filterFullName))
            {
                whereStr += " AND (FirstName + \" \" + LastName).Contains(@2)";
            }

            if (!String.IsNullOrEmpty(filterGender))
            {
                whereStr += " AND Gender = " + filterGender.ToLower();
            }

            if (!String.IsNullOrEmpty(filterNationalCode))
            {
                whereStr += " AND NationalCode.Contains(@3)";
            }

            if (!String.IsNullOrEmpty(filterMobile))
            {
                whereStr += " AND Mobile.Contains(@4)";
            }

            DateTime? birthDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterBirthDateFrom))
            {
                filterBirthDateFrom =
                    filterBirthDateFrom.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiBirthDateFrom = PersianDateTime.Parse(filterBirthDateFrom);
                birthDateFromMiladi = shamsiBirthDateFrom.ToDateTime();
                whereStr += " AND BirthDate != null AND BirthDate >= @5";
            }

            DateTime? birthDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterBirthDateTo))
            {
                filterBirthDateTo =
                    filterBirthDateTo.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiBirthDateTo = PersianDateTime.Parse(filterBirthDateTo);
                birthDateToMiladi = shamsiBirthDateTo.ToDateTime();
                whereStr += " AND BirthDate != null AND BirthDate <= @6";
            }

            DateTime? insertDateFromMiladi = null;
            if (!string.IsNullOrEmpty(filterInsertDateFrom))
            {
                filterInsertDateFrom =
                    filterInsertDateFrom.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiInsertDateFrom = PersianDateTime.Parse(filterInsertDateFrom);
                insertDateFromMiladi = shamsiInsertDateFrom.ToDateTime();
                whereStr += " AND InsertDate >= @7";
            }

            DateTime? insertDateToMiladi = null;
            if (!string.IsNullOrEmpty(filterInsertDateTo))
            {
                filterInsertDateTo =
                    filterInsertDateTo.Replace("۰", "0")
                        .Replace("۱", "1")
                        .Replace("۲", "2")
                        .Replace("۳", "3")
                        .Replace("۴", "4")
                        .Replace("۵", "5")
                        .Replace("۶", "6")
                        .Replace("۷", "7")
                        .Replace("۸", "8")
                        .Replace("۹", "9");
                PersianDateTime shamsiInsertDateTo = PersianDateTime.Parse(filterInsertDateTo);
                insertDateToMiladi = shamsiInsertDateTo.ToDateTime();
                whereStr += " AND InsertDate <= @8";
            }

            if (!String.IsNullOrEmpty(filterProvinceId))
            {
                whereStr += " AND City.ProvinceId = " + filterProvinceId;
            }

            if (!String.IsNullOrEmpty(filterCityId))
            {
                whereStr += " AND CityId = " + filterCityId;
            }

            if (!String.IsNullOrEmpty(filterStudyFieldId))
            {
                whereStr += " AND StudyFieldId = " + filterStudyFieldId;
            }

            var dbModelList = new List<Applicant>();

            dbModelList = _table.Include(x => x.City).Where(whereStr, filterFirstName, filterLastName, filterFullName, filterGender,
                filterNationalCode, filterMobile, birthDateFromMiladi, birthDateToMiladi,
                insertDateFromMiladi, insertDateToMiladi).ToList();

            totalRecord = dbModelList.Count();

            dbModelList = dbModelList
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var uiModelList = new List<ApplicantViewModel>();
            _mapper.Map(dbModelList, uiModelList);

            foreach (var uiModel in uiModelList)
            {
                if (uiModel.CityId is not null)
                {
                    var cityViewModel = _cityService.Get(uiModel.CityId.Value);
                    uiModel.ProvinceId = cityViewModel.ProvinceId;
                    uiModel.ProvinceName = cityViewModel.ProvinceName;
                }
                
            }

            return uiModelList;
        }

        public bool IsDuplicateByMobile(int? applicantId, string mobile)
        {
            if (applicantId != null)
            {
                return _table.Any(x => x.ApplicantId != (int)applicantId && x.Mobile == mobile);
            }
            else
            {
                return _table.Any(x => x.Mobile == mobile);
            }
        }

        public bool IsDuplicateByNationalCode(int? applicantId, string nationalCode)
        {
            if (applicantId != null)
            {
                return _table.Any(x => x.ApplicantId != (int)applicantId && x.NationalCode == nationalCode);
            }
            else
            {
                return _table.Any(x => x.NationalCode == nationalCode);
            }
        }

        public bool SetEducationalCertificateFileName(int applicantId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId);

            dbModel.EducationalCertificateFileName = fileName;

            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetIdentityCertificateFirstPageFileName(int applicantId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId);

            dbModel.IdentityCertificateFirstPageFileName = fileName;

            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetIdentityCertificateSecondPageFileName(int applicantId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId);

            dbModel.IdentityCertificateSecondPageFileName = fileName;

            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetNationalCardBackFileName(int applicantId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId);

            dbModel.NationalCardBackFileName = fileName;

            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetNationalCardFrontFileName(int applicantId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId);

            dbModel.NationalCardFrontFileName = fileName;

            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetPersonalImageFileName(int applicantId, string fileName)
        {
            var dbModel = _table.SingleOrDefault(x => x.ApplicantId == applicantId);

            dbModel.PersonalImageFileName = fileName;

            _table.Attach(dbModel);

            _database.Entry(dbModel).State = EntityState.Modified;

            try
            {
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
