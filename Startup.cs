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
using Microsoft.EntityFrameworkCore;
using Rocket_Elevator_RESTApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace Rocket_Elevator_RESTApi
{
    public class Startup
    {
        private const Newtonsoft.Json.ReferenceLoopHandling ignore = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.AddSwaggerGen();

            services.AddDbContext<InformationContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("MSQL")));

;

            services.AddMvc().AddNewtonsoftJson(setupAction: options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
