using Newtonsoft.Json.Linq;

namespace Mercedes_Benz.io_Challenge.ViewModels
{
    public class SearchViewModel
    {
        private string[] _Model { get; set; }
        private string[] _Fuel { get; set; }
        private string[] _Transmission { get; set; }
        private string[] _Dealer { get; set; }
        public object Model {
            get => _Model;
            set => _Model = ConvertToArray(value);
        }

        public object Fuel {
            get => _Fuel;
            set => _Fuel = ConvertToArray(value);
        }
        public object Transmission {
            get => _Transmission;
            set => _Transmission = ConvertToArray(value);
        }

        public object Dealer {
            get => _Dealer;
            set => _Dealer = ConvertToArray(value); 
            
        }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }


        private string[] ConvertToArray(object input)
        {
            switch (input)
            {
                case string[] _:
                    return (string[])input;
                case JArray _:
                    return ((JArray) input).ToObject<string[]>();
                case string _:
                    return new[] { (string)input };
                default:
                    return null;
            }
        }

        public string[][] GetParameters() {
            return new[] {_Model, _Fuel, _Transmission, _Dealer};
        }

    }
}
