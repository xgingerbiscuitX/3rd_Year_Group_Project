using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UnavailableRoom : IUnavailableRoom
    {
            public int ID { get; set; }
            public int Type { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }

        public UnavailableRoom(int id, int type, DateTime start, DateTime end) 
        {
            this.ID = id;
            this.Type = type;
            this.Start = start;
            this.End = end;
        }
    }
}
