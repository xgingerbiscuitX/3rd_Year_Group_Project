using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Console;
using System.Collections.Specialized;
using Software_Engineering_Project;
using System.Windows.Threading;
using SoftwareEngineeringT1.ViewModels;
using SoftwareEngineeringT1;
using System.Threading;
using BusinessLayer;
using DataAccessLayer;
using BusinessEntities;
using MaterialDesignThemes.Wpf;

namespace SoftwareEngineeringTeam1
{
    public partial class Test : Window
    {
        public IAccessHandler Model;
        public MainWindow FC;
        public List<ItemMenu> sidePanelItemsList;
        public IDataLayer _Datalayer;

        TimeSpan _time;
        DispatcherTimer _timer;


        public Test(MainWindow parent, IAccessHandler _Model)
        {
            Model = _Model;
            FC = parent;

            _Datalayer = IDatalayer.GetInstance();
            _Model = AccessHandler.GetInstance(_Datalayer);

            InitializeComponent();

            _time = TimeSpan.FromSeconds(0);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            Refresh();           
        }

        public void Refresh()
        {
            sidePanelItemsList = GetSideItems(Model.CurrentUser);
            if (sidePanelItemsList.Any())
            {
                foreach (var a in sidePanelItemsList)
                {
                    SidePanelMenu.Children.Add(new UserControlMenuItem(a, this));
                }
            }
        }

        public List<ItemMenu> GetSideItems(IStaff CStaff)
        {
            EmpDisplay.Text = String.Concat(CStaff.Name, " ", CStaff.LName);

            List<ItemMenu> sidePanelItemList = new List<ItemMenu>();
            var receptionistFunc = new List<SubItem>();
            var userManagement = new List<SubItem>();
            var clean = new List<SubItem>();
            var settingsFunc = new List<SubItem>();
            var stock = new List<SubItem>();
            var leisurecenter = new List<SubItem>();
            var bar = new List<SubItem>();


            //Assigning sidebar items

            switch (CStaff.EmployeeType)
            {
                case "Receptionist":
                    Model.CurrentUser = new Receptionist(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone,CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    receptionistFunc.Add(new SubItem("Manage reservations", new UC_ManageReservations(this, Model)));
                    receptionistFunc.Add(new SubItem("Make Resrevation", new UC_CheckRoomAvailability(this, Model)));
                    var ReceptionistFunctionsListRec = new ItemMenu("Reception", receptionistFunc, PackIconKind.AccountTie);
                    sidePanelItemList.Add(ReceptionistFunctionsListRec);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListRec = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListRec);

            return sidePanelItemList;

                case "CEO":
                    Model.CurrentUser = new CEO(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    //receptionistFunc.Add(new SubItem("Make Resrevation", new UC_MakeReservation(this, Model)));
                    receptionistFunc.Add(new SubItem("Manage reservations", new UC_ManageReservations(this, Model)));
                    receptionistFunc.Add(new SubItem("Make Reservation",new UC_CheckRoomAvailability(this,Model)));
                    //receptionistFunc.Add(new SubItem("Check in", new UC_CheckInGuest(this, Model)));
                    //receptionistFunc.Add(new SubItem("Check out", new UC_CheckOutGuest(this, Model)));
                    var ReceptionistFunctionsListCEO = new ItemMenu("Reception", receptionistFunc, PackIconKind.AccountTie);
                    sidePanelItemList.Add(ReceptionistFunctionsListCEO);

                   
                   // userManagement.Add(new SubItem("Add staff", new UC_AddStaff(this, Model)));
                    userManagement.Add(new SubItem("Manage staff", new UC_ManageStaff(this, Model)));
                    userManagement.Add(new SubItem("Manage rooms", new UC_ManageRooms(this, Model)));
                    userManagement.Add(new SubItem("Add order", new UC_AddOrder(this, Model)));
                    var userManagementListCEO = new ItemMenu("Manage", userManagement, PackIconKind.AccountTie);
                    sidePanelItemList.Add(userManagementListCEO);

                   
                    clean.Add(new SubItem("Record clean", new UC_RecordRoomCleaning(this, Model)));
                    clean.Add(new SubItem("Cleaning schedule", new UC_ViewCleaningSchedule(this, Model)));
                    clean.Add(new SubItem("Replenish Cart", new UC_ReplenishCart(this, Model)));
                    var cleanListCEO = new ItemMenu("Cleaning", clean, PackIconKind.AccountTie);
                    sidePanelItemList.Add(cleanListCEO);

                    
                    leisurecenter.Add(new SubItem("Check in/out", new UC_LeisureCenterCheckInOut(this, Model)));
                    leisurecenter.Add(new SubItem("Manage members", new UC_ManageMember(this, Model)));
                    leisurecenter.Add(new SubItem("View occupancy", new UC_ViewLeisureCenterOcupancy(this, Model)));
                    leisurecenter.Add(new SubItem("Add Member", new UC_AddMember(this, Model)));

                    leisurecenter.Add(new SubItem("View Leisure Cleaning", new UC_LeisureCleanView(this, Model)));
                    leisurecenter.Add(new SubItem("Record Cleaning", new UC_RecordCleanLeisure(this, Model)));
                    var leisurecenterlistCEO = new ItemMenu("Leisure", leisurecenter, PackIconKind.SwimmingPool);
                    sidePanelItemList.Add(leisurecenterlistCEO);

                    bar.Add(new SubItem("Record bar sale", new UC_RecordBarSale(this, Model)));
                    bar.Add(new SubItem("Record COVID Info", new UC_RecordCustomerCOVID(this, Model)));
                    var barItemListCEO = new ItemMenu("Bar", bar, PackIconKind.Bar);
                    sidePanelItemList.Add(barItemListCEO);


                    stock.Add(new SubItem("Manage stock", new UC_ManageStock(this, Model)));
                    stock.Add(new SubItem("Move Stock", new UC_MoveStock(this, Model)));
                    var stockListCEO = new ItemMenu("Stock", stock, PackIconKind.TruckDelivery);
                    sidePanelItemList.Add(stockListCEO);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListCEO = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListCEO);

                    return sidePanelItemList;

                case "Cleaner":
                    Model.CurrentUser = new Cleaner(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    clean.Add(new SubItem("Record clean", new UC_RecordRoomCleaning(this, Model)));
                    clean.Add(new SubItem("Cleaning schedule", new UC_ViewCleaningSchedule(this, Model)));
                    var cleanListCleaner = new ItemMenu("Cleaning", clean, PackIconKind.Brush);
                    sidePanelItemList.Add(cleanListCleaner);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncList = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncList);

                    return sidePanelItemList;

                case "Cleaning Manager":
                    Model.CurrentUser = new Cleaning_Manager(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    clean.Add(new SubItem("Record clean", new UC_RecordRoomCleaning(this, Model)));
                    clean.Add(new SubItem("Cleaning schedule", new UC_ViewCleaningSchedule(this, Model)));
                    var cleanListCManager = new ItemMenu("Cleaning", clean, PackIconKind.Brush);
                    sidePanelItemList.Add(cleanListCManager);

                  //  userManagement.Add(new SubItem("Add staff", new UC_AddStaff(this, Model)));
                    userManagement.Add(new SubItem("Manage staff", new UC_ManageStaff(this, Model)));
                    var userManagementListCManager = new ItemMenu("Manage", userManagement, PackIconKind.AccountTie);
                    sidePanelItemList.Add(userManagementListCManager);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListCManager = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListCManager);

                    return sidePanelItemList;

                case "Bar Staff":
                    Model.CurrentUser = new Bar_Staff(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    bar.Add(new SubItem("Record bar sale", new UC_RecordBarSale(this, Model)));
                    bar.Add(new SubItem("Move Stock", new UC_MoveStock(this, Model)));
                    var barItemListBarStaff = new ItemMenu("Bar", bar, PackIconKind.Bar);
                    sidePanelItemList.Add(barItemListBarStaff);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListBarStaff = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListBarStaff);

                    return sidePanelItemList;

                case "Bar Manager":
                    Model.CurrentUser = new Bar_Manager(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    bar.Add(new SubItem("Record bar sale", new UC_RecordBarSale(this, Model)));
                    bar.Add(new SubItem("Manage stock", new UC_ManageStock(this, Model)));
                    bar.Add(new SubItem("Move Stock", new UC_MoveStock(this, Model)));
                    var barItemListBarManager = new ItemMenu("Bar", bar, PackIconKind.Bar);
                    sidePanelItemList.Add(barItemListBarManager);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListBarManager = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListBarManager);

                    return sidePanelItemList;

                case "Leisure Centre Manager":
                    Model.CurrentUser = new Leisure_Center_Manager(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                   
                    userManagement.Add(new SubItem("Manage staff", new UC_ManageStaff(this, Model)));
                    userManagement.Add(new SubItem("View Leisure Cleaning", new UC_LeisureCleanView(this, Model)));
                    userManagement.Add(new SubItem("Record Cleaning", new UC_RecordCleanLeisure(this, Model)));
                    var userManagementList = new ItemMenu("Manage", userManagement, PackIconKind.AccountTie);
                    sidePanelItemList.Add(userManagementList);

                    leisurecenter.Add(new SubItem("Check in/out", new UC_LeisureCenterCheckInOut(this, Model)));
                    leisurecenter.Add(new SubItem("Manage members", new UC_ManageMember(this, Model)));
                    leisurecenter.Add(new SubItem("View occupancy", new UC_ViewLeisureCenterOcupancy(this, Model)));
                    leisurecenter.Add(new SubItem("Add Member", new UC_AddMember(this, Model)));
                    leisurecenter.Add(new SubItem("Record Cleaning", new UC_RecordCleanLeisure(this, Model)));

                    var leisurecenterlistLCManager = new ItemMenu("Leisure", leisurecenter, PackIconKind.SwimmingPool);
                    sidePanelItemList.Add(leisurecenterlistLCManager);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListLCManager = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListLCManager);

                    return sidePanelItemList;

                case "Leisure Centre Staff":
                    Model.CurrentUser = new Leisure_Center_Staff(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    leisurecenter.Add(new SubItem("Check in/out", new UC_LeisureCenterCheckInOut(this, Model)));
                    leisurecenter.Add(new SubItem("View occupancy", new UC_ViewLeisureCenterOcupancy(this, Model)));
                    leisurecenter.Add(new SubItem("Add Member", new UC_AddMember(this, Model)));
                    var leisurecenterlistLCStaff = new ItemMenu("Leisure", leisurecenter, PackIconKind.SwimmingPool);
                    sidePanelItemList.Add(leisurecenterlistLCStaff);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListLCStaff = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListLCStaff);

                    return sidePanelItemList;

                case "Reception Manager":
                    Model.CurrentUser = new Reception_Manager(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                    receptionistFunc.Add(new SubItem("Make reservation", new UC_CheckRoomAvailability(this, Model)));
                    receptionistFunc.Add(new SubItem("Manage reservations", new UC_ManageReservations(this, Model)));
                    receptionistFunc.Add(new SubItem("Check in", new UC_CheckInGuest(this, Model)));
                    receptionistFunc.Add(new SubItem("Check out", new UC_CheckOutGuest(this, Model)));
                    var ReceptionistFunctionsListRM = new ItemMenu("Reception", receptionistFunc, PackIconKind.AccountTie);
                    sidePanelItemList.Add(ReceptionistFunctionsListRM);

                    
                   // userManagement.Add(new SubItem("Add staff", new UC_AddStaff(this, Model)));
                    userManagement.Add(new SubItem("Manage staff", new UC_ManageStaff(this, Model)));
                    userManagement.Add(new SubItem("Manage members", new UC_ManageMember(this, Model)));

                    var userManagementListRM = new ItemMenu("Manage", userManagement, PackIconKind.AccountTie);
                    sidePanelItemList.Add(userManagementListRM);

                    
                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListRM = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListRM);

                    return sidePanelItemList;

                case "Stock Manager":
                    Model.CurrentUser = new Stock_Manager(CStaff.EmpNo, CStaff.Name, CStaff.LName, CStaff.Address, CStaff.Email, CStaff.Mphone, CStaff.HPhone, CStaff.NextToKin, CStaff.NextToKinPhoneNo, CStaff.NextToKinRel, CStaff.PPSN, CStaff.UserName, CStaff.Password, CStaff.EmployeeType);
                  //  userManagement.Add(new SubItem("Add staff", new UC_AddStaff(this, Model)));
                    userManagement.Add(new SubItem("Manage staff", new UC_ManageStaff(this, Model)));
                    var userManagementListSM = new ItemMenu("Manage", userManagement, PackIconKind.AccountTie);
                    sidePanelItemList.Add(userManagementListSM);

                    stock.Add(new SubItem("Manage stock", new UC_ManageStock(this, Model)));
                    stock.Add(new SubItem("Move Stock", new UC_MoveStock(this, Model)));
                    var stockListSM = new ItemMenu("Stock", stock, PackIconKind.TruckDelivery);
                    sidePanelItemList.Add(stockListSM);

                    settingsFunc.Add(new SubItem("Log out", new UC_Logout(this, Model)));
                    var settingsFuncListSM = new ItemMenu("Settings", settingsFunc, PackIconKind.Settings);
                    sidePanelItemList.Add(settingsFuncListSM);

                    return sidePanelItemList;


                default:

                    var ReceptionistFunctionsListDEF = new ItemMenu("Reception", null, PackIconKind.AccountTie);
                    sidePanelItemList.Add(ReceptionistFunctionsListDEF);

                    return sidePanelItemList;
            }
            

        }

        internal void SwitchScreen(object sender)
        {
            UserControl screen = ((UserControl)sender);
            Type screenType = screen.GetType();
            UserControl newS = ((UserControl)Activator.CreateInstance(screenType, this, Model));
            if(screen!=null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(newS);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _Datalayer.displayConStat();
        }
    }
}
