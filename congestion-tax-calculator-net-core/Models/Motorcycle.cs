using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public class Motorcycle : BaseVehicle
    {
        public override string GetVehicleType()
        {
            return "Motorcycle";
        }

        public Motorcycle()
        {
            isExempted = true;
        }
    }
}