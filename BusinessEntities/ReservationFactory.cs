using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ReservationFactory
    {
        private static IReservation Reservation = null;

        public static IReservation GetReservation(int ResNo, string name, string lname, DateTime StartDate, DateTime EndDate, int status, DateTime CheckInDate, DateTime CheckOutDate, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomNo, int roomType)
        {
            if (Reservation != null)  // ie is Factory is primed with an object. 
                return Reservation;
            else
                return new Reservation(ResNo, name, lname, StartDate, EndDate, status, CheckInDate, CheckOutDate, email, Address1, Address2, Address3, phone, specialreq, NoChildren, NoAdults, roomNo, roomType);
        }

        public static IReservation GetReservation(int ResNo, string name, string lname, DateTime StartDate, DateTime EndDate, int status, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomType)
        {
            if (Reservation != null)  // ie is Factory is primed with an object. 
                return Reservation;
            else
                return new Reservation(ResNo, name, lname, StartDate, EndDate, status, email, Address1, Address2, Address3, phone, specialreq, NoChildren, NoAdults, roomType);
        }
        public static IReservation GetReservation(int ResNo, string name, string lname, DateTime StartDate, DateTime EndDate, int status, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomType, DateTime checkInDate, int roomNo)
        {
            if (Reservation != null)  // ie is Factory is primed with an object. 
                return Reservation;
            else
                return new Reservation(ResNo, name, lname, StartDate, EndDate, status, email, Address1, Address2, Address3, phone, specialreq, NoChildren, NoAdults, roomType,checkInDate,roomNo);
        }

        public static IReservation GetReservation(string name, string lname, DateTime StartDate, DateTime EndDate, int status, string email, string Address1, string Address2, string Address3, string phone, string specialreq, int NoChildren, int NoAdults, int roomType)
        {
            if (Reservation != null)  // ie is Factory is primed with an object. 
                return Reservation;
            else
                return new Reservation(name, lname, StartDate, EndDate, status, email, Address1, Address2, Address3, phone, specialreq, NoChildren, NoAdults, roomType);
        }

        public static void SetReservation(IReservation aReservation)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            Reservation = aReservation;
        }
    }
}
