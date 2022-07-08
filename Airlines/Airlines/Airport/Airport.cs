using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Airlines
{
    public class Airport
    {
        public List<string[]> DestinationСities = new List<string[]>();
        string _name;
        string _cityAirport;
        public string Name { get => _name; set => _name = value; }
        public string CityAirport { get => _cityAirport; set => _cityAirport = value; } // место нахождения аэропорта
        public Airport(string Name, string CityAirport)
        {
            this.Name = Name;
            this.CityAirport = CityAirport;

        }
        public void ReadDestinationCity()
        {
            int count = File.ReadAllLines($@"Flights/{_cityAirport}.txt").Length;
            using StreamReader str = new($@"Flights/{_cityAirport}.txt", Encoding.Default);
            for (int i = 0; i <= count - 1; i++)
                DestinationСities.Add(str.ReadLine().Split('|'));
        }
    }
}
