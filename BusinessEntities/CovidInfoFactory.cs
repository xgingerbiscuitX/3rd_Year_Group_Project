using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CovidInfoFactory
    {
        private static ICovidInfo Info = null;

        public static ICovidInfo GetCovidInfo(string fName, string lName, string phoneNo, DateTime date)
        {
            if (Info != null) 
                return Info;
            else
                return new CovidInfo(fName, lName, phoneNo, date);
        }

        public static void SetCovidInfo(ICovidInfo covidInfo)
        {
            Info = covidInfo;
        }
    }
}
