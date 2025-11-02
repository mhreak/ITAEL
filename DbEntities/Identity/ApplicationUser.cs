using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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