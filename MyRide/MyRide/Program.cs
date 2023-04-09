using System.Net;
using System.Reflection;
using System.Xml.Linq;
using RideHailingApp.Location;
using RideHailingApp.Admin;
using RideHailingApp.Driver;
using RideHailingApp.Ride;
using RideHailingApp.Passenger;
using RideHailingApp.Vehicle;
using System;
using Newtonsoft.Json;


namespace MyRide
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read existing data from file
            string filePath = @"C:\My data\MyRideConsoleApplication\MyRide\drivers.txt";
            int option;
            Console.ForegroundColor = ConsoleColor.Green;

            do
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("             WELCOME TO MYRIDE");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1. Book a Ride");
                Console.WriteLine("2. Enter as Driver");
                Console.WriteLine("3. Enter as Admin");
                Console.WriteLine("4. Stop Program ");

                Console.WriteLine("Press 1 to 3 to select an option:");
                option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Phone no: ");
                    string phoneNo = Console.ReadLine();

                    Console.Write("Enter Start Location: ");
                    string[] startLocationInput = Console.ReadLine().Split(',');
                    float startLatitude = float.Parse(startLocationInput[0]);
                    float startLongitude = float.Parse(startLocationInput[1]);
                    Location startLocation = new Location(startLongitude, startLatitude);

                    Console.Write("Enter End Location: ");
                    string[] endLocationInput = Console.ReadLine().Split(',');
                    float endLatitude = float.Parse(endLocationInput[0]);
                    float endLongitude = float.Parse(endLocationInput[1]);
                    Location endLocation = new Location(endLongitude, endLatitude);

                    Console.Write("Enter Ride Type: ");
                    string rideType = Console.ReadLine();

                    Ride newRide = new Ride();
                    newRide.AssignPassenger(new Passenger(name, phoneNo));
                    newRide.GetLocations(startLocation, endLocation);

                    Driver nearestAvailableDriver = SearchNearestAvailableDriver(startLocation, rideType);
                    if(nearestAvailableDriver != null)
                    {
                        newRide.AssignDriver(nearestAvailableDriver);

                        int price = newRide.CalculatePrice(rideType);
                        Console.WriteLine("-------------------- THANK YOU ------------------");
                        Console.WriteLine("Price for this ride is: " + price);
                        Console.Write("Enter ‘Y’ if you want to Book the ride, enter ‘N’ if you want to cancel operation: ");
                        string confirmation = Console.ReadLine();

                        if (confirmation.ToUpper() == "Y")
                        {
                            newRide.passenger.BookRide(name, phoneNo, startLatitude, startLongitude, endLatitude, endLongitude, rideType);
                            newRide.passenger.GiveRating();
                            Console.WriteLine("Happy Travel...!");
                        }
                        else
                        {
                            Console.WriteLine("Ride cancelled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No driver available with "+ rideType);
                    }

                    static Driver SearchNearestAvailableDriver(Location startLocation, string rideType)
                    {
                        // Assume drivers is a list of available drivers
                        List<Driver> drivers = new List<Driver>();

                        drivers.Add(new Driver(1, "John", 30, "M", "123 Main St", "555-1234", new Location(1, 1), new Vehicle("Car", "Honda Civic", "ABC123"), new List<int> { 3, 4, 5 }, true));
                        drivers.Add(new Driver(2, "Jane", 25, "F", "456 Elm St", "555-5678", new Location(2, 2), new Vehicle("Bike", "Harley Davidson", "XYZ789"), new List<int> { 4, 5 }, true));
                        drivers.Add(new Driver(3, "Bob", 40, "M", "789 Oak St", "555-9012", new Location(3, 3), new Vehicle("Rickshaw", "Suzuki Bolan", "DEF456"), new List<int> { 3, 4 }, false));

                        Driver nearestDriver = null;
                        double smallestDistance = double.MaxValue;

                        foreach (Driver driver in drivers)
                        {
                            if (driver.Availability && driver.Vehicle.Type.ToLower() == rideType.ToLower())
                            {
                                double distance = Location.CalculateDistance(startLocation, driver.CurrLocation);
                                if (distance < smallestDistance)
                                {
                                    smallestDistance = distance;
                                    nearestDriver = driver;
                                }
                            }
                        }

                        return nearestDriver;
                    }
                }

               

                else if (option == 2)
                {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("            WELCOME TO Driver Portal");
                    Console.WriteLine("-------------------------------------------");

                    Console.WriteLine("Enter Your ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Your Name: ");
                    string name = Console.ReadLine();

                    bool foundDriver = false;

                   
                    List<string> drivers = File.ReadAllLines(filePath).ToList();

                    foreach(var driver in drivers )
                    {
                        string[] tempDriver = driver.Split(',');
                        if(Convert.ToInt32(tempDriver[0]) == id && tempDriver[1].ToString().ToLower().Trim() == name.ToLower().Trim())
                        {
                            //Driver driverObj = JsonConvert.DeserializeObject<Driver>(driver);
                            Driver driver1= new Driver();
                            foundDriver = true;

                            Console.WriteLine("Hello " + tempDriver[1] + "!");

                            Console.WriteLine("Enter your current Location: ");

                            string[] locationInput = Console.ReadLine().Split(',');
                            float x = float.Parse(locationInput[0].Trim());
                            float y = float.Parse(locationInput[1].Trim());
                            Location currentLocation = new Location(x, y);
                            driver1.UpdateLocation(currentLocation);

                            int temp;
                            do
                            {
                                Console.WriteLine("1. Change availability ");
                                Console.WriteLine("2. Change Location ");
                                Console.WriteLine("3. Exit as Driver ");
                                Console.WriteLine("Choose one from above options.");

                                temp = Convert.ToInt32(Console.ReadLine());

                                if (temp == 1)
                                {
                                    Console.WriteLine("Are you available? Enter a boolean value (True or False):");
                                    string userInput = Console.ReadLine();
                                    bool myBool = bool.Parse(userInput);
                                    driver1.UpdateAvailability(myBool);
                                }
                                else if (temp == 2)
                                {
                                    Console.WriteLine("Update your Location: ");
                                    locationInput = Console.ReadLine().Split(',');
                                    x = float.Parse(locationInput[0].Trim());
                                    y = float.Parse(locationInput[1].Trim());
                                    currentLocation.SetLocation(x, y);
                                    driver1.UpdateLocation(currentLocation);
                                }

                            } while (temp != 3);
                        }
                       
                    }

                    

                    if (!foundDriver)
                    {
                        Console.WriteLine("Error! You are not an authenticated user of this application.");
                    }
                }

                else if (option == 3)
                {
                    Admin newAdmin = new Admin();
                    Console.WriteLine("1. Add Driver");
                    Console.WriteLine("2. Remove Driver");
                    Console.WriteLine("3. Update Driver");
                    Console.WriteLine("4. Search Driver");
                    Console.WriteLine("5. Exit as Admin");

                    Console.WriteLine();

                    Console.WriteLine("Choose one from above options.");

                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        newAdmin.AddDriver();
                    }

                    else if (choice == 2)
                    {
                        newAdmin.RemoveDriver();
                    }

                    else if (choice == 3)
                    {
                        
                        newAdmin.updateDriver();
                    }
                    else if (choice == 4)
                    {
                        newAdmin.SearchDriver();
                    }
                }
            } while (option != 4);
        }

    }
}


