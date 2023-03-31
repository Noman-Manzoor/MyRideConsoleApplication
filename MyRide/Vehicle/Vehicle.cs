namespace  RideHailingApp.Vehicle
{
    public class Vehicle
    {
        public string Type { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }

        public Vehicle(string v1, string v2, string v3)
        {
            this.Type = v1;
            this.Model = v2;
            this.LicensePlate = v3;
        }
    }
}