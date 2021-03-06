﻿using DiscordRPGBot.BusinessLogic.Middleware;
using DiscordRPGBot.BusinessLogic.Models;
using DiscordRPGBot.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections;

namespace DiscordRPGBot.Microservices
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // CONFIGURATION
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection"); 
                options.Database = Configuration.GetConnectionString("DatabaseName");
                options.ApiKey = Environment.GetEnvironmentVariable("API_KEY");
            });

            // SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DiscordRPGBot API", Version = "v1" });
                c.OperationFilter<SwaggerAddAPIKeyHeader>();
            });

            // DI
            services.AddTransient<IDiscordRPGBotRepository, DiscordRPGBotRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiscordRPGBot API V1");
            });
            app.UseAPIKeyMessageHandlerMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
