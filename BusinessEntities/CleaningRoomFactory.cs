using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CleaningRoomFactory
    {
        private static ICleaningRooms CleaningRoom = null;

        public static ICleaningRooms GetCleaningRooms(int Room_No, string Type, DateTime CheckOutDate)
        {
            if (CleaningRoom != null)  // ie is Factory is primed with an object. 
                return CleaningRoom;
            else
                return new CleaningRoom(Room_No, Type, CheckOutDate);
        }
        public static void SetCleaningRooms(ICleaningRooms aCleaningRoom)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            CleaningRoom = aCleaningRoom;
        }
    }
}
