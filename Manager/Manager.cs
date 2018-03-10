using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Domain.Classes;

namespace MercedesManager
{
    public static class Manager
    {
        private static ConcurrentBag<Booking> _bookings = new ConcurrentBag<Booking>();
        private static ConcurrentBag<Dealer> _dealers = new ConcurrentBag<Dealer>();

        #region Initializer
        public static void Init(IEnumerable<Dealer> dealers, IEnumerable<Booking> bookings)
        {
            _bookings = new ConcurrentBag<Booking>(bookings);
            _dealers = new ConcurrentBag<Dealer>(dealers);
        }
        #endregion

        #region Basic Functions
        public static bool AddDealer(Dealer dealer)
        {
            try
            {
                _dealers.Add(dealer);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static string AddBooking(Booking booking)
        {
            try
            {
                Booking matchedBooking = _bookings.FirstOrDefault(x => x.PickupDate == booking.PickupDate && x.VehicleId == booking.VehicleId && x.CancelledAt == null);

                if (matchedBooking != null)
                    return "Duplicate";
                Vehicle vehicle = _dealers.Select(x => x.Vehicles).SelectMany(x => x).FirstOrDefault(x => x.Id == booking.VehicleId);
                if (vehicle == null)
                    return "Vehicle does not exist";
                string dayOfWeek = booking.PickupDate.DayOfWeek.ToString().ToLower();
                string time = booking.PickupDate.ToString("HHmm");
                if (vehicle.Availability.ContainsKey(dayOfWeek) && vehicle.Availability[dayOfWeek].Contains(time))
                {
                    _bookings.Add(booking);
                }
                else
                {
                    return "Schedule time is not available";
                }
            }
            catch (Exception)
            {
                return "Unknown Error";
            }
            return "OK";
        }

        public static IEnumerable<Dealer> GetDealers()
        {
            return _dealers.ToList();
        }

        public static IEnumerable<Booking> GetBookings()
        {
            return _bookings.ToList();
        }

        public static IEnumerable<Vehicle> GetVehicles()
        {
            return _dealers.Select(x => x.Vehicles).SelectMany(x => x).ToList();
        }
        #endregion

        #region Business-Specific Functions
        public static IEnumerable<Vehicle> GetVehiclesByParameter(string[][] input)
        {
            return _dealers
                .Where(x => input?[3] == null || input[3].Contains(x.Name))
                .Select(x => x.Vehicles)
                .Select(y => y
                    .Where(z =>
                        (input?[0] == null || input[0].Contains(z.Model)) &&
                        (input?[1] == null || input[1].Contains(z.Fuel)) &&
                        (input?[2] == null || input[2].Contains(z.Transmission)))).SelectMany(x => x).ToList();
        }
        public static IEnumerable<Dealer> FindDealersOrderedByDistance(string[][] input, (double Latitude, double Longitude) coordinates)
        {
            return _dealers
                .Where(z =>
                        input?[3] == null || input[3].Contains(z.Name))
                .Where(z =>
                    z.Vehicles.Any(y =>
                    (input?[0] == null || input[0].Contains(y.Model)) &&
                    (input?[1] == null || input[1].Contains(y.Fuel)) &&
                    (input?[2] == null || input[2].Contains(y.Transmission))))
                    .OrderBy(x => Math.Sqrt(Math.Pow(x.Latitude - coordinates.Latitude, 2) + Math.Pow(x.Longitude - coordinates.Longitude, 2))).ToList();
        }

        public static IEnumerable<Dealer> FindDealersInsidePolygon(string[][] input, (double Latitude, double Longitude)[] coordinates) {
            return _dealers.Where(z => 
            input?[3] == null || input[3].Contains(z.Name))
            .Where(z => z.Vehicles
                .Any(y => 
                (input?[0] == null || input[0].Contains(y.Model)) && 
                (input?[1] == null || input[1].Contains(y.Fuel)) && 
                (input?[2] == null || input[2].Contains(y.Transmission))))
                .Where(x => InsidePolygon(coordinates, x)).ToList();
            
        }

        private static bool InsidePolygon((double Latitude, double Longitude)[] coordinates, Dealer dealer) {
            bool result = false;
            int j = coordinates.Length - 1;
            for (int i = 0; i < coordinates.Length; i++) {
                if (coordinates[i].Latitude < dealer.Latitude && coordinates[j].Latitude >= dealer.Latitude || coordinates[j].Latitude < dealer.Latitude && coordinates[i].Latitude >= dealer.Latitude) {
                    if (coordinates[i].Longitude + (dealer.Latitude - coordinates[i].Latitude) / (coordinates[j].Latitude - coordinates[i].Latitude) * (coordinates[j].Longitude - coordinates[i].Longitude) < dealer.Longitude)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
        public static bool CancelBooking(string id, DateTime cancellation, string reason)
        {
            Booking booking = _bookings.FirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                return false;
            }
            booking.CancelledReason = reason;
            booking.CancelledAt = cancellation;
            return true;
        }
        #endregion
    }
}
