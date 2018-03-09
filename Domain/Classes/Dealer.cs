using System.Collections.Generic;

namespace Domain.Classes
{
    public class Dealer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Closed { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
