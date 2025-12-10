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
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<SystemSMS> SystemSMS { get; set; }
        public DbSet<SMSPattern> SMSPattern { get; set; }
        public DbSet<StudyField> StudyField { get; set; }
        public DbSet<BankGateway> BankGateway { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<ExamResource> ExamResource { get; set; }
        public DbSet<ExamQuestion> ExamQuestion { get; set; }
        public DbSet<CommissionRule> CommissionRule { get; set; }
        public DbSet<ContactMessage> ContactMessage { get; set; }
        public DbSet<JobAnnouncement> JobAnnouncement { get; set; }
        public DbSet<WalletCommission> WalletCommission { get; set; }
        public DbSet<OnlineTransaction> OnlineTransaction { get; set; }
        public DbSet<ExamResourceOrder> ExamResourceOrder { get; set; }
        public DbSet<ExamQuestionOption> ExamQuestionOption { get; set; }
        public DbSet<Skill_ExamResource> Skill_ExamResource { get; set; }
        public DbSet<InterviewAppointment> InterviewAppointment { get; set; }
        public DbSet<JobAnnouncement_Exam> JobAnnouncement_Exam { get; set; }
        public DbSet<ApplicantExamAttempt> ApplicantExamAttempt { get; set; }
        public DbSet<JobAnnouncement_Skill> JobAnnouncement_Skill { get; set; }
        public DbSet<StudyField_ExamResource> StudyField_ExamResource { get; set; }
        public DbSet<JobAnnouncementCategory> JobAnnouncementCategory { get; set; }
        public DbSet<Applicant_JobAnnouncement> Applicant_JobAnnouncement { get; set; }
        public DbSet<JobAnnouncement_StudyField> JobAnnouncement_StudyField { get; set; }
        public DbSet<ApplicantExamQuestionAnswer> ApplicantExamQuestionAnswer { get; set; }
        public DbSet<JobAnnouncement_ExamResource> JobAnnouncement_ExamResource { get; set; }
        public DbSet<Wallet_Collaborator_CommissionRule> Wallet_Collaborator_CommissionRule { get; set; }
        public DbSet<JobAnnouncement_JobAnnouncementCategory> JobAnnouncement_JobAnnouncementCategory { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Applicant_JobAnnouncement>().HasKey(table => new
            {
                table.ApplicantId,
                table.JobAnnouncementId
            });

            modelBuilder.Entity<ApplicantExamQuestionAnswer>(b =>
            {
                b.HasKey(x => new { x.ExamQuestionId, x.ApplicantExamAttemptId });

                b.HasOne(x => x.ApplicantExamAttempt)
                 .WithMany(a => a.ApplicantExamQuestionAnswerList)
                 .HasForeignKey(x => x.ApplicantExamAttemptId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.ExamQuestion)
                 .WithMany(q => q.ApplicantExamQuestionAnswerList)
                 .HasForeignKey(x => x.ExamQuestionId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.ExamQuestionOption)
                 .WithMany(o => o.ApplicantExamQuestionAnswerList)
                 .HasForeignKey(x => x.ExamQuestionOptionId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.Property(x => x.AnswerText).HasMaxLength(4000);
                b.Property(x => x.InsertDate).IsRequired();
            });

            modelBuilder.Entity<ExamResourceOrderItem>()
                        .HasOne(eri => eri.ExamResourceOrder)
                        .WithMany(ero => ero.ExamResourceOrderItemList)
                        .HasForeignKey(eri => eri.ExamResourceOrderId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamResourceOrderItem>()
                        .HasOne(eri => eri.ExamResource)
                        .WithMany(er => er.ExamResourceOrderItem_List)
                        .HasForeignKey(eri => eri.ExamResourceId)
                        .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            modelBuilder.Entity<ApplicantExamAttempt>(b =>
            {
                b.Property(x => x.FinalScore).HasPrecision(10, 2); // choose precision/scale for your domain
            });

            modelBuilder.Entity<JobAnnouncement_JobAnnouncementCategory>().HasKey(table => new
            {
                table.JobAnnouncementId,
                table.JobAnnouncementCategoryId
            });

            modelBuilder.Entity<JobAnnouncement_Skill>().HasKey(table => new
            {
                table.SkillId,
                table.JobAnnouncementId
            });

            modelBuilder.Entity<JobAnnouncement_StudyField>().HasKey(table => new
            {
                table.StudyFieldId,
                table.JobAnnouncementId
            });

            modelBuilder.Entity<JobAnnouncement_Exam>().HasKey(table => new
            {
                table.ExamId,
                table.JobAnnouncementId
            });

            modelBuilder.Entity<JobAnnouncement_ExamResource>().HasKey(table => new
            {
                table.ExamResourceId,
                table.JobAnnouncementId
            });

            modelBuilder.Entity<Skill_ExamResource>().HasKey(table => new
            {
                table.SkillId,
                table.ExamResourceId
            });

            modelBuilder.Entity<StudyField_ExamResource>().HasKey(table => new
            {
                table.StudyFieldId,
                table.ExamResourceId
            });

            modelBuilder.Entity<Wallet_Collaborator_CommissionRule>().HasKey(table => new
            {
                table.WalletId,
                table.CollaboratorId,
                table.CommissionRuleId
            });

            modelBuilder.Entity<OnlineTransaction>()
                        .HasOne(e => e.Applicant_JobAnnouncement)
                        .WithMany()
                        .HasForeignKey(e => new { e.ApplicantId, e.JobAnnouncementId })
                        .IsRequired();

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
