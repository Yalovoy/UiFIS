using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DeliveryCostCalculator
{
    public class DeliveryService
    {
        private readonly JavaScriptSerializer _jsonSerializer;
        private readonly List<TransportRate> _transportRates;
        private readonly Dictionary<string, Coordinates> _cityDatabase;

        private readonly string _userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";
        private DateTime _lastRequest = DateTime.MinValue;

        public event EventHandler<string> ErrorOccurred;
        public event EventHandler<string> StatusChanged;

        public DeliveryService()
        {
            _jsonSerializer = new JavaScriptSerializer();
            _transportRates = new List<TransportRate>
            {
                new TransportRate { Name = "Автомобиль", RatePerKm = 40, SpeedKmh = 60 },
                new TransportRate { Name = "Грузовик", RatePerKm = 60, SpeedKmh = 50 },
                new TransportRate { Name = "Мотоцикл", RatePerKm = 25, SpeedKmh = 70 }
            };

            _cityDatabase = new Dictionary<string, Coordinates>(StringComparer.OrdinalIgnoreCase)
            {
                { "москва", new Coordinates { Latitude = 55.7558, Longitude = 37.6173 } },
                { "moscow", new Coordinates { Latitude = 55.7558, Longitude = 37.6173 } },
                { "красная площадь", new Coordinates { Latitude = 55.7536, Longitude = 37.6215 } },
                
                { "санкт-петербург", new Coordinates { Latitude = 59.9343, Longitude = 30.3351 } },
                { "питер", new Coordinates { Latitude = 59.9343, Longitude = 30.3351 } },
                { "spb", new Coordinates { Latitude = 59.9343, Longitude = 30.3351 } },
                { "невский проспект", new Coordinates { Latitude = 59.9343, Longitude = 30.3351 } },
              
                { "казань", new Coordinates { Latitude = 55.8304, Longitude = 49.0661 } },
                { "kazan", new Coordinates { Latitude = 55.8304, Longitude = 49.0661 } },
                
                { "екатеринбург", new Coordinates { Latitude = 56.8389, Longitude = 60.6057 } },
                { "ekb", new Coordinates { Latitude = 56.8389, Longitude = 60.6057 } },
                
                { "новосибирск", new Coordinates { Latitude = 55.0084, Longitude = 82.9357 } },
                { "nsk", new Coordinates { Latitude = 55.0084, Longitude = 82.9357 } },
                { "красный проспект", new Coordinates { Latitude = 55.0415, Longitude = 82.9346 } },
                
                { "нижний новгород", new Coordinates { Latitude = 56.2965, Longitude = 43.9361 } },
                { "nnov", new Coordinates { Latitude = 56.2965, Longitude = 43.9361 } },
                
                { "самара", new Coordinates { Latitude = 53.2415, Longitude = 50.2212 } },
                { "samara", new Coordinates { Latitude = 53.2415, Longitude = 50.2212 } },
                
                { "ростов-на-дону", new Coordinates { Latitude = 47.2357, Longitude = 39.7015 } },
                { "rostov", new Coordinates { Latitude = 47.2357, Longitude = 39.7015 } },
                
                { "сочи", new Coordinates { Latitude = 43.6028, Longitude = 39.7342 } },
                { "sochi", new Coordinates { Latitude = 43.6028, Longitude = 39.7342 } },
                { "курортный проспект", new Coordinates { Latitude = 43.5855, Longitude = 39.7303 } },
                
                { "владивосток", new Coordinates { Latitude = 43.1155, Longitude = 131.8855 } },
                { "vladivostok", new Coordinates { Latitude = 43.1155, Longitude = 131.8855 } }
            };
        }

        public List<TransportRate> GetTransportRates() => _transportRates;

        public async Task<Coordinates> GeocodeAddressAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                OnErrorOccurred("Адрес не может быть пустым");
                return null;
            }

            if (TryParseCoordinates(address, out Coordinates coords))
            {
                OnStatusChanged($"Распознаны координаты: {coords}");
                return coords;
            }

            string lowerAddress = address.ToLower().Trim();
            foreach (var kvp in _cityDatabase)
            {
                if (lowerAddress.Contains(kvp.Key))
                {
                    OnStatusChanged($"Найдено в базе: {kvp.Value}");
                    return kvp.Value;
                }
            }

            OnStatusChanged($"Поиск через API: {address}");

            try
            {
                var timeSinceLast = DateTime.Now - _lastRequest;
                if (timeSinceLast.TotalSeconds < 1)
                {
                    await Task.Delay(1000 - (int)timeSinceLast.TotalMilliseconds);
                }

                string encodedAddress = Uri.EscapeDataString(address);
                string url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = _userAgent;
                request.Headers.Add("Accept-Language", "ru");
                request.Timeout = 10000;

                _lastRequest = DateTime.Now;

                using (var response = await Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse, request.EndGetResponse, null))
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    var results = _jsonSerializer.Deserialize<object[]>(json);

                    if (results != null && results.Length > 0)
                    {
                        var first = results[0] as Dictionary<string, object>;
                        if (first != null)
                        {
                            double lat = Convert.ToDouble(first["lat"], CultureInfo.InvariantCulture);
                            double lon = Convert.ToDouble(first["lon"], CultureInfo.InvariantCulture);

                            OnStatusChanged($"Найдены координаты: {lat:F4}, {lon:F4}");
                            return new Coordinates { Latitude = lat, Longitude = lon };
                        }
                    }

                    OnErrorOccurred($"Адрес не найден: {address}");
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка геокодирования: {ex.Message}");
            }

            return null;
        }

        public async Task<RouteInfo> BuildRouteAsync(Coordinates from, Coordinates to, string transportType)
        {
            if (from == null || to == null)
            {
                OnErrorOccurred("Координаты не могут быть null");
                return null;
            }

            OnStatusChanged($"Поиск маршрута через GraphHopper...");

            try
            {
                string fromStr = $"{from.Latitude.ToString(CultureInfo.InvariantCulture)},{from.Longitude.ToString(CultureInfo.InvariantCulture)}";
                string toStr = $"{to.Latitude.ToString(CultureInfo.InvariantCulture)},{to.Longitude.ToString(CultureInfo.InvariantCulture)}";

                string url = $"https://graphhopper.com/api/1/route?point={fromStr}&point={toStr}&vehicle=car&key=&locale=ru";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = _userAgent;
                request.Timeout = 15000;

                using (var response = await Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse, request.EndGetResponse, null))
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    var result = _jsonSerializer.Deserialize<Dictionary<string, object>>(json);

                    if (result != null && result.ContainsKey("paths"))
                    {
                        var paths = result["paths"] as object[];
                        if (paths != null && paths.Length > 0)
                        {
                            var firstPath = paths[0] as Dictionary<string, object>;
                            if (firstPath != null)
                            {
                                double distance = Convert.ToDouble(firstPath["distance"]) / 1000.0;
                                double time = Convert.ToDouble(firstPath["time"]) / 1000.0 / 3600.0;

                                TransportRate selectedTransport = _transportRates.Find(t => t.Name == transportType)
                                    ?? _transportRates[0];

                                double cost = distance * selectedTransport.RatePerKm;

                                OnStatusChanged($"Маршрут построен: {distance:F1} км");

                                return new RouteInfo
                                {
                                    DistanceKm = distance,
                                    DurationHours = time,
                                    FromCoords = from,
                                    ToCoords = to,
                                    TransportType = transportType,
                                    Cost = cost,
                                    CalculationTime = DateTime.Now
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnStatusChanged($"Ошибка GraphHopper: {ex.Message}. Использую прямое расстояние.");
            }

            return CalculateDirectRoute(from, to, transportType);
        }

        private RouteInfo CalculateDirectRoute(Coordinates from, Coordinates to, string transportType)
        {
            double distance = CalculateDistance(from, to);

            TransportRate selectedTransport = _transportRates.Find(t => t.Name == transportType)
                ?? _transportRates[0];

            double duration = distance / selectedTransport.SpeedKmh;
            double cost = distance * selectedTransport.RatePerKm;

            OnStatusChanged($"Использовано прямое расстояние: {distance:F1} км");

            return new RouteInfo
            {
                DistanceKm = distance,
                DurationHours = duration,
                FromCoords = from,
                ToCoords = to,
                TransportType = transportType,
                Cost = cost,
                CalculationTime = DateTime.Now
            };
        }

        private double CalculateDistance(Coordinates from, Coordinates to)
        {
            double R = 6371; // Радиус Земли в км

            double lat1 = from.Latitude * Math.PI / 180;
            double lat2 = to.Latitude * Math.PI / 180;
            double deltaLat = (to.Latitude - from.Latitude) * Math.PI / 180;
            double deltaLon = (to.Longitude - from.Longitude) * Math.PI / 180;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                      Math.Cos(lat1) * Math.Cos(lat2) *
                      Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        public async Task<RouteInfo> CalculateDeliveryAsync(string fromInput, string toInput, string transportType)
        {
            OnStatusChanged("Начинаем расчет...");

            var fromCoords = await GeocodeAddressAsync(fromInput);
            if (fromCoords == null)
            {
                OnErrorOccurred($"Не удалось определить координаты для: {fromInput}");
                return null;
            }

            var toCoords = await GeocodeAddressAsync(toInput);
            if (toCoords == null)
            {
                OnErrorOccurred($"Не удалось определить координаты для: {toInput}");
                return null;
            }

            var route = await BuildRouteAsync(fromCoords, toCoords, transportType);
            if (route != null)
            {
                route.FromAddress = fromInput;
                route.ToAddress = toInput;
            }

            return route;
        }

        private double GetRateForTransport(string transportType)
        {
            foreach (var rate in _transportRates)
                if (rate.Name == transportType) return rate.RatePerKm;
            return 40;
        }

        private bool TryParseCoordinates(string input, out Coordinates coords)
        {
            coords = null;
            if (string.IsNullOrWhiteSpace(input)) return false;

            try
            {
                string[] parts = input.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    string latStr = parts[0].Trim().Replace(',', '.');
                    string lonStr = parts[1].Trim().Replace(',', '.');

                    if (double.TryParse(latStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lat) &&
                        double.TryParse(lonStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
                    {
                        if (lat >= -90 && lat <= 90 && lon >= -180 && lon <= 180)
                        {
                            coords = new Coordinates { Latitude = lat, Longitude = lon };
                            return true;
                        }
                    }
                }
            }
            catch { }

            return false;
        }

        protected virtual void OnErrorOccurred(string message)
            => ErrorOccurred?.Invoke(this, message);

        protected virtual void OnStatusChanged(string message)
            => StatusChanged?.Invoke(this, message);
    }
}