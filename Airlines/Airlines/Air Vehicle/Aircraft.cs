using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public abstract class Aircraft
    {
        string _name;
        string _bortnumber;
        double _priceTime;
        public string Name { get => _name; set => _name = value; }
        public string Bortnumber { get => _bortnumber; set => _bortnumber = value; }
        public double PriceTime // цена за час полета
        {
            get => _priceTime;
            set
            {
                if (value > 0) _priceTime = value;
                else throw new Exception("Цена за час полета не должна быть ниже или равна 0");
            }
        } 
        public Aircraft()
        {

        }
        public Aircraft(string Name, string Bortnumber, double PriceTime)
        {
            this.Name = Name;
            this.Bortnumber= Bortnumber;
            this.PriceTime = PriceTime;
        }
        // Метод возвращающий нужное значения в зависимости от параметра
        public abstract double GetSeatPrice(string Seats);
        public abstract void WriteBaseAircraft();
        public abstract void ReadBaseAircraft();
        public virtual string Info(string info)
        {

            return info switch
            {
                "NameAircraft" => _name,
                "BortNumber" => _bortnumber,
                _ => "",
            };
        }

    }
}
