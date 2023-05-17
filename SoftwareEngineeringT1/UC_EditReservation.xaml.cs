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
    /// <summary>
    /// Interaction logic for UC_EditReservation.xaml
    /// </summary>
    public partial class UC_EditReservation : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        public UC_EditReservation(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Refresh();
            Populate_Details();
            
        }
        public void Populate_Details()
        {
            ReservationNo.Text = Model.CurrentReservation.ReservationNo.ToString();
            FirstName.Text = Model.CurrentReservation.Name;
            LastName.Text = Model.CurrentReservation.LName;
            ADate.Text = Model.CurrentReservation.StartDate.ToString();
            DDate.Text = Model.CurrentReservation.EndDate.ToString();
            Address1.Text = Model.CurrentReservation.Address1;
            Address2.Text = Model.CurrentReservation.Address2;
            Address3.Text = Model.CurrentReservation.Address3;
            Email.Text = Model.CurrentReservation.Email;
            Phone.Text = Model.CurrentReservation.Phone;
            Request.Text = Model.CurrentReservation.SpecialReq;
            CB_roomType.SelectedItem = GetRoomID(0);
            Adults.Text = Model.CurrentReservation.NoAdults.ToString();
            Children.Text = Model.CurrentReservation.NoChildren.ToString();
            CInDate.Text = Model.CurrentReservation.CheckInDate.ToString();
            COutDate.Text = Model.CurrentReservation.CheckOutDate.ToString();
            foreach (string rt in Model.RoomTypeList)
            {
                CB_roomType.Items.Add(rt);
            }
        }
        public string GetRoomID(int type)
        {
            if (type == 0)
            {
                int ID = Convert.ToInt32(Model.CurrentReservation.RoomType);
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
            else if (type == 1)
            {
                string roomType = CB_roomType.SelectedItem.ToString();
                switch (roomType)
                {
                    case "Single None-Smoking":
                        return "1";
                    case "Single Smoking":
                        return "2";
                    case "Double None-Smoking":
                        return "3";
                    case "Double Smoking":
                        return "4";
                    case "Twin None-Smoking":
                        return "5";
                    case "Twin Smoking":
                        return "6";
                    default:
                        return "1";
                }
            }
            else
            {
                return "";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            FirstName.Clear();
            LastName.Clear();
            Address1.Clear();
            Address2.Clear();
            Address3.Clear();
            Email.Clear();
            Phone.Clear();

            RoomType.Clear();
            Adults.Clear();
            Children.Clear();
            Request.Clear();

            UC_ManageReservations c = new UC_ManageReservations(parent, Model);
            ContextSwitcher._context.SwitchScreen(c);
        }

        private void Refresh()
        {
            Model.refreshReservationList();
            Model.refreshRoomTypeList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reservation editedReservation = Model.CurrentReservation;
            editedReservation.ReservationNo = Convert.ToInt32(ReservationNo.Text);
            editedReservation.Name = FirstName.Text.ToString();
            editedReservation.LName = LastName.Text.ToString();
            editedReservation.Address1 = Address1.Text.ToString();
            editedReservation.Address2 = Address2.Text.ToString();
            editedReservation.Address3 = Address3.Text.ToString();
            editedReservation.Email = Email.Text.ToString();
            editedReservation.Phone = Phone.Text.ToString();
            editedReservation.StartDate = Convert.ToDateTime(ADate.Text.ToString());
            editedReservation.EndDate = Convert.ToDateTime(DDate.Text.ToString());
            editedReservation.SpecialReq = Request.Text.ToString();
            editedReservation.RoomType = Convert.ToInt32(GetRoomID(1));
            editedReservation.NoAdults = Convert.ToInt32(Adults.Text.ToString());
            editedReservation.NoChildren = Convert.ToInt32(Children.Text.ToString());
            editedReservation.CheckInDate = Convert.ToDateTime(CInDate.Text.ToString());
            editedReservation.CheckOutDate = Convert.ToDateTime(COutDate.Text.ToString());



            try
            {
                Model.editReservation(editedReservation);

                MessageBox.Show("Edited Reservation");
                Edited();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Edited()
        {
            FirstName.Clear();
            LastName.Clear();
            Address1.Clear();
            Address2.Clear();
            Address3.Clear();
            Email.Clear();
            Phone.Clear();

            RoomType.Clear();
            Adults.Clear();
            Children.Clear();
            Request.Clear();

            UC_ManageReservations c = new UC_ManageReservations(parent, Model);
            ContextSwitcher._context.SwitchScreen(c);
        }
    }
}
