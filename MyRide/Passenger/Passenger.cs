using System;
using RideHailingApp.Location;


namespace RideHailingApp.Passenger
{
    public class Passenger
    {
        // Attributes
        public string Name { get; set; }
        public string PhoneNo { get; set; }

        // Constructor
        public Passenger(string name, string phoneNo)
        {
            Name = name;
            PhoneNo = phoneNo;
        }

        // Functions
        public void BookRide(string name, string ph, float startLat, float endLat, float startLong, float endlLand, string rideTyype)
        {
            string filePath = @"C:\My data\MyRideConsoleApplication\MyRide\rides.txt";

            Console.Write("Driver ID: ");
            int driverId = int.Parse(Console.ReadLine());

            // Create the file if it doesn't exist
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine($"Passenger name: {name}, PH : {ph}, StartingLocation :( {startLat},{endLat}), EndingLocation :( {endLat},{endlLand}), Ride Type: {rideTyype}, DriverId : {driverId}");
                }
            }
            else
            {
                // Write more data to the file
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"Passenger name: {name}, PH : {ph}, StartingLocation :( {startLat},{endLat}), EndingLocation :( {endLat},{endlLand}), Ride Type: {rideTyype}, DriverId : {driverId}");
                }
            }

            Console.WriteLine("Ride booked successfully!");
        }


        public void GiveRating()
        {
            Console.Write("Please enter your rating (1 to 5): ");
            string ratingStr = Console.ReadLine();
            int rating = 0;
            bool isNumeric = int.TryParse(ratingStr, out rating);

            if (isNumeric && rating >= 1 && rating <= 5)
            {
                Console.WriteLine("Thank you for your rating of " + rating);
            }
            else
            {
                Console.WriteLine("Invalid rating. Please enter a numeric value between 1 and 5.");
            }
        }
    }
}
