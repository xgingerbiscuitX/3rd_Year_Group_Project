using BusinessEntities;
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
using System.Windows.Shapes;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for SetDates.xaml
    /// </summary>
    public partial class SetDates : Window
    {
        private IAccessHandler Model;
        private Test parent;
        private IRoom room;

        public DateTime startDate { get; set; } = new DateTime();
        public DateTime endDate { get; set; } = new DateTime();
        public SetDates(IRoom room, IAccessHandler Model, Test parent)
        {
            InitializeComponent();
            this.room = room;
            this.Model = Model;
            this.parent = parent;
        }

        private void B_EndDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TB_EndDate.Text = Calander.SelectedDate.Value.ToString("dd/MM/yyyy");
            }

            catch
            {
                MessageBox.Show("Please select an end date !");
            }
        }

        private void B_StartDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TB_StartDate.Text = Calander.SelectedDate.Value.ToString("dd/MM/yyyy");
            }
            catch
            {
                MessageBox.Show("Please select a start date !");
            }
        }

        private void Set_Room_Unavailability_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(TB_StartDate.Text, out DateTime sDate) && DateTime.TryParse(TB_EndDate.Text, out DateTime eDate))
            {
                if(eDate > sDate)
                {
                    startDate = eDate;
                    endDate = sDate;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("End date cannot be before the start date");
                }
            }
            else
            {
                MessageBox.Show("Please select a start date and an end date");
            }

            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
