using System.Collections.Generic;
using RiskAnalysis.Helpers;
namespace RiskAnalysis.Structures
{
    public class PricingResults
    {
        public Dictionary<GreekType, double> Results { get; }
        public double Mtm { get; }

        public PricingResults(Dictionary<GreekType, double> results,double mtm)
        {
            Results = results;
            Mtm = mtm;
        }
    }
}
