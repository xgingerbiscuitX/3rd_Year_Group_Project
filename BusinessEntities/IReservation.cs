using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IReservation
    {
        int ReservationNo { get; set; }
        string Name { get; set; }
        string LName { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        int Status { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        string Email { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string Phone { get; set; }
        string SpecialReq { get; set; }
        int NoChildren { get; set; }
        int NoAdults { get; set; }
        int RoomNo { get; set; }
        int RoomType { get; set; }
    }
}
