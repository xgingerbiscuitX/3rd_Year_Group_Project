using BusinessEntities;
using BusinessLayer;
using DataAccessLayer;
using Software_Engineering_Project;
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
using System.Windows.Shapes;

namespace SoftwareEngineeringT1
{

    public partial class W_ChangeStaffPermissions : Window
    {
        private IAccessHandler Model;
        private IStaff staff = new Staff();
        private UC_ManageStaff parent;
        public W_ChangeStaffPermissions(IStaff s, UC_ManageStaff parent, IAccessHandler Model)
        {
            InitializeComponent();
            staff = s;
            this.Model = Model;
            this.parent = parent;
            Refresh();
            Display_Details();
        }

        private void Refresh()
        {
            Model.refreshUserList();
        }

        public void Display_Details() {  
            Details_EmpNo.Content = staff.EmpNo;
            Details_FName.Content = string.Concat("First Name: ", staff.Name);
            Details_LName.Content = string.Concat("Last Name: ", staff.LName);
            Details_StreetAddress.Content = string.Concat("Street Address: ", staff.Address);
            Details_PhoneNo.Content = string.Concat("Phone number: ", staff.HPhone);
            Details_PPSN.Content = string.Concat("PPSN: ", staff.PPSN);
            Details_NextToKinName.Content = string.Concat("Next-to-kin Name: ", staff.NextToKin);
            Details_NextToKinPhone.Content = string.Concat("Next-to-kin Phone: ", staff.NextToKinPhoneNo);
            Details_NextToKinRelationship.Content = string.Concat("Next-to-kin Relationship: ", staff.NextToKinRel);
            Details_EmpType.Content = string.Concat("Employee Type: ", staff.EmployeeType);
            CB_StaffType.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result =  MessageBox.Show("Are you sure you want staff changed to a " + CB_StaffType.Text +"?","",MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    staff.EmployeeType = CB_StaffType.Text;
                    Model.editUser(staff);

                    Refresh();
                    parent.Refresh();
                    
                    
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
