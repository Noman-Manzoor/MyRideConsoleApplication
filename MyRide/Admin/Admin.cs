using RideHailingApp.Location;
using RideHailingApp.Driver;
namespace RideHailingApp.Admin
{
    public class Admin
    {
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

            string ph = "";
            bool available = true;
            Driver.Driver newDriver = new Driver.Driver(id, name, age, gender, address, ph, new Location.Location(0, 0), new Vehicle.Vehicle(type, model, licensePlate), new List<int>(new int[] { 3, 4, 5 }), available);
            drivers.Add(newDriver);

            Console.WriteLine("Driver added successfully!");
        }

        public void updateDriver()
        {
            Console.WriteLine("Enter driver ID to update:");
            int id = Convert.ToInt32(Console.ReadLine());

            // Find driver with given ID
            Driver.Driver driver = drivers.Find(d => d.ID == id);

            if (driver == null)
            {
                Console.WriteLine("Driver not found!");
                return;
            }

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

            Console.WriteLine("Driver updated successfully!");
        }

        public void RemoveDriver()
        {
            Console.Write("Enter Driver ID to remove: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Driver.Driver driverToRemove = drivers.Find(driver => driver.ID == id);

            if (driverToRemove == null)
            {
                Console.WriteLine("Driver with ID {0} not found!", id);
                return;
            }

            drivers.Remove(driverToRemove);
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

            var filteredDrivers = drivers.Where(driver =>
                (id == 0 || driver.ID == id) &&
                (string.IsNullOrEmpty(name) || driver.Name == name) &&
                (age == 0 || driver.Age == age) &&
                (string.IsNullOrEmpty(gender) || driver.Gender == gender) &&
                (string.IsNullOrEmpty(address) || driver.Address == address) &&
                (string.IsNullOrEmpty(type) || driver.Vehicle.Type == type) &&
                (string.IsNullOrEmpty(model) || driver.Vehicle.Model == model) &&
                (string.IsNullOrEmpty(licensePlate) || driver.Vehicle.LicensePlate == licensePlate));

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
    