using SoftwareEngineeringT1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Reservation : IReservation
    {
        public int ReservationNo { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Phone { get; set; }
        public string SpecialReq { get; set; }
        public int NoChildren { get; set; }
        public int NoAdults { get; set; }
        public int RoomNo { get; set; }
        public int RoomType { get; set; }

        public int getReservationNo()
        {
            return this.ReservationNo;
        }
        public string getEmpName()
        {
            return this.Name;
        }
        public string getEmpLName()
        {
            return this.LName;
        }

        public DateTime getStartDate()
        {
            return this.StartDate;
        }

        public DateTime getEndDate()
        {
            return this.EndDate;
        }
        public int getStatus()
        {
            return this.Status;
        }

        public DateTime getCheckInDate()
        {
            return this.CheckInDate;
        }
        public DateTime getCheckOutDate()
        {
            return this.CheckOutDate;
        }

        public string getEmail()
        {
            return this.Email;
        }

        public string getAddress1()
        {
            return this.Address1;
        }
        public string getAddress2()
        {
            return this.Address2;
        }
        public string getAddress3()
        {
            return this.Address3;
        }
        public string getPhoneNo()
        {
            return this.Phone;
        }
        public string getSpecialReq()
        {
            return this.SpecialReq;
        }
        public int getNoChildren()
        {
            return this.NoChildren;
        }
        public int getNoAdults()
        {
            return this.NoAdults;
        }



        public List<ItemMenu> printForm()
        {
            return null;
        }

        public Reservation()
        {
            this.Name = "Unknown";
            this.LName = "Unknown";
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MinValue;
            this.Status = 0;
            this.CheckInDate = DateTime.MinValue;
            this.CheckOutDate = DateTime.MinValue;
            this.Email = "Unknown";
            this.Address1 = "Unknown";
            this.Address2 = "Unknown";
            this.Address3 = "Unknown";
            this.Phone = "Unknown";
            this.SpecialReq = "Unknown";
            this.NoChildren = 0;
            this.NoAdults = 0;
        }


        public Reservation(int no, string name, string lname, int status, string email, string phone, string specialreq)
        {
            this.ReservationNo = no;
            this.Name = name;
            this.LName = lname;
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MinValue;
            this.Status = status;
            this.CheckInDate = DateTime.MinValue;
            this.CheckOutDate = DateTime.MinValue;
            this.Email = email;
            this.Address1 = "Unknown";
            this.Address2 = "Unknown";
            this.Address3 = "Unknown";
            this.Phone = phone;
            this.SpecialReq = specialreq;
            this.NoChildren = 0;
            this.NoAdults = 0;
            this.RoomType = 0;
        }
        public Reservation(int ResNo, string name, string lname, DateTime StartDate, DateTime EndDate, int status, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomType)
        {
            this.ReservationNo = ResNo;
            this.Name = name;
            this.LName = lname;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Status = status;
            this.Email = email;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.Address3 = Address3;
            this.Phone = phone;
            this.SpecialReq = specialreq;
            this.NoChildren = NoChildren;
            this.NoAdults = NoAdults;
            this.RoomType = roomType;
        }

        public Reservation(int ResNo, string name, string lname, DateTime StartDate, DateTime EndDate, int status, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomType, DateTime checkInDate, int roomNo)
        {
            this.ReservationNo = ResNo;
            this.Name = name;
            this.LName = lname;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Status = status;
            this.Email = email;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.Address3 = Address3;
            this.Phone = phone;
            this.SpecialReq = specialreq;
            this.NoChildren = NoChildren;
            this.NoAdults = NoAdults;
            this.RoomType = roomType;
            this.CheckInDate = checkInDate;
            this.RoomNo = roomNo;
        }

        public Reservation(string name, string lname, DateTime StartDate, DateTime EndDate, int status, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomType)
        {
            this.Name = name;
            this.LName = lname;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Status = status;
            this.Email = email;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.Address3 = Address3;
            this.Phone = phone;
            this.SpecialReq = specialreq;
            this.NoChildren = NoChildren;
            this.NoAdults = NoAdults;
            this.RoomType = roomType;
            this.CheckInDate = DateTime.MinValue;
            this.RoomNo = 0;
        }

        public Reservation(int ResNo, string name, string lname, DateTime StartDate, DateTime EndDate, int status, DateTime CheckInDate, DateTime CheckOutDate, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomNo, int roomType)
        {
            this.ReservationNo = ResNo;
            this.Name = name;
            this.LName = lname;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Status = status;
            this.CheckInDate = CheckInDate;
            this.CheckOutDate = CheckOutDate;
            this.Email = email;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.Address3 = Address3;
            this.Phone = phone;
            this.SpecialReq = specialreq;
            this.NoChildren = NoChildren;
            this.NoAdults = NoAdults;
            this.RoomType = roomType;
            this.RoomNo = roomNo;
        }
    }
}
