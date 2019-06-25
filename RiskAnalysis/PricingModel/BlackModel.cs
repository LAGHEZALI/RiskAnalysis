using System;
using RiskAnalysis.Instruments;
using RiskAnalysis.MarketData;
using RiskAnalysis.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace RiskAnalysis.PricingModel
{
    public class BlackModelOption  //: IPricer 
    {
        public BlackModelOption()
        {
        }

        public double PriceBlackModel(OptionInstrument option, double riskFactor)
        {
            var maturity = (option.ExpirationDate - Data.ParamDate).TotalDays / 365.00;
            var d1 = (Math.Log(riskFactor / option.Strike) + 0.5 * 
                      Math.Pow(Data.Volatility, 2) * maturity) / (Data.Volatility * Math.Sqrt(maturity));
            var d2 = d1 - Math.Sqrt(maturity);

            if (option.Type == OptionType.Call)
            {
                var x = Math.Exp(-Data.Rate * maturity)
                        * (riskFactor
                           * Tools.Phi(d1) - option.Strike * Tools.Phi(d2));
                return Math.Exp(-Data.Rate * (maturity))
                   * (riskFactor
                   * Tools.Phi(d1) - option.Strike * Tools.Phi(d2));
            }
            else
            {
                return Math.Exp(-Data.Rate * maturity)
                   * (-riskFactor * Tools.Phi(-d1) + option.Strike * Tools.Phi(-d2));
            }
        }

        public Dictionary<GreekType, double> ComputeGreeks(List<GreekType> greekTocompute,
            double riskFactor, OptionInstrument option)
        {
            var results = new Dictionary<GreekType, double>();
            foreach (var greek in greekTocompute)
            {
                switch (greek)
                {
                    case GreekType.Delta:
                        results.Add(greek, CompuetDelta(option, riskFactor));
                        if(option.Sens == Way.Sell)
                            results[greek] = results[greek] * -1;
                        break;

                    case GreekType.Vega:
                        results.Add(greek, ComputeVega(option, riskFactor));
                        if(option.Sens == Way.Sell)
                            results[greek] = results[greek] * -1;
                        break;

                    case GreekType.Tetha:
                        results.Add(greek, ComputeTheta(option, riskFactor));
                        if (option.Sens == Way.Sell)
                            results[greek] = results[greek] * -1;
                        break;

                    case GreekType.Rho:
                        results.Add(greek, CompuetDelta(option, riskFactor));
                        if(option.Sens == Way.Sell)
                            results[greek] = results[greek] * -1;
                        break;

                    case GreekType.Gamma:
                        results.Add(greek, ComputeGamma(option, riskFactor));
                        if (option.Sens == Way.Sell)
                            results[greek] = results[greek] * -1;
                        break;
                }
            }
            return results;

        }

        double CompuetDelta(OptionInstrument option, double riskFacor)
        {
            var diffDate = (option.ExpirationDate - Data.ParamDate).TotalDays / 365.00;
            var d1 = (Math.Log(riskFacor / option.Strike) + 0.5 * Math.Pow(Data.Volatility, 2) * diffDate) / (Data.Volatility * Math.Sqrt(diffDate));
            if (option.Type == OptionType.Call)
                return Math.Exp(-Data.Rate * diffDate) * Tools.Phi(d1);
            else
                return Math.Exp(-Data.Rate * diffDate) * (Tools.Phi(d1) - 1);
        }

        double ComputeVega(OptionInstrument option, double riskFactor)
        {
            var diffDate = (option.ExpirationDate - Data.ParamDate).TotalDays / 365.00;
            var d1 = (Math.Log(riskFactor / option.Strike) + 0.5 * Math.Pow(Data.Volatility, 2) * diffDate) / (Data.Volatility * Math.Sqrt(diffDate));
            return riskFactor * Math.Exp(-Data.Rate * diffDate) * Tools.NormalDensity(d1) * Math.Sqrt(diffDate);
        }

        double ComputeTheta(OptionInstrument option, double riskFactor)
        {
            var maturity = (option.ExpirationDate - Data.ParamDate).TotalDays / 365.00;
            var d1 = (Math.Log(riskFactor / option.Strike) + 0.5 * Math.Pow(Data.Volatility, 2) * maturity) /
                     (Data.Volatility * Math.Sqrt(maturity));
            var d2 = d1 - Math.Sqrt(maturity);
            if (option.Type == OptionType.Call)
                return -riskFactor * Math.Exp(-Data.Rate * maturity) * Tools.NormalDensity(d1) * Data.Volatility / (2 * Math.Sqrt(maturity))
                + Data.Rate * riskFactor * Math.Exp(-Data.Rate * maturity) * Tools.Phi(d1) - Math.Exp(-Data.Rate * maturity)*Data.Rate * option.Strike * Tools.Phi(d2);
            else
                return -riskFactor * Math.Exp(-Data.Rate * maturity) * Tools.NormalDensity(d1) * Data.Volatility / (2 * Math.Sqrt(maturity))
                - Data.Rate * riskFactor * Math.Exp(-Data.Rate * maturity) * Tools.Phi(-d1) + Data.Rate * option.Strike * Math.Exp(-Data.Rate * maturity) * Tools.Phi(-d2);
        }

        double ComputeRho(OptionInstrument option, double riskFactor)
        {
            var maturity = (option.ExpirationDate - Data.ParamDate).TotalDays / 365.00;
            var d1 = (Math.Log(riskFactor / option.Strike) + 0.5 * Math.Pow(Data.Volatility, 2) * maturity) /
                     (Data.Volatility * Math.Sqrt(maturity));
            var d2 = d1 - Math.Sqrt(maturity);
            if (option.Type == OptionType.Call)
                return option.Strike * maturity * Tools.Phi(d2);
            else
            {
                return -option.Strike * maturity * Tools.Phi(-d2);
            }
        }

        double ComputeGamma(OptionInstrument option, double riskFactor)
        {
            var diffDate = (option.ExpirationDate - Data.ParamDate).TotalDays / 365.00;
            var d1 = (Math.Log(riskFactor / option.Strike) + 0.5 * Math.Pow(Data.Volatility, 2) * diffDate) / (Data.Volatility * Math.Sqrt(diffDate));
            return Math.Exp(-Data.Rate * diffDate) * Tools.NormalDensity(d1) / (riskFactor * Data.Volatility * Math.Sqrt(diffDate));
        }
       
    }
}
