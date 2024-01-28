using TaxCalculator.Data.Repositories;
using TaxCalculator.Services.TaxCalculators;

namespace TaxCalculator.Services
{
    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly ITaxRepository _taxRepository;
        private readonly Dictionary<string, Func<ITaxCalculator>> _calculatorFactories;

        public TaxCalculatorFactory(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
            _calculatorFactories = new Dictionary<string, Func<ITaxCalculator>>
            {
                //Using type name because different postal codes can share the same type (from the given example)
                {"Progressive", () => new ProgressiveTaxCalculator(_taxRepository)},
                {"Flat Value", () => new FlatValueTaxCalculator(_taxRepository)},
                {"Flat Rate", () => new FlatRateTaxCalculator(_taxRepository)}
            };
        }

        public ITaxCalculator GetTaxCalculator(string type)
        {
            if (_calculatorFactories.TryGetValue(type, out var factory))
            {
                return factory();
            }

            throw new ArgumentNullException(type, "Unsupported tax calculation type!");
        }
    }
}
