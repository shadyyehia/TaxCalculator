using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public class BaseVehicle: IVehicle
    {
        public bool isExempted { get; set; } = false;
      
        public virtual string GetVehicleType()
        {
            return "";
        }

    }
}