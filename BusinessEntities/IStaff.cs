using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IStaff
    {
        int EmpNo { get; set; }
        string Name { get; set; }
        string LName { get; set; }
        string Address { get; set; }
        string Email { get; set; }
        int HPhone { get; set; }
        int Mphone { get; set; }
        string NextToKin { get; set; }
        int NextToKinPhoneNo { get; set; }
        string NextToKinRel { get; set; }
        string PPSN { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string EmployeeType { get; set; }
    }
}
