using MaterialDesignThemes.Wpf;
using SoftwareEngineeringT1;
using SoftwareEngineeringT1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public abstract class Employee : Staff
    {
        public Employee() { }

        public Employee(int no, string name, string Lname, string address, int mPhone, string PPSN, string Username, string Password, string empType) : base(no, name, Lname, address, mPhone, PPSN, Username, Password, empType)
        {

        }

        public Employee(int no, string name, string Lname, string address, string email, int hPhone, int mPhone, string nextToKin, int nextToKinPhoneNo, string nextToKinRelationship, string PPSN, string Username, string Password, string empType) : base(no, name, Lname, address, email, hPhone, mPhone, nextToKin, nextToKinPhoneNo, nextToKinRelationship, PPSN, Username, Password, empType)
        {

        }

        
    }
}
