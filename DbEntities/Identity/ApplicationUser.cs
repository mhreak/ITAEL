using Microsoft.AspNetCore.Identity;

namespace DbEntities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public int? ApplicantId { get; set; }

        public string OTP { get; set; }

        public DateTime? OTPExpirationDate { get; set; }
    }
}