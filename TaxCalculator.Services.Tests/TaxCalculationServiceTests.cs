using Autofac;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaxCalculator.IoC;
using TaxCalculator.Repositories;
using TaxCalculator.Core;

namespace TaxCalculator.Services.Tests
{
    [TestFixture]
    public class TaxCalculationServiceTests
    {
        private ITaxCalculationService taxCalculationService;

        private Mock<ITaxCalculationTypeRepository> taxCalculationTypeRepositoryMock;
        private Mock<ITaxCalculationRepository> taxCalculationRepositoryMock;

        private readonly string nullPostalCode = null;
        private readonly string validPostalCode = "A100";

        private readonly string progressivePostalCode = "1000";
        private readonly string flatValuePostalCode = "A100";

        private readonly decimal invalidAnnualIncome = -1000M;
        private readonly decimal validAnnualIncome = 1000M;

        private readonly List<TaxCalculationType> taxCalculationTypes = new List<TaxCalculationType>
        {
            new TaxCalculationType{ Id = 1, PostalCode = "7441", Type = "Progressive" },
            new TaxCalculationType{ Id = 2, PostalCode = "A100", Type = "FlatValue" },
            new TaxCalculationType{ Id = 3, PostalCode = "7000", Type = "FlatRate" },
            new TaxCalculationType{ Id = 4, PostalCode = "1000", Type = "Progressive" }
        };

        [SetUp]
        public void Setup()
        {
            taxCalculationTypeRepositoryMock = new Mock<ITaxCalculationTypeRepository>();
            taxCalculationRepositoryMock = new Mock<ITaxCalculationRepository>();
            var taxCalculationTypeRepository = taxCalculationTypeRepositoryMock.Object;
            var taxCalculationRepository = taxCalculationRepositoryMock.Object;

            taxCalculationTypeRepositoryMock.Setup(repo => repo.GetTaxCalculationTypesAsync()).ReturnsAsync(taxCalculationTypes);
            taxCalculationRepositoryMock.Setup(repo => repo.InsertOrUpdateTaxCalculationAsync(It.IsAny<TaxCalculation>())).ReturnsAsync(1);

            var container = IoCManager.BuildContainer(
                containerBuilder =>
                {
                    containerBuilder.RegisterModule(new InitializationModule("TEST"));
                    containerBuilder.RegisterInstance(taxCalculationTypeRepository).As<ITaxCalculationTypeRepository>();
                    containerBuilder.RegisterInstance(taxCalculationRepository).As<ITaxCalculationRepository>();
                    
                });

            this.taxCalculationService = container.Resolve<ITaxCalculationService>();
        }

        [Test]
        public void WhenGettingTaxAmountWithANullPostalCodeThenExpectAnArgumentNullException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await this.taxCalculationService.GetTaxAmount(nullPostalCode, validAnnualIncome));

            Assert.IsTrue(ex.Message.Contains("postalCode"));
        }

        [Test]
        public void WhenGettingTaxAmountWithAnEmptyPostalCodeThenExpectAnArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await this.taxCalculationService.GetTaxAmount(validPostalCode, invalidAnnualIncome));

            Assert.IsTrue(ex.Message.Contains("annualIncome"));
        }

        //TODO: test for decimal.Max income

        [Test]
        public async Task WhenGettingTaxAmountWithValidParametersThenExpectSuccess()
        {
            var result = await this.taxCalculationService.GetTaxAmount(progressivePostalCode, validAnnualIncome);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1 >= 0);

            taxCalculationRepositoryMock.Verify(m => m.InsertOrUpdateTaxCalculationAsync(It.IsAny<TaxCalculation>()));
        }

        [TestCase("A100", 0, ExpectedResult = 0)]
        [TestCase("A100", 10000, ExpectedResult = 10000*0.05)]
        [TestCase("A100", 200000, ExpectedResult = 10000)]
        [TestCase("A100", 200001, ExpectedResult = 10000)]
        public async Task<decimal> WhenGettingTaxAmountWithFlatValuePostalCodeParametersThenExpectCorrectValues(string postalCode, decimal annualIncome)
        {
            var result = await this.taxCalculationService.GetTaxAmount(postalCode, annualIncome);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1 >= 0);

            taxCalculationRepositoryMock.Verify(m => m.InsertOrUpdateTaxCalculationAsync(It.IsAny<TaxCalculation>()));

            return result.Item1;
        }

        [TestCase("7000", 0, ExpectedResult = 0)]
        [TestCase("7000", 10000, ExpectedResult = 10000*0.175)]
        public async Task<decimal> WhenGettingTaxAmountWithFlatRatePostalCodeParametersThenExpectCorrectValues(string postalCode, decimal annualIncome)
        {
            var result = await this.taxCalculationService.GetTaxAmount(postalCode, annualIncome);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1 >= 0);

            return result.Item1;
        }

        [TestCase("1000", 0, ExpectedResult = 0)]
        [TestCase("1000", 1000, ExpectedResult = 1000 * 0.10)]
        [TestCase("1000", 8350, ExpectedResult = (8350 * 0.10))]
        [TestCase("1000", 8351, ExpectedResult = (8350 * 0.10) + (1 * 0.15))]
        [TestCase("1000", 8352, ExpectedResult = (8350 * 0.10) + (2 * 0.15))]
        [TestCase("1000", 33950, ExpectedResult = (8350 * 0.10) + ((33950 - 8350) * 0.15))]
        [TestCase("1000", 33951, ExpectedResult = (8350 * 0.10) + ((33950 - 8350) * 0.15) + (1 * 0.25))]
        [TestCase("1000", 33952, ExpectedResult = (8350 * 0.10) + ((33950 - 8350) * 0.15) + (2 * 0.25))]
        [TestCase("7441", 33952, ExpectedResult = (8350 * 0.10) + ((33950 - 8350) * 0.15) + (2 * 0.25))]
        public async Task<decimal> WhenGettingTaxAmountWithProgressivePostalCodeParametersThenExpectCorrectValues(string postalCode, decimal annualIncome)
        {
            var result = await this.taxCalculationService.GetTaxAmount(postalCode, annualIncome);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1 >= 0);

            return result.Item1;
        }

        [Test]
        public async Task WhenGettingTaxAmountWithProgressivePostalCodeThenExpectProgressiveTaxCalculationType()
        {
            var result = await this.taxCalculationService.GetTaxAmount(progressivePostalCode, validAnnualIncome);

            taxCalculationRepositoryMock.Verify(m => m.InsertOrUpdateTaxCalculationAsync(It.IsAny<TaxCalculation>()));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1 >= 0);
        }
    }
}