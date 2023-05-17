using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MoveStockFactory
    {
        private static IMoveStock movestock = null;

        public static IMoveStock GetMStock(int id, string name, string type, string size, int Bar, int storage, int amount)
        {
            if (movestock != null)  // ie is Factory is primed with an object. 
                return movestock;
            else
                return new MoveStock(id, name, type, size, Bar, storage, amount);
        }
        public static void SetMStock(IMoveStock aMStock)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            movestock = aMStock;
        }
    }
}
