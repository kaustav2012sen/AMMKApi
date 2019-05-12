using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InventoryManagementSystem
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public IConfigurationRoot Configuration
        {
            get;
            set;
        }
        public static string ConnectionString
        {
            get;
            private set;
        }
        public static string AllowedDomains
        {
            get;
            private set;
        }
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().
                SetBasePath(env.ContentRootPath).
                AddJsonFile("appSettings.json").
                Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            AllowedDomains = Configuration["AllowedIPDomain"];

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.WithOrigins(AllowedDomains)
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}");
            });
        }

        
    }
}
