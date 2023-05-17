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
using System.Net.Mail;
using ZXing;
using BusinessEntities;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_MakeReservation.xaml
    /// </summary>
    public partial class UC_MakeReservation : UserControl
    {
        IAccessHandler Model;
        private Test parent;
       // private DateTime startDate;
       // private DateTime endDate;
        public UC_MakeReservation(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Refresh();
            onload();
        }
        private void onload() {
           ADate.Text = Model.startDate.ToString();
            DDate.Text = Model.endDate.ToString();
            RoomType.Text = Model.roomType.ToString();
            int counter = 1;
            foreach ( string rt in Model.RoomTypeList)
            {
                if (rt == Model.roomType)
                {
                    RoomType.Tag = counter;
                    break;
                }
                counter++;
            }
        //   ArrayList room = Model.RoomTypeList;
          //  RoomType.Text = "1";
        
        }

        private void Refresh()
        {
            Model.refreshReservationList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fName = FirstName.Text.ToString();
            string lName = LastName.Text.ToString();
            string address1 = Address1.Text.ToString();
            string address2 = Address2.Text.ToString();
            string address3 = Address3.Text.ToString();
            string email = Email.Text.ToString();
            string PhoneNo = Phone.Text.ToString();
            DateTime arrivalD = Convert.ToDateTime(ADate.Text.ToString());
            DateTime departD = Convert.ToDateTime(DDate.Text.ToString());
            string SpecialRequest = Request.Text.ToString();
            int roomType = Convert.ToInt32(RoomType.Tag);

            int adultNo = 0;
            int childernNo = 0;
            try
            {
                adultNo = Convert.ToInt32(Adults.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Entry in 'Number of Adults' Field");
                return;
            }
            try
            {
                childernNo = Convert.ToInt32(Children.Text.ToString());
            }
            catch (Exception)
            {

                MessageBox.Show("Invalid Entry in 'Number of Children' Field");
                return;

            }


            try
            {
                Model.addNewReservation(fName, lName, arrivalD, departD, address1, address2, address3, email, PhoneNo, SpecialRequest, roomType, adultNo, childernNo);

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("bloomfieldcentre01@gmail.com");
                mail.To.Add(Email.Text);
                mail.Subject = "Bloomfield reservation details";
                mail.Body = string.Concat("Thank you for making a reservation at Bloomfield !\nYour reservation details are the following:\n","First name: ",FirstName.Text,"\nLast name: ",LastName.Text,"\nPhone: ",Phone.Text,"\nAddress1: ",Address1.Text,"\nAddress2: ",Address2.Text,"\nAddress3: ",Address3.Text,"\nEmail: ", Email.Text,"\nArrival date: ",ADate.Text,"\nDeparture date: ",DDate.Text,"\nRoom type: ", RoomType.Text,"\nNumber of children: ",Children.Text,"\nNumber of adults: ",Adults.Text,"\nSpecial request: ",Request.Text,"\n\nAttached is your unique reservation QR code.\nYou can show this code to the receptionist upon your arrival or use it to identify yourself at any occasion.\nNote: You can check in by passing your reservation details to the receptionist without the code");

                System.Net.Mail.Attachment att;
                att = new System.Net.Mail.Attachment(System.AppDomain.CurrentDomain.BaseDirectory + @"\ex.jpg");
                mail.Attachments.Add(att);
                
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("bloomfieldcentre01@gmail.com", "bloomfield123");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                MessageBox.Show("Added Reservation");
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FirstName.Clear();
            LastName.Clear();
            Address1.Clear();
            Address2.Clear();
            Address3.Clear();
            Email.Clear();
            Phone.Clear();

           
            Adults.Clear();
            Children.Clear();
            Request.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UC_CheckRoomAvailability mr = new UC_CheckRoomAvailability(parent, Model);
            ContextSwitcher._context.SwitchScreen(mr);
        }
    }
}
