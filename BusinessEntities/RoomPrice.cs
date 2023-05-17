using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class RoomPrice : IRoomPrice
    {
        public string RoomName { get; set; }
        public double Pricing { get; set; }


        public RoomPrice(string name, double pricing)
        {
            this.RoomName = name;
            this.Pricing = pricing;
        }

        public RoomPrice()
        {
            this.RoomName = "";
            this.Pricing = 0;
        }


    }
}
