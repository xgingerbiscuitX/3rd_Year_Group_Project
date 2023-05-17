using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BarSaleFactory
    {
        private static IBarSale BarSale = null;

        public static IBarSale GetBarSale(string Order_No, string Item_ID, string Item_Name, int Quantity)
        {
            if (BarSale != null)  // ie is Factory is primed with an object. 
                return BarSale;
            else
                return new BarSale(Order_No, Item_ID, Item_Name, Quantity);
        }
        public static void SetBarSale(IBarSale aBarSale)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            BarSale = aBarSale;
        }
    }
}
