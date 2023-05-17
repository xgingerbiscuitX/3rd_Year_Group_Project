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
    public abstract class Elevated_Employee : Staff
    {

        public Elevated_Employee() { }
        public Elevated_Employee(int no, string name, string Lname, string address, string email, int hPhone, int mPhone, string nextToKin, int nextToKinPhoneNo, string nextToKinRelationship, string PPSN, string Username, string Password, string empType) : base(no, name, Lname, address, email, hPhone, mPhone, nextToKin, nextToKinPhoneNo, nextToKinRelationship, PPSN, Username, Password, empType)
        {

        }

        public Elevated_Employee(int no, string name, string Lname, string address, int mPhone,string PPSN, string Username, string Password, string empType) : base(no, name, Lname, address, mPhone, PPSN, Username, Password, empType)
        {

        }



        //Code I have from another project could be reused
        //public virtual bool Remove_Staff()
        //{
        //    WriteLine("You are a " + Global.Logged_In_Employee.getEmpType());
        //    Write("Employee number: ");
        //    string EmpNo = ReadLine();

        //    string query = "CALL Delete_staff('" + EmpNo + "');";

        //    if(DH.Delete(query))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public virtual bool Add_Staff()
        //{
        //    WriteLine("You are a " + Global.Logged_In_Employee.getEmpType());
        //    WriteLine("Adding Staff...");

        //    string EmpNo = Guid.NewGuid().ToString("N");

        //    Write("Name: ");
        //    string name = ReadLine();

        //    Write("Address: ");
        //    string address = ReadLine();

        //    Write("Mobile phone number: ");
        //    int MPhone = Convert.ToInt32(ReadLine());

        //    Write("PPSN: ");
        //    string PPSN = ReadLine();

        //    Write("Username: ");
        //    string username = ReadLine();

        //    Write("Password: ");
        //    string password = CPT.Encrypt(ReadLine());

        //    Write("Employee type: ");
        //    string EmpType = ReadLine();

        //    Staff newRec = new Staff(EmpNo, name, address, MPhone, PPSN, EmpType);
        //    string query = "CALL Add_Staff('" + EmpNo + "','" + name + "','" + address + "','" + Mphone + "','" + PPSN + "','" + username + "','" + password + "','" + EmpType + "');";
        //    if(DH.Write(query, newRec))
        //    {
        //        return true;
        //    }

        //    return false;


        //}

        //public bool Change_Permissions()
        //{
        //    Write("Employee number: ");
        //    string EmpNo = ReadLine();

        //    Write("Employee type: ");
        //    string EmpType = ReadLine();

        //    string query = "CALL Update_Staff_Permissions('" + EmpNo + "','" + EmpType + "');";
        //    if (DH.Update(query, EmpNo))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
