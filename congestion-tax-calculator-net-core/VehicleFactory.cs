using congestion.calculator.Helpers;
using congestion.calculator.model;



public class VehicleFactory
{
    //sh
    public IVehicle CreateVehicle(string vehicleType)
    {
        if (vehicleType == Enums.Vehicles.Car.ToString())
        {
            return new Car();
        }
        else if (vehicleType == Enums.Vehicles.Diplomat.ToString())
        {
            return new Diplomat();
        }
        else if (vehicleType == Enums.Vehicles.Emergency.ToString())
        {
            return new Emergency();
        }
        else if (vehicleType == Enums.Vehicles.Foreign.ToString())
        {
            return new Foreign();
        }
        else if (vehicleType == Enums.Vehicles.Military.ToString())
        {
            return new Military();
        }
        else if (vehicleType == Enums.Vehicles.MotorBike.ToString())
        {
            return new Motorbike();
        }
        else if (vehicleType == Enums.Vehicles.Motorcycle.ToString())
        {
            return new Motorcycle();
        }
        else if (vehicleType == Enums.Vehicles.Tractor.ToString())
        {
            return new Tractor();
        }

        return new Car();

    }
}

