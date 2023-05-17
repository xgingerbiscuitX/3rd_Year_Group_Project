using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IStock
    {
        int StockID { get; set; }
        string NameStock { get; set; }
        string Type { get; set; }
        int Amount { get; set; }
        double Price { get; set; }
        string Size { get; set; }
        string Location { get; set; }
    }
}
