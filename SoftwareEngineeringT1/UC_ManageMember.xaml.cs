using BusinessEntities;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Software_Engineering_Project;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_ManageMember.xaml
    /// </summary>
    public partial class UC_ManageMember : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        private ILeisureMember SelectedMember = new LeisureMember();
        public UC_ManageMember(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshMemberList(); 
            Populate_Members();
        }
        public void Refresh()
        {
            LeisureCentreMembers.Items.Clear();
            Model.refreshMemberList();
            Populate_Members();
        }
        public void Populate_Members()
        {
            if (Model.MemberList != null)
            {
                foreach (LeisureMember member in Model.MemberList)
                {
                    LeisureCentreMembers.Items.Add(new ListBoxItem { Content = string.Concat(member.FirstName, " ", member.LastName), Tag = member.ID });
                }
            }
        }

        private void LeisureCentreMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeisureCentreMembers.SelectedItem != null)
            {
                string val = LeisureCentreMembers.SelectedItem.ToString();

                string joinedName = ((ListBoxItem)LeisureCentreMembers.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split(' ');

                string name = splitName[0];

                int memberID = Convert.ToInt32(((ListBoxItem)LeisureCentreMembers.SelectedItem).Tag.ToString());

                ArrayList MemberList = Model.MemberList;

                foreach (LeisureMember m in MemberList)
                {

                    if (m.ID.Equals(memberID) && m.FirstName.Equals(name))
                    {
                        Details_MemberID.Content = string.Concat("Membership ID: ", m.ID);
                        Details_FName.Content = string.Concat("First Name: ", m.FirstName);
                        Details_LName.Content = string.Concat("Last Name: ", m.LastName);
                        Details_ExpiryDate.Content = string.Concat("Expiry Date: ", m.ExpireDate);
                        Details_Age.Content = string.Concat("Age: ", m.Age);
                        Details_MedicalConditions.Content = string.Concat("Medical Conditions: ", m.MedicalConditions);
                        Details_Email.Content = string.Concat("Email: ", m.Email);
                        Details_Phone.Content = string.Concat("Phone: ", m.Phone);
                        Details_InLeisureCentre.Content = string.Concat("In Leisure Centre: ", m.InLeisure);
                        Details_AreaType.Content = string.Concat("Area Type: ", m.AreaType);

                        SelectedMember = m;
                        Model.CurrentMember = m;
                        return;
                    }
                }
            }
        }

        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {
            if (LeisureCentreMembers.SelectedItem != null)
            {

                string val = LeisureCentreMembers.SelectedItem.ToString();
                string joinedName = ((ListBoxItem)LeisureCentreMembers.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split(' ');
                string name = splitName[0];
                int memberID = Convert.ToInt32(((ListBoxItem)LeisureCentreMembers.SelectedItem).Tag.ToString());

                ArrayList MemberList = Model.MemberList;

                foreach (LeisureMember m in MemberList)
                {

                    if (m.ID.Equals(memberID) && m.FirstName.Equals(name))
                    {
                        try {
                            W_RemoveMember Member = new W_RemoveMember(m, this,Model);
                            Member.ShowDialog();
                                if (Member.DialogResult == true)
                            {
                                Model.DeleteMember(m);

                                LeisureCentreMembers.Items.Remove(LeisureCentreMembers.SelectedItem);
                                Refresh();
                                return;
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void RenewMember_Click(object sender, RoutedEventArgs e)
        {
            UC_RenewLeisureMember m = new UC_RenewLeisureMember(parent, Model);
            ContextSwitcher._context.SwitchScreen(m);
        }


        private void Searchtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
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
                    LeisureCentreMembers.Items.Clear();

                    foreach (LeisureMember member in Model.MemberList)
                    {
                        if (member.ID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.FirstName.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || member.LastName.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            LeisureCentreMembers.Items.Add(new ListBoxItem { Content = string.Concat(member.FirstName, " ", member.LastName), Tag = member.ID });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = LeisureCentreMembers.Items;
                    LeisureCentreMembers.Items.Clear();

                    foreach (LeisureMember member in Model.MemberList)
                    {
                        LeisureCentreMembers.Items.Add(new ListBoxItem { Content = string.Concat(member.FirstName, " ", member.LastName), Tag = member.ID });
                    }
                }
            }
        }

        private void Add_Member_Click(object sender, RoutedEventArgs e)
        {
            UC_AddMember m = new UC_AddMember(parent,Model);
            ContextSwitcher._context.SwitchScreen(m);
        }
    }
    
}
