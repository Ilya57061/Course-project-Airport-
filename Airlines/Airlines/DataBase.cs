using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Airlines
{
    public class DataBase
    {
        private string _connectString = "Server = DESKTOP-VT57MSN\\SQLEXPRESS;Database=AirlinesOOP;trusted_connection=true; Encrypt=False;TrustServerCertificate=False";
        public void GetAircraft(List<Helicopter> helicopters)
        {
            using (SqlConnection con = new(_connectString))
            {
                string getListHelicopters = "select * from Helicopters";
                SqlCommand cmd = new SqlCommand(getListHelicopters, con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    helicopters.Add(new Helicopter(Convert.ToDouble((decimal)dataReader["PilotSeat"]), Convert.ToDouble((decimal)dataReader["SecondSeat"]),
                        (string)dataReader["Name"], (string)dataReader["Bortnumber"], Convert.ToDouble((decimal)dataReader["PriceTime"])));
                }   
                dataReader.Close();
            }
        }
        public void GetAircraft(List<Plane> planes)
        {
            using (SqlConnection con = new(_connectString))
            {
                string getListHelicopters = "select * from Planes";
                SqlCommand cmd = new SqlCommand(getListHelicopters, con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    planes.Add(new Plane(
                        (string)dataReader["Name"], (string)dataReader["Bortnumber"], Convert.ToDouble((decimal)dataReader["PriceTime"]),
                        Convert.ToDouble((decimal)dataReader["FirstClass"]), Convert.ToDouble((decimal)dataReader["BusinessClass"]), Convert.ToDouble((decimal)dataReader["EconomyClass"])));
                }
                dataReader.Close();
            }
        }
        public void GetFlights(List<string[]> DestinationСities, string CityAirport)
        {
            using (SqlConnection con = new(_connectString))
            {
                string getListHelicopters = $"select * from Flights where [CityAirport] like '{CityAirport}' ";
                SqlCommand cmd = new SqlCommand(getListHelicopters, con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    DestinationСities.Add(new string[] { (string)dataReader["DestinationCity"], Convert.ToString((double)dataReader["FlyTime"]) });
                }
                dataReader.Close();
            }
        }
        public void WriteTickets(Ticket ticket, string seat)
        {
            using (SqlConnection con = new(_connectString))
            {
                con.Open();
                string WriteTicket = $"insert into Tickets values ('{ticket.flight.DestinationCity}','{ticket.flight.DateStart}'," +
                    $"'{ticket.flight.Aircraft.Name}','{ticket.flight.Aircraft.Bortnumber}','{ticket.PriceTicket(seat)}'," +
                    $"'{ticket.passenger.FirstName}','{ticket.passenger.LastName}','{ticket.passenger.DocNumber}','{ticket.passenger.Phone}','{ticket.passenger.Mail}'," +
                    $"'{ticket.buyer.FirstName}','{ticket.buyer.LastName}','{ticket.buyer.Address}','{ticket.buyer.Phone}','{ticket.buyer.Mail}')";
                SqlCommand cmd = new SqlCommand(WriteTicket, con);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddAircraft(Plane Plane)
        {
            using (SqlConnection con = new(_connectString))
            {
                con.Open();
                string WriteAircraft = $"insert into Planes values ('{Plane.Bortnumber}','{Plane.Name}'" +
                    $",'{Plane.PriceTime}','{Plane.FirstClass}','{Plane.BusinessClass}','{Plane.EconomyClass}')";
                SqlCommand cmd = new SqlCommand(WriteAircraft, con);
                cmd.ExecuteNonQuery();
            }

        }
        public void AddAircraft(Helicopter Helicopter)
        {
            using (SqlConnection con = new(_connectString))
            {
                con.Open();
                string WriteAircraft = $"insert into Helicopters values ('{Helicopter.Bortnumber}','{Helicopter.Name}'" +
                    $",'{Helicopter.PriceTime}','{Helicopter.PilotSeat}','{Helicopter.SecondSeat}')";
                SqlCommand cmd = new SqlCommand(WriteAircraft, con);
                cmd.ExecuteNonQuery();
            }

        }
        public void DeleteAircraft(Plane Plane)
        {
            using (SqlConnection con = new(_connectString))
            {
                con.Open();
                string WriteAircraft = $"delete from Planes where Bortnumber like '{Plane.Bortnumber}'";
                SqlCommand cmd = new SqlCommand(WriteAircraft, con);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteAircraft(Helicopter Helicopter)
        {
            using (SqlConnection con = new(_connectString))
            {
                con.Open();
                string WriteAircraft = $"delete from Helicopters where Bortnumber like '{Helicopter.Bortnumber}'";
                SqlCommand cmd = new SqlCommand(WriteAircraft, con);
                cmd.ExecuteNonQuery();
            }

        }

    }
}
