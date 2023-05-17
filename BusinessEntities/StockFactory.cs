using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class StockFactory
    {
        private static IStock stock = null;

        public static IStock GetStock(int ID, string Name, string type, int amount, double price, string size, string location)
        {
            if (stock != null)  // ie is Factory is primed with an object. 
                return stock;
            else
                return new Stock(ID, Name, type, amount, price, size, location);
        }

        public static void SetStock(IStock aStock)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            stock = aStock;
        }
    }
}
