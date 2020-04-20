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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentsMicroservice.DBContexts;
using StudentsMicroservice.Repository;
using Microsoft.EntityFrameworkCore;
namespace StudentsMicroservice
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


            services.AddDbContext<StudentContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("StudentDB"));
            });
            services.AddTransient<IStudentRepository, StudentRepository>();

            /*services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200/",
                                                          "http://localhost:4200/")
                                      .AllowAnyHeader()
                                                  .AllowAnyMethod().AllowCredentials();

                                  });
            });*/

            //services.AddCors(c=>c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:44600")));

            /*  services.AddCors(options =>
              {
                 options.AddPolicy("CorsPolicy",
                       builder => builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());
              });*/

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
             builder => builder.WithOrigins("http://localhost:4200")
                         .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
            });
            /* options.AddDefaultPolicy( builder =>
         builder.SetIsOriginAllowed(_ => true)
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());
         });*/
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors("AllowOrigin");

            app.UseCors(options => options.WithOrigins("http://localhost:4200"));

            //app.UseCors("MyAllowSpecificOrigins");
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            /* if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }
             else
             {
                 app.UseHsts();
             }
             app.UseCors("CorsPolicy");
             app.UseHttpsRedirection();
             app.UseMvc();*/
        }
    }
}
