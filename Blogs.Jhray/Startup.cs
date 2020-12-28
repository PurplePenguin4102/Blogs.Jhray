using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blogs.Jhray.Data;
using Blogs.Jhray.Database;
using Microsoft.EntityFrameworkCore;
using Blogs.Jhray.Database.Entities;
using Blogs.Jhray.Areas.Identity.Pages;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.Logging;
using Blogs.Jhray.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Blogs.Jhray.Security;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.ResponseCompression;

namespace Blogs.Jhray
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var cn = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogContext>(opt =>
            {
                opt.UseNpgsql(cn);
            });

            // our identity context was scaffolded under a different context, meaning we don't need to worry about uses in BlogContext
            services.AddDefaultIdentity<BlogsJhrayUser>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlogContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<BlogsJhrayUser>>();
            services.AddScoped<BlogService>();
            services.AddTransient(svc => new DapperService(cn));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("content-creator", policy 
                    => policy.Requirements.Add(new EmailRequirement("joseph.h.ray@protonmail.com")));
            });
            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddResponseCompression(options => 
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "image/svg+xml", "image/x-icon" }));
            services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true; // optional
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseRouting();

            app.ApplicationServices
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
