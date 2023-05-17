using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class GuestFactory
    {
        private static IGuest Guest = null;

        public static IGuest GetGuest(int guestID, string fName, string lName, bool isAdult, int reservationID, bool inCentre)
        {
            if (Guest != null)  // ie is Factory is primed with an object. 
                return Guest;
            else
                return new Guest(guestID, fName, lName, isAdult, reservationID, inCentre);
        }

        public static void SetGuest(IGuest aGuest)
        {
            Guest = aGuest;
        }

    }
}
