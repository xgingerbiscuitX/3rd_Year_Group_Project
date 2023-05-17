using BusinessEntities;
using BusinessLayer;
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
    /// <summary>
    /// Interaction logic for W_DeleteStaff.xaml
    /// </summary>
    public partial class W_DeleteStaff : Window
    {
        private IAccessHandler Model;
        private IStaff Staff = new Staff();
        private UC_ManageStaff parent;
        public W_DeleteStaff(IStaff S,UC_ManageStaff parent,IAccessHandler Model)
        {
            InitializeComponent();
            Staff = S;
            this.Model = Model;
            this.parent = parent;
            Populate_Details();
        }
        public void Populate_Details() {
            stNo.Text = Staff.EmpNo.ToString();
            
            fname.Text = Staff.Name.ToString();
            lname.Text = Staff.LName.ToString();
            Address.Text = Staff.Address.ToString();
            Email.Text = Staff.Email.ToString();
            MobPhone.Text = Staff.Mphone.ToString();
            ppsn.Text = Staff.PPSN.ToString();
            EType.Text = Staff.EmployeeType.ToString();
            EUser.Text = Staff.UserName.ToString();
            NTK.Text = Staff.NextToKin.ToString();
            NTKPhone.Text = Staff.NextToKinPhoneNo.ToString();
            NTKRelation.Text = Staff.NextToKinRel.ToString();

        
        
        
        
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
