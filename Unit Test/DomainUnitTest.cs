using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Test
{
    [TestClass]
    public class DomainUnitTest
    {
        [TestMethod]
        public void GetAndSetBookingPropertiesTest()
        {
            Booking booking = new Booking {
                VehicleId = "123",
                CancelledAt = new DateTime(2018, 10, 22),
                CancelledReason = "no show",
                PickupDate = new DateTime(2018, 10, 23),
                Id = "aaaa",
                FirstName = "abc",
                LastName = "bcd",
                CreatedAt = new DateTime(2018, 09, 22)};

            Assert.AreEqual("123", booking.VehicleId);
            Assert.AreEqual(new DateTime(2018, 10, 22), booking.CancelledAt);
            Assert.AreEqual("no show", booking.CancelledReason);
            Assert.AreEqual(new DateTime(2018, 10, 23), booking.PickupDate);
            Assert.AreEqual("aaaa", booking.Id);
            Assert.AreEqual("abc", booking.FirstName);
            Assert.AreEqual("bcd", booking.LastName);
            Assert.AreEqual(new DateTime(2018, 09, 22), booking.CreatedAt);
        }
        [TestMethod]
        public void GetAndSetVehiclePropertiesTest() {
            Vehicle vehicle = new Vehicle {
                Id = "123123123",
                Model = "Class A",
                Fuel = "Petrol",
                Transmission = "Automatic",
                Availability = new Dictionary<string, List<string>> {{"monday", new List<string> {"1000", "1030"}}}};

            Assert.AreEqual("123123123",vehicle.Id);
            Assert.AreEqual("Class A", vehicle.Model);
            Assert.AreEqual("Petrol", vehicle.Fuel);
            Assert.AreEqual("Automatic", vehicle.Transmission);
            Assert.IsTrue(vehicle.Availability.ContainsKey("monday"));
            Assert.IsFalse(vehicle.Availability.ContainsKey("tuesday"));
            Assert.IsTrue(vehicle.Availability["monday"].Contains("1000"));
            Assert.IsTrue(vehicle.Availability["monday"].Contains("1030"));
            Assert.IsFalse(vehicle.Availability["monday"].Contains("1100"));

        }

        [TestMethod]
        public void GetAndSetDealerPropertiesTest() {
            Dealer dealer = new Dealer {
                Id = "123",
                Name = "aaaa",
                Latitude = 23.02,
                Longitude = 21.02,
                Closed = new List<string> {"monday"},
                Vehicles = new List<Vehicle> {new Vehicle {Id = "abcd", Model = "B"}}
            };

            Assert.AreEqual("123", dealer.Id);
            Assert.AreEqual("aaaa", dealer.Name);
            Assert.AreEqual(23.02, dealer.Latitude);
            Assert.AreEqual(21.02, dealer.Longitude);
            Assert.AreNotEqual(21.02, dealer.Latitude);
            Assert.AreNotEqual(23.02, dealer.Longitude);
            Assert.IsTrue(dealer.Closed.Contains("monday"));
            Assert.IsFalse(dealer.Closed.Contains("tuesday"));
            Assert.IsTrue(dealer.Vehicles.Count(x => x.Id == "abcd") > 0);
            Assert.IsTrue(dealer.Vehicles.Count(x => x.Id == "ddd") == 0);
            Assert.IsTrue(dealer.Vehicles.Count(x => x.Model == "B") > 0);
        }
    }
}
