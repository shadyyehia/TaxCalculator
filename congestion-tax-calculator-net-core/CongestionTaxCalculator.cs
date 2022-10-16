using System;
using congestion.calculator.model;
using System.Linq;
using DBAccess.Models;

public class CongestionTaxCalculator : ITaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    TaxCalculatorParameters _cityParams { get; set; }

    /// <summary>
    /// CongestionTaxCalculator parametrized based on city
    /// </summary>
    /// <param name="_periodOfTimeWhenConsequentPasses">Max time period, if vehicle passed many times within (_consequentPassesInByMinutes) minutes </param>
    public CongestionTaxCalculator(TaxCalculatorParameters _params )
    {
        _cityParams = _params;
    }
    public int GetTax(IVehicle vehicle, DateTime[] dates)
    {
        if (dates == null || dates.Count() == 0)
            throw new Exception("Dates array is empty");

        //SH: Check if all dates are in same day
        DateTime dt = dates.FirstOrDefault();       
        bool notSameDay = dates.Any(x => x.Date != dt.Date);
        if (notSameDay)
            throw new Exception("Dates are not on the same day");

        DateTime intervalStart = dt;
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            //long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            //SH: BUG FIX
            TimeSpan ts = date- intervalStart;
            double diffInMillies = ts.TotalMilliseconds;


            //long minutes = diffInMillies / 1000 / 60;

            double minutes = diffInMillies / 1000 / 60;

            //if (minutes <= 60)
            if (minutes <= _cityParams.PeriodOfTimeWhenConsequentPasses)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }


    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }


    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        //String vehicleType = vehicle.GetVehicleType();
        //return vehicleType.Equals(Enums.TollFreeVehicles.Motorcycle.ToString()) ||
        //       vehicleType.Equals(Enums.TollFreeVehicles.Tractor.ToString()) ||
        //       vehicleType.Equals(Enums.TollFreeVehicles.Emergency.ToString()) ||
        //       vehicleType.Equals(Enums.TollFreeVehicles.Diplomat.ToString()) ||
        //       vehicleType.Equals(Enums.TollFreeVehicles.Foreign.ToString()) ||
        //       vehicleType.Equals(Enums.TollFreeVehicles.Military.ToString());
        return vehicle.isExempted;
    }

   

  
}