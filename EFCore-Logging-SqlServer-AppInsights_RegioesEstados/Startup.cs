using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.ApplicationInsights.DependencyCollector;
using APIRegioes.Data;

namespace APIRegioes
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIRegioes", Version = "v1" });
            });

            // InsightsTelemetry: not necessary for ths example
            //if (!String.IsNullOrWhiteSpace(Configuration["ApplicationInsights:InstrumentationKey"]))
            //{
            //    services.AddApplicationInsightsTelemetry(Configuration);
            //    services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>(
            //        (module, o) =>
            //        {
            //            module.EnableSqlCommandTextInstrumentation = true;
            //        });
            //}

            services.AddDbContext<RegioesContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("BaseDadosGeograficos"));
                if (_env.IsDevelopment())
                    options.EnableSensitiveDataLogging(); //show 'parameters values' in log //https://elanderson.net/2019/03/entity-framework-core-show-parameter-values-in-logging/
                    //show SQL Statements in logs, see "appsettings.Development.json"
                    //"Microsoft.Hosting.Lifetime": "Information",
                    //"Microsoft.EntityFrameworkCore": "Information",
                    //"Microsoft.EntityFrameworkCore.Database.Command": "Information"
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIRegioes v1"));

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