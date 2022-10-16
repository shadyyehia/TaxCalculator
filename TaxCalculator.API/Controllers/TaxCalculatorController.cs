using congestion.calculator;
using congestion.calculator.model;
using DBAccess;
using DBAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Globalization;

namespace TaxCalculator.API.Controllers
{
    /// <summary>
    /// Tax Calculator main class
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaxCalculatorController : ControllerBase
    {
      
        private readonly ILogger<TaxCalculatorController> _logger;
        IDB_Manager _dbmanager;
        IConfiguration _config;


        /// <summary>
        /// Tax Calculator constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbManager"></param>
        public TaxCalculatorController(ILogger<TaxCalculatorController> logger, IDB_Manager dbManager, IConfiguration configuration )
        {
            _logger = logger;
            _dbmanager = dbManager;
            _config = configuration;

        }


        /// <summary>
        /// Calculate the total toll fee for one day
        /// </summary>
        /// <param name="_input">Calculator Input : { the vehicle type, date and time of all passes on one day } </param>
        /// <returns>ActionResult of string</returns>
        /// <remarks>
        ///  Sample request post data : { 
        ///"vehicleType": "Car",
        /// "city" : "Gothenburg",
        ///"dates": [
        ///  "2013-02-08 15:47:00","2013-02-08 16:50:00"
        ///]}
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public ActionResult CalculateTax([FromBody] CalculatorInput _input)
        {

            try
            {
                //Read city parameters from external store (sqllite)
                //sqllitemanager injected by the built in DI framework
                string connectionStr = _config.GetConnectionString("connectionStr");
                TaxCalculatorParameters _params = _dbmanager.ReadData(connectionStr,_input.City);


                // pass city parameters
                ITaxCalculator taxCalculator = new CongestionTaxCalculator(_params);
               
                //create vehicle object
                var vehicleFactory = new VehicleFactory();
                IVehicle c = vehicleFactory.CreateVehicle(_input.VehicleType);

                //parse dates from string to DateTime
                DateTime[] dateTimeList = _input.Dates.Select(dateString =>
                DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)).ToArray();

                //calculate taxes based on city parameters
                int totalFee = taxCalculator.GetTax(c, dateTimeList);

               
                return Ok(totalFee);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
    }
}