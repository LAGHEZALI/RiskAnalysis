using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RiskAnalysis.Helpers;
using RiskAnalysis.Instruments;
using RiskAnalysis.MarketData;
using RiskAnalysis.Structures;
using Newtonsoft.Json;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest2
    {
        [Test]
        [TestCaseSource("TestCase")]
        public void Reference(DataTestCase testCase)
        {
            Data.InitMarketData(testCase.Vol,
                                testCase.Rate,
                                testCase.EvalDate);

            var pricingCfg = new PricingConfiguration(testCase.GreekTypes);
            var pricer = new Pricer();
            var results = pricer.Results(testCase.Option, pricingCfg);
            var json = JsonConvert.SerializeObject(results, Formatting.Indented);
            var msg = "The reference is generated";
            var isGenereted = true; 
            try
            {
                using (StreamWriter file =
                    File.CreateText(@"C:\Users\AHMED\source\repos\ConsoleApp1\ResultPricing.text"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, results);
                }
            }
            catch (Exception e)
            {
                isGenereted = false;
                msg =string.Format("The reference cannot be generated:{0}", e.Message);
            }
            finally
            {
                if (isGenereted == false)
                    Assert.Fail(msg);
                else
                    Assert.Pass(msg);
                    
            }
        }

        [Test]
        [TestCaseSource("TestCase")]
        public void Actual(DataTestCase testCase)
        {
            Data.InitMarketData(testCase.Vol,
                testCase.Rate,
                testCase.EvalDate);

            var pricingCfg = new PricingConfiguration(testCase.GreekTypes);
            var pricer = new Pricer();
            PricingResults expectedResults = null;
            var actualResults = pricer.Results(testCase.Option, pricingCfg);
            var error = string.Empty;
            try
            {
                using (StreamReader file = File.OpenText(@"C:\Users\AHMED\source\repos\ConsoleApp1\ResultPricing.text"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    expectedResults = (PricingResults)serializer.Deserialize(file, typeof(PricingResults));
                }
            }
            catch (Exception e)
            {
               Assert.Fail(error = string.Format("Not Deserialize result: {0}", e.Message));
            }

            if (actualResults.CompareResult(expectedResults)) Assert.Pass("No regression detected");
            else
                // save ecart
                Assert.Fail("regression detected");
            
        }

        private static DataTestCase TesCase()
        {
            return new DataTestCase()
            {
                GreekTypes = new List<GreekType>()
                {
                    GreekType.Delta,
                    GreekType.Gamma,
                    GreekType.Rho,
                    GreekType.Tetha,
                    GreekType.Vega,
                },
                Option = new OptionInstrument(100,
                                             new DateTime(1, 3, 2019),
                                              OptionType.Call,
                                              Way.Buy,Underlying.Petrol,
                                              new DateTime(1, 5, 2019)),
                Vol = 0.25,
                Rate = 0.05,
                EvalDate = new DateTime(1, 1, 2019),
            };
        }
    }

    public static class ExtensionMethod
    {
        public static bool CompareResult(this PricingResults actual, PricingResults expected)
        {
            if ((expected.Mtm == actual.Mtm) && actual.Results.CompareGreek(expected.Results)) return true; 
            else return false;
        }

        public static bool CompareGreek(this Dictionary<GreekType, double> actualGreeks, Dictionary<GreekType, double> expectedGreeks)
        {
            bool isEqual = true;
            foreach (var itemActual in actualGreeks)
                if (actualGreeks[itemActual.Key] != expectedGreeks[itemActual.Key]) isEqual = false;
                    
            return isEqual;
        }
    }

       
}
