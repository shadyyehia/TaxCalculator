using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public class Tractor : BaseVehicle
    {
        public override string GetVehicleType()
        {
            return "Tractor";
        }

        public Tractor()
        {
            isExempted = true;
        }
    }
}