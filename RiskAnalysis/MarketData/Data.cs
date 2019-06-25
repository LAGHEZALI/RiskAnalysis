using RiskAnalysis.Helpers;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace RiskAnalysis.MarketData
{
    public static class Data
    {
        public static double Rate { get; set; }
        public static double Volatility { get; set; }
        public static Dictionary<DateTime, double> ForwardCurve { get;  }
        public static DateTime ParamDate { get; set; }
        
        public static void InitMarketData(double volatility, double rate,DateTime paramDate)
        {
            Volatility = volatility;
            Rate = rate;
            ParamDate = paramDate;
        }
        public static Dictionary<DateTime, double> LoadForwardCurve(Underlying underlying)
        {
            //load from excel

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlworkbook = xlApp.Workbooks.Open(
                @"D:\Workspace\VS2019\ConsoleApp1\futureData\FutureData.xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Excel.Worksheet xlWorkSheet;
            switch (underlying)
            {
                case Underlying.Petrol:
                    xlWorkSheet = (Excel.Worksheet)xlworkbook.Worksheets.get_Item(1);
                    break;
                case Underlying.BaseMetal:
                    xlWorkSheet = (Excel.Worksheet)xlworkbook.Worksheets.get_Item(2);
                    break;
                //etc
                //.
                //.
                default:
                    throw new Exception("Le categorie de sous-jacent est incorrect");
            }
            DateTime[] expirationDates = (DateTime[])Tools.columnToArray<DateTime>(xlWorkSheet, 2);
            double[] priceFutures = (double[])Tools.columnToArray<double>(xlWorkSheet, 3);
            Dictionary<DateTime,double> forwardCurve = new Dictionary<DateTime, double>();
            int i = 0; 
            foreach (var expirationDate in expirationDates)
            {
              forwardCurve.Add(expirationDate , priceFutures[i]);
              i++;
            }
            return forwardCurve;
        }
    }
}
