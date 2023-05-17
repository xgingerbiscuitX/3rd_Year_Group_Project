using BusinessLayer;
using BusinessEntities;
using SoftwareEngineeringTeam1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using System.Drawing;

namespace SoftwareEngineeringT1
{
    public partial class UC_CheckInGuest : UserControl
    {
        private IAccessHandler Model;
        private Test parent;

        public UC_CheckInGuest(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.parent = parent;
            this.Model = Model;
            Refresh();
            InsertData();
        }

        private void Refresh()
        {
            Model.refreshGuestList();
            Model.refreshReservationList();
        }

        private void InsertData() 
        {
            try
            {
                Reservation r = Model.CurrentReservation;
                this.listView.Items.Add(new TwoColumnData("Reservation No:", r.ReservationNo.ToString()));
                this.listView.Items.Add(new TwoColumnData("First Name:", r.Name));
                this.listView.Items.Add(new TwoColumnData("Last Name:", r.LName));
                this.listView.Items.Add(new TwoColumnData("Start Date:", r.StartDate.ToString()));
                this.listView.Items.Add(new TwoColumnData("End Date:", r.EndDate.ToString()));
                this.listView.Items.Add(new TwoColumnData("Status:", r.Status.ToString()));
                this.listView.Items.Add(new TwoColumnData("Email:", r.Email));
                this.listView.Items.Add(new TwoColumnData("Address 1:", r.Address1));
                this.listView.Items.Add(new TwoColumnData("Address 2:", r.Address2));
                this.listView.Items.Add(new TwoColumnData("Address 3:", r.Address3));
                this.listView.Items.Add(new TwoColumnData("Phone No:", r.Phone));
                this.listView.Items.Add(new TwoColumnData("Special Request:", r.SpecialReq));

                foreach (Room room in Model.roomList)
                {
                    if (room.RoomType == Model.CurrentReservation.RoomType && room.Availability.Equals("Available") && room.Status.Equals("Vacant"))
                    {
                        ListBox_Rooms.Items.Add(room.RoomNo);
                    }
                }
            }
            catch (NullReferenceException)
            {
            }
           

        }

        private void Button_Click_CheckIn(object sender, RoutedEventArgs e)
        {  
            if (ListBox_Rooms.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a room");
                return;
            }

            string room = ListBox_Rooms.SelectedItem.ToString();

            MessageBoxResult result = MessageBox.Show("Are you sure you want to check in " + FirstName.Text + " " + LastName.Text + " into room " + room + "?", "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    String ageType = ((ComboBoxItem)CB_Age.SelectedItem).Tag.ToString();
                    bool isAdult = true;
                    if (ageType == "Child")
                    {
                        isAdult = false;
                    }
                    if (!Model.checkInGuest(FirstName.Text, LastName.Text, isAdult , room, Model.CurrentReservation.ReservationNo.ToString()))
                    {
                        MessageBox.Show("An error occurred while trying to check-in guest");
                        return;
                    }
                    UC_ManageReservations m = new UC_ManageReservations(parent, Model);
                    ContextSwitcher._context.SwitchScreen(m);
                    Refresh();
                    break;
                default:
                    break;
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            UC_ManageReservations m = new UC_ManageReservations(parent, Model);
            ContextSwitcher._context.SwitchScreen(m);
        }
    }
}
