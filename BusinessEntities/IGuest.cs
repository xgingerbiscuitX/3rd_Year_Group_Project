using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IGuest
    {
        int GuestID { get; set; }
        string FName { get; set; }
        string LName { get; set; }
        bool IsAdult { get; set; }
        int ReservationID { get; set; }
        bool InLeisureCentre { get; set; }
    }
}
