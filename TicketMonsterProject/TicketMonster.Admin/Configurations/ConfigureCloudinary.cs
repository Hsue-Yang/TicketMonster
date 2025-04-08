using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketMonster.Admin.Helpers;
using TicketMonster.Admin.Interface;

namespace TicketMonster.Admin.Configurations
{
  
        public static class ConfigureCloudinary
        {
            public static void AddCloudinarySettings(this IServiceCollection services, IConfiguration configuration)
            {
            var CloudName = configuration.GetValue<string>("CloudinarySettings:CloudName");
            var ApiKey = configuration.GetValue<string>("CloudinarySettings:ApiKey");
            var ApiSecret = configuration.GetValue<string>("CloudinarySettings:ApiSecret");



            var cloudinaryAccount = new Account(
                   CloudName,
                   ApiKey,
                   ApiSecret);

            var cloudinary = new Cloudinary(cloudinaryAccount);
            services.AddSingleton(cloudinary);

            // 註冊IPhotoUploader實現
            services.AddTransient<IPhotoUploader, CloudinaryPhotoUploader>();
            }
        }
    
}
