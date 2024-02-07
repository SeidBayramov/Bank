using Bank.Business.Services;
using Bank.Business.Services.Implementations;
using Bank.Business.Services.Interface;
using Bank.Core.Entities.Account;
using Bank.DAL.Context;
using Bank.DAL.Repositories.Absrtactions;
using Bank.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bank.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

        
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ISliderRepository, SliderRepository>();
            builder.Services.AddScoped<ISliderService, SliderService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBankIconRepository, BankIconRepository>();
            builder.Services.AddScoped<ICurrecnyRepository, CurrencyRepository>();
            builder.Services.AddScoped<IQuestionsRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuestionService,QuestionService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<IBankIconService, BankIconService>();
            builder.Services.AddScoped<LayoutService>();


            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
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

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //builder.Services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.AccessDeniedPath = "/Home/AccessDeniedCustom";
            //});

            builder.Services.AddAuthentication().AddCookie();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Service}/{action=Index}/{id?}"
                    );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}