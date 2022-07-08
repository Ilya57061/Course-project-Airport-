using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public class Plane : Aircraft
    {
        public static List<Plane> planes = new();
        double _firstClass;
        double _businessClass;
        double _economyClass;
        public double FirstClass
        {
            get => _firstClass;
            set
            {
                if (value >= 0) _firstClass = value;
                else throw new Exception("Цена первого класса не должна быть меньше 0");
            }
        }
        public double BusinessClass
        {
            get => _businessClass;
            set
            {
                if (value >= 0) _businessClass = value;
                else throw new Exception("Цена бизнес-класса не должна быть меньше 0");
            }
        }
        public double EconomyClass
        {
            get => _economyClass;
            set
            {
                if (value >= 0) _economyClass = value;
                else throw new Exception("Цена эконом-класса не должна быть меньше 0");
            }
        }
        public Plane()
        {

        }
        public Plane(string Name, string Bortnumber, double PriceTime, double FirstClass, double BusinessClass, double EconomyClass)
            : base(Name, Bortnumber, PriceTime)
        {
            this.FirstClass = FirstClass;
            this.BusinessClass = BusinessClass;
            this.EconomyClass= EconomyClass;
            this.PriceTime = PriceTime;
        }
        public override double GetSeatPrice(string seatClass)
        {
            return seatClass switch
            {
                "FirstClass" => _firstClass,
                "BusinessClass" => _businessClass,
                "EconomyClass" => _economyClass,
                _ => 0,
            };
        }
        public override void ReadBaseAircraft()
        {
            int count = File.ReadAllLines(@"Aircrafts/Planes.txt").Length;
            using StreamReader str = new(@"Aircrafts/Planes.txt", Encoding.Default);
            for (int i = 0; i <= count - 1; i++)
            {
                string[] line = str.ReadLine().Split('|');
                planes.Add(new Plane(line[3], line[4], double.Parse(line[5]), double.Parse(line[0]), double.Parse(line[1]), double.Parse(line[2])));
            }
        }

        public override void WriteBaseAircraft()
        {
            using StreamWriter str = new(@"Aircrafts/Planes.txt", false);
            foreach (var item in planes)
                str.WriteLine($"{item.GetSeatPrice("FirstClass")}|{item.GetSeatPrice("BusinessClass")}|{item.GetSeatPrice("EconomyClass")}|{item.Name}|{item.Bortnumber}|{item.PriceTime}");
        }
    }
}
