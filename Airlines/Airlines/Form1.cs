namespace Airlines
{
    public partial class Form1 : Form
    {
        #region отвечает за дизайн Формы
        private Button currentButton;
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(60, 60, 90);
                    currentButton.ForeColor = Color.Gainsboro;

                }
            }
        }
        private void DisableButton()
        {
            foreach (Control item in panelMenu.Controls)
            {
                if (item.GetType() == typeof(Button) && item.Name != "buttonExit")
                {
                    item.BackColor = Color.FromArgb(51, 51, 76);
                    item.ForeColor = Color.Gainsboro;
                }
            }
        }
        private void DisableGroupBox()
        {
            foreach (GroupBox item in panel1.Controls)
                if (item.GetType() == typeof(GroupBox))
                {
                    item.Visible = false;
                    item.Size = new Size(830, 440);
                    item.Location = new Point(185, 65);

                }
        }
        private void ButtonPanelAircraft_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "Измените список транспорта";
            groupBoxAirctafts.Visible = true;
        }
        private void ButtonPanelBuyer_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "Зарегистрируйте покупателя";
            groupBoxBuyer.Visible = true;

        }

        private void ButtonPanelPassenger_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "Зарегистрируйте пассажира";
            groupBoxPassenger.Visible = true;
        }

        private void ButtonPanelFlight_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "Подтвердите рейс";
            groupBoxFlight.Visible = true;
        }

        private void ButtonPanelTicket_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "Информация о билете";
            groupBoxTicket.Visible = true;
        }


        #endregion
        DataBase DataBase = new DataBase();
        Airport airport = new Airport("Airlines", "Минск");
        Helicopter helicopter = new();
        Plane plane = new();
        Buyer buyer;
        Passenger passenger;
        Flight flight;
        Ticket ticket;
        string seat;
        public Form1()
        {
            InitializeComponent();
            Size = new Size(1010, 500);
        }
        private string CheckBox(ComboBox box) => box.SelectedItem.ToString();

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataBase.GetFlights(airport.DestinationСities, airport.CityAirport);
                DataBase.GetAircraft(Helicopter.helicopters);
                DataBase.GetAircraft(Plane.planes);

                //plane.ReadBaseAircraft();
                //helicopter.ReadBaseAircraft();
                //airport.ReadDestinationCity();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            labelLogo.Text = airport.Name;
            LabelTitle.Text = "Регистрация билетов на рейс";
            dateTimePickerFlight.Format = DateTimePickerFormat.Time;
            comboBoxFlightSelectAircraft.Items.Add("Самолеты");
            comboBoxFlightSelectAircraft.Items.Add("Вертолеты");
            comboBoxAddSelectAircraft.Items.Add("Самолеты");
            comboBoxAddSelectAircraft.Items.Add("Вертолеты");
            foreach (var item in airport.DestinationСities)
                listBoxFligtCity.Items.Add(item[0]);
        }


        // Кнопка отвечающая за регистрацию пассажира. Объявление объекта класса Passenger //
        private void Button1_Click(object sender, EventArgs e)
        {
            string docNumber = textBoxDoc.Text;
            string name = textBoxPassenName.Text;
            string lastName = textBox3PassenLName.Text;
            string phone = textBoxPassenPhone.Text;
            string mail = textBoxPassenMail.Text;
            passenger = new Passenger(name, lastName, phone, mail, docNumber);
            MessageBox.Show("Пассажир зарегистрирован", "Внимание!");
        }

        // Кнопка отвечающая за регистрации покупателя. Объявление объекта класса Buyer //
        private void Button2_Click(object sender, EventArgs e)
        {
            string address = textBoxAddress.Text;
            string name = textBoxBuyerName.Text;
            string lastName = textBoxLastName.Text;
            string phone = textBoxBuyerPhone.Text;
            string mail = textBoxBuyerMail.Text;
            buyer = new Buyer(name, lastName, phone, mail, address);
            MessageBox.Show("Покупатель зарегистрирован", "Внимание!");
        }

        // Кнопка для объявления объектов класса Flight и Ticket. //
        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxFligtCity.SelectedIndex < 0) throw new Exception("Требуется выбрать город назначения");
                else if (comboBoxFlightListAircraft.SelectedItem == null) throw new Exception("Требуется выбрать транспорт в списке");
                else
                {
                    // переменная хранящая время полета от города Аэропорта до города назначения
                    double flyTime = double.Parse(airport.DestinationСities[listBoxFligtCity.SelectedIndex][1]);
                    // переменная хранящая город назначения.
                    string destination = airport.DestinationСities[listBoxFligtCity.SelectedIndex][0];
                    // объявление объекта класса Flight;
                    flight = new Flight(dateTimePickerFlight.Value, flyTime, destination);
                    if (CheckBox(comboBoxFlightSelectAircraft) == "Самолеты")
                    {
                        // Добавление нового самолета в flight;
                        flight.SetAircraft(Plane.planes[comboBoxFlightListAircraft.SelectedIndex]);

                        // условия для возвращения значения цены выбранного класса места в самолете // 
                        if (listBoxFlightPriceSit.SelectedIndex == 0)
                            seat = "FirstClass";
                        else if (listBoxFlightPriceSit.SelectedIndex == 1)
                            seat = "BusinessClass";
                        else
                            seat = "EconomyClass";
                    }
                    else if (CheckBox(comboBoxFlightSelectAircraft) == "Вертолеты")
                    {
                        // Добавление нового вертолета в flight;
                        flight.SetAircraft(Helicopter.helicopters[comboBoxFlightListAircraft.SelectedIndex]);

                        // условия для возвращения значения цены выбранного класса места в вертолете // 
                        if (listBoxFlightPriceSit.SelectedIndex == 0)
                            seat = "PilotSeat";
                        else
                            seat = "SecondSeat";
                    }
                }

                // объявление объекта класса Ticket
                if (flight == null) throw new Exception("Требуется заполнить все поля рейса");
                else if (passenger == null) throw new Exception("Требуется зарегистрировать пассажира");
                else if (buyer == null) throw new Exception("Требуется зарегистрировать покупателя");
                else ticket = new Ticket(flight, passenger, buyer);

                #region Заполнение данных о билете в GroupBoxTicket
                labelDoc.Text = passenger.Info("Doc");
                labelFName.Text = passenger.Info("firstName");
                labelLName.Text = passenger.Info("lastName");

                labelBuyerFName.Text = buyer.Info("firstName");
                labelBuyerLName.Text = buyer.Info("lastName");

                labelDestinationCity.Text = flight.DestinationCity;
                labelDateTimeStart.Text = ticket.DateStart.ToString();
                labelNameAircraft.Text = flight.Aircraft.Info("NameAircraft");
                labelBortNumber.Text = flight.Aircraft.Info("BortNumber");

                labelPriceTicket.Text = ticket.PriceTicket(seat).ToString();
                #endregion
                MessageBox.Show("Регистрация прошла успешно", "Внимание!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка");
            }

        }
        // Кнопка отвечающая за добавление объектов planes и helicopters вводимых пользователем //  
        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {

                string name = textBoxNameAicraft.Text;
                string bortnumber = textBoxBortNumber.Text;
                double price = double.Parse(textBoxPriceFly.Text);
                if (comboBoxAddSelectAircraft.SelectedItem == null)
                    throw new Exception("Требуется выбрать вид транспортного средства");

                else if (CheckBox(comboBoxAddSelectAircraft) == "Самолеты")
                {
                    double firstClass = double.Parse(textBoxFirstClass.Text);
                    double businessClass = double.Parse(textBoxBussinessClass.Text);
                    double economyClass = double.Parse(textBoxEconomyClass.Text);
                    plane = new Plane(name, bortnumber, price, firstClass, businessClass, economyClass);
                    Plane.planes.Add(plane);
                    DataBase.AddAircraft(plane);
                }
                else if (CheckBox(comboBoxAddSelectAircraft) == "Вертолеты")
                {
                    double pilotSeat = double.Parse(textBoxFirstClass.Text);
                    double secondSeat = double.Parse(textBoxBussinessClass.Text);
                    helicopter = new Helicopter(pilotSeat, secondSeat, name, bortnumber, price);
                    Helicopter.helicopters.Add(helicopter);
                    DataBase.AddAircraft(helicopter);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка!");
            }

            ComboBoxAddSelectAircraft_SelectedIndexChanged(sender, e);
        }

        // Кнопка отвечающая за удаление выбранного летального аппарата planes или helicopters //
        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxAddSelectAircraft == null)
                    throw new Exception("Требуется выбрать вид летательного аппарата");
                else if (listBoxAircraftList.SelectedItem == null)
                    throw new Exception("Требуется выбрать объект в списке");
                else
                {
                    if (CheckBox(comboBoxAddSelectAircraft) == "Самолеты")
                    {
                        DataBase.DeleteAircraft(Plane.planes[listBoxAircraftList.SelectedIndex]);
                        Plane.planes.RemoveAt(listBoxAircraftList.SelectedIndex);

                    }
                    else if (CheckBox(comboBoxAddSelectAircraft) == "Вертолеты")
                    {
                        DataBase.DeleteAircraft(Helicopter.helicopters[listBoxAircraftList.SelectedIndex]);
                        Helicopter.helicopters.RemoveAt(listBoxAircraftList.SelectedIndex);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка!");
            }
            ComboBoxAddSelectAircraft_SelectedIndexChanged(sender, e);
        }
        // Кнопка отвечающая за сохранение билета в файл //
        private void Button7_Click(object sender, EventArgs e)
        {

            DataBase.WriteTickets(ticket, seat);
            ticket.SaveTicket(seat);
        }

        // Событие отвечающее за заполнение ComboBox списком летальных аппаратов //
        private void ComboBoxFlightSelectAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckBox(comboBoxFlightSelectAircraft) == "Самолеты")
            {
                comboBoxFlightListAircraft.Items.Clear();
                foreach (var item in Plane.planes)
                    comboBoxFlightListAircraft.Items.Add($"{item.Name}, {item.Bortnumber}");

            }
            else if (CheckBox(comboBoxFlightSelectAircraft) == "Вертолеты")
            {
                comboBoxFlightListAircraft.Items.Clear();
                foreach (var item in Helicopter.helicopters)
                    comboBoxFlightListAircraft.Items.Add($"{item.Name}, {item.Bortnumber}");
            }

        }
        // Событие comboBox отвечающая за заполнение ListBox с ценами за места //
        private void ComboBoxFlightListAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxFlightListAircraft.SelectedIndex;
            if (CheckBox(comboBoxFlightSelectAircraft) == "Самолеты")
            {
                listBoxFlightPriceSit.Items.Clear();
                // возвращается цена места зависимо от его класса и выбранного самолета из списка
                listBoxFlightPriceSit.Items.Add($"Первый класс: {Plane.planes[index].GetSeatPrice("FirstClass")}");
                listBoxFlightPriceSit.Items.Add($"Бизнесс-класс: {Plane.planes[index].GetSeatPrice("BusinessClass")}");
                listBoxFlightPriceSit.Items.Add($"Эконом-класс: {Plane.planes[index].GetSeatPrice("EconomyClass")}");
            }
            else if (CheckBox(comboBoxFlightSelectAircraft) == "Вертолеты")
            {
                listBoxFlightPriceSit.Items.Clear();
                // возвращается цена места зависимо от его класса и выбранного вертолета из списка 
                listBoxFlightPriceSit.Items.Add($"Место с пилотом: {Helicopter.helicopters[index].GetSeatPrice("PilotSeat")}");
                listBoxFlightPriceSit.Items.Add($"Место в кабине: {Helicopter.helicopters[index].GetSeatPrice("SecondSeat")}");
            }
        }

        // Событие comboBox, при котором в зависимости от выбора летательного аппарата формируются значения для ввода данных //
        private void ComboBoxAddSelectAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAircraftList.Items.Clear();
            if (CheckBox(comboBoxAddSelectAircraft) == "Самолеты")
            {

                foreach (Control item in groupBoxAirctafts.Controls)
                    item.Visible = true;
                labelPriceFirtsClass.Text = "Цена первого класса:";
                labelPriceBussinessClass.Text = "Цена бизнес-класса:";
                labelPriceEconomyClass.Text = "Цена эконом-класса:";
                foreach (var item in Plane.planes)
                    listBoxAircraftList.Items.Add($"{item.Name}, {item.Bortnumber}");
            }
            else if (CheckBox(comboBoxAddSelectAircraft) == "Вертолеты")
            {
                foreach (Control item in groupBoxAirctafts.Controls)
                    item.Visible = true;
                labelPriceEconomyClass.Visible = false;
                textBoxEconomyClass.Visible = false;
                labelPriceFirtsClass.Text = "Цена места с пилотом:";
                labelPriceBussinessClass.Text = "Цена места в кабине:";
                foreach (var item in Helicopter.helicopters)
                    listBoxAircraftList.Items.Add($"{item.Name}, {item.Bortnumber}");
            }
        }

        // Кнопка отвечающая за выход из формы и вызова метода перезаписи списков в файл //
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            plane.WriteBaseAircraft();
            helicopter.WriteBaseAircraft();
            Application.Exit();
        }

    }
}