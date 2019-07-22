using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.IoC
{
    public static class IoCManager
    {
        public static IContainer BuildContainer(Action<ContainerBuilder> execution)
        {
            var containerBuilder = new ContainerBuilder();

            execution.Invoke(containerBuilder);

            var container = containerBuilder.Build();

            return container;
        }
    }
}
