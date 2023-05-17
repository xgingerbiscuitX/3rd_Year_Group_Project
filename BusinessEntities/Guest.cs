using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Guest : IGuest
    {
        public int GuestID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public bool IsAdult { get; set; }
        public int ReservationID { get; set; }
        public bool InLeisureCentre { get; set; }

        public Guest(int id, string fName, string lName, bool isAdult, int resID, bool inLeisureCentre)
        {
            this.GuestID = id;
            this.FName = fName;
            this.LName = lName;
            this.IsAdult = isAdult;
            this.ReservationID = resID;
            this.InLeisureCentre = inLeisureCentre;
        }

        public Guest()
        {
        }
    }
}
