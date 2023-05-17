using BusinessEntities;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;


namespace SoftwareEngineeringT1
{

    public partial class UC_ManageReservations : UserControl
    {
        private IAccessHandler Model;
        private IReservation SelectedReservation = new Reservation();
        private Test parent;


        private readonly CollectionViewSource viewSource = new CollectionViewSource();
        public UC_ManageReservations(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshReservationList();
            Populate_Reservations();
        }
        public void Refresh()
        {
            ReservationListBox.Items.Clear();
            Model.refreshReservationList();
            Populate_Reservations();
        }
        public void Populate_Reservations()
        {
            if (Model.ReservationList != null)
            {
                foreach (Reservation reservation in Model.ReservationList)
                {
                    //MessageBox.Show(reservation.CheckOutDate + "\n" + Convert.ToDateTime("01/01/0001"));
                    if(reservation.CheckOutDate == Convert.ToDateTime("01/01/0001"))
                    ReservationListBox.Items.Add(new ListBoxItem { Content = string.Concat(reservation.Name, " ", reservation.LName), Tag = reservation.ReservationNo });
                }
            }
        }

        private void ReservationListBox_Selected(object sender, RoutedEventArgs e)
        {
            if (ReservationListBox.SelectedItem != null)
            {
                string val = ReservationListBox.SelectedItem.ToString();

                string joinedName = ((ListBoxItem)ReservationListBox.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split(' ');

                string name = splitName[0];

                int resNo = Convert.ToInt32(((ListBoxItem)ReservationListBox.SelectedItem).Tag.ToString());

                ArrayList ReservationList = Model.ReservationList;

                foreach (Reservation r in ReservationList)
                {

                    if (r.ReservationNo.Equals(resNo) && r.Name.Equals(name))
                    {
                        Details_ResNo.Content = string.Concat("Reservation No: ", r.ReservationNo);
                        Details_FName.Content = string.Concat("First Name: ", r.Name);
                        Details_LName.Content = string.Concat("Last Name: ", r.LName);
                        Details_StartDate.Content = string.Concat("Start Date: ", r.StartDate);
                        Details_EndDate.Content = string.Concat("End Date: ", r.EndDate);
                        Details_Status.Content = string.Concat("Status: ", r.Status);
                        Details_CheckInDate.Content = string.Concat("Check In Date: ", r.CheckInDate);
                        Details_CheckOutDate.Content = string.Concat("Check Out Date: ", r.CheckOutDate);
                        Details_Email.Content = string.Concat("Email: ", r.Email);
                        Details_Address1.Content = string.Concat("Address Line 1: ", r.Address1);
                        Details_Address2.Content = string.Concat("Address Line 2: ", r.Address2);
                        Details_Address3.Content = string.Concat("Address Line 3: ", r.Address3);
                        Details_Phone.Content = string.Concat("Phone: ", r.Phone);
                        Details_RoomType.Content = string.Concat("Room Type: ", GetRoomType(r));
                        Details_SpecialReq.Content = string.Concat("Special Requirements: ", r.SpecialReq);
                        Details_NoChildren.Content = string.Concat("Children: ", r.NoChildren);
                        Details_NoAdults.Content = string.Concat("Adults: ", r.NoAdults);

                        SelectedReservation = r;
                        Model.CurrentReservation = r;
                        return;
                    }
                }
            }


        }
        public string GetRoomType(Reservation r)
        {
            int ID = Convert.ToInt32(r.RoomType);
            switch (ID)
            {
                case 1:
                    return "Single None-Smoking";
                case 2:
                    return "Single Smoking";
                case 3:
                    return "Double None-Smoking";
                case 4:
                    return "Double Smoking";
                case 5:
                    return "Twin None-Smoking";
                case 6:
                    return "Twin Smoking";
                default:
                    return "Single None-Smoking";
            }
        }

        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }

        private void cancelReservation_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation.ReservationNo == 0)
            {
                MessageBox.Show("You must have a reservation selected");
            }
            else
            {
                W_CancelReservationPopup cancelConfirm = new W_CancelReservationPopup(SelectedReservation, this, Model);
                cancelConfirm.ShowDialog();

                if (cancelConfirm.DialogResult == true)
                {
                    Refresh();
                    Cancel_Successful.Foreground = System.Windows.Media.Brushes.Red;
                    Cancel_Successful.Content = "Reservation Cancelled";
                    Cancel_Successful.Visibility = Visibility.Visible;
                    Refresh();
                }
                else if (cancelConfirm.DialogResult == false)
                {
                    Cancel_Successful.Foreground = System.Windows.Media.Brushes.Red;
                    Cancel_Successful.Content = "Reservation Not Cancelled";
                    Cancel_Successful.Visibility = Visibility.Visible;
                }
            }


        }

        private void CheckIn_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a reservation first");
                return;
            }
            if (Model.CurrentReservation.Status == 2)
            {
                MessageBox.Show("The reservation selected cannot be checked in as it has been checked out");
                return;
            }
            UC_CheckInGuest c = new UC_CheckInGuest(parent, Model);
            ContextSwitcher._context.SwitchScreen(c);
            Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a reservation first");
                return;
            }
            if (Model.CurrentReservation.Status != 1)
            {
                MessageBox.Show("The reservation selected has already been checked out");
                return;
            }
            UC_CheckOutGuest m = new UC_CheckOutGuest(parent, Model);
            ContextSwitcher._context.SwitchScreen(m);
            Refresh();
        }


       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ReservationListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a reservation first");
                return;
            }
            UC_EditReservation m = new UC_EditReservation(parent, Model);
            ContextSwitcher._context.SwitchScreen(m);
            Refresh();
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            QRReader mr = new QRReader();
            mr.ShowDialog();

            string qrcodeResult = QRReader.resultat;

            //MessageBox.Show("OUTSIDE FOREACH");

            foreach(ListBoxItem selected in ReservationListBox.Items)
            {
                //MessageBox.Show("Tag: " + selected.Tag.ToString() + "\nQrResult: " + qrcodeResult);
                if (selected.Tag.ToString() == qrcodeResult)
                {
                    //MessageBox.Show("INSIDE FOREACH");
                    ReservationListBox.SelectedItem = selected;
                }
            }

            foreach (Reservation r in Model.ReservationList)
            {
                if (r.ReservationNo.ToString().Equals(qrcodeResult.ToString()))
                {
                    Details_ResNo.Content = string.Concat("Reservation No: ", r.ReservationNo);
                    Details_FName.Content = string.Concat("First Name: ", r.Name);
                    Details_LName.Content = string.Concat("Last Name: ", r.LName);
                    Details_StartDate.Content = string.Concat("Start Date: ", r.StartDate);
                    Details_EndDate.Content = string.Concat("End Date: ", r.EndDate);
                    Details_Status.Content = string.Concat("Status: ", r.Status);
                    Details_CheckInDate.Content = string.Concat("Check In Date: ", r.CheckInDate);
                    Details_CheckOutDate.Content = string.Concat("Check Out Date: ", r.CheckOutDate);
                    Details_Email.Content = string.Concat("Email: ", r.Email);
                    Details_Address1.Content = string.Concat("Address Line 1: ", r.Address1);
                    Details_Address2.Content = string.Concat("Address Line 2: ", r.Address2);
                    Details_Address3.Content = string.Concat("Address Line 3: ", r.Address3);
                    Details_Phone.Content = string.Concat("Phone: ", r.Phone);
                    Details_SpecialReq.Content = string.Concat("Special Requirements: ", r.SpecialReq);
                    Details_NoChildren.Content = string.Concat("Children: ", r.NoChildren);
                    Details_NoAdults.Content = string.Concat("Adults: ", r.NoAdults);

                    SelectedReservation = r;
                    Model.CurrentReservation = r;
                    return;
                }
            }

            return;

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
                    ReservationListBox.Items.Clear();

                    foreach (IReservation item in Model.ReservationList)
                    {
                        if (item.Name.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || item.LName.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || item.ReservationNo.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            if (item.CheckOutDate == Convert.ToDateTime("01/01/0001"))
                                ReservationListBox.Items.Add(new ListBoxItem { Content = string.Concat(item.Name, " ", item.LName), Tag = item.ReservationNo });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = ReservationListBox.Items;
                    ReservationListBox.Items.Clear();

                    foreach (IReservation item in Model.ReservationList)
                    {
                        if (item.CheckOutDate == Convert.ToDateTime("01/01/0001"))
                            ReservationListBox.Items.Add(new ListBoxItem { Content = string.Concat(item.Name, " ", item.LName), Tag = item.ReservationNo });
                    }
                }
            }
        }
    }
}

