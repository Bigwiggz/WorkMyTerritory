using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkMyTerritory.BusinessLayer.ValidationLogic;
using WorkMyTerritory.Extensions.Email.EmailInterfaces;
using WorkMyTerritory.Extensions.Email.EmailServices;
using WorkMyTerritory.Extensions.EmailExtensionInterfaces;
using WorkMyTerritory.Models;
using WorkMyTerritory.Models.ModelExtentions;
using WorkMyTerritory.Models.ModelInterfaces;
using WorkMyTerritory.Models.ValidationLogic;
using WorkMyTerritory.Services;
using WorkMyTerritory.Services.Email.BaseInterfaces;
using WorkMyTerritory.Services.Email.BaseModels;
using WorkMyTerritory.ViewModels;

namespace WorkMyTerritory
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

            // General Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            //Configure-Register Email Services
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailSenderExtensions, EmailSenderExtensions>();

            //To add security and extend the UserManager Class
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options=>{
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddUserManager<MyManager>()
            .AddRoleManager<MyRoles>()
            .AddDefaultTokenProviders();

            // Add application services.
            services.Configure<AuthMessageSenderOptions>(Configuration);

           //Set inactivity timeout to 10 days
            services.ConfigureApplicationCookie(o => {
                o.ExpireTimeSpan = TimeSpan.FromDays(10);
                o.SlidingExpiration = true;
            });

            //Set Token Email lifespan to 3 hours
            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));


            //Add session data
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".UserData.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //Add Cookie Policy Options
            services.Configure<CookiePolicyOptions>(options =>
            {
                // consent required
                options.CheckConsentNeeded = context => true; 
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMemoryCache();

            //DB Entity Services
            services.AddScoped<ICampaignSpecialEventsRepository, CampaignSpecialEventsRepository>();
            services.AddScoped<ICongregationRepository, CongregationRepository>();
            services.AddScoped<ICongregationTerritoriesRepository, CongregationTerritoriesRepository>();
            services.AddScoped<IHouseRecordsRepository, HouseRecordsRepository>();
            services.AddScoped<IPublishersRepository, PublishersRepository>();
            services.AddScoped<IServiceGroupsRepository, ServiceGroupsRepository>();
            services.AddScoped<ITerritoryPeopleRepository, TerritoryPeopleRepository>();
            services.AddScoped<ITerritoryWorkAssignmentRepository, TerritoryWorkAssignmentRepository>();

            //Add Controllers Service
            services.AddControllersWithViews();

            //Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Add MVC services and fluent validation
            services.AddMvc().AddFluentValidation(fv => { });
             
            //Add register fluent validators
            services.AddTransient<IValidator<ServiceGroupViewModel>,ServiceGroupValidator>();
            services.AddTransient<IValidator<CampaignAddSpecialEventsViewModel>, CampaignAddValidator>();
            services.AddTransient<IValidator<CampaignSpecialEventsViewModel>, CampaignEditValidator>();
            services.AddTransient<IValidator<TerritoryViewModel>, TerritoryValidator>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
