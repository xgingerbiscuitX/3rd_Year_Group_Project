using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class OrdersFactory
    {
        private static IOrder Order = null;

        public static IOrder GetOrder(string Order_No, string Item_ID, string Item_Name, int Quantity)
        {
            if (Order != null)  // ie is Factory is primed with an object. 
                return Order;
            else
                return new Order(Order_No, Item_ID, Item_Name, Quantity);
        }
        public static void SetOrder(IOrder aOrder)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
           Order = aOrder;
        }
    }
}
