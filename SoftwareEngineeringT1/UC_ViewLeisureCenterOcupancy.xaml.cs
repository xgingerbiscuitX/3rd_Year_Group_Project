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
    /// <summary>
    /// Interaction logic for UC_ViewLeisureCenterOcupancy.xaml
    /// </summary>
    public partial class UC_ViewLeisureCenterOcupancy : UserControl
    {
        private int maxOccupancy = 50;
        private int occupancyCounter = 0;
        IAccessHandler Model;
        private Test parent;
        public UC_ViewLeisureCenterOcupancy(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            InsertData();
        }

        private void InsertData()
        {

            try
            {
                foreach (IGuest guest in Model.GuestList)
                {
                    if (guest.InLeisureCentre)
                    {
                        lb_Occupants.Items.Add(new ListBoxItem() { Content = guest.FName + " " + guest.LName, Tag = guest });
                        occupancyCounter++;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("guest insert error");
            }
            try
            {
                
                foreach (ILeisureMember member in Model.MemberList)
                {
                    if (member.InLeisure == 1)
                    {
                        occupancyCounter++;
                        lb_Occupants.Items.Add(new ListBoxItem() { Content = member.FirstName + " " + member.LastName, Tag = member });
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("member insert error");

            }
            tb_occupantNumber.Text = "(" + occupancyCounter +"/" + maxOccupancy +")";
        }

        private void lb_Occupants_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           var selecteedItem = (lb_Occupants.SelectedItem as ListBoxItem).Tag;
            listView.Items.Clear();

                ILeisureMember m = (selecteedItem as ILeisureMember);
            if (m != null)
            {
                listView.Items.Add(new TwoColumnData("Member Type:", "Member"));
                listView.Items.Add(new TwoColumnData("First Name:", m.FirstName.ToString()));
                listView.Items.Add(new TwoColumnData("LastName:", m.LastName.ToString()));
                listView.Items.Add(new TwoColumnData("Expirey Date:", m.ExpireDate.ToString()));
                listView.Items.Add(new TwoColumnData("Age:", m.Age.ToString()));
                listView.Items.Add(new TwoColumnData("Medical Conditions: ", m.MedicalConditions.ToString()));
                listView.Items.Add(new TwoColumnData("Email:", m.Email.ToString()));
                listView.Items.Add(new TwoColumnData("Phone:", m.Phone.ToString()));
                listView.Items.Add(new TwoColumnData("Area Type:", m.AreaType.ToString()));
                return;
            }
                IGuest g = (selecteedItem as IGuest);
            if (g != null)
            {
                listView.Items.Add(new TwoColumnData("Member Type:", "Hotel Guest"));
                listView.Items.Add(new TwoColumnData("First Name:", g.FName.ToString()));
                listView.Items.Add(new TwoColumnData("Last Name:", g.LName.ToString()));
                listView.Items.Add(new TwoColumnData("Adult:", g.IsAdult.ToString()));
                listView.Items.Add(new TwoColumnData("Reservation No:", g.ReservationID.ToString()));
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            UC_LeisureCenterCheckInOut m = new UC_LeisureCenterCheckInOut(parent,Model);
            ContextSwitcher._context.SwitchScreen(m);
        }
    }
}
