using BusinessEntities;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections;
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
    public partial class UC_ManageRooms : UserControl
    {
        private IAccessHandler Model;
        private IRoom selectedRoom = new Room();
        private Test parent;

        public UC_ManageRooms(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.parent = parent;
            this.Model = Model;
            Refresh();
            ClearListBox();
            PopulateRoomList();
        }

        void Refresh()
        {
            ClearListBox();
            Model.refreshRoomList();
            PopulateRoomList();
        }

        void PopulateRoomList()
        {
            if (Model.roomList != null)
            {
                foreach (Room room in Model.roomList)
                {
                    //MessageBox.Show(String.Concat(staff.Name + " ", staff.EmpNo + " " + staff.LName));
                    RoomListBox.Items.Add(new ListBoxItem { Content = String.Concat("Room No:", " ", room.RoomNo) });

                }
            }
        }

        void ClearListBox()
        {
            RoomListBox.Items.Clear();
        }

        private void RoomList_Selected(object sender, RoutedEventArgs e)
        {
            if (RoomListBox.SelectedItem != null)
            {
                string val = ((ListBoxItem)RoomListBox.SelectedValue).Content.ToString();
                string[] splitName = val.Split(' ');

                string no = splitName[2];



                ArrayList RoomList = Model.roomList;

                foreach (Room a in RoomList)
                {
                    if (a.RoomNo.ToString() == no)
                    {
                        string roomtype = "";

                        switch(a.RoomType)
                        {
                            case 1:
                                roomtype = "Single None-Smoking";
                                break;

                            case 2:
                                roomtype = "Single Smoking";
                                break;

                            case 3:
                                roomtype = "Double None-Smoking";
                                break;

                            case 4:
                                roomtype = "Double Smoking";
                                break;

                            case 5:
                                roomtype = "Twin None-Smoking";
                                break;

                            case 6:
                                roomtype = "Twin Smoking";
                                break;
                        }

                        RoomNo.Content = string.Concat("Room No: ", a.RoomNo);
                        RoomType.Content = string.Concat("Type: ", roomtype);
                        Status.Content = string.Concat("Status: ", a.Status);
                        Availability.Content = string.Concat("Availability: ", a.Availability);
                        Pricing.Content = string.Concat("Pricing: €",a.Pricing);

                        selectedRoom = a;
                        return;
                    }
                }
            }
        }

        private void Make_Available_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to make the room available ?" , "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    selectedRoom.Availability = "Available";

                    Model.editRoom(selectedRoom);
                    Model.editRoomUnavailability(selectedRoom, "01/01/01", "01/01/01");
                    Model.refreshRoomList();
                    Refresh();


                    //staff.EmployeeType = CB_StaffType.Text;
                    //Model.editUser(staff);

                    //parent.Refresh();

                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void Make_Unavailable_Click(object sender, RoutedEventArgs e)
        {

            SetDates s = new SetDates(selectedRoom, Model, parent);
            s.ShowDialog();

            if (s.DialogResult == true)
            {
                selectedRoom.Availability = "Unavailable";
                Model.editRoom(selectedRoom);
                Model.editRoomUnavailability(selectedRoom, s.startDate.ToString("dd/MM/yyyy"), s.endDate.ToString("dd/MM/yyyy"));
                Refresh();
            }   

        }

        private void Change_room_pricing_Click(object sender, RoutedEventArgs e)
        {
            UC_ChangeRoomPricing c = new UC_ChangeRoomPricing(parent, Model);
            ContextSwitcher._context.SwitchScreen(c);
        }

        

        private void Searchtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

            if (Searchtxt.Text == "")
            {
                Refresh();
            }

            if (Searchtxt.Text != "Search")
            {

                string userInput = Searchtxt.Text;

                if (String.IsNullOrEmpty(Searchtxt.Text.Trim()) == false)
                {
                    RoomListBox.Items.Clear();

                    foreach (Room room in Model.roomList)
                    {
                        if (room.RoomNo.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            RoomListBox.Items.Add(new ListBoxItem { Content = String.Concat("Room No:", " ", room.RoomNo) });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = RoomListBox.Items;
                    RoomListBox.Items.Clear();

                    foreach (Room room in Model.roomList)
                    {
                        RoomListBox.Items.Add(new ListBoxItem { Content = String.Concat("Room No:", " ", room.RoomNo) });
                    }
                }
            }
        }
    }
}
