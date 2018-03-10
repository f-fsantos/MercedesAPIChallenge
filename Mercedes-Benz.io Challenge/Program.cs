using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Classes;
using MercedesManager;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;

namespace Mercedes_Benz.io_Challenge
{
    public class Program
    {
        public static void Main(string[] args) {
            JObject json = JObject.Parse(File.ReadAllText("dataset.json"));
            Dictionary<string, List<JObject>> jsonSerialized = json.ToObject<Dictionary<string, List<JObject>>>();
            List<Dealer> dealers = jsonSerialized["dealers"].Select(x => x.ToObject<Dealer>()).ToList();
            List<Booking> bookings = jsonSerialized["bookings"].Select(x => x.ToObject<Booking>()).ToList();
            Manager.Init(dealers, bookings);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
