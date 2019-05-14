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
using Microsoft.EntityFrameworkCore;
using HabitsServiceApi.Models;
using Microsoft.EntityFrameworkCore.InMemory;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using HabitsServiceApi.Interfaces;
using HabitsServiceApi.Services;

namespace HabitsServiceApi
{
    public class Startup
    {
        private IHostingEnvironment env;
        
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            env = hostingEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var rabbitWrapper = new RabbitWrapper.Wrapper("amqp://services:services@192.168.137.1:5672/");
            rabbitWrapper.Start();
            var DB_URL = "database-1.cxens1qv0ud6.us-east-1.rds.amazonaws.com";
            var DB_PORT = "5432";
            var DB_USER = "rosa";
            var DB_PASSWORD = "rosa2019";

            services.AddSingleton(rabbitWrapper);
            services.AddHostedService<PresenceService>();
            services.AddScoped<IHabitsService, HabitsService>();
            services.AddDbContext<HabitsContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options.UseInMemoryDatabase("habitsDb");
                }
                else
                {
                    string connection = $"Host={DB_URL};Username={DB_USER};Password={DB_PASSWORD};Database=postgres";
                    options.UseNpgsql(connection);
                }
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseMvc(options => {
                options.MapRoute(
                    "api",
                    "api/{controller}/{id?}");
            });
        }
    }
}
