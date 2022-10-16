using congestion.calculator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ITaxCalculator
{
    int GetTax(IVehicle vehicle, DateTime[] dates);
    int GetTollFee(DateTime date, IVehicle vehicle);
}

