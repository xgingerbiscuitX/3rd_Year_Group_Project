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
    /// Interaction logic for UC_RecordRoomCleaning.xaml
    /// </summary>
    public partial class UC_RecordRoomCleaning : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        public UC_RecordRoomCleaning(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Load_Rooms();
        }
        private void Load_Rooms() 
        {
            LB_Rooms.Items.Clear();
            foreach (ICleaningRooms room in Model.ToBeCleanedList)
            {
                if (room.CheckOutDate.Date <= DateTime.Now.Date)// <= in case a room didnt get cleaned on a day, it still needs cleaning
                {
                    LB_Rooms.Items.Add(room.Room_No);
                }
            }
        
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Rooms.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a room");
                return;
            }

            if (Model.roomCleaned(Convert.ToInt32(LB_Rooms.SelectedItem)))
            {
                MessageBox.Show("Room Cleaning Recorded");
                Load_Rooms();
            }
            else
            {
                MessageBox.Show("An error occured while trying to record the room cleaning");
            }
        }
    }
}
