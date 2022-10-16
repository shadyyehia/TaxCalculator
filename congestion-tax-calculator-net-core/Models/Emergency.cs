using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public class Emergency : BaseVehicle
    {
        public override string GetVehicleType()
        {
            return "Emergency";
        }
        public Emergency()
        {
            isExempted = true;
        }
    }
}