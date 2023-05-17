using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class StaffFactory
    {
        private static IStaff Staff = null;

        public static IStaff GetStaff(int EmpNo, string name, string lname, string address, string email, int HPhone, int MPhone, string NextToKin, int NextToKinPhoneNo, string NextToKinRel, string PPSN, string Username, string Password, string EmpType)
        {
            if (Staff != null)  // ie is Factory is primed with an object. 
                return Staff;
            else
                return new Staff(EmpNo, name, lname, address, email, HPhone, MPhone, NextToKin, NextToKinPhoneNo, NextToKinRel, PPSN, Username, Password, EmpType); // Factory coughs up a regular user (for production code) 
        }
        public static void SetUser(IStaff aStaff)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            Staff = aStaff;
        }
    }
}
