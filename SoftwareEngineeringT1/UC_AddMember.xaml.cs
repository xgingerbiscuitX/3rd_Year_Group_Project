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
    /// Interaction logic for UC_AddMember.xaml
    /// </summary>
    public partial class UC_AddMember : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
       
        public UC_AddMember(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;

        }

        private void Monthly_Checked(object sender, RoutedEventArgs e)
        {
            DateTime month = DateTime.Now;
            month = month.AddMonths(1);
            expdate.Text = month.ToString();
        }

        private void Six_Months_Checked(object sender, RoutedEventArgs e)
        {
            DateTime month = DateTime.Now;
            month = month.AddMonths(6);
            expdate.Text = month.ToString();
        }

        private void Year_Checked(object sender, RoutedEventArgs e)
        {
            DateTime month = DateTime.Now;
            month = month.AddMonths(12);
            expdate.Text = month.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fName = fname.Text;
            string lName = lname.Text;
            int Age = Convert.ToInt32(age.Text);
            string Email = email.Text;
            string Phone = phone.Text;
            string medCon = MedCon.Text;
            string areaType = ((ListBoxItem)box1.SelectedValue).Content.ToString();
            DateTime date = Convert.ToDateTime(expdate.Text);

            try
            {
                Model.addNewMember(fName, lName, date, Age, medCon, Email, Phone, areaType);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            fname.Clear();
            lname.Clear();
            age.Clear();
            email.Clear();
            phone.Clear();
            MedCon.Clear();
            box1.UnselectAll();
            expdate.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fname.Clear();
            lname.Clear();
            age.Clear();
            email.Clear();
            phone.Clear();
            MedCon.Clear();
            box1.UnselectAll();
            expdate.Clear();
        }
    }
}
