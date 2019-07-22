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
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

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

            services.Configure<DbContextSettings>(this.Configuration.GetSection(
                //Constants.Settings.DbContextSettings
                "Db:Connections:taxCalc"
                ));

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
