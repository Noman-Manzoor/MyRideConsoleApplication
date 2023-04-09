using RideHailingApp.Location;
using RideHailingApp.Driver;
namespace RideHailingApp.Admin
{
    public class Admin
    {
        string filePath = @"C:\My data\MyRideConsoleApplication\MyRide\drivers.txt";

        // Attributes
        static List<Driver.Driver> drivers;
        static int idCount = 200;
        // Constructor
        public Admin()
        {
            drivers = new List<Driver.Driver>();
            drivers.Add(new Driver.Driver(100, "John Doe", 30, "Male", "123 Main St", "555-1234", new Location.Location(37, -122), new Vehicle.Vehicle("Bike", "Corolla", "24234"), new List<int> { 4, 5, 3 }, true));
            drivers.Add(new Driver.Driver(200, "Jane Smith", 25, "Female", "456 Elm St", "555-5678", new Location.Location(37, -122), new Vehicle.Vehicle("Honda", "Accord", "34355"), new List<int> { 5, 5, 4 }, false));
        }

        // Functions
        public void AddDriver()
        {
            //string path = @"../Data/drivers.txt";

            Console.WriteLine("Enter details for the new driver:");
            int id = ++idCount;
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Gender: ");
            string gender = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Vehicle Type: ");
            string type = Console.ReadLine();

            Console.Write("Vehicle Model: ");
            string model = Console.ReadLine();

            Console.Write("Vehicle License Plate: ");
            string licensePlate = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string ph = Console.ReadLine();

            bool available = true;
            Driver.Driver newDriver = new Driver.Driver(id, name, age, gender, address, ph, new Location.Location(0, 0), new Vehicle.Vehicle(type, model, licensePlate), new List<int>(new int[] { 3, 4, 5 }), available);



            // Create the file if it doesn't exist
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine($"{newDriver.ID},{newDriver.Name},{newDriver.CurrLocation.Latitude},{newDriver.CurrLocation.Longitude},{newDriver.Age},{newDriver.Gender},{newDriver.Address},{newDriver.PhoneNo},{newDriver.Vehicle.Type},{newDriver.Vehicle.Model},{newDriver.Vehicle.LicensePlate},{string.Join(",", newDriver.Ratings)}");
                }
            }
            else
            {
                // Write more data to the file
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"{newDriver.ID},{newDriver.Name},{newDriver.CurrLocation.Latitude},{newDriver.CurrLocation.Longitude},{newDriver.Age},{newDriver.Gender},{newDriver.Address},{newDriver.PhoneNo},{newDriver.Vehicle.Type},{newDriver.Vehicle.Model},{newDriver.Vehicle.LicensePlate},{string.Join(",", newDriver.Ratings)}");
                }
            }
            Console.WriteLine("Driver added successfully!");
        }



        public void updateDriver()
        {
            Console.WriteLine("Enter driver ID to update:");
            int id = Convert.ToInt32(Console.ReadLine());

            //// Find driver with given ID
            //Driver.Driver driver = drivers.Find(d => d.ID == id);
            Driver.Driver driver = new Driver.Driver();

            driver.ID = id;
            //if (driver == null)
            //{
            //    Console.WriteLine("Driver not found!");
            //    return;
            //}

            Console.WriteLine("Enter fields to update (leave empty to skip):");

            Console.Write("Name: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                driver.Name = name;
            }

            Console.Write("Age: ");
            string ageInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(ageInput))
            {
                int age = Convert.ToInt32(ageInput);
                driver.Age = age;
            }

            Console.Write("Gender: ");
            string gender = Console.ReadLine();
            if (!string.IsNullOrEmpty(gender))
            {
                driver.Gender = gender;
            }

            Console.Write("Address: ");
            string address = Console.ReadLine();
            if (!string.IsNullOrEmpty(address))
            {
                driver.Address = address;
            }

            Console.Write("Vehicle Type: ");
            string type = Console.ReadLine();
            if (!string.IsNullOrEmpty(type))
            {
                driver.Vehicle.Type = type;
            }

            Console.Write("Vehicle Model: ");
            string model = Console.ReadLine();
            if (!string.IsNullOrEmpty(model))
            {
                driver.Vehicle.Model = model;
            }

            Console.Write("Vehicle License Plate: ");
            string licensePlate = Console.ReadLine();
            if (!string.IsNullOrEmpty(licensePlate))
            {
                driver.Vehicle.LicensePlate = licensePlate;
            }

            // Read existing data from file
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // Find the line corresponding to the updated driver
            string updatedLine = $"{driver.ID},{driver.Name},{driver.Age},{driver.Gender},{driver.Address},{driver.PhoneNo},{driver.Vehicle.Type},{driver.Vehicle.Model},{driver.Vehicle.LicensePlate},{string.Join(",", driver.Ratings)}";
            int index = lines.FindIndex(line => line.StartsWith($"{driver.ID},"));

            if (index == -1)
            {
                Console.WriteLine("Driver not found in file!");
                return;
            }

            // Update the line in the list
            lines[index] = updatedLine;

            // Write the updated list back to the file
            File.WriteAllLines(filePath, lines);

            Console.WriteLine("Driver updated successfully!");
        }


        public void RemoveDriver()
        {
            Console.Write("Enter Driver ID to remove: ");
            int id = Convert.ToInt32(Console.ReadLine());

            List<string> lines = File.ReadAllLines(filePath).ToList();

            // Find the line corresponding to the updated driver
            int index = lines.FindIndex(line => line.StartsWith($"{id},"));

            if (index == -1)
            {
                Console.WriteLine("Driver not found in file!");
                return;
            }

            lines.RemoveAt(index);
       
            // Write the updated list back to the file
            File.WriteAllLines(filePath, lines);

            Console.WriteLine("Driver removed successfully!");
        }

        public void SearchDriver()
        {

            Console.WriteLine("Enter the search parameters:");

            Console.Write("Driver ID: ");
            string idStr = Console.ReadLine();
            int id = 0;
            if (!string.IsNullOrEmpty(idStr))
            {
                id = Convert.ToInt32(idStr);
            }

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            string ageStr = Console.ReadLine();
            int age = 0;
            if (!string.IsNullOrEmpty(ageStr))
            {
                age = Convert.ToInt32(ageStr);
            }

            Console.Write("Gender: ");
            string gender = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Vehicle Type: ");
            string type = Console.ReadLine();

            Console.Write("Vehicle Model: ");
            string model = Console.ReadLine();

            Console.Write("Vehicle License Plate: ");
            string licensePlate = Console.ReadLine();

            // Open the file and read the data using a StreamReader
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                var filteredDrivers = new List<Driver.Driver>();
                while ((line = sr.ReadLine()) != null)
                {
                    // Parse the line and create a new Driver object
                    string[] fields = line.Split(',');
                    int driverID = Convert.ToInt32(fields[0]);
                    string driverName = fields[1];
                    int driverAge = Convert.ToInt32(fields[2]);
                    string driverGender = fields[3];
                    string driverAddress = fields[4];
                    string vehicleType = fields[5];
                    string vehicleModel = fields[6];
                    string vehicleLicensePlate = fields[7];

                    var driver = new Driver.Driver();

                    driver.ID = driverID;
                    driver.Name= driverName;
                    driver.Age= driverAge;
                    driver.Gender= driverGender;
                    driver.Address= driverAddress;
                    driver.Vehicle.Type= vehicleType;
                    driver.Vehicle.Model= vehicleModel;
                    driver.Vehicle.LicensePlate = vehicleLicensePlate;

                    // Check if the driver matches the search criteria
                    if ((id == 0 || driver.ID == id) &&
                        (string.IsNullOrEmpty(name) || driver.Name == name) &&
                        (age == 0 || driver.Age == age) &&
                        (string.IsNullOrEmpty(gender) || driver.Gender == gender) &&
                        (string.IsNullOrEmpty(address) || driver.Address == address) &&
                        (string.IsNullOrEmpty(type) || driver.Vehicle.Type == type) &&
                        (string.IsNullOrEmpty(model) || driver.Vehicle.Model == model) &&
                        (string.IsNullOrEmpty(licensePlate) || driver.Vehicle.LicensePlate == licensePlate))
                    {
                        filteredDrivers.Add(driver);
                    }
                }

                // Print the search results
                if (filteredDrivers.Any())
                {
                    Console.WriteLine("Search Results:");
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Name\tAge\tGender\tV.Type\tV.Model\tV.License");
                    Console.WriteLine("-----------------------------------------------------------------------");
                    foreach (var driver in filteredDrivers)
                    {
                        Console.WriteLine($"{driver.Name}\t{driver.Age}\t{driver.Gender}\t{driver.Vehicle.Type}\t{driver.Vehicle.Model}\t{driver.Vehicle.LicensePlate}");
                    }
                    Console.WriteLine("-----------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("No drivers found with the given search parameters.");
                }
            }
        }




    }
}
    