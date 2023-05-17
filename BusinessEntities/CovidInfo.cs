using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CovidInfo : ICovidInfo
    {
        public string FirstName { get; set ; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public DateTime Date { get; set; }

        public CovidInfo(string fName, string lName, string phoneNo, DateTime date)
        {
            FirstName = fName;
            LastName = lName;
            PhoneNo = phoneNo;
            Date = date;
        }
    }

}
