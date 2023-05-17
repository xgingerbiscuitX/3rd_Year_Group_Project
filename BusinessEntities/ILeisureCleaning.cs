using System;

namespace BusinessEntities
{
    public interface ILeisureCleaning
    {
        int Reference_No { get; set; }
        string Employee_Name { get; set; }
        string AreaCleaned { get; set; }
        string Comment { get; set; }
        DateTime dateDone { get; set; }
    
}
}