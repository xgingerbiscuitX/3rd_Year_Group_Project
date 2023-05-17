using BusinessLayer;
using DataAccessLayer;
using Software_Engineering_Project;
using SoftwareEngineeringTeam1;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

    public partial class UC_AddStaff : UserControl
    {
        private IAccessHandler Model;
        private Test parent;

        public UC_AddStaff(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;

        }

        private void goBack_button_Click(object sender, RoutedEventArgs e)
        {
            UC_ManageStaff newscreen = new UC_ManageStaff(parent, Model);
            ContextSwitcher._context.SwitchScreen(newscreen);
        }

        private void Refresh()
        {
            Model.refreshUserList();
        }

        private void UC_AddStaff_Load(object sender, EventArgs e)
        {
            MessageBox.Show("PLEASE");
        }

        private void AddStaffMember_Button_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.Show(PPSN.Text);

            string fName = FirstName.Text;
            string lName = LastName.Text;
            string address = StreetAddress.Text;
            string email = Email.Text;
            int homePhone = Convert.ToInt32(HomePhone.Text);
            int mobilePhone = Convert.ToInt32(MobilePhone.Text);
            string nextToKin = NextToKin.Text;
            int nextToKinPhone = Convert.ToInt32(NextToKinPhone.Text);
            string nextToKinRelation = NextToKinRelation.Text;
            string ppsn = PPSN.Text;
            string username = Username.Text;
            string password = Password.Text;
            string employeeType = EmployeeType.Text;
            try
            {
                Model.addNewUser(fName, lName, address, email, homePhone, mobilePhone, nextToKin, nextToKinPhone, nextToKinRelation, ppsn, username, Crypt.Encrypt(password), employeeType);
                MessageBox.Show("Added staff");
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
