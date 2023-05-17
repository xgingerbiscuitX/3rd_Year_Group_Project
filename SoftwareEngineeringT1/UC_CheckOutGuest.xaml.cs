using BusinessEntities;
using BusinessLayer;
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

namespace SoftwareEngineeringT1
{

    public partial class UC_CheckOutGuest : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        public UC_CheckOutGuest(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
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

                foreach (IGuest g in Model.GuestList)
                {
                    if (g.ReservationID == r.ReservationNo)
                    {
                        this.LB_Guest.Items.Add(new ListBoxItem() { Content = g.FName + " " + g.LName, Tag = g.GuestID });
                    }
                }

            }
            catch (NullReferenceException)
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You sure you want to check out all guest on the selected reservation?", "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (!Model.checkOutGuest())
                    {
                        MessageBox.Show("ERROR: DATABASE ERROR CHECKING OUT GUEST");
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
    }
}
