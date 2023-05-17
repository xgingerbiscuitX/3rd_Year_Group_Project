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
    /// Interaction logic for UC_ViewCleaningSchedule.xaml
    /// </summary>
    public partial class UC_ViewCleaningSchedule : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        public UC_ViewCleaningSchedule(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshToBeCleanedList();
        }
        public void Refresh()
        {
            CleaningScheduleListBox.Items.Clear();
        }
        private void CleaningCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
            ArrayList ToBeCleanedList = Model.ToBeCleanedList;
            if (ToBeCleanedList != null)
            {
                foreach (CleaningRoom room in ToBeCleanedList)
                {
                    if(room.CheckOutDate == CleaningCalendar.SelectedDate)
                    {
                        CleaningScheduleListBox.Items.Add(new ListBoxItem { Content = string.Concat(room.CheckOutDate.Date, " ", room.Type), Tag = room.Room_No});
                    }
                    
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UC_SchedualRoomClean rn = new UC_SchedualRoomClean(parent,Model);
            ContextSwitcher._context.SwitchScreen(rn);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
