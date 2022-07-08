namespace Airlines
{
    public partial class Form1 : Form
    {
        #region �������� �� ������ �����
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
            LabelTitle.Text = "�������� ������ ����������";
            groupBoxAirctafts.Visible = true;
        }
        private void ButtonPanelBuyer_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "��������������� ����������";
            groupBoxBuyer.Visible = true;

        }

        private void ButtonPanelPassenger_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "��������������� ���������";
            groupBoxPassenger.Visible = true;
        }

        private void ButtonPanelFlight_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "����������� ����";
            groupBoxFlight.Visible = true;
        }

        private void ButtonPanelTicket_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            DisableGroupBox();
            LabelTitle.Text = "���������� � ������";
            groupBoxTicket.Visible = true;
        }


        #endregion
        DataBase DataBase = new DataBase();
        Airport airport = new Airport("Airlines", "�����");
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
                DataBase.GetFlights(airport.Destination�ities, airport.CityAirport);
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
            LabelTitle.Text = "����������� ������� �� ����";
            dateTimePickerFlight.Format = DateTimePickerFormat.Time;
            comboBoxFlightSelectAircraft.Items.Add("��������");
            comboBoxFlightSelectAircraft.Items.Add("���������");
            comboBoxAddSelectAircraft.Items.Add("��������");
            comboBoxAddSelectAircraft.Items.Add("���������");
            foreach (var item in airport.Destination�ities)
                listBoxFligtCity.Items.Add(item[0]);
        }


        // ������ ���������� �� ����������� ���������. ���������� ������� ������ Passenger //
        private void Button1_Click(object sender, EventArgs e)
        {
            string docNumber = textBoxDoc.Text;
            string name = textBoxPassenName.Text;
            string lastName = textBox3PassenLName.Text;
            string phone = textBoxPassenPhone.Text;
            string mail = textBoxPassenMail.Text;
            passenger = new Passenger(name, lastName, phone, mail, docNumber);
            MessageBox.Show("�������� ���������������", "��������!");
        }

        // ������ ���������� �� ����������� ����������. ���������� ������� ������ Buyer //
        private void Button2_Click(object sender, EventArgs e)
        {
            string address = textBoxAddress.Text;
            string name = textBoxBuyerName.Text;
            string lastName = textBoxLastName.Text;
            string phone = textBoxBuyerPhone.Text;
            string mail = textBoxBuyerMail.Text;
            buyer = new Buyer(name, lastName, phone, mail, address);
            MessageBox.Show("���������� ���������������", "��������!");
        }

        // ������ ��� ���������� �������� ������ Flight � Ticket. //
        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxFligtCity.SelectedIndex < 0) throw new Exception("��������� ������� ����� ����������");
                else if (comboBoxFlightListAircraft.SelectedItem == null) throw new Exception("��������� ������� ��������� � ������");
                else
                {
                    // ���������� �������� ����� ������ �� ������ ��������� �� ������ ����������
                    double flyTime = double.Parse(airport.Destination�ities[listBoxFligtCity.SelectedIndex][1]);
                    // ���������� �������� ����� ����������.
                    string destination = airport.Destination�ities[listBoxFligtCity.SelectedIndex][0];
                    // ���������� ������� ������ Flight;
                    flight = new Flight(dateTimePickerFlight.Value, flyTime, destination);
                    if (CheckBox(comboBoxFlightSelectAircraft) == "��������")
                    {
                        // ���������� ������ �������� � flight;
                        flight.SetAircraft(Plane.planes[comboBoxFlightListAircraft.SelectedIndex]);

                        // ������� ��� ����������� �������� ���� ���������� ������ ����� � �������� // 
                        if (listBoxFlightPriceSit.SelectedIndex == 0)
                            seat = "FirstClass";
                        else if (listBoxFlightPriceSit.SelectedIndex == 1)
                            seat = "BusinessClass";
                        else
                            seat = "EconomyClass";
                    }
                    else if (CheckBox(comboBoxFlightSelectAircraft) == "���������")
                    {
                        // ���������� ������ ��������� � flight;
                        flight.SetAircraft(Helicopter.helicopters[comboBoxFlightListAircraft.SelectedIndex]);

                        // ������� ��� ����������� �������� ���� ���������� ������ ����� � ��������� // 
                        if (listBoxFlightPriceSit.SelectedIndex == 0)
                            seat = "PilotSeat";
                        else
                            seat = "SecondSeat";
                    }
                }

                // ���������� ������� ������ Ticket
                if (flight == null) throw new Exception("��������� ��������� ��� ���� �����");
                else if (passenger == null) throw new Exception("��������� ���������������� ���������");
                else if (buyer == null) throw new Exception("��������� ���������������� ����������");
                else ticket = new Ticket(flight, passenger, buyer);

                #region ���������� ������ � ������ � GroupBoxTicket
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
                MessageBox.Show("����������� ������ �������", "��������!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "������");
            }

        }
        // ������ ���������� �� ���������� �������� planes � helicopters �������� ������������� //  
        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {

                string name = textBoxNameAicraft.Text;
                string bortnumber = textBoxBortNumber.Text;
                double price = double.Parse(textBoxPriceFly.Text);
                if (comboBoxAddSelectAircraft.SelectedItem == null)
                    throw new Exception("��������� ������� ��� ������������� ��������");

                else if (CheckBox(comboBoxAddSelectAircraft) == "��������")
                {
                    double firstClass = double.Parse(textBoxFirstClass.Text);
                    double businessClass = double.Parse(textBoxBussinessClass.Text);
                    double economyClass = double.Parse(textBoxEconomyClass.Text);
                    plane = new Plane(name, bortnumber, price, firstClass, businessClass, economyClass);
                    Plane.planes.Add(plane);
                    DataBase.AddAircraft(plane);
                }
                else if (CheckBox(comboBoxAddSelectAircraft) == "���������")
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
                MessageBox.Show($"{ex.Message}", "������!");
            }

            ComboBoxAddSelectAircraft_SelectedIndexChanged(sender, e);
        }

        // ������ ���������� �� �������� ���������� ���������� �������� planes ��� helicopters //
        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxAddSelectAircraft == null)
                    throw new Exception("��������� ������� ��� ������������ ��������");
                else if (listBoxAircraftList.SelectedItem == null)
                    throw new Exception("��������� ������� ������ � ������");
                else
                {
                    if (CheckBox(comboBoxAddSelectAircraft) == "��������")
                    {
                        DataBase.DeleteAircraft(Plane.planes[listBoxAircraftList.SelectedIndex]);
                        Plane.planes.RemoveAt(listBoxAircraftList.SelectedIndex);

                    }
                    else if (CheckBox(comboBoxAddSelectAircraft) == "���������")
                    {
                        DataBase.DeleteAircraft(Helicopter.helicopters[listBoxAircraftList.SelectedIndex]);
                        Helicopter.helicopters.RemoveAt(listBoxAircraftList.SelectedIndex);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "������!");
            }
            ComboBoxAddSelectAircraft_SelectedIndexChanged(sender, e);
        }
        // ������ ���������� �� ���������� ������ � ���� //
        private void Button7_Click(object sender, EventArgs e)
        {

            DataBase.WriteTickets(ticket, seat);
            ticket.SaveTicket(seat);
        }

        // ������� ���������� �� ���������� ComboBox ������� ��������� ��������� //
        private void ComboBoxFlightSelectAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckBox(comboBoxFlightSelectAircraft) == "��������")
            {
                comboBoxFlightListAircraft.Items.Clear();
                foreach (var item in Plane.planes)
                    comboBoxFlightListAircraft.Items.Add($"{item.Name}, {item.Bortnumber}");

            }
            else if (CheckBox(comboBoxFlightSelectAircraft) == "���������")
            {
                comboBoxFlightListAircraft.Items.Clear();
                foreach (var item in Helicopter.helicopters)
                    comboBoxFlightListAircraft.Items.Add($"{item.Name}, {item.Bortnumber}");
            }

        }
        // ������� comboBox ���������� �� ���������� ListBox � ������ �� ����� //
        private void ComboBoxFlightListAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxFlightListAircraft.SelectedIndex;
            if (CheckBox(comboBoxFlightSelectAircraft) == "��������")
            {
                listBoxFlightPriceSit.Items.Clear();
                // ������������ ���� ����� �������� �� ��� ������ � ���������� �������� �� ������
                listBoxFlightPriceSit.Items.Add($"������ �����: {Plane.planes[index].GetSeatPrice("FirstClass")}");
                listBoxFlightPriceSit.Items.Add($"�������-�����: {Plane.planes[index].GetSeatPrice("BusinessClass")}");
                listBoxFlightPriceSit.Items.Add($"������-�����: {Plane.planes[index].GetSeatPrice("EconomyClass")}");
            }
            else if (CheckBox(comboBoxFlightSelectAircraft) == "���������")
            {
                listBoxFlightPriceSit.Items.Clear();
                // ������������ ���� ����� �������� �� ��� ������ � ���������� ��������� �� ������ 
                listBoxFlightPriceSit.Items.Add($"����� � �������: {Helicopter.helicopters[index].GetSeatPrice("PilotSeat")}");
                listBoxFlightPriceSit.Items.Add($"����� � ������: {Helicopter.helicopters[index].GetSeatPrice("SecondSeat")}");
            }
        }

        // ������� comboBox, ��� ������� � ����������� �� ������ ������������ �������� ����������� �������� ��� ����� ������ //
        private void ComboBoxAddSelectAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAircraftList.Items.Clear();
            if (CheckBox(comboBoxAddSelectAircraft) == "��������")
            {

                foreach (Control item in groupBoxAirctafts.Controls)
                    item.Visible = true;
                labelPriceFirtsClass.Text = "���� ������� ������:";
                labelPriceBussinessClass.Text = "���� ������-������:";
                labelPriceEconomyClass.Text = "���� ������-������:";
                foreach (var item in Plane.planes)
                    listBoxAircraftList.Items.Add($"{item.Name}, {item.Bortnumber}");
            }
            else if (CheckBox(comboBoxAddSelectAircraft) == "���������")
            {
                foreach (Control item in groupBoxAirctafts.Controls)
                    item.Visible = true;
                labelPriceEconomyClass.Visible = false;
                textBoxEconomyClass.Visible = false;
                labelPriceFirtsClass.Text = "���� ����� � �������:";
                labelPriceBussinessClass.Text = "���� ����� � ������:";
                foreach (var item in Helicopter.helicopters)
                    listBoxAircraftList.Items.Add($"{item.Name}, {item.Bortnumber}");
            }
        }

        // ������ ���������� �� ����� �� ����� � ������ ������ ���������� ������� � ���� //
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            plane.WriteBaseAircraft();
            helicopter.WriteBaseAircraft();
            Application.Exit();
        }

    }
}