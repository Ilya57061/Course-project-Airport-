using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public class Helicopter : Aircraft
    {
        public static List<Helicopter> helicopters = new();

        double _pilotSeat;
        double _secondSeat;
        public double PilotSeat
        {
            get => _pilotSeat;
            set
            {
                if (value >= 0) _pilotSeat = value;
                else throw new Exception("Цена места с пилотом не должна быть меньше 0");
            }
        }
        public double SecondSeat
        {
            get => _secondSeat;
            set
            {
                if (value >= 0) _secondSeat = value;
                else throw new Exception("Цена места на борту не должна быть меньше 0");
            }
        }
        public Helicopter()
        {

        }
        public Helicopter(double PilotSeat, double SecondSeat, string Name, string Bortnumber, double PriceTime)
           : base(Name, Bortnumber, PriceTime)
        {
            this.PilotSeat = PilotSeat;
            this.SecondSeat = SecondSeat;
        }
        public override double GetSeatPrice(string seat)
        {
            return seat switch
            {
                "PilotSeat" => _pilotSeat,
                "SecondSeat" => _secondSeat,
                _ => 0,
            };
        }
        public override void WriteBaseAircraft()
        {
            using StreamWriter str = new(@"Aircrafts/Helicopters.txt", false);
            foreach (var item in helicopters)
                str.WriteLine($"{item.GetSeatPrice("PilotSeat")}|{item.GetSeatPrice("SecondSeat")}|{item.Name}|{item.Bortnumber}|{item.PriceTime}");
        }
        public override void ReadBaseAircraft()
        {
            int count = File.ReadAllLines(@"Aircrafts/Helicopters.txt").Length;
            using StreamReader str = new(@"Aircrafts/Helicopters.txt", Encoding.Default);
            for (int i = 0; i <= count - 1; i++)
            {
                string[] line = str.ReadLine().Split('|');
                helicopters.Add(new Helicopter(double.Parse(line[0]), double.Parse(line[1]), line[2], line[3], double.Parse(line[4])));
            }
        }
       
    }
}
