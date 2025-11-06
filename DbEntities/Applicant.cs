namespace DbEntities
{
    [Table(name: "Applicant")]
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string NationalCode { get; set; }

        [Required]
        public bool Gender { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }

        [Required]
        public int StudyFieldId { get; set; }

        [ForeignKey("StudyFieldId")]
        public virtual StudyField StudyField { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public short? EnglishLanguageLevel { get; set; }

        [MaxLength(50)]
        public string PersonalImageFileName { get; set; }

        [MaxLength(50)]
        public string NationalCardFrontFileName { get; set; }

        [MaxLength(50)]
        public string NationalCardBackFileName { get; set; }

        [MaxLength(50)]
        public string IdentityCertificateFirstPageFileName { get; set; }

        [MaxLength(50)]
        public string IdentityCertificateSecondPageFileName { get; set; }

        [MaxLength(50)]
        public string EducationalCertificateFileName { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        public virtual ICollection<ExamResourceOrder> ExamResourceOrderList { get; set; } = [];
        public virtual ICollection<ApplicantExamAttempt> ApplicantExamAttempt { get; set; } = [];
        public virtual ICollection<InterviewAppointment> InterviewAppointmentList { get; set; } = [];
    }
}
