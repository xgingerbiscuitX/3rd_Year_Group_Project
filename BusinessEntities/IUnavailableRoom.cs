using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IUnavailableRoom
    {
        int ID { get; set; }
        int Type { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
    }
}
