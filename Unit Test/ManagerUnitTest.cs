using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Domain.Classes;
using MercedesManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Unit_Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ManagerUnitTest {
        public List<Dealer> dealers;
        public List<Booking> bookings;
        [TestInitialize]
        public void SetUp() {
            JObject json = JObject.Parse(File.ReadAllText("dataset.json"));
            Dictionary<string, List<JObject>> jsonSerialized = json.ToObject<Dictionary<string, List<JObject>>>();
            dealers = jsonSerialized["dealers"].Select(x => x.ToObject<Dealer>()).ToList();
            bookings = jsonSerialized["bookings"].Select(x => x.ToObject<Booking>()).ToList();
            Manager.Init(dealers, bookings);
        }
        [TestMethod]
        public void ConstructorTest() {
            Manager.Init(dealers, bookings);
        }

        [TestMethod]
        public void GetDealersTest() {
            List<Dealer> dealersList = (List<Dealer>) Manager.GetDealers();
            Assert.AreEqual(dealersList.Count, dealers.Count);
            Assert.IsTrue(dealersList.Contains(dealers[0]));
        }
        [TestMethod]
        public void GetBookingsTest()
        {
            List<Booking> bookingsList = (List<Booking>)Manager.GetBookings();
            Assert.AreEqual(bookingsList.Count, bookings.Count);
            Assert.IsTrue(bookingsList.Contains(bookings[0]));
        }

        [TestMethod]
        public void GetVehicles() {
            List<Vehicle> vehicles = (List<Vehicle>) Manager.GetVehicles();
            Assert.AreEqual(dealers.Select(x => x.Vehicles).SelectMany(x => x).Count(), vehicles.Count);
        }
        [TestMethod]
        public void AddDealerTest()
        {
            Dealer dealer = new Dealer {Closed = new List<string> {"monday", "tuesday"}, Id = "abc", Latitude = 23.11, Longitude = 21.99, Vehicles = null, Name = "MB Faro"};
            Assert.IsTrue(Manager.AddDealer(dealer));
            Dealer newDealer = Manager.GetDealers().FirstOrDefault(x => x.Id == "abc");
            Assert.IsNotNull(newDealer);
            Assert.AreEqual(newDealer, dealer);
            Assert.AreEqual(newDealer.Name, dealer.Name);
        }

        [TestMethod]
        public void FindDealersByDistanceTest() {
            string[][] input = new string[4][];
            List<Dealer> dealersList = Manager.FindDealersOrderedByDistance(input, (19.3, 20.0)).ToList();
            Assert.AreEqual(3, dealersList.Count);
        }
    }
}
