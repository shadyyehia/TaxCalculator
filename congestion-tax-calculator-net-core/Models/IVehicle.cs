using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.model
{
    public interface IVehicle
    {
        string GetVehicleType();

         bool isExempted { get; set; }
    }
}