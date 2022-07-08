using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public class Ticket
    {
        public Flight flight;
        public Passenger passenger;
        public Buyer buyer;
        public DateTime DateStart { get => flight.DateStart; }
        public Ticket(Flight flight, Passenger passenger, Buyer buyer)
        {
            this.flight = flight;
            this.passenger = passenger;
            this.buyer = buyer;
        }
        public double PriceTicket(string seat) => flight.Aircraft.GetSeatPrice(seat) + flight.PriceFly; // цена места + стоимость полета
      //  Метод сохраняющий в файл информацию о билете //
        public void SaveTicket(string seat)
        {
            using StreamWriter str = new(@"Tickets/AllTickets.txt", true);
            str.WriteLine($"Рейс:|Город назначения: {flight.DestinationCity}|Дата и время вылета: {DateStart}" +
                $"|Название транспорта: {flight.Aircraft.Name}|Бортовой номер: {flight.Aircraft.Bortnumber}" +
                $"|Цена билета: {PriceTicket(seat)}");
            str.WriteLine($"Пассажир:|Имя: {passenger.FirstName}|Фамилия: {passenger.LastName}|Номер документа: {passenger.DocNumber}" +
                $"|Телефон: {passenger.Phone}|Электронная почта: {passenger.Mail}");
            str.WriteLine($"Покупатель:|Имя:{buyer.FirstName}|Фамилия: {buyer.LastName}|Телефон: {buyer.Phone}|Электронная почта: {buyer.Mail}|Домашний адрес: {buyer.Address}");
            str.WriteLine($"=========================================================================================================");
        }
    }
}
