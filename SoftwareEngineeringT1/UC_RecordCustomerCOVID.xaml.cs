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
using BusinessEntities;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_RecordCustomerCOVID.xaml
    /// </summary>
    public partial class UC_RecordCustomerCOVID : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        public UC_RecordCustomerCOVID(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.parent = parent;
            this.Model = Model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Model.recordCovidInfo(FirstName.Text,LastName.Text, Phone.Text)) 
            {
                MessageBox.Show("Successfully recoreded Entry");
                return;
            }
            MessageBox.Show("An Error occurred while trying to record entry");
        }
    }
}
