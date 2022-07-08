using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public class Flight
    {
        DateTime _dateStart;
        Aircraft _aircraft;
        double _flyTime;
        string _destinationCity;
        public DateTime DateStart { get => _dateStart; set => _dateStart = value; }
        public Aircraft Aircraft { get => _aircraft; }
        public double FlyTime
        {
            get => _flyTime;
            set
            {
                if (value >= 0) _flyTime = value;
                else throw new Exception("Время полета не может быть меньше 0");
            }
        } // время полета
        public Flight(DateTime DateStart, double FlyTime, string DestinationCity)
        {
            this.DateStart = DateStart;
            this.FlyTime = FlyTime;
            this.DestinationCity = DestinationCity;
        }
        public string DestinationCity { get => _destinationCity; set => _destinationCity = value; } // город назначения
        public double PriceFly { get => FlyTime * Aircraft.PriceTime; } // цена полета
  
        public void SetAircraft(Plane air) => _aircraft = air;
        public void SetAircraft(Helicopter air) => _aircraft = air;
    }
}
