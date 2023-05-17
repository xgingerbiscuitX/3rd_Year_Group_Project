using MaterialDesignThemes.Wpf;
using SoftwareEngineeringT1;
using SoftwareEngineeringT1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
     public class Leisure_Center_Staff : Employee
    {
        public Leisure_Center_Staff(int no, string name, string LName, string address, string email, int hPhone, int mPhone, string nextToKin, int nextToKinPhoneNo, string nextToKinRelationship, string PPSN, string Username, string Password, string empType) : base(no, name, LName, address, email, hPhone, mPhone, nextToKin, nextToKinPhoneNo, nextToKinRelationship, PPSN, Username, Password, empType)
        {
            this.EmpNo = no;
            this.Name = name;
            this.LName = LName;
            this.Address = address;
            this.Email = email;
            this.HPhone = hPhone;
            this.Mphone = Mphone;
            this.NextToKin = nextToKin;
            this.NextToKinPhoneNo = nextToKinPhoneNo;
            this.NextToKinRel = nextToKinRelationship;
            this.PPSN = PPSN;
            this.UserName = UserName;
            this.Password = Password;
            this.EmployeeType = empType;
        }
        /*public override List<ItemMenu> printForm()
        {
            //Initialise empty ItemMenu list
            List<ItemMenu> sidePanelItemList = new List<ItemMenu>();

            //Here we can add the items we want to display in the menu

            var leisurecenter = new List<SubItem>();
            leisurecenter.Add(new SubItem("Check in/out", new UC_LeisureCenterCheckInOut()));
            leisurecenter.Add(new SubItem("View occupancy", new UC_ViewLeisureCenterOcupancy()));
            var leisurecenterlist = new ItemMenu("Settings", leisurecenter, PackIconKind.SwimmingPool);
            sidePanelItemList.Add(leisurecenterlist);

            var settingsFunc = new List<SubItem>();
            settingsFunc.Add(new SubItem("Log out", new UC_Logout()));
            var settingsFuncList = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
            sidePanelItemList.Add(settingsFuncList);

            return sidePanelItemList;
        }*/
    }
}
