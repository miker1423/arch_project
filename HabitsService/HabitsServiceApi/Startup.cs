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
            var rabbitWrapper = new RabbitWrapper.Wrapper(Configuration.GetConnectionString("RABBIT_URL"));
            rabbitWrapper.Start();
            var DB_URL = Configuration.GetConnectionString("DB_URL");
            var DB_PORT = Configuration.GetConnectionString("DB_PORT");
            var DB_USER = Configuration.GetConnectionString("DB_USER");
            var DB_PASSWORD = Configuration.GetConnectionString("DB_PASSWORD");

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
                    options.UseNpgsql($"{DB_USER}:{DB_PASSWORD}@{DB_URL}:{DB_PORT}");
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
