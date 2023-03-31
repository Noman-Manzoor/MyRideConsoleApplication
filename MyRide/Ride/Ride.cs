using RideHailingApp.Driver;
using RideHailingApp.Passenger;
using RideHailingApp.Location;

namespace RideHailingApp.Ride
{
    public class Ride
    {
        // Attributes
        private Location.Location start_location;
        private Location.Location end_location;
        private int price;
        public Driver.Driver driver;
        public Passenger.Passenger passenger;

        // Constructor
        public Ride()
        {
            // Empty constructor
        }

        // Functions
        public void AssignPassenger(Passenger.Passenger passenger)
        {
            this.passenger = passenger;
        }

        public void AssignDriver(Driver.Driver driver)
        {
            double distance = Location.Location.CalculateDistance(start_location, driver.CurrLocation);

            if (driver.Availability)
            {
                this.driver = driver;
                driver.UpdateAvailability(false);
                Console.WriteLine("Driver assigned to the ride successfully.");
            }
            else
            {
                Console.WriteLine("Driver is not available for this ride.");
            }
        }


        public void GetLocations(Location.Location start_location, Location.Location end_location)
        {
            this.start_location = start_location;
            this.end_location = end_location;
        }

        public int  CalculatePrice(string vehicleType)
        {
            double fuelPrice = 2.5; 
            double fuelAverage = 0;
            double companyCommission = 0;

            switch (vehicleType.ToLower())
            {
                case "bike":
                    fuelAverage = 50;
                    companyCommission = 0.05;
                    break;
                case "rickshaw":
                    fuelAverage = 35;
                    companyCommission = 0.1;
                    break;
                case "car":
                    fuelAverage = 15;
                    companyCommission = 0.2;
                    break;
                default:
                    Console.WriteLine("Invalid vehicle type entered.");
                    return 0;
            }

            double distance = Location.Location.CalculateDistance(start_location, end_location);
            price = (int)Math.Round((distance * fuelPrice / fuelAverage) + (distance * companyCommission));
            return price;
        }
    }
}
