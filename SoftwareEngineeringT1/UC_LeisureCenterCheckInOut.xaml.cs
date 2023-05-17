using BusinessEntities;
using BusinessLayer;
using Software_Engineering_Project;
using SoftwareEngineeringTeam1;
using System;
using System.Collections;
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
    public partial class UC_LeisureCenterCheckInOut : UserControl
    {

        private IAccessHandler Model;
        private Test parent;
        private ILeisureMember selectedMember = new LeisureMember();
        private IGuest selectedGuest = new Guest();

        private ArrayList LeisureList = new ArrayList();

        public UC_LeisureCenterCheckInOut(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshMemberList();
            Model.refreshGuestList();
            Populate_MemberList();
        }

        private void Populate_MemberList()
        {
            ArrayList LeisureList = Model.MemberList;
            ArrayList GuestList = Model.GuestList;

            foreach (LeisureMember member in LeisureList)
            {
                MemberList.Items.Add(new ListBoxItem { Content = String.Concat(member.FirstName, " ", member.LastName), Tag = member.ID });
            }

            foreach (IGuest guest in GuestList)
            {
                MemberList.Items.Add(new ListBoxItem { Content = String.Concat(guest.FName, " ", guest.LName), Tag = guest.GuestID });
            }


        }

        private void Refresh()
        {
            MemberList.Items.Clear();
            Model.refreshMemberList();
            Model.refreshGuestList();
            Populate_MemberList();
        }

        private void MemberList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MemberList.SelectedItem != null)
            {
                string val = MemberList.SelectedItem.ToString();

                string joinedName = ((ListBoxItem)MemberList.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split(' ');

                string name = splitName[0];

                int memID = Convert.ToInt32(((ListBoxItem)MemberList.SelectedItem).Tag.ToString());

                foreach (ILeisureMember a in Model.MemberList)
                {
                    //MessageBox.Show(a.InLeisure.ToString());
                    if (a.ID.Equals(memID) && a.FirstName.Equals(name))
                    {
                        Finame.Content = string.Concat("First name: ", a.FirstName);
                        Laname.Content = string.Concat("Last name: ", a.LastName);
                        Age.Content = string.Concat("Age: ", a.Age);
                        Status.Content = string.Concat("Status: ", CheckStatus(a.InLeisure));
                        Membership.Content = string.Concat("Membership expiry: ", a.ExpireDate);
                        Conditions.Content = string.Concat("Medical conditions: ", a.MedicalConditions);
                        selectedMember = a;
                        selectedGuest = null;
                        return;
                    }
                }

                foreach (IGuest a in Model.GuestList)
                {

                    if (a.GuestID.Equals(memID) && a.FName.Equals(name))
                    {
                        Finame.Content = string.Concat("First name: ", a.FName);
                        Laname.Content = string.Concat("Last name: ", a.LName);
                        Age.Content = string.Concat("Adult: ",a.IsAdult);
                        Status.Content = string.Concat("Status: ", (a.InLeisureCentre == true) ? "Checked-In" : "Checked-Out");
                        Membership.Content = string.Concat("Reservation ID: ", a.ReservationID);
                        Conditions.Content = string.Concat("Guest ID: ", a.GuestID);
                        selectedGuest = a;
                        selectedMember = null;
                        return;
                    }
                }


            }
        }

        private string CheckStatus(byte status)
        {
            switch(status)
            {
                case 1:
                    return "Checked-in";
                case 0:
                    return "Checked-out";
                default:
                    return "Unknown";
            }
        }

        private void Check_in_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMember != null && selectedMember.InLeisure != 1)
            {
                Model.ChangeMemberStatus(selectedMember, 1);
            }

            else if(selectedGuest != null && selectedGuest.InLeisureCentre == false)
            {
                Model.ChangeGuestStatus(selectedGuest, true);
            }

            else
            {
                MessageBox.Show("Member is already checked into the leisure center");
            }

            Refresh();
        }

        private void Check_Out_Click(object sender, RoutedEventArgs e)
        {

            if (selectedMember != null && selectedMember.InLeisure != 0)
            {
                Model.ChangeMemberStatus(selectedMember, 0);
            }

            else if (selectedGuest != null && selectedGuest.InLeisureCentre == true)
            {
                Model.ChangeGuestStatus(selectedGuest, false);
            }

            else
            {
                MessageBox.Show("Member is already checked into the leisure center");
            }

            Refresh();
        }

        private void Searchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "")
            {
                Refresh();
            }

            if (Searchtxt.Text != "Search")
            {

                string userInput = Searchtxt.Text;

                if (String.IsNullOrEmpty(Searchtxt.Text.Trim()) == false)
                {
                    MemberList.Items.Clear();

                    foreach (ILeisureMember member in Model.MemberList)
                    {
                        if (member.ID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.FirstName.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.LastName.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            MemberList.Items.Add(new ListBoxItem { Content = String.Concat(member.FirstName, " ", member.LastName), Tag = member.ID });
                        }
                    }
                    foreach (IGuest member in Model.GuestList)
                    {
                        if (member.GuestID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.FName.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.LName.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            MemberList.Items.Add(new ListBoxItem { Content = String.Concat(member.FName, " ", member.LName), Tag = member.GuestID });
                        }
                    }
                    

                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = MemberList.Items;
                    MemberList.Items.Clear();

                    foreach (ILeisureMember member in Model.MemberList)
                    {
                        if (member.ID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.FirstName.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.LastName.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            MemberList.Items.Add(new ListBoxItem { Content = String.Concat(member.FirstName, " ", member.LastName), Tag = member.ID });
                        }
                    }
                    foreach (IGuest member in Model.GuestList)
                    {
                        if (member.GuestID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.FName.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.LName.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            MemberList.Items.Add(new ListBoxItem { Content = String.Concat(member.FName, " ", member.LName), Tag = member.GuestID });
                        }
                    }
                }
            }

        }

        private void Searchtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }

        private void Check_Occupancy_Click(object sender, RoutedEventArgs e)
        {
            UC_ViewLeisureCenterOcupancy mr = new UC_ViewLeisureCenterOcupancy(parent, Model);
            ContextSwitcher._context.SwitchScreen(mr);
        }
    }
}
