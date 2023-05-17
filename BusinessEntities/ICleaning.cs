using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface ICleaningRooms
    {
        int Room_No { get; set; }
        string Type { get; set; }
        DateTime CheckOutDate { get; set; }
    }
}

