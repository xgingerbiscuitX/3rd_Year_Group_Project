using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public  interface IMoveStock
    {
        int ID { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string Size { get; set; }
        int InBar { get; set; }
        int InStorage { get; set; }
        int Amount { get; set; }
    }
}
