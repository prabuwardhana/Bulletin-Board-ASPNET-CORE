using System;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services.BlobStorageService;
using Services.BlobStorageService.Configurations;
using Services.EmailService;
using Services.EmailService.Configurations;
using Services.EmailService.Interface;
using Services.IdentityUserService;
using Services.RepositoryServices;

namespace BulletinBoard.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqliteContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options => 
            {
                var connectionString = configuration.GetConnectionString("sqliteConnection");
                options.UseSqlite(connectionString, b => b.MigrationsAssembly("BulletinBoard"));
            });
        
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureRepositoryService(this IServiceCollection services)
        {
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        public static void ConfigureIdentityUser(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<RepositoryContext>()
                    .AddDefaultTokenProviders();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthService, UserAuthService>();
        }
        
        public static void ConfigureIdentityOptions(this IServiceCollection services) =>
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;                
            });
        
        public static void ConfigureCookie(this IServiceCollection services) =>
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Authentication/LogIn";
                options.AccessDeniedPath = "/Error/AccessDenied";
                options.SlidingExpiration = true;
            });

        public static void ConfigureAzureBlobService(this IServiceCollection services, IConfiguration configuration)
        {
            var blobConfig = configuration.GetSection(AzureBlobStorageConfig.AzureBlobOption).Get<AzureBlobStorageConfig>();

            services.AddSingleton(blobConfig);
            services.AddSingleton<IBlobService, BlobService>();
        }

        public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection(EmailConfiguration.EmailSenderConfiguration).Get<EmailConfiguration>();
                
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}