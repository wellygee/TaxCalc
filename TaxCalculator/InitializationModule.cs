using Autofac;
using System;
using System.Data.SqlClient;
using TaxCalculator.Data;
using TaxCalculator.IoC;
using TaxCalculator.Repositories;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public class InitializationModule : InitializationModuleBase
    {
        public InitializationModule(string environment) : base(environment)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            var workingDirectory = AppContext.BaseDirectory;
            //var configurationBuilder = new ConfigurationBuilder();

            //configurationBuilder.SetBasePath(workingDirectory);

            //Directory.GetFiles(workingDirectory, "*.appsettings.json").ToList().ForEach(file => { configurationBuilder.AddJsonFile(file, true); });
            //Directory.GetFiles(workingDirectory, $"*.appsettings.{this.Environment}.json").ToList().ForEach(file => { configurationBuilder.AddJsonFile(file, true); });
            //var configuration = configurationBuilder.Build();

            // builder.RegisterInstance(configuration).As<IConfiguration>();

            // builder.RegisterType<SqlConnectionFactory>().As<IConnectionFactory<SqlConnection>>();// .WithAttributeFiltering();
            builder.RegisterGeneric(typeof(TaxCalcRepository<>)).As(typeof(ISqlRepository<>));
            builder.RegisterType<TaxCalcDbContextFactory>().As<ITaxCalcDbContextFactory>();
            builder.RegisterType<TaxCalculationService>().As<ITaxCalculationService>();
            builder.RegisterType<TaxCalculationTypeRepository>().As<ITaxCalculationTypeRepository>();

            base.Load(builder);
        }
    }
}
