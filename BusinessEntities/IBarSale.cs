using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IBarSale
    {
        string Order_No { get; set; }
        string Item_ID { get; set; }
        string Item_Name { get; set; }
        int Quantity { get; set; }
    }
}
