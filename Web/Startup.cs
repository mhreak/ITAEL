using AutoMapper;
using DbConnection;
using DbEntities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Identity;
using Web.Service;
using Web.Service.Identity;
using Web.Service.Identity.Interface;
using Web.Service.Interface;
using static Web.ModelBinder.PersianDateModelBinder;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            //**
            //**
            services.AddAutoMapper(typeof(Startup));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //**
            //**
            // End Of Auto Mapper Configurations

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddMvc(
                options =>
                {
                    options.EnableEndpointRouting = false;
                    options.ModelBinderProviders.Insert(0, new PersianDateModelBinderProvider());
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddKendo();

            //services.AddBreadcrumbs(GetType().Assembly, options =>
            //{
            //    options.TagName = "nav";
            //    options.TagClasses = "";
            //    options.OlClasses = "breadcrumb";
            //    options.LiClasses = "breadcrumb-item";
            //    options.ActiveLiClasses = "breadcrumb-item active";
            //    options.SeparatorElement = "<li class=\"separator\">/</li>";
            //});

            services.AddDbContext<ApplicationDbContext>(
                item => item.UseSqlServer(
                    Configuration.GetConnectionString("ApplicationDbConnectionString")));

            services.AddIdentity<ApplicationUser, CustomRole>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(15);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();


            services.AddTransient<IUnitOfWork, ApplicationDbContext>();

            //register services
            services.AddScoped<IUtilService, UtilService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<ISystemSMSService, SystemSMSService>();
            services.AddScoped<IStudyFieldService, StudyFieldService>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IReferralCodeService, ReferralCodeService>();
            services.AddScoped<ICommissionRuleService, CommissionRuleService>();
            services.AddScoped<IJobAnnouncementService, JobAnnouncementService>();
            services.AddScoped<IWalletCommissionService, WalletCommissionService>();
            services.AddScoped<IApplicationUserManagerService, ApplicationUserManagerService>();
            services.AddScoped<IJobAnnouncement_Skill_Service, JobAnnouncement_Skill_Service>();
            services.AddScoped<IJobAnnouncementCategoryService, JobAnnouncementCategoryService>();
            services.AddScoped<IApplicant_JobAnnouncement_Service, Applicant_JobAnnouncement_Service>();
            services.AddScoped<IJobAnnouncement_StudyField_Service, JobAnnouncement_StudyField_Service>();
            services.AddScoped<IWallet_ReferralCode_CommissionRule_Service, Wallet_ReferralCode_CommissionRule_Service>();
            services.AddScoped<IJobAnnouncement_JobAnnouncementCategory_Service, JobAnnouncement_JobAnnouncementCategory_Service>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
