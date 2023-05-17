using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class LeisureMemberFactory
    {
        private static ILeisureMember member = null;

        public static ILeisureMember GetMember(int ID, string fname, string lname, DateTime date, int age, string medicalConditions, string email, string phone, byte inLC, string areaType)
        {
            if (member != null)  // ie is Factory is primed with an object. 
                return member;
            else
                return new LeisureMember(ID, fname, lname, date, age, medicalConditions, email, phone,inLC, areaType);
        }
        public static void SetMember(ILeisureMember aMember)   // This provides a seam in the factory where I can prime the factory with the user it will then cough up. (for test code) 
        {
            member = aMember;
        }
    }
}
