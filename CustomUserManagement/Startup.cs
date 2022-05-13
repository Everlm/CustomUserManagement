using CustomUserManagement.Data;
using CustomUserManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NToastNotify;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CustomUserManagement.Security;
using CustomUserManagement.Settings;
using CustomUserManagement.Services.EmailService;


namespace CustomUserManagement
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
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.EmailService.MailService>();

            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";


            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation");

            // Changes token lifespan of all token types
            services.Configure<DataProtectionTokenProviderOptions>(o =>
                    o.TokenLifespan = TimeSpan.FromHours(5));

            // Changes token lifespan of just the Email Confirmation Token type
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>
                    o.TokenLifespan = TimeSpan.FromDays(3));

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                                o.TokenLifespan = TimeSpan.FromHours(5));

            services.AddRazorPages().AddRazorRuntimeCompilation().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 5000
            });

            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });

            //Claims policy
            services.AddAuthorization(options =>
            {
                //Claims Policy
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                ////Roles Policy
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin", "SuperAdmin"));

                //Custom Policys Admin
                options.AddPolicy("EditRolePolicy", policy =>
                       policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.InvokeHandlersAfterFailure = false;

                //Policys whit conditions
                //options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
                //context.User.IsInRole("Admin") &&
                //context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                //context.User.IsInRole("Super Admin")));

                //options.AddPolicy("EditRolePolicy",
                //  policy => policy.RequireClaim("Edit Role"/*, "false"*/));

            });

            //Add hanlder 1
            services.AddSingleton<IAuthorizationHandler,
               CanEditOnlyOtherAdminRolesAndClaimsHandler>();

            //Add handler 2
            //services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

            //rute for view AccesDenied
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/RoleManager/AccessDenied");
            });

            //autethication run app
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNToastNotify();
            app.UseNotyf();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
