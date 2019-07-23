using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TaxCalculator.Data;
using TaxCalculator.IoC;

namespace TaxCalculator.Api
{
    /// <summary>
    /// The start up class
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// The startup class
        /// </summary>
        /// <param name="configuration">Config object</param>
        /// <param name="hostingEnvironment">Hosting environment</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// The config services metho
        /// </summary>
        /// <param name="services">Services <see cref="IServiceCollection"/></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Tax Calculator API", Version = "V1" });
                c.IncludeXmlComments("TaxCalculator.Api.xml");
            });

            services.AddCors(o => o.AddPolicy("AllOrigins", p => {
                p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                }));

            services.Configure<DbSettings>(Configuration);
            
            var builder = new ContainerBuilder();

            builder.Populate(services);
            var container = IoCManager.BuildContainer(
                containerBuilder =>
                {
                    containerBuilder.RegisterModule(new InitializationModule(this.HostingEnvironment.EnvironmentName));
                    containerBuilder.Populate(services);
                });


            return new AutofacServiceProvider(container);
        }

        /// <summary>
        /// The configure
        /// </summary>
        /// <param name="app">Application builder instance</param>
        /// <param name="env">Environment settings</param>
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tax Calculator V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
