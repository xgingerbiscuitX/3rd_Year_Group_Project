using System;

namespace BusinessEntities
{
    public interface ICovidInfo
    {
         string FirstName { get; set; }
         string LastName { get; set; }
         string PhoneNo { get; set; }
         DateTime Date { get; set; }
    }
}