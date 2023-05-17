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

namespace SoftwareEngineeringT1
{
    
    public partial class UC_ManageStaff : UserControl
    {
        private IAccessHandler Model;
        private IStaff SelectedUser = new Staff();
        private Test parent;
        

        public UC_ManageStaff(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshUserList();
            Populate_Staff();
        }

        public void RefreshDetails()
        {
            Staff_Details.Items.Clear();
        }

        public void Refresh()
        {
            StaffListbox.Items.Clear();
            Model.refreshUserList();
            Populate_Staff();
        }


        public void Populate_Staff()
        {
            if (Model.UserList != null)
            {
                foreach (Staff staff in Model.UserList)
                {
                    //MessageBox.Show(String.Concat(staff.Name + " ", staff.EmpNo + " " + staff.LName));
                    StaffListbox.Items.Add(new ListBoxItem { Content = string.Concat(staff.Name, " ", staff.LName), Tag = staff.EmpNo });

                }
            }
        }

        private void StaffListbox_Selected(object sender, RoutedEventArgs e)
        {
            RefreshDetails();

            if (StaffListbox.SelectedItem != null)
            {
                string val = StaffListbox.SelectedItem.ToString();

                string joinedName = ((ListBoxItem)StaffListbox.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split(' ');

                string name = splitName[0];

                int empNo = Convert.ToInt32(((ListBoxItem)StaffListbox.SelectedItem).Tag.ToString());

                ArrayList StaffList = Model.UserList;

                foreach (Staff a in StaffList)
                {

                    if (a.EmpNo.Equals(empNo) && a.Name.Equals(name))
                    {
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Employee No: ", ((ListBoxItem)StaffListbox.SelectedValue).Content.ToString()) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("First name: ", a.Name) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Last name: ", a.LName) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Address: ", a.Address) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Phone No: ", a.HPhone) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("PPSN: ", a.PPSN) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Next to kin name: ", a.NextToKin) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Next to kin phone: ", a.NextToKinPhoneNo) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Next to kin relationship: ", a.NextToKinRel) });
                        Staff_Details.Items.Add(new ListBoxItem { Content = string.Concat("Employee type: ", a.EmployeeType) });
                        SelectedUser = a;
                        return;
                    }
                }
            }

              
        }

        private void ChangePermissions_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser.EmpNo == 0)
            {
                MessageBox.Show("You must have a staff member selected");
            }
            else
            {
                W_ChangeStaffPermissions changeStaff = new W_ChangeStaffPermissions(SelectedUser, this, Model);
                changeStaff.ShowDialog();
            }
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {if (StaffListbox.SelectedIndex != -1)
            {
                string joinedName = ((ListBoxItem)StaffListbox.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split(' ');

                string name = splitName[0];

                int empNo = Convert.ToInt32(((ListBoxItem)StaffListbox.SelectedItem).Tag.ToString());

                foreach (IStaff a in Model.UserList)
                {
                    if (a.EmpNo.Equals(empNo) && a.Name.Equals(name))
                    {
                        W_DeleteStaff DStaff = new W_DeleteStaff(a, this, Model);
                        DStaff.ShowDialog();

                        // var result = MessageBox.Show("Are you sure you want to delete '" + name + "'?", "Delete Staff", MessageBoxButton.YesNo);
                        if (DStaff.DialogResult == true)
                        {
                            Model.deleteUser(a);
                            StaffListbox.Items.Remove(StaffListbox.SelectedItem);
                            Refresh();
                            return;
                        }
                    }
                }
            }
            else {
                MessageBox.Show("Click a Staff Member first");
            }
            
        }
        private void AddStaff_Button_Click(object sender, RoutedEventArgs e)
        {
            UC_AddStaff newscreen = new UC_AddStaff(parent, Model);
            ContextSwitcher._context.SwitchScreen(newscreen);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
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
                    StaffListbox.Items.Clear();

                    foreach (Staff staff in Model.UserList)
                    {
                        if (staff.EmpNo.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || staff.Name.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || staff.LName.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            StaffListbox.Items.Add(new ListBoxItem { Content = string.Concat(staff.Name, " ", staff.LName), Tag = staff.EmpNo });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = StaffListbox.Items;
                    StaffListbox.Items.Clear();

                    foreach (Staff staff in Model.UserList)
                    {
                        StaffListbox.Items.Add(new ListBoxItem { Content = string.Concat(staff.Name, " ", staff.LName), Tag = staff.EmpNo });
                    }
                }
            }
        }

        private void Searchtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }
    }
}
