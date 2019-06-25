using System;
using RiskAnalysis.Helpers;
namespace RiskAnalysis.Instruments
{
    public class OptionInstrument
    {
        //Quantity
        //Currency  
        public OptionInstrument(double strike, DateTime expirationDate,
                                OptionType type, Way sens,Underlying sousJacent,
                                DateTime riskFactorDate)
        {
            Strike = strike;
            ExpirationDate = expirationDate;
            Type = type;
            Sens = sens;
            SousJacent = sousJacent;
            RiskFactorDate = riskFactorDate; 
        }
        public double Strike { get; set; }
        public DateTime ExpirationDate { get; set; }
        public OptionType Type { get; set; }
        public Way Sens { get; set; }
        public Underlying SousJacent { get; set; }
        public DateTime RiskFactorDate { get; set; }

    }
}
