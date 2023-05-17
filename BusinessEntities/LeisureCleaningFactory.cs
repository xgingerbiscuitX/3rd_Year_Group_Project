using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class LeisureCleaningFactory
    {
        private static ILeisureCleaning Cleaning = null;

        public static ILeisureCleaning GetLeisureCleaning(int No, string Name, string Areas, string Comments, DateTime date)
        {

            if (Cleaning != null)
                return Cleaning;
            else
            {

                return new LeisureCleaning(No, Name, Areas, Comments, date);
            }


        }
    }
}
