﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GrooveMessengerAPI.Configurations;
using AutoMapper;
using Microsoft.Extensions.Logging;
using GrooveMessengerAPI.Middlewares;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerAPI.Auth;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAPIClient",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });

                options.AddPolicy("AllowHubClient",
               builder =>
               {
                   builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
               });
            });
            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "687824117544-nvc2uojbm14hc330gl8qh3lsrtl3tc4a.apps.googleusercontent.com";
                options.ClientSecret = "ugBBsDjYS-Rz20RVlx9r7Blo";
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            RegisterAuth(services);
            RegisterIdentity(services);
            RegisterAutoMapperProfiles(services);

            DiConfiguration.Register(services);

            // Register SignalR
            services.AddSignalR();
            services.AddScoped<IAuthEmailSenderUtil, AuthEmailSenderUtil>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
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

            //app.UseCors("AllowAPIClient");
            app.UseCors("AllowHubClient");


            app.UseAuthentication();

            ConfigureLog(loggerFactory);

            RegisterMiddlewares(app);
            SeedRootUserDatabase(serviceProvider);
            // Using SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/chatHub");
            });

            app.UseMvc(routes =>
            {
                RegisterRouting(routes);
            });


        }
    }
}
