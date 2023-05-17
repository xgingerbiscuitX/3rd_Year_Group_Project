using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BarSale : IBarSale
    {
        public string Order_No { get; set; }
        public string Item_ID { get; set; }
        public string Item_Name { get; set; }
        public int Quantity { get; set; }

        public BarSale(string Order_No, string Item_ID, string Item_Name, int Quantity)
        {
            this.Order_No = Order_No;
            this.Item_ID = Item_ID;
            this.Item_Name = Item_Name;
            this.Quantity = Quantity;
        }
    }
}
