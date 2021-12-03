using edu_development_REST.Data;
using edu_development_REST.Entities;
using edu_development_REST.Helpers;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;




namespace edu_development_REST
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                    //  options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
                    options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });



            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseMembershipRepository, CourseMembershipRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddControllers();

            services.AddSwaggerGen(e =>
            {
                e.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Edu Test API",
                    Description = "A simple educational API designed to showcase Swagger and OpenApi"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                e.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(e =>
            {
                e.SwaggerEndpoint("/swagger/v1/swagger.json", "Edu Test API V1");
                e.RoutePrefix = string.Empty;
            }
            );

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
