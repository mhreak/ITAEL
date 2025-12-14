namespace Web.Model
{
    public class UpdateProfileViewModel
    {
        public ApplicantViewModel ApplicantViewModel { get; set; }

        public string PersonalImageFileUrl { get; set; }

        public string EducationalCertificateFileUrl { get; set; }

        public string NationalCardFrontFileUrl { get; set; }

        public string NationalCardBackFileUrl { get; set; }

        public string IdentityCertificateFirstPageFileUrl { get; set; }

        public string IdentityCertificateSecondPageFileUrl { get; set; }
    }
}
