using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskAnalysis ;
using RiskAnalysis.Helpers;
using RiskAnalysis.Instruments;

namespace UnitTestProject1
{
    public class DataTestCase
    {
        public DataTestCase()
        {

        }
        public DataTestCase(List<GreekType> greekTypes,  double vol, double rate, OptionInstrument option,DateTime evalDate )
        {
            GreekTypes = greekTypes;
            Vol = vol;
            Rate = rate;
            Option = option;
            EvalDate = evalDate;
        }

        public List<GreekType> GreekTypes { get; set; }
        public OptionInstrument Option { get; set; }
        public double Vol { get; set; }
        public double Rate { get; set; }
       public DateTime EvalDate { get; set; }
        
    }
}
