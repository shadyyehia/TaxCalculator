using congestion.calculator;
using congestion.calculator.model;
using DBAccess.Models;
using DBAccess;
using System;
using System.Data.SqlClient;
using System.Globalization;
using Xunit;

namespace congestion_tax_calculator_net_core.tests
{
    public class CongestionTaxCalculator_Tests
    {
        ITaxCalculator taxCalculator;
        public CongestionTaxCalculator_Tests()
        {
            //assum it's Gothenburg city params
            TaxCalculatorParameters _GothenBurg_Params = new TaxCalculatorParameters() {
                PeriodOfTimeWhenConsequentPasses = 60 };


            // pass city parameters
            taxCalculator = new CongestionTaxCalculator(_GothenBurg_Params);
        }


        [Theory]
        [InlineData("2013-01-14 21:00:00", 0)]
        [InlineData("2013-01-15 21:00:00", 0)]
        [InlineData("2013-02-07 06:23:27", 8)]
        [InlineData("2013-02-07 15:27:00", 13)]
        [InlineData("2013-02-08 06:27:00", 8)]
        [InlineData("2013-02-08 06:20:27", 8)]
        [InlineData("2013-02-08 14:35:00", 8)]
        [InlineData("2013-02-08 15:29:00", 13)]
        [InlineData("2013-02-08 15:47:00", 18)]
        [InlineData("2013-02-08 16:01:00", 18)]
        [InlineData("2013-02-08 16:48:00", 18)]
        [InlineData("2013-02-08 17:49:00", 13)]
        [InlineData("2013-02-08 18:29:00", 8)]
        [InlineData("2013-02-08 18:35:00", 0)]
        [InlineData("2013-03-26 14:25:00", 0)]
        [InlineData("2013-03-28 14:07:27", 0)]
        public void GetTollFree_HappyScenario(string passingDate,int expectedTollFee)
        {
            
          
            Car c = new Car(); 
            
            DateTime dt = DateTime.ParseExact(passingDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            int tollFee =taxCalculator.GetTollFee(dt, c);

            Assert.Equal(expectedTollFee, tollFee);

        }

        [Fact]
        public void GetTax_HappyScenario()
        {
           

            Car c = new Car();
            DateTime dt1 = DateTime.ParseExact("2013-02-08 06:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact("2013-02-08 07:18:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt3 = DateTime.ParseExact("2013-02-08 08:29:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime[] dateTimeList = new[] { dt1, dt2, dt3 }; 

            int totalFee = taxCalculator.GetTax(c, dateTimeList); //BUG: This returned 13, after fix it returns 21

            Assert.Equal(39, totalFee);

        }


        [Fact]
        public void GetTax_IfTwoDatesLessThan60Min()
        {
           

            Car c = new Car();
            DateTime dt1 = DateTime.ParseExact("2013-02-08 06:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//8
            DateTime dt2 = DateTime.ParseExact("2013-02-08 06:32:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//13
            DateTime dt3 = DateTime.ParseExact("2013-02-08 08:29:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//13
            DateTime[] dateTimeList = new[] { dt1, dt2, dt3 };

            int totalFee = taxCalculator.GetTax(c, dateTimeList); //BUG: This returned 13, after fix it returns 21

            Assert.Equal(26, totalFee);

        }

        [Fact]
        public void GetTax_IfNotSameDay()
        {
           

            Car c = new Car();
            DateTime dt1 = DateTime.ParseExact("2013-02-08 06:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact("2013-02-08 06:20:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt3 = DateTime.ParseExact("2013-02-09 08:29:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime[] dateTimeList = new[] { dt1, dt2, dt3 };

           


            Action act = () => taxCalculator.GetTax(c, dateTimeList);
            //assert
            Exception exception = Assert.Throws<Exception>(act);
            //The thrown exception can be used for even more detailed assertions.
            Assert.Equal("Dates are not on the same day", exception.Message);

        }


        [Fact]
        public void GetTax_IfWeekEnds()
        {
           

            Car c = new Car();
            DateTime dt1 = DateTime.ParseExact("2013-03-09 06:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//8
            DateTime dt2 = DateTime.ParseExact("2013-03-09 07:20:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//18
         
            DateTime[] dateTimeList = new[] { dt1, dt2 };

            int totalFee = taxCalculator.GetTax(c, dateTimeList); //BUG: This returned 13, after fix it returns 21

            Assert.Equal(0, totalFee);

        }


        [Fact]
        public void GetTax_IfTollFreeVehicle()
        {
           

            Tractor c = new Tractor();
            DateTime dt1 = DateTime.ParseExact("2013-03-05 06:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//8
            DateTime dt2 = DateTime.ParseExact("2013-03-05 07:20:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//18

            DateTime[] dateTimeList = new[] { dt1, dt2 };

            int totalFee = taxCalculator.GetTax(c, dateTimeList); //BUG: This returned 13, after fix it returns 21

            Assert.Equal(0, totalFee);

        }

        [Fact]
        public void GetTax_MoreThan60()
        {
           

            Car c = new Car();
            DateTime dt1 = DateTime.ParseExact("2013-03-05 06:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact("2013-03-05 07:01:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt3 = DateTime.ParseExact("2013-03-05 08:02:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt4 = DateTime.ParseExact("2013-03-05 09:03:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt5 = DateTime.ParseExact("2013-03-05 10:04:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt6 = DateTime.ParseExact("2013-03-05 11:05:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt7 = DateTime.ParseExact("2013-03-05 12:06:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt8 = DateTime.ParseExact("2013-03-05 13:07:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt9 = DateTime.ParseExact("2013-03-05 14:08:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt10 = DateTime.ParseExact("2013-03-05 15:09:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt11 = DateTime.ParseExact("2013-03-05 16:09:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt12 = DateTime.ParseExact("2013-03-05 17:09:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dt13 = DateTime.ParseExact("2013-03-05 18:09:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            DateTime[] dateTimeList = new[] { dt1, dt2,dt3,dt4,dt5,dt6,dt7,dt8,dt9,dt10,dt11,dt12 ,dt13};

            int totalFee = taxCalculator.GetTax(c, dateTimeList); //BUG: This returned 13, after fix it returns 21

            Assert.Equal(60, totalFee);

        }


        [Fact]
        public void GetTax_IfEmptyDatesArray()
        {
           

            Car c = new Car();
            DateTime[] dateTimeList = new DateTime[] { };




            Action act = () => taxCalculator.GetTax(c, dateTimeList);
            //assert
            Exception exception = Assert.Throws<Exception>(act);
            //The thrown exception can be used for even more detailed assertions.
            Assert.Equal("Dates array is empty", exception.Message);

        }


        [Fact]
        public void CreateVehicle_HappyScenario()
        {
            var vehicleFactory = new VehicleFactory();

            var vehicle =vehicleFactory.CreateVehicle("Car");

            Assert.Equal("Car", vehicle.GetVehicleType());
        }


    }
}
