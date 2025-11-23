using DbEntities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Web.Model;
using Web.Service;
using Web.Service.Interface;

namespace Web.Extensions
{
    public static class ExtensionsMethod
    {
        public static void UseSeedDatabase(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            UserManager<ApplicationUser> userManager =
                (UserManager<ApplicationUser>)scope.ServiceProvider
                                                   .GetService(typeof(UserManager<ApplicationUser>));

            RoleManager<CustomRole> roleManager =
                (RoleManager<CustomRole>)scope.ServiceProvider
                                              .GetService(typeof(RoleManager<CustomRole>));

            var settingService = scope.ServiceProvider.GetService<ISettingService>();


            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                var managerRole = new CustomRole { Name = "Admin" };
                roleManager.CreateAsync(managerRole);
            }

            if (roleManager.FindByNameAsync("Manager").Result == null)
            {
                var managerRole = new CustomRole { Name = "Manager" };
                roleManager.CreateAsync(managerRole);
            }

            if (roleManager.FindByNameAsync("Applicant").Result == null)
            {
                var managerRole = new CustomRole { Name = "Applicant" };
                roleManager.CreateAsync(managerRole);
            }

            if (userManager.FindByNameAsync("mhreak").Result == null)
            {
                var user = new ApplicationUser() { PhoneNumber = "09135709239", UserName = "mhreak", Name = "کاربر ادمین" };
                userManager.CreateAsync(user, "H@ra_8674");
                userManager.AddToRoleAsync(user, "Admin");
            }

            if ((settingService.Get("SMSPanelUsername")) == null)
            {
                var settingViewModel = new SettingViewModel()
                                       {
                                           SettingId = 1,
                                           SettingName = "نام کاربری پنل پیامکی",
                                           SettingKey = "SMSPanelUsername",
                                           SettingValue = "u09137731766"
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("SMSPanelPassword")) == null)
            {
                var settingViewModel = new SettingViewModel()
                                       {
                                           SettingId = 2,
                                           SettingName = "رمز عبور پنل پیامکی",
                                           SettingKey = "SMSPanelPassword",
                                           SettingValue = "Faraz@1929441200082710"
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("SMSSenderNumber")) == null)
            {
                var settingViewModel = new SettingViewModel()
                                       {
                                           SettingId = 3,
                                           SettingName = "شماره ارسال کننده پیامک",
                                           SettingKey = "SMSSenderNumber",
                                           SettingValue = "5000125475"
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ServiceSMSSenderNumber")) == null)
            {
                var settingViewModel = new SettingViewModel()
                                       {
                                           SettingId = 4,
                                           SettingName = "شماره ارسال کننده پیامک خدماتی",
                                           SettingKey = "ServiceSMSSenderNumber",
                                           SettingValue = "5000125475"
                };

                settingService.Add(settingViewModel);
            }
        }
    }
}
