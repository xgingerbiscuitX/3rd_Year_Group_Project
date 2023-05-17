using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Room : IRoom
    {
        public int RoomNo { get; set; }
        public int RoomType { get; set; }
        public string Status { get; set; }
        public string Availability { get; set; }
        public int Pricing { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }


        public Room(int roomno, int roomtype, string status, string availability, int pricing, string StartDate, string EndDate)
        {
            this.RoomNo = roomno;
            this.RoomType = roomtype;
            this.Status = status;
            this.Availability = availability;
            this.Pricing = pricing;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }

        public Room()
        {
            this.RoomNo = 0;
            this.RoomType = 0;
            this.Status = "";
            this.Availability = "";
            this.Pricing = 0;
            this.StartDate = "1/1/0001";
            this.EndDate = "1/1/0001";

        }


    }
}
