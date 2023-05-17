using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class LeisureCleaning :ILeisureCleaning
    {
        public int Reference_No { get; set; }
        public string Employee_Name { get; set; }
        public string AreaCleaned { get; set; }
        public string Comment { get; set; }
        public DateTime dateDone { get; set; }


        public LeisureCleaning()
        {
            Reference_No = 0;
            Employee_Name = "Unknown";
            AreaCleaned = "Unknown";
            Comment = "Unknown";
            dateDone = DateTime.Now;




        }
        public LeisureCleaning(int No, string name, string Areas, string Com, DateTime date)
        {
            Reference_No = No;
            Employee_Name = name;
            AreaCleaned = Areas;
            Comment = Com;
            dateDone = date;




        }


    }
}
