using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface ILeisureMember
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime ExpireDate { get; set; }
        int Age { get; set; }
        string MedicalConditions { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        byte InLeisure { get; set; }
        string AreaType { get; set; }
    }
}