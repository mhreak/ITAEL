using DbConnection;
using DbEntities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Web;
using Web.Identity;
using Web.Service;
using Web.Service.Identity;
using Web.Service.Identity.Interface;
using Web.Service.Interface;
using static Web.ModelBinder.PersianDateModelBinder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper((_ => { }), typeof(AutomapperProfile).Assembly);

builder.Services.AddControllersWithViews()
       .AddRazorRuntimeCompilation();

builder.Services.AddMvc(
           options =>
           {
               options.EnableEndpointRouting = false;
               options.ModelBinderProviders.Insert(0, new PersianDateModelBinderProvider());
           })
       .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddKendo();

//services.AddBreadcrumbs(GetType().Assembly, options =>
//{
//    options.TagName = "nav";
//    options.TagClasses = "";
//    options.OlClasses = "breadcrumb";
//    options.LiClasses = "breadcrumb-item";
//    options.ActiveLiClasses = "breadcrumb-item active";
//    options.SeparatorElement = "<li class=\"separator\">/</li>";
//});

builder.Services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbConnectionString")));

builder.Services.AddIdentity<ApplicationUser, CustomRole>(options =>
                                                          {
                                                              options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(15);
                                                          })
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders()
       .AddErrorDescriber<PersianIdentityErrorDescriber>();


builder.Services.AddTransient<IUnitOfWork, ApplicationDbContext>();

//register services
builder.Services.AddScoped<IUtilService, UtilService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<ISystemSMSService, SystemSMSService>();
builder.Services.AddScoped<IStudyFieldService, StudyFieldService>();
builder.Services.AddScoped<IRoleManagerService, RoleManagerService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IReferralCodeService, ReferralCodeService>();
builder.Services.AddScoped<ICommissionRuleService, CommissionRuleService>();
builder.Services.AddScoped<IJobAnnouncementService, JobAnnouncementService>();
builder.Services.AddScoped<IWalletCommissionService, WalletCommissionService>();
builder.Services.AddScoped<IApplicationUserManagerService, ApplicationUserManagerService>();
builder.Services.AddScoped<IJobAnnouncement_Skill_Service, JobAnnouncement_Skill_Service>();
builder.Services.AddScoped<IJobAnnouncementCategoryService, JobAnnouncementCategoryService>();
builder.Services.AddScoped<IApplicant_JobAnnouncement_Service, Applicant_JobAnnouncement_Service>();
builder.Services.AddScoped<IJobAnnouncement_StudyField_Service, JobAnnouncement_StudyField_Service>();
builder.Services.AddScoped<IWallet_ReferralCode_CommissionRule_Service, Wallet_ReferralCode_CommissionRule_Service>();
builder.Services.AddScoped<IJobAnnouncement_JobAnnouncementCategory_Service, JobAnnouncement_JobAnnouncementCategory_Service>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // In production use the exception handler, not the developer page
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
                 {
                     // area-aware route (maps /{area}/{controller}/{action})
                     endpoints.MapControllerRoute(
                         name: "areas",
                         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                     // default route
                     endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}");
                 });

app.Run();