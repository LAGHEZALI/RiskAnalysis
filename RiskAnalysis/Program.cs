using System;
using System.Collections.Generic;
using RiskAnalysis.Helpers;
using RiskAnalysis.Instruments;
using RiskAnalysis.MarketData;
using RiskAnalysis.Structures;

namespace RiskAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime expirationDate = DateTime.Parse("01/05/2019");
            DateTime riskFactorDate = DateTime.Parse("01/09/2019");
            DateTime paramDate = DateTime.Parse("01/04/2019");
            var option = new OptionInstrument(60,expirationDate,OptionType.Put,
                                              Way.Sell,Underlying.Petrol,riskFactorDate);
            Data.InitMarketData(0.25,0.05,paramDate);
            
            var  pricingcfg = new PricingConfiguration(
                new List<GreekType>
                {
                    GreekType.Delta,
                    GreekType.Gamma,
                    GreekType.Vega,
                    GreekType.Rho,
                    GreekType.Tetha
                });
            var pricer = new Pricer();
            var results = pricer.Results(option, pricingcfg);

            Console.WriteLine("Le prix de l'option :" + results.Mtm);
            foreach (var result in results.Results)
            {
                Console.WriteLine("La valeur de "+result.Key.ToString()+" : "+result.Value);
            }
    

            /*// prcing europeene option
            var maturity = DateTime.Parse("01/05/2019");
            var paramDate = DateTime.Parse("01/04/2019");
            var riskFctorDate = DateTime.Parse("01/09/2019");
            var diif = (paramDate-maturity).TotalDays; 
            var option = new OptionInstrument(60, maturity);
            var interpolator = new InterpolatorFuturePrice("CL", riskFctorDate);
            var riskFator = interpolator.InterpolationBackStepProcess(); 
            var optionData = new OptionMarketData(riskFator, paramDate, 0.05, 0.25);

            var pricer = new OptionPricer(option, optionData,"call");
           

            var listGreeks = new List<string>()
            {
              "Delta",
              "Vega",
              "Theta",
              "Rho",
              "Gamma"
            };

            PrintResult print = new PrintResult(pricer,true,true,listGreeks);
            print.Print();
            */

            // InitMarketData
            // Construction of Product + PricingConfiguration
            // Call the method Price

        }
    }
}
