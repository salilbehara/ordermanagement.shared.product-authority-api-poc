﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ordermanagement.shared.product_authority_api.Extensions;
using ordermanagement.shared.product_authority_api.ServiceAbstractions;
using ordermanagement.shared.product_authority_api_data_access;
using Serilog;

namespace ordermanagement.shared.product_authority_api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            // Init Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Build an intermediate service provider
            services.BuildServiceProvider();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion

            #region Configuration Setup
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ProductAuthorityDatabaseContext>()
                .BuildServiceProvider();

            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion

            services.AddWebApiVersioningConfiguration();
            services.AddSwaggerConfiguration();
            services.AddHealthCheckConfiguration(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseCors("CorsPolicy")
                .UseStaticFiles()
                .UseHealthChecksMiddleware()
                .UseSwaggerMiddleware(provider)
                .UseMvc();
        }
    }
}
