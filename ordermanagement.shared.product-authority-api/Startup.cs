using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ordermanagement.shared.product_authority_api.Extensions;
using ordermanagement.shared.product_authority_database;
using ordermanagement.shared.product_authority_infrastructure;
using Serilog;
using System.Reflection;

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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            #endregion

            #region Configuration Setup
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ProductAuthorityDatabaseContext>()
                .BuildServiceProvider();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            #endregion

            services.AddWebApiVersioningConfiguration();
            services.AddSwaggerConfiguration();
            services.AddHealthCheckConfiguration(Configuration);

            // Add common FluentMigrator services
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddPostgres()
                    // Set the connection string
                    .WithGlobalConnectionString(Configuration["ConnectionStrings:ProductDatabase"])
                    //.WithGlobalConnectionString("Server=ordermanagement-shared-product-db-lz.cluster-cww2jj360xkz.us-east-1.rds.amazonaws.com; Port=3306; Database=product; User Id=master; Password=uaFYR5L89a;")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(MigrationSequence).Assembly).For.Migrations());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, IMigrationRunner migrationRunner )
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

            // Execute the migrations
            migrationRunner.MigrateUp();
        }
    }
}