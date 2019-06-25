using RiskAnalysis.Instruments;
using RiskAnalysis.PricingModel;
using RiskAnalysis.InterpolatorsParam;

namespace RiskAnalysis.Structures
{
    public class Pricer
    {
        public Pricer()
        {

        }

        public PricingResults Results(OptionInstrument option, PricingConfiguration pricingcfg)
        {
            var blackModel = new BlackModelOption();
            double riskfacor = Interpolators.InterpolateForward(option.SousJacent, option.RiskFactorDate);
            double price = blackModel.PriceBlackModel(option, riskfacor);
            var resultGreeks = blackModel.ComputeGreeks(pricingcfg.GreeksToCompute, riskfacor, option);
            return new PricingResults(resultGreeks,price);
        }
    }
}
