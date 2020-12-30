using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Insurance.BusinessLogicLayer;
using Insurance.DataAccessLayer;
using Insurance.Entity;
using Insurance.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Insurance.Web
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
            services.AddControllers().AddJsonOptions(opts => { 
                opts.JsonSerializerOptions.PropertyNamingPolicy = null;
                
            });
            //   services.AddScoped<IWrapperService, IWrapperService>();


            services.AddScoped(typeof(IAdvisorRepository), typeof(AdvisorRepository));
            services.AddScoped(typeof(ICarrierRepository), typeof(CarrierRepository));
            services.AddScoped(typeof(IContractRepository), typeof(ContractRepository));
            services.AddScoped(typeof(IMGARepository), typeof(MGARepository));


            services.AddTransient<IAdvisorService, AdvisorService>();
            services.AddTransient<ICarrierService, CarrierService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IMGAService, MGAService>();





            services.AddDbContext<InsuranceContext>(item => item.UseSqlServer(Configuration.GetConnectionString("InsuranceContext")));
             services.AddCors(); // Make sure you call this previous to AddMvc
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigin",
            //        builder => builder.WithOrigins("https://insuranceapp.azurewebsites.net"));
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
      
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=carrier}/{action=get}");
            });
        }
    }
}
