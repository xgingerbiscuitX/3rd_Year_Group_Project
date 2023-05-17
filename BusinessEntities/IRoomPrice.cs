using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IRoomPrice
    {
        string RoomName { get; set; }
        double Pricing { get; set; }

    }
}
