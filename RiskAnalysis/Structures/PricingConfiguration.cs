using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskAnalysis.Helpers;
namespace RiskAnalysis.Structures
{
    public class PricingConfiguration
    {
        public List<GreekType> GreeksToCompute { get; set; }

        public PricingConfiguration(List<GreekType> greeksToCompute)
        {
            this.GreeksToCompute = greeksToCompute;
        }
    }
}
