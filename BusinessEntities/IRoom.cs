using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IRoom
    {
        int RoomNo { get; set; }
        int RoomType { get; set; }
        string Status { get; set; }
        string Availability { get; set; }
        int Pricing { get; set; }
        string StartDate { get; set; }
        string EndDate { get; set; }

    }
}
