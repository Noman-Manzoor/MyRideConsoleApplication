using RideHailingApp.Vehicle;
using RideHailingApp.Location;
namespace RideHailingApp.Driver
{
    public class Driver
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public Location.Location CurrLocation { get; set; }
        public Vehicle.Vehicle Vehicle { get; set; }
        public List<int> Ratings { get; set; }
        public bool Availability { get; set; }

        public Driver(int id, string name, int age, string gender, string address, string ph, Location.Location location, Vehicle.Vehicle vehicle, List<int> ratings, bool availabality)
        {
            this.ID = id;
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
            this.Address = address;
            this.PhoneNo = ph;
            this.CurrLocation = location;
            this.Vehicle = vehicle;
            this.Ratings = ratings;
            this.Availability = availabality;
        }

        public Driver()
        {
            this.ID = 0;
            this.Name = "";
            this.Age = 0;
            this.Gender = "";
            this.Address = "";
            this.PhoneNo = "";
            this.CurrLocation = new Location.Location(0, 0);
            this.Vehicle = new Vehicle.Vehicle("", "","");
            this.Ratings = new List<int>();
            this.Availability = false;
        }

        public void UpdateAvailability(bool isAvailable)
        {
            Availability = isAvailable;
        }

        public double GetRating()
        {
            if (Ratings == null || Ratings.Count == 0)
            {
                return 0;
            }

            return Ratings.Average();
        }

        public void UpdateLocation(Location.Location newLocation)
        {
            CurrLocation = newLocation;
        }
    }


    
}