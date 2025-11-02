using DbEntities;
using DbEntities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbConnection
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserClaim, CustomUserRole,
            CustomUserLogin, CustomRoleClaim, CustomUserToken>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }


        public DbSet<City> City { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<SystemSMS> SystemSMS { get; set; }
        public DbSet<StudyField> StudyField { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<CommissionRule> CommissionRule { get; set; }
        public DbSet<JobAnnouncement> JobAnnouncement { get; set; }
        public DbSet<WalletCommission> WalletCommission { get; set; }
        public DbSet<JobAnnouncement_Skill> JobAnnouncement_Skill { get; set; }
        public DbSet<JobAnnouncementCategory> JobAnnouncementCategory { get; set; }
        public DbSet<Applicant_JobAnnouncement> Applicant_JobAnnouncement { get; set; }
        public DbSet<JobAnnouncement_StudyField> JobAnnouncement_StudyField { get; set; }
        public DbSet<Wallet_Collaborator_CommissionRule> Wallet_ReferralCode_CommissionRule { get; set; }
        public DbSet<JobAnnouncement_JobAnnouncementCategory> JobAnnouncement_JobAnnouncementCategory { get; set; }
 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Applicant_JobAnnouncement>().HasKey(table => new {
                table.ApplicantId,
                table.JobAnnouncementId
            });

            modelBuilder.Entity<JobAnnouncement_JobAnnouncementCategory>().HasKey(table => new {
                table.JobAnnouncementId,
                table.JobAnnouncementCategoryId
            });

            modelBuilder.Entity<JobAnnouncement_Skill>().HasKey(table => new {
                table.JobAnnouncementId,
                table.SkillId
            });

            modelBuilder.Entity<JobAnnouncement_StudyField>().HasKey(table => new {
                table.JobAnnouncementId,
                table.StudyFieldId
            });

            modelBuilder.Entity<Wallet_Collaborator_CommissionRule>().HasKey(table => new {
                table.WalletId,
                table.CollaboratorId,
                table.CommissionRuleId
            });

            modelBuilder.Entity<ApplicationUser>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<CustomRole>().ToTable("Role");
            modelBuilder.Entity<CustomRoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRole");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<CustomUserToken>().ToTable("UserToken");


            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType
                {
                    PaymentTypeId = 1,
                    PaymentTypeName = "پرداخت آنلاین"
                },
                new PaymentType
                {
                    PaymentTypeId = 2,
                    PaymentTypeName = "پرداخت کارت به کارت"
                },
                new PaymentType
                {
                    PaymentTypeId = 3,
                    PaymentTypeName = "پرداخت از اعتبار کیف پول"
                });
        }
    }
}
