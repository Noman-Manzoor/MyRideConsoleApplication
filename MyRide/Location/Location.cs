namespace RideHailingApp.Location
{
    public class Location
    {
        // Attributes
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        // Constructor
        public Location(float latitude, float longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        // Methods
        public void SetLocation(float latitude, float longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public static double CalculateDistance(Location location1, Location location2)
        {
            double lat1 = location1.Latitude;
            double lon1 = location1.Longitude;
            double lat2 = location2.Latitude;
            double lon2 = location2.Longitude;

            double theta = lon1 - lon2;
            double dist = Math.Sin(DegreesToRadians(lat1)) * Math.Sin(DegreesToRadians(lat2)) + Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) * Math.Cos(DegreesToRadians(theta));
            dist = Math.Acos(dist);
            dist = RadiansToDegrees(dist);
            dist = dist * 60 * 1.1515 * 1.609344; // Conversion to kilometers

            return dist;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private static double RadiansToDegrees(double radians)
        {
            return radians * 180.0 / Math.PI;
        }

    }
}