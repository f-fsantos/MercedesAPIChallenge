using System.Collections.Generic;

namespace Domain.Classes
{
    public class Vehicle
    {
        public string Id { get; set; }    
        public string Model { get; set; }    
        public string Fuel { get; set; }    
        public string Transmission { get; set; }    
        public Dictionary<string, List<string>> Availability { get; set; }    
    }
}
