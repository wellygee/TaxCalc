using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.IoC
{
    public class InitializationModuleBase : Module
    {
        protected string Environment { get; }

        public InitializationModuleBase(string environment)
        {
            this.Environment = environment;
        }
    }
}
