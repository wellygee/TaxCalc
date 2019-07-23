using Autofac;
using TaxCalculator.Data;
using TaxCalculator.IoC;
using TaxCalculator.Repositories;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public class InitializationModule : InitializationModuleBase
    {
        public InitializationModule(string environment) : base(environment) { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(TaxCalcRepository<>)).As(typeof(ISqlRepository<>));
            builder.RegisterType<TaxCalcDbContextFactory>().As<ITaxCalcDbContextFactory>();
            builder.RegisterType<TaxCalculationService>().As<ITaxCalculationService>();
            builder.RegisterType<TaxCalculationTypeRepository>().As<ITaxCalculationTypeRepository>();
            builder.RegisterType<TaxCalculationRepository>().As<ITaxCalculationRepository>();

            base.Load(builder);
        }
    }
}
