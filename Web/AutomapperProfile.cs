using System;
using Web.Model;
using AutoMapper;
using DbEntities;
using Web.Model.Identity;
using DbEntities.Identity;
using System.Globalization;

namespace Web
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Setting, SettingViewModel>();
            CreateMap<SettingViewModel, Setting>();

            CreateMap<CustomRole, RoleViewModel>();
            CreateMap<RoleViewModel, CustomRole>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName));
            CreateMap<ApplicationUser, ApplicationUser>();

            CreateMap<CustomUserRole, UserRoleViewModel>();
            CreateMap<UserRoleViewModel, CustomUserRole>();

            CreateMap<PaymentType, PaymentTypeViewModel>();
            CreateMap<PaymentTypeViewModel, PaymentType>();

            CreateMap<Applicant, ApplicantViewModel>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => (!string.IsNullOrEmpty(x.FirstName) || !string.IsNullOrEmpty(x.LastName)) ? x.FirstName + " " + x.LastName : x.Mobile))
                .ForMember(dest => dest.GenderStr, src => src.MapFrom(x => x.Gender ? "آقا" : "خانم"))
                .ForMember(dest => dest.StudyFieldName, src => src.MapFrom(x => x.StudyField.StudyFieldName))
                .ForMember(dest => dest.ProvinceId, src => src.MapFrom(x => x.City.ProvinceId))
                .ForMember(dest => dest.ProvinceName, src => src.MapFrom(x => x.City.Province.ProvinceName))
                .ForMember(dest => dest.CityName, src => src.MapFrom(x => x.City.CityName))
                .ForMember(dest => dest.EnglishLanguageLevelStr, src => src.MapFrom(x => x.EnglishLanguageLevel != null ? ((short)x.EnglishLanguageLevel == 1 ? "عالی" :
                    ((short)x.EnglishLanguageLevel == 2 ? "متوسط" : ((short)x.EnglishLanguageLevel == 3 ? "ضعیف" : "نامعتبر"))) : ""))
                .ForMember(dest => dest.ShamsiBirthDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<ApplicantViewModel, Applicant>();


            CreateMap<JobAnnouncement, JobAnnouncementViewModel>()
                .ForMember(dest => dest.GenderStr, src => src.MapFrom(x => x.Gender != null ? (((bool)x.Gender) ? "آقا" : "خانم") : "مشخص نشده"))
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"))
                .ForMember(dest => dest.ShamsiPublishDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.ShamsiExpirationDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.ShamsiExamDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.JobTypeStr, src => src.MapFrom(x => x.JobType != null ? ((short)x.JobType == 1 ? "حضوری" :
                    ((short)x.JobType == 2 ? "غیرحضوری" : "حضوری / غیرحضوری")) : ""))
                .ForMember(dest => dest.JobTimeTypeStr, src => src.MapFrom(x => x.JobTimeType != null ? ((short)x.JobTimeType == 1 ? "تمام وقت" :
                    ((short)x.JobType == 2 ? "پاره وقت" : "تمام وقت / پاره وقت")) : ""));
            CreateMap<JobAnnouncementViewModel, JobAnnouncement>();

            CreateMap<JobAnnouncementCategory, JobAnnouncementCategoryViewModel>()
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"));
            CreateMap<JobAnnouncementCategoryViewModel, JobAnnouncementCategory>();

            CreateMap<JobAnnouncement_JobAnnouncementCategory, JobAnnouncement_JobAnnouncementCategory_ViewModel>()
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.JobAnnouncementCategoryName, src => src.MapFrom(x => x.JobAnnouncementCategory.CategoryName))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<JobAnnouncement_JobAnnouncementCategory_ViewModel, JobAnnouncement_JobAnnouncementCategory>();

            CreateMap<Skill, SkillViewModel>()
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"));
            CreateMap<SkillViewModel, Skill>();

            CreateMap<JobAnnouncement_Skill, JobAnnouncement_Skill_ViewModel>()
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.SkillName, src => src.MapFrom(x => x.Skill.SkillName))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<JobAnnouncement_Skill_ViewModel, JobAnnouncement_Skill>();

            CreateMap<Company, CompanyViewModel>()
                .ForMember(dest => dest.JobAnnouncementCount, src => src.MapFrom(x => x.JobAnnouncementList.Count))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<CompanyViewModel, Company>();

            CreateMap<StudyField, StudyFieldViewModel>()
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"));
            CreateMap<StudyFieldViewModel, StudyField>();

            CreateMap<Wallet, WalletViewModel>()
               .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"))
               .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<WalletViewModel, Wallet>();

            CreateMap<Wallet, WalletViewModel>()
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<WalletViewModel, Wallet>();

            //CreateMap<ReferralCode, ReferralCodeViewModel>()
            //   .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"))
            //   .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            //CreateMap<ReferralCodeViewModel, ReferralCode>();

            CreateMap<CommissionRule, CommissionRuleViewModel>()
                .ForMember(dest => dest.CommissionBasedOnStr, src => src.MapFrom(x => x.CommissionBasedOn == 1 ? "تعداد ثبت نام" : "مبلغ ثبت نام"))
                .ForMember(dest => dest.CommissionTypeStr, src => src.MapFrom(x => x.CommissionType == 1 ? "پورسانت درصدی" : "پورسانت به ازای هر ثبت نام"))
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.Min, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.Max, src => src.MapFrom(x => ""));
            CreateMap<CommissionRuleViewModel, CommissionRule>();

            CreateMap<Wallet_Collaborator_CommissionRule, Wallet_Collaborator_CommissionRule_ViewModel>()
               .ForMember(dest => dest.WalletName, src => src.MapFrom(x => x.Wallet.WalletName))
               .ForMember(dest => dest.ReferralCode, src => src.MapFrom(x => x.Collaborator.ReferralCode))
               .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<Wallet_Collaborator_CommissionRule_ViewModel, Wallet_Collaborator_CommissionRule>();

            CreateMap<SystemSMS, SystemSMSViewModel>()
                .ForMember(dest => dest.ShamsiSendDate, src => src.MapFrom(x => ""))
                .ForMember(dest => dest.SMSTypeStr, src => src.MapFrom(x => x.SMSType == 1 ?
                    "لیست قیمت هوشمند" : (x.SMSType == 2 ? "تبریک تولد" : (x.SMSType == 3 ? "ثبت درخواست فعالسازی گارانتی" :
                    (x.SMSType == 4 ? "رد شدن درخواست فعالسازی گارانتی" : (x.SMSType == 5 ? "فعال شدن گارانتی" :
                    (x.SMSType == 6 ? "پایان دوره گارانتی" : (x.SMSType == 7 ? "ارسال زیلینک" :
                    (x.SMSType == 8 ? "ارسال لینک لیست قیمت" : "نامعتبر")))))))));
            CreateMap<SystemSMSViewModel, SystemSMS>();

            CreateMap<JobAnnouncement_StudyField, JobAnnouncement_StudyField_ViewModel>()
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.StudyFieldName, src => src.MapFrom(x => x.StudyField.StudyFieldName))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<JobAnnouncement_StudyField_ViewModel, JobAnnouncement_StudyField>();

            CreateMap<Applicant_JobAnnouncement, Applicant_JobAnnouncement_ViewModel>()
                .ForMember(dest => dest.ApplicantFullName, src => src.MapFrom(x => x.Applicant.FirstName + " " + x.Applicant.LastName))
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ""));
            CreateMap<Applicant_JobAnnouncement_ViewModel, Applicant_JobAnnouncement>();

            CreateMap<Province, ProvinceViewModel>()
                .ForMember(dest => dest.CityCount, src => src.MapFrom(x => x.CityList.Count))
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"));
            CreateMap<ProvinceViewModel, Province>();

            CreateMap<City, CityViewModel>()
                .ForMember(dest => dest.ProvinceName, src => src.MapFrom(x => x.Province.ProvinceName))
                .ForMember(dest => dest.ApplicantCount, src => src.MapFrom(x => x.ApplicantList.Count))
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"));
            CreateMap<CityViewModel, City>();

            CreateMap<ApplicantExamAttempt, ApplicantExamAttemptViewModel>()
                .ForMember(dest => dest.ApplicantFullName, src => src.MapFrom(x => x.Applicant.FirstName + " " + x.Applicant.LastName))
                .ForMember(dest => dest.ExamTitle, src => src.MapFrom(x => x.Exam.Title))
                .ForMember(dest => dest.ShamsiStartTime, src => src.MapFrom(x => ConvertToShamsiDateWithTime(x.StartTime)))
                .ForMember(dest => dest.ShamsiEndTime, src => src.MapFrom(x => ConvertToShamsiDateWithTime(x.EndTime)))
                .ForMember(dest => dest.StatusStr, src => src.MapFrom(x => x.Status == 1 ?
                "قبول شده" : (x.Status == 2 ? "رد شده" : (x.Status == 3 ? "در حال بررسی" : "نامعتبر"))));
            CreateMap<ApplicantExamAttemptViewModel, ApplicantExamAttempt>();

            CreateMap<ApplicantExamQuestionAnswer, ApplicantExamQuestionAnswerViewModel>()
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)));
            CreateMap<ApplicantExamQuestionAnswerViewModel, ApplicantExamQuestionAnswer>();

            CreateMap<Collaborator, CollaboratorViewModel>()
                .ForMember(dest => dest.ActiveStr, src => src.MapFrom(x => x.Active ? "فعال" : "غیرفعال"));
            CreateMap<CollaboratorViewModel, Collaborator>();

            CreateMap<Exam, ExamViewModel>();
            CreateMap<ExamViewModel, Exam>();

            CreateMap<ExamQuestion, ExamQuestionViewModel>()
                .ForMember(dest => dest.ExamTitle, src => src.MapFrom(x => x.Exam.Title))
                .ForMember(dest => dest.TypeStr, src => src.MapFrom(x => x.Type == 1 ? "چندگزینه ای" : (x.Type == 2 ? "تشریحی" : "نامعتبر")));
            CreateMap<ExamQuestionViewModel, ExamQuestion>();

            CreateMap<ExamQuestionOption, ExamQuestionOptionViewModel>()
                .ForMember(dest => dest.IsCorrectAnswerStr, src => src.MapFrom(x => x.IsCorrectAnswer ? "بله" : "خیر"));
            CreateMap<ExamQuestionOptionViewModel, ExamQuestionOption>();

            CreateMap<ExamResource, ExamResourceViewModel>()
                .ForMember(dest => dest.TypeStr, src => src.MapFrom(x => x.Type == 1 ? "فیزیکی" : (x.Type == 2 ? "مجازی" : "نامعتبر")))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)));
            CreateMap<ExamResourceViewModel, ExamResource>();

            CreateMap<ExamResourceOrder, ExamResourceOrderViewModel>()
                .ForMember(dest => dest.ApplicantFullName, src => src.MapFrom(x => x.Applicant.FirstName + " " + x.Applicant.LastName))
                .ForMember(dest => dest.StatusStr, src => src.MapFrom(x => x.Status == 1 ? 
                "درحال بررسی" : (x.Status == 2 ? "پرداخت شده" : (x.Status == 3 ? 
                "ارسال شده" : (x.Status == 4 ? "تحویل داده شده" : (x.Status == 5 ? "دانلود شده" : "نامعتبر"))))))
                .ForMember(dest => dest.ResourceName, src => src.MapFrom(x => x.ExamResource.ResourceName))
                .ForMember(dest => dest.ShamsiDeliveryDate, src => src.MapFrom(x => ConvertToShamsiDate(x.DeliveryDate)))
                .ForMember(dest => dest.ShamsiOrderDate, src => src.MapFrom(x => ConvertToShamsiDate(x.OrderDate)));
            CreateMap<ExamResourceOrderViewModel, ExamResourceOrder>();

            CreateMap<InterviewAppointment, InterviewAppointmentViewModel>()
                .ForMember(dest => dest.ApplicantFullName, src => src.MapFrom(x => x.Applicant.FirstName + " " + x.Applicant.LastName))
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.StatusStr, src => src.MapFrom(x => x.Status == 1 ? 
                "درحال بررسی" : (x.Status == 2 ? "تأیید شده" : (x.Status == 3 ? "لغو شده" : (x.Status == 4 ? "تکمیل شده" : "نامعتبر")))))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)));
            CreateMap<InterviewAppointmentViewModel, InterviewAppointment>();

            CreateMap<JobAnnouncement_Exam, JobAnnouncement_Exam_ViewModel>()
                .ForMember(dest => dest.ExamTitle, src => src.MapFrom(x => x.Exam.Title))
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.ShamsiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)))
                .ForMember(dest => dest.ShamsiStartTime, src => src.MapFrom(x => ConvertToShamsiDateWithTime(x.StartTime)))
                .ForMember(dest => dest.ShamsiEndTime, src => src.MapFrom(x => ConvertToShamsiDateWithTime(x.EndTime)))
                .ForMember(dest => dest.AllowNavigateToPreviousQuestionStr, src => src.MapFrom(x => x.AllowNavigateToPreviousQuestion ? "بله" : "خیر"))
                .ForMember(dest => dest.RandomizeQuestionsStr, src => src.MapFrom(x => x.RandomizeQuestions ? "بله" : "خیر"))
                .ForMember(dest => dest.RandomizeOptionsStr, src => src.MapFrom(x => x.RandomizeOptions ? "بله" : "خیر"));
            CreateMap<JobAnnouncement_Exam_ViewModel, JobAnnouncement_Exam>();

            CreateMap<JobAnnouncement_ExamResource, JobAnnouncement_ExamResource_ViewModel>()
                .ForMember(dest => dest.JobAnnouncementTitle, src => src.MapFrom(x => x.JobAnnouncement.Title))
                .ForMember(dest => dest.ResourceName, src => src.MapFrom(x => x.ExamResource.ResourceName))
                .ForMember(dest => dest.ShmasiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)));
            CreateMap<JobAnnouncement_ExamResource_ViewModel, JobAnnouncement_ExamResource>();

            CreateMap<Skill_ExamResource, Skill_ExamResource_ViewModel>()
                .ForMember(dest => dest.ResourceName, src => src.MapFrom(x => x.ExamResource.ResourceName))
                .ForMember(dest => dest.SkillName, src => src.MapFrom(x => x.Skill.SkillName))
                .ForMember(dest => dest.ShmasiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)));
            CreateMap<Skill_ExamResource_ViewModel, Skill_ExamResource>();

            CreateMap<StudyField_ExamResource, StudyField_ExamResource_ViewModel>()
                .ForMember(dest => dest.ResourceName, src => src.MapFrom(x => x.ExamResource.ResourceName))
                .ForMember(dest => dest.StudyFieldName, src => src.MapFrom(x => x.StudyField.StudyFieldName))
                .ForMember(dest => dest.ShmasiInsertDate, src => src.MapFrom(x => ConvertToShamsiDate(x.InsertDate)));
            CreateMap<StudyField_ExamResource_ViewModel, StudyField_ExamResource>();
        }

        private string ConvertToShamsiDate(DateTime? date)
        {
            if (date == null) return null;

            try
            {
                if (date.Value < new DateTime(622, 3, 22) || date.Value > new DateTime(9999, 12, 31))
                    return null; // یا می‌تونی پیام "نامعتبر" برگردونی

                PersianCalendar pc = new PersianCalendar();
                return $"{pc.GetYear(date.Value)}/{pc.GetMonth(date.Value):00}/{pc.GetDayOfMonth(date.Value):00}";
            }
            catch
            {
                return null; // یا "تاریخ نامعتبر"
            }
        }

        private string ConvertToShamsiDateWithTime(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return string.Empty;

            var pc = new PersianCalendar();
            var dt = dateTime.Value;
            string shamsiDateTime = $"{pc.GetYear(dt)}/" +
                                    $"{pc.GetMonth(dt):00}/" +
                                    $"{pc.GetDayOfMonth(dt):00} " +
                                    $"{dt.Hour:00}:{dt.Minute:00}";
            return shamsiDateTime;
        }
    }
}
