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
using BusinessLayer;
using DataAccessLayer;
using Software_Engineering_Project;
using SoftwareEngineeringTeam1;

namespace SoftwareEngineeringT1
{

    public partial class UC_Logout : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        public UC_Logout(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            Model.LogOut();
            MainWindow showLogin = new MainWindow();
            Application.Current.Windows[0].Close();
            showLogin.ShowDialog();
        }
    }
}
