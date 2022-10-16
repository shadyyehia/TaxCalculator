using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.API.Controllers
{
    /// <summary>
    /// {VehicleType, Passing Dates for this vehicle }
    /// </summary>
    public class CalculatorInput
    {
        /// <summary>   
        /// The vehicle type, based on this type the taxes are calculated.\
        /// Available types: { Car, Motorbike, Motorcycle, Tractor, Diplomat, Emergency, Foreign, Military }.
        /// </summary>
        [Required(ErrorMessage ="Missing VehicleType")]
        public string VehicleType { get; set; }


        /// <summary>   
        /// The vehicle type, based on this type the taxes are calculated.\
        /// Available types: { Car, Motorbike, Motorcycle, Tractor, Diplomat, Emergency, Foreign, Military }.
        /// </summary>
        [Required(ErrorMessage = "Missing City")]
        public string City { get; set; }

        /// <summary>
        /// Passing dates for the vehicle.\
        /// String must be in format: yyyy-MM-dd HH:mm:ss .\
        /// The year must be 2013 or you will get zero. Example: 2013-02-08 15:47:00      
        /// </summary>
        [Required(ErrorMessage = "Missing Dates")]
        public List<string> Dates { get; set; } 
    }
}
