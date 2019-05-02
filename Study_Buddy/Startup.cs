using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;
using Domain.DataContexts;
using WebUI.Services;


namespace WebUI
{
    public class Startup
    {
        private string _AuthenticationConnection;
        private string _ApplicationDataConnection;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Obscures the password with user secrets
            var builder = new SqlConnectionStringBuilder(
            Configuration.GetConnectionString("UserAuthentication"));
            builder.Password = Configuration["dbPassword"];

            _AuthenticationConnection = builder.ConnectionString;

            var builder2 = new SqlConnectionStringBuilder(
            Configuration.GetConnectionString("ApplicationData"));
            builder2.Password = Configuration["dbPassword"];

            _ApplicationDataConnection = builder2.ConnectionString;


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Set the Authentication DB Context to the protected string
            services.AddDbContext<UserAuthenticationDbContext>(options =>
            options.UseSqlServer(_AuthenticationConnection));

            // Set the Application data DB Context to the protected string
            services.AddDbContext<ApplicationDataContext>(options =>
            options.UseSqlServer(_ApplicationDataConnection));

            // Requires the user to confirm their email to login to the app.
            services.AddDefaultIdentity<IdentityUser>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<UserAuthenticationDbContext>();

            services.AddAuthentication()
            .AddFacebook(Options =>
            {
                Options.AppId = Configuration["Authentication:Facebook:AppId"];
                Options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            })
            /*.AddMicrosoftAccount(Options =>
            {
                Options.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                Options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            })*/
            .AddGoogle(Options =>
            {
                Options.ClientId = Configuration["Authentication:Google:ClientId"];
                Options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            // Changes the security token lifespan to 3 hours to protect accounts 
            services.Configure<DataProtectionTokenProviderOptions>(o =>
               o.TokenLifespan = TimeSpan.FromHours(3));

           // Sets the properties of the <class> to the matching Configuration Key Values
            services.Configure<AuthMessageSenderOptions>(Configuration);

            // Set dependancy injection values
            services.AddTransient<IEmailSender,EmailSender>();
            services.AddScoped<IDataRepository<Question>, QuestionRepository>();
            services.AddScoped<IDataRepository<Quiz>, QuizRepository>();
            services.AddScoped<IUserDataRepository, UserDataRepository>();
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddSessionStateTempDataProvider();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
          
            });
        }
    }
}
