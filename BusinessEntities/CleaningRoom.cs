using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CleaningRoom : ICleaningRooms
    {
        public int Room_No { get; set; }
        public string Type { get; set; }
        public DateTime CheckOutDate { get; set; }
        public CleaningRoom()
        {
            this.Room_No = 0;
            this.Type = "";
            this.CheckOutDate = DateTime.Now;
        }
        public CleaningRoom(int Room_No, string Name, DateTime CheckOutDate)
        {
            this.Room_No = Room_No;
            this.Type = Name;
            this.CheckOutDate = CheckOutDate;
        }
    }
}
