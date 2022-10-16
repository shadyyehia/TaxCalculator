using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public class Foreign : BaseVehicle
    {
        public override string GetVehicleType()
        {
            return "Foreign";
        }

        public Foreign()
        {
            isExempted = true;
        }
    }
}