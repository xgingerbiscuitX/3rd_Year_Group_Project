using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class LeisureMember : ILeisureMember
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Age { get; set; }
        public string MedicalConditions { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte InLeisure { get; set; }
        public string AreaType { get; set; }
        public LeisureMember()
        {
            this.ID = 0;
            this.FirstName = "Unknown";
            this.LastName = "Unknown";
            this.ExpireDate = DateTime.Now;
            this.Age = 0;
            this.MedicalConditions = "Unknown";
            this.Email = "Unknown";
            this.Phone = "Unknown";
        }
        public LeisureMember(int ID, string fname, string lname, DateTime date, int age, string medicalConditions, string email, string phone, byte inLC, string areaType)
        {
            this.ID = ID;
            this.FirstName = fname;
            this.LastName = lname;
            this.ExpireDate = date;
            this.Age = age;
            this.MedicalConditions = medicalConditions;
            this.Email = email;
            this.Phone = phone;
            this.InLeisure = inLC;
            this.AreaType = areaType;
        }
    }
}
