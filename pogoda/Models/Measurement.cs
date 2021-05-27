using System.Collections.Generic;

namespace pogoda.Models
{
    public class Measurement
    {
        public string? day { get; set; }
        public double value { get; set; }
    }

    public class StationMeasurement
    {
        public string? station { get; set; }
        public List<Measurement> temperature { get; set; }
        public List<Measurement> pressure { get; set; }
        public List<Measurement> moisture { get; set; }
        public List<Measurement> windSpeed { get; set; }
    }
}
