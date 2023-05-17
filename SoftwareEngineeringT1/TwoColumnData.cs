using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineeringT1
{
    public class TwoColumnData
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public TwoColumnData(string s1, string s2)
        {
            this.Title = s1;
            this.Details = s2;
        }
    }
}
