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
    /// Interaction logic for UC_RecordCleanLeisure.xaml
    /// </summary>
    public partial class UC_RecordCleanLeisure : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        private string list;
        public UC_RecordCleanLeisure(Test parent, IAccessHandler Model)
        {
            InitializeComponent();

            this.Model = Model;
            this.parent = parent;
        }
        private void Refresh() {
        
          if (CleaningList.SelectedIndex != -1)
            {
                CleaningList.SelectedIndex = -1;
            }

            commentBox.Text = "None";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CleaningList.SelectedIndex != -1)
            {


                //list += listings.ToString();
                list = ((ListBoxItem)CleaningList.SelectedValue).Content.ToString();


                // MessageBox.Show(list);
                string comment = commentBox.Text;

                Model.RecordClean(list, comment);
                Refresh();


            }
            else {
                MessageBox.Show("Need to Select Area");
            
            
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
