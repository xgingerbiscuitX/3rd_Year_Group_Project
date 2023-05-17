using SoftwareEngineeringT1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Staff : IStaff
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int HPhone { get; set; }
        public int Mphone { get; set; }
        public string NextToKin { get; set; }
        public int NextToKinPhoneNo { get; set; }
        public string NextToKinRel { get; set; }
        public string PPSN { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeType { get; set; }

        public int getEmpNum()
        {
            return this.EmpNo;
        }
        public string getEmpName()
        {
            return this.Name;
        }
        public string getEmpLName()
        {
            return this.LName;
        }

        public string getEmpStreetAddress()
        {
            return this.Address;
        }

        public int getEmpPhoneNo()
        {
            return this.HPhone;
        }
        public string getEmpNextToKinName()
        {
            return this.NextToKin;
        }

        public string getEmpNextToKinPhone()
        {
            return this.Name;
        }
        public string getEmpNextToKinRelationship()
        {
            return this.NextToKinRel;
        }

        public string getEmpPPSN()
        {
            return this.PPSN;
        }

        public string getEmpType()
        {
            return this.EmployeeType;
        }


        public List<ItemMenu> printForm()
        {
            return null;
        }

        public Staff()
        {
            this.Name = "Unknown";
            this.LName = "Unknown";
            this.Address = "Unknown";
            this.Email = "Unknown";
            this.HPhone = 0;
            this.Mphone = 0;
            this.NextToKin = "Unknown";
            this.NextToKinPhoneNo = 0;
            this.NextToKinRel = "Unknown";
            this.PPSN = "Unknown";
            this.EmpNo = 0;
            this.EmployeeType = "Unknown";
        }

        public Staff(int no, string name, string lname, string address, int MPhone, string PPSN, string username, string password, string EmpType)
        {
            this.Name = name;
            this.LName = lname;
            this.Address = address;
            this.Email = "Unknown";
            this.HPhone = 0;
            this.Mphone = MPhone;
            this.NextToKin = "Unknown";
            this.NextToKinPhoneNo = 0;
            this.NextToKinRel = "Unknown";
            this.PPSN = PPSN;
            this.UserName = username;
            this.Password = password;
            this.EmpNo = no;
            this.EmployeeType = EmpType;
        }

        public Staff(int EmpNo, string name, string lname, string address, string email, int HPhone, int MPhone, string NextToKin, int NextToKinPhoneNo, string NextToKinRel, string PPSN, string username, string password, string EmpType)
        {
            this.Name = name;
            this.LName = lname;
            this.Address = address;
            this.Email = email;
            this.HPhone = HPhone;
            this.Mphone = MPhone;
            this.NextToKin = NextToKin;
            this.NextToKinPhoneNo = NextToKinPhoneNo;
            this.NextToKinRel = NextToKinRel;
            this.PPSN = PPSN;
            this.UserName = username;
            this.Password = password;
            this.EmpNo = EmpNo;
            this.EmployeeType = EmpType;
        }
    }
}
