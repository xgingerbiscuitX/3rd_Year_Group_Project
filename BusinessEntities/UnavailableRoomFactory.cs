using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UnavailableRoomFactory
    {
        private static IUnavailableRoom room = null;

        public static IUnavailableRoom GetUnavailableRoom(int id, int type, DateTime start, DateTime end)
        {
            if (room != null)  
                return room;
            else
                return new UnavailableRoom(id,type,start,end);
        }

        public static void SetUnavailableRoom(IUnavailableRoom unavailableRoom) 
        {
            room = unavailableRoom;
        }
    }
}
