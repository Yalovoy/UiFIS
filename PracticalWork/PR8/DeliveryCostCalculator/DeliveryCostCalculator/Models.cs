using System;

namespace DeliveryCostCalculator
{
    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude:F4}, {Longitude:F4}";
        }
    }

    public class RouteInfo
    {
        public double DistanceKm { get; set; }
        public double DurationHours { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public Coordinates FromCoords { get; set; }
        public Coordinates ToCoords { get; set; }
        public string TransportType { get; set; }
        public double Cost { get; set; }
        public DateTime CalculationTime { get; set; } = DateTime.Now;
    }

    public class TransportRate
    {
        public string Name { get; set; }
        public double RatePerKm { get; set; }
        public double SpeedKmh { get; set; }

        public override string ToString()
        {
            return $"{Name} ({RatePerKm} руб./км)";
        }
    }
}