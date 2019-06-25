using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;


namespace RiskAnalysis.Helpers
{
    public class Tools
    {
        public static double Phi(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x) / Math.Sqrt(2.0);
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * y);
        }

        static public double NormalDensity(double x)
        {

            return Math.Exp(-x*x*0.5)*1 / Math.Sqrt(2 * Math.PI); 
        }

        //récuperer la colonne d'excel sous forme d'un tableau 
        public static IEnumerable<T> columnToArray<T>(Excel.Worksheet xlWorksheet, int indexColumn)
        { 
            Type type = typeof(T);
            Excel.Range column = xlWorksheet.UsedRange.Columns[indexColumn];
            var arrayValues = (Array)column.Cells.Value;
            var arrayfilter = arrayValues.OfType<T>().ToArray();
            return arrayfilter;
        }
    }
}
