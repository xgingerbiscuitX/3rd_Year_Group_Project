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
    public class CEO : Elevated_Employee
    {
        public CEO() { }
        public CEO(int no, string name, string Lname, string address, string email, int hPhone, int mPhone, string nextToKin, int nextToKinPhoneNo, string nextToKinRelationship, string PPSN, string Username, string Password, string empType) : base(no, name, Lname, address, email, hPhone, mPhone, nextToKin, nextToKinPhoneNo, nextToKinRelationship, PPSN, Username, Password, empType)
        {
            this.EmpNo = no;
            this.Name = name;
            this.LName = Lname;
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


    /*    public override List<ItemMenu> printForm()
        {
            //Initialise empty ItemMenu list
            List<ItemMenu> sidePanelItemList = new List<ItemMenu>();

            //Here we can add the items we want to display in the menu

            var receptionistFunc = new List<SubItem>();
            receptionistFunc.Add(new SubItem("Make reservation", new UC_MakeReservation()));
            receptionistFunc.Add(new SubItem("Manage reservations", new UC_ManageReservations()));
            receptionistFunc.Add(new SubItem("Check in", new UC_CheckInGuest()));
            receptionistFunc.Add(new SubItem("Check out", new UC_CheckOutGuest()));
            var ReceptionistFunctionsList = new ItemMenu("Reception", receptionistFunc, PackIconKind.AccountTie);
            sidePanelItemList.Add(ReceptionistFunctionsList);

            var userManagement = new List<SubItem>();
            userManagement.Add(new SubItem("Add staff", new UC_AddStaff()));
            userManagement.Add(new SubItem("Manage staff", new UC_ManageStaff()));
            var userManagementList = new ItemMenu("Management", userManagement, PackIconKind.AccountTie);
            sidePanelItemList.Add(userManagementList);

            var clean = new List<SubItem>();
            clean.Add(new SubItem("Record clean", new UC_RecordRoomCleaning()));
            clean.Add(new SubItem("Cleaning schedule", new UC_ViewCleaningSchedule()));
            var cleanList = new ItemMenu("Cleaning", clean, PackIconKind.AccountTie);
            sidePanelItemList.Add(cleanList);

            var leisurecenter = new List<SubItem>();
            leisurecenter.Add(new SubItem("Check in/out", new UC_LeisureCenterCheckInOut()));
            leisurecenter.Add(new SubItem("Manage members", new UC_ManageMember()));
            leisurecenter.Add(new SubItem("View occupancy", new UC_ViewLeisureCenterOcupancy()));
            var leisurecenterlist = new ItemMenu("Settings", leisurecenter, PackIconKind.SwimmingPool);
            sidePanelItemList.Add(leisurecenterlist);

            var stock = new List<SubItem>();
            stock.Add(new SubItem("Manage stock", new UC_ManageStock()));
            var stockList = new ItemMenu("Stock", stock, PackIconKind.TruckDelivery);
            sidePanelItemList.Add(stockList);

            var settingsFunc = new List<SubItem>();
            settingsFunc.Add(new SubItem("Log out", new UC_Logout()));
            var settingsFuncList = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
            sidePanelItemList.Add(settingsFuncList);

            return sidePanelItemList;
        }*/
    }
}
