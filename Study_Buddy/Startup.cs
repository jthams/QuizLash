using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Study_Buddy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Study_Buddy.Services;
using System.Data.SqlClient;

namespace Study_Buddy
{
    public class Startup
    {
        private string _connection = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Obscures the password with user secrets 
            //to protect the account in source control

            var builder = new SqlConnectionStringBuilder(
            Configuration.GetConnectionString("DefaultConnection"));
            builder.Password = Configuration["dbPassword"];

            _connection = builder.ConnectionString;



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            // Set the DB Context to the protected string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_connection));

            // Requires the user to confirm their email to login to the app.
            services.AddDefaultIdentity<IdentityUser>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Changes the security token lifespan to 3 hours to protect accounts 
            services.Configure<DataProtectionTokenProviderOptions>(o =>
               o.TokenLifespan = TimeSpan.FromHours(3));

           // Sets the properties of the <class> to the matching Configuration Key Values
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IEmailSender,EmailSender>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
