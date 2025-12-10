using Web.Model;
using DbEntities.Identity;
using Web.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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

            #region Settings
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

            if ((settingService.Get("BaseUrl")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 5,
                    SettingName = "آدرس پایه",
                    SettingKey = "BaseUrl",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("AboutUs_Description")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 6,
                    SettingName = "متن جزئیات درباره ما",
                    SettingKey = "AboutUs_Description",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_PrimaryLandline")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 7,
                    SettingName = "تلفن ثابت اصلی تماس با ما",
                    SettingKey = "ContactUs_PrimaryLandline",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_SecondaryLandline")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 8,
                    SettingName = "تلفن ثابت فرعی تماس با ما",
                    SettingKey = "ContactUs_SecondaryLandline",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_PrimaryMobileNumber")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 9,
                    SettingName = "تلفن همراه اصلی تماس با ما",
                    SettingKey = "ContactUs_PrimaryMobileNumber",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_SecondaryMobileNumber")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 10,
                    SettingName = "تلفن همراه فرعی تماس با ما",
                    SettingKey = "ContactUs_SecondaryMobileNumber",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_TelegramID")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 11,
                    SettingName = "آی دی تلگرام تماس با ما",
                    SettingKey = "ContactUs_TelegramID",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_InstagramID")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 12,
                    SettingName = "آی دی اینستاگرام تماس با ما",
                    SettingKey = "ContactUs_InstagramID",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_WhatsAppNumber")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 13,
                    SettingName = "شماره واتس اپ تماس با ما",
                    SettingKey = "ContactUs_WhatsAppNumber",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_Email")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 14,
                    SettingName = "ایمیل تماس با ما",
                    SettingKey = "ContactUs_Email",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }

            if ((settingService.Get("ContactUs_Address")) == null)
            {
                var settingViewModel = new SettingViewModel()
                {
                    SettingId = 15,
                    SettingName = "آدرس تماس با ما",
                    SettingKey = "ContactUs_Address",
                    SettingValue = ""
                };

                settingService.Add(settingViewModel);
            }
            #endregion


        }
    }
}
