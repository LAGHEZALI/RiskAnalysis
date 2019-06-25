using System;
using System.Collections.Generic;
using System.Linq;
using RiskAnalysis.Helpers;

namespace RiskAnalysis.InterpolatorsParam
{
    public static class Interpolators
    {
        public static double InterpolateForward(Underlying underlying, DateTime riskFactorDate)
        {
            var forwardCurve = MarketData.Data.LoadForwardCurve(underlying);
            return BackStepInterpolation(riskFactorDate, forwardCurve);
        }

        public static double InterpolateVolatility(Underlying underlying, DateTime riskFactorDate)
        {   
            return 0.0;
        }

        public static double InterpolateInterestRate(/* currency, */DateTime riskFactorDate)
        {
            return MarketData.Data.Rate;
        }

        private static double BackStepInterpolation( DateTime riskFactorDate, Dictionary<DateTime, double> curve)
        {
            var expirationDates= curve.Keys.ToList();
            var priceFutures = curve.Values.ToList(); 
            if (DateTime.Compare(riskFactorDate, expirationDates[0]) <= 0)
                return priceFutures[0];

            if (DateTime.Compare(riskFactorDate, expirationDates[expirationDates.Count - 1]) >= 0)
                return priceFutures[expirationDates.Count - 1];

            for (int i = 1; i < expirationDates.Count - 1; i++)
            {
                if (riskFactorDate <= expirationDates[i] && riskFactorDate > expirationDates[i - 1])
                    return priceFutures[i];
            }

            throw new Exception("la date est introuvable !");
        }
    }
}
