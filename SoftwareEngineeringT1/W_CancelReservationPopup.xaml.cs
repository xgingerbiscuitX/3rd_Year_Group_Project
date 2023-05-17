using BusinessEntities;
using BusinessLayer;
using DataAccessLayer;
using Software_Engineering_Project;
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
    /// Interaction logic for W_CancelReservationPopup.xaml
    /// </summary>
    public partial class W_CancelReservationPopup : Window
    {
        private IAccessHandler Model;
        private IReservation reservation = new Reservation();
        private UC_ManageReservations parent;
        public W_CancelReservationPopup(IReservation r, UC_ManageReservations parent, IAccessHandler Model)
        {
            InitializeComponent();
            reservation = r;
            this.Model = Model;
            this.parent = parent;
            Populate_Details();
        }

        private void Refresh()
        {
            Model.refreshReservationList();
        }
        public void Populate_Details()
        {
            resNo.Text = Model.CurrentReservation.ReservationNo.ToString();
            fname.Text = Model.CurrentReservation.Name;
            lname.Text = Model.CurrentReservation.LName;
            startDate.Text = Model.CurrentReservation.StartDate.ToString();
            endDate.Text = Model.CurrentReservation.EndDate.ToString();
            checkInDate.Text = Model.CurrentReservation.CheckInDate.ToString();
            checkOutDate.Text = Model.CurrentReservation.CheckOutDate.ToString();
            status.Text = Model.CurrentReservation.Status.ToString();
            address1.Text = Model.CurrentReservation.Address1;
            address2.Text = Model.CurrentReservation.Address2;
            address3.Text = Model.CurrentReservation.Address3;
            email.Text = Model.CurrentReservation.Email;
            phone.Text = Model.CurrentReservation.Phone;
            specialReq.Text = Model.CurrentReservation.SpecialReq;
            noAdults.Text = Model.CurrentReservation.NoAdults.ToString();
            noChildren.Text = Model.CurrentReservation.NoChildren.ToString();
        } 

        private void CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            try
            {
                Model.deleteReservation(Model.CurrentReservation);
                MessageBox.Show("Reservation removed from DB");
                Model.refreshReservationList();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void DENY_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
