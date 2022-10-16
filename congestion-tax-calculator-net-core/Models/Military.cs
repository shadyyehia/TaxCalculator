using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public class Military : BaseVehicle
    {
        public override string GetVehicleType()
        {
            return "Military";
        }

        public Military()
        {
            isExempted = true;
        }
    }
}