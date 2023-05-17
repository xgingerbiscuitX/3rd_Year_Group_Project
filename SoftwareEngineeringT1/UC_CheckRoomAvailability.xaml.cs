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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_CheckRoomAvailability.xaml
    /// </summary>
    public partial class UC_CheckRoomAvailability : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        private List<DateTime> fullRooms;
        public UC_CheckRoomAvailability(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshRoomList();
            fullRooms = new List<DateTime>();
            LoadRoomTypes();
            
        }

        private void LoadRoomTypes() {
            Model.refreshRoomTypeList();
            foreach (string rt in Model.RoomTypeList)
            {
                CB_roomType.Items.Add(rt);
            }
        }

        private void CB_roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            int typeID = Convert.ToInt32(CB_roomType.SelectedIndex) + 1; //as the list is read in order, if we do index(which starts at 0) + 1 it will have the same id as the room type selected
            fullRooms = Model.GetFullRooms(typeID);
            Calander.BlackoutDates.Clear();
            foreach (DateTime date in fullRooms)
            {
                while(Calander.SelectedDate == date)
                {
                    Calander.SelectedDate = date.AddDays(1);
                }
                Calander.BlackoutDates.Add(new CalendarDateRange(date));
            }
        }


        private void Calander_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Dictionary<int,int> data = Model.GetRoomAvailability(Calander.SelectedDate.Value);

            listView.Items.Clear();
            int id = 1;
            foreach (string rt in Model.RoomTypeList)
            {
                int counter = 0;
                foreach (IRoom room in Model.roomList)
                {
                    if (room.RoomType == (Model.RoomTypeList.IndexOf(rt) + 1))
                    {
                        counter++;
                    }
                }
                listView.Items.Add(new TwoColumnData(rt.ToString() ,data[id] + "/" + counter.ToString()));
                id++;
            }
        }

        private void B_MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (CB_roomType.SelectedIndex > -1)
            {
                if (DateTime.TryParse(TB_StartDate.Text,out DateTime sDate) && DateTime.TryParse(TB_EndDate.Text, out DateTime eDate))
                {
                    if (eDate < sDate)
                    {
                        MessageBox.Show("You cannot have an end date before the start date");
                        return;
                    }
                    if (eDate < DateTime.Now.Date || sDate < DateTime.Now.Date )
                    {
                        MessageBox.Show("You cannot reserve a room in the past!");
                        return;
                    }
                    Model.endDate = eDate;
                    Model.startDate = sDate;
                    Model.roomType = CB_roomType.Text;
                    foreach (DateTime fullDate in fullRooms)
                    {
                        if (fullDate >= sDate && fullDate <=eDate)
                        {
                            MessageBox.Show("You cannot make a reservation with the selected room type as some days selected are fully booked");
                            return;
                        }
                    }
                    UC_MakeReservation mr = new UC_MakeReservation(parent,Model);
                    ContextSwitcher._context.SwitchScreen(mr);
                }
                else
                {
                    MessageBox.Show("Please select a start date and an end date");
                    return;
                }
            }
            else 
            {
                MessageBox.Show("Please select a room type");
            }
        }

        private void B_EndDate_Click(object sender, RoutedEventArgs e)
        {
            TB_EndDate.Text = Calander.SelectedDate.Value.ToString();
        }

        private void B_StartDate_Click(object sender, RoutedEventArgs e)
        {
            TB_StartDate.Text = Calander.SelectedDate.Value.ToString();
        }
    }
}
