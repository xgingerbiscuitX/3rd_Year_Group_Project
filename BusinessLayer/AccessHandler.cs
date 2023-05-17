using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using DataAccessLayer;
using BusinessEntities;
using System.Collections;
using BusinessLayer;
using System.Windows;
using ZXing;
using System.Drawing;

namespace Software_Engineering_Project
{

    public partial class AccessHandler : IAccessHandler
    {
        private static IAccessHandler modelSingletonInstance;  // Model object is a singleton so only one instance allowed
        static readonly object padlock = new object(); // Need this for thread safety on the Model singleton creation. ie in GetInstance () method
        private IDataLayer dataLayer;
        public Staff currentUser;
        public Reservation currentReservation;
        public ILeisureMember currentMember;
        //public Staff Currentstaff;
        public ArrayList userList = new ArrayList();
        public ArrayList RoomList = new ArrayList();
        public ArrayList reservationList = new ArrayList();
        public ArrayList orderList = new ArrayList();
        public ArrayList barSaleList = new ArrayList();
        public ArrayList roomPricingList = new ArrayList();
        public ArrayList GuestList { get; set; } = new ArrayList();
        public ArrayList RoomTypeList { get; set; } = new ArrayList();
        public ArrayList memberList = new ArrayList();
        public DateTime endDate { get; set; }
        public DateTime startDate { get; set; }
        public string roomType { get; set; }
        public ArrayList toBeCleanedList = new ArrayList();
        public ArrayList stockList = new ArrayList();
        public ArrayList moveStockList = new ArrayList();
        public ArrayList barStockList = new ArrayList();
        public ArrayList unavailableRoomList = new ArrayList();
        public ArrayList LeisureCleanList;
        public ArrayList commonItemsList = new ArrayList();


        public IDataLayer DataLayer
        {
            get { return dataLayer; }
            set { dataLayer = value; }
        }
        public ArrayList UnavailableRoomList
        {
            get
            {
                return unavailableRoomList;
            }
        }

        public Staff CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
        public Staff Currentstaff
        {
            get { return Currentstaff; }
            set { Currentstaff = value; }
        }
        public Reservation CurrentReservation
        {
            get
            {
                return currentReservation;
            }
            set
            {
                currentReservation = value;
            }
        }
        public ILeisureMember CurrentMember
        {
            get
            {
                return currentMember;
            }
            set
            {
                currentMember = value;
            }
        }

        public ArrayList UserList
        {
            get
            {
                return userList;
            }
        }
        public ArrayList roomList
        {
            get
            {
                return RoomList;
            }
        }
        public ArrayList ReservationList
        {
            get
            {
                return reservationList;
            }
        }

        public ArrayList OrderList
        {
            get
            {
                return OrderList;
            }
        }
        public ArrayList BarSaleList
        {
            get
            {
                return barSaleList;
            }
        }

        public ArrayList MemberList
        {
            get
            {
                return memberList;
            }
        }

        public ArrayList ToBeCleanedList
        {
            get
            {
                return toBeCleanedList;
            }
            set {

            }
        }


        public ArrayList StockList
        {
            get
            {
                return stockList;
            }
        }
        public ArrayList MoveStockList
        {
            get
            {
                return moveStockList;
            }
        }
        public ArrayList BarStockList
        {
            get
            {
                return barStockList;
            }
        }
        public ArrayList RoomPricingList
        {
            get
            {
                return roomPricingList;
            }
        }
        public ArrayList Leisure_CleanList
        {
            get
            {
                return LeisureCleanList;

            }
        }
        public ArrayList CommonItemsList
        {
            get
            {
                return commonItemsList;
            }
        }

        public static IAccessHandler GetInstance(IDataLayer _DataLayer) // With Singleton pattern this method is used rather than constructor
        {
            lock (padlock) //   only one thread can entry this block of code
            {
                if (modelSingletonInstance == null)
                {
                    modelSingletonInstance = new AccessHandler(_DataLayer);
                }
                return modelSingletonInstance;
            }
        }
        private AccessHandler(IDataLayer _DataLayer)  // The constructor is private as its a singleton and I only allow one instance which is created with the GetInstance() method
        {
            userList = new ArrayList();
            dataLayer = _DataLayer;
            userList = dataLayer.getAllUsers();
        }


        //Refresh lists super useful, added by lukasz ;)))
        public ArrayList refreshUserList()
        {
            return userList = dataLayer.getAllUsers();
        }
        public ArrayList refreshRoomList()
        {
            RoomList = dataLayer.GetAllRooms();
            foreach (IRoom room in RoomList)
            {
                switch (DateTime.Compare(DateTime.Now, Convert.ToDateTime(room.EndDate)))
                {
                    case 1:
                        room.Availability = "Available";
                        break;
                }
            }

            return RoomList;
        }
        public ArrayList refreshReservationList()
        {
            return reservationList = dataLayer.getAllReservations();
        }
        public ArrayList refreshOrderList()
        {
            return orderList = dataLayer.getAllOrders();
        }
        public ArrayList refreshBarSaleList()
        {
            return barSaleList = dataLayer.getAllBarSales();
        }
        public ArrayList refreshMemberList()
        {
            return memberList = dataLayer.getAllMembers();
        }
        public ArrayList refreshGuestList()
        {
            return GuestList = dataLayer.getAllGuests();
        }
        public ArrayList refreshRoomTypeList()
        {
            return RoomTypeList = dataLayer.getAllRoomTypes();
        }
        public ArrayList refreshToBeCleanedList()
        {
            return toBeCleanedList = dataLayer.getToBeCleanedList();
        }
        public ArrayList refreshStockList()
        {
            return stockList = dataLayer.getAllStock();
        }
        public ArrayList refreshMoveStockList()
        {
            return moveStockList = dataLayer.getAllMoveStock();
        }
        public ArrayList refreshRoomPricingList()
        {
            return roomPricingList = dataLayer.getRoomPricing();
        }
        public ArrayList refreshBarStockList()
        {
            return barStockList = dataLayer.getAllBarStock();
        }
        public ArrayList refreshUnavailableRoomList()
        {
            return unavailableRoomList = dataLayer.getUnavailableRooms();
        }

        public ArrayList refreshLeisureCleanList() {

            return LeisureCleanList = dataLayer.getAllCleanList();

        }
        public ArrayList refreshCommonItemsList()
        {
            return commonItemsList = DataLayer.getAllCommonItems();
        }
        ~AccessHandler()
        {
            tearDown();
        }

        public void tearDown()
        {
            DataLayer.closeConnection();
        }

        public bool Login(string username, string password)
        {
            foreach (Staff staff in userList)
            {
                //MessageBox.Show("What is being checked: " + staff.UserName + " - " + staff.Password + "\nWhat You typed in: " + username + " - " + password);
                if (username == staff.UserName && Crypt.Encrypt(password) == staff.Password)
                {
                    CurrentUser = staff;
                    return true;
                }
            }

            return false;
        }

        public void LogOut()
        {
            CurrentUser = null;
        }

        public bool ChangeMemberStatus(ILeisureMember theMember, byte status)
        {
            DataLayer.ChangeMemberStatus(theMember, status);
            return true;
        }

        public bool DeleteMember(ILeisureMember leisureMember)
        {
            DataLayer.deleteMember(leisureMember);
            return true;

        }

        public bool ChangeGuestStatus(IGuest theGuest, bool status)
        {
            DataLayer.ChangeGuestStatus(theGuest, status);
            return true;
        }


        public bool addNewUser(string name, string lname, string address, string email, int HPhone, int MPhone, string NextToKin, int NextToKinPhoneNo, string NextToKinRel, string PPSN, string username, string password, string EmpType)
        {
            try
            {
                int maxId = 0;
                foreach (Staff staff in UserList)
                {
                    if (staff.EmpNo > maxId)
                        maxId = staff.EmpNo;
                }
                IStaff theStaff = StaffFactory.GetStaff(maxId + 1, name, lname, address, email, HPhone, MPhone, NextToKin, NextToKinPhoneNo, NextToKinRel, PPSN, username, password, EmpType);   // Using a Factory to create the user entity object. ie seperating object creation from business logic
                UserList.Add(theStaff);
                DataLayer.addNewUserToDB(theStaff);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool deleteUser(IStaff user)
        {
            DataLayer.deleteUserFromDB(user);
            UserList.Remove(user);
            return true;
        }

        public bool editUser(IStaff user)
        {
            DataLayer.editUserInDB(user);
            return true;
        }

        public bool editRoom(IRoom room)
        {
            DataLayer.editRoomInDB(room);
            return true;
        }

        public bool editRoomUnavailability(IRoom room, string s, string e)
        {
            DataLayer.editRoomUnavailability(room, s, e);
            return true;
        }
        public bool editReservation(IReservation r)
        {
            DataLayer.editReservation(r);
            return true;
        }
        public bool addCommonItemDB(IStock s)
        {
            DataLayer.addCommonItemDB(s);
            return true;
        }
        public bool removeCommonItemDB(IStock s)
        {
            DataLayer.removeCommonItemDB(s);
            return true;
        }
        public string getUserTypeForCurrentuser()
        {
            return currentUser.EmployeeType;
        }

        public bool checkInGuest(string fName, string lName, bool isAdult, string roomNo, string reservationNo)
        {

            return DataLayer.CheckInGuest(fName, lName, isAdult, roomNo, reservationNo);
        }
        public bool changeRoomPricing(IRoomPrice roomPrice)
        {
            return DataLayer.ChangeRoomPricing(roomPrice);
        }

        public int getmaxresid()
        {
            return DataLayer.getmaxresid();
        }


        public bool addNewReservation(string fName, string lName, DateTime arrivalD, DateTime departD, string address1, string address2, string address3, string email, string PhoneNo, string SpecialRequest, int roomType, int adultNo, int childernNo)
        {
            try
            {
                //  DateTime checkIn = DateTime.MinValue, checkOut = DateTime.MinValue;
                int status = 3;

                IReservation theReserv = ReservationFactory.GetReservation(fName, lName, arrivalD, departD, status, email, address1, address2, address3, PhoneNo, SpecialRequest, adultNo, childernNo, roomType);   // Using a Factory to create the reservation entity object. ie seperating object creation from business logic
                ReservationList.Add(theReserv);
                DataLayer.addReservationDB(theReserv);

                int newmax = this.getmaxresid();
                theReserv.ReservationNo = newmax;


                

                var qrcodewriter = new BarcodeWriter();
                //MessageBox.Show(System.AppDomain.CurrentDomain.BaseDirectory);

                qrcodewriter.Format = BarcodeFormat.QR_CODE;

                qrcodewriter.Write(newmax.ToString()).Save(System.AppDomain.CurrentDomain.BaseDirectory + @"\ex.jpg");

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }


        }

        public bool addNewOrder(string orderID, string ItemID, string Item_name, int Quantity)
        {
            IOrder theOrder = OrdersFactory.GetOrder(orderID, ItemID, Item_name, Quantity);

            try
            {
                orderList.Add(theOrder);
                DataLayer.addNewOrderToDB(theOrder);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }


        }
        public bool addNewBarSale(string orderID, string ItemID, string Item_name, int Quantity)
        {
            IBarSale theBarSale = BarSaleFactory.GetBarSale(orderID, ItemID, Item_name, Quantity);

            try
            {
                barSaleList.Add(theBarSale);
                DataLayer.addNewBarSaleToDB(theBarSale);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }


        }


        public bool deleteReservation(IReservation reservation)
        {
            DataLayer.deleteReservationFromDB(reservation);
            ReservationList.Remove(reservation);
            return true;
        }
        public bool addNewMember(string fname, string lname, DateTime date, int age, string medicalConditions, string email, string phone, string areaType)
        {
            try
            {

                byte inleisurecenter = 1;
                int maxId = 0;
                foreach (LeisureMember CMember in memberList)
                {
                    if (CMember.ID > maxId)
                        maxId = CMember.ID;
                }

                ILeisureMember member = LeisureMemberFactory.GetMember(maxId + 1, fname, lname, date, age, medicalConditions, email, phone, inleisurecenter, areaType);   // Using a Factory to create the user entity object. ie seperating object creation from business logic
                memberList.Add(member);
                //MessageBox.Show(member.AreaType);
                DataLayer.addLeisureMemberDB(member);
                return true;
            }

            catch (System.Exception)
            {
                return false;
            }


        }
        public bool checkOutGuest()
        {
            IReservation r = this.CurrentReservation;
            r.Status = 2; //changing status to checked out
            r.CheckOutDate = DateTime.Now; //changing check out time to current time
            bool rBool = DataLayer.editReservation(r);

            List<IGuest> guests = new List<IGuest>();
            foreach (IGuest g in GuestList)
            {
                if (g.ReservationID == r.ReservationNo)
                {
                    guests.Add(g);
                }
            }
            bool gBool = DataLayer.deleteGuests(guests);
            if (gBool && rBool)
            {
                this.ReservationList.Clear();
                this.reservationList = DataLayer.getAllReservations();
                return true;
            }
            return false;
        }

        public List<DateTime> GetFullRooms(int roomType)
        {
            refreshUnavailableRoomList();
            int capacity = 0;
            foreach (IRoom r in RoomList)
            {
                if (r.RoomType == roomType)
                {
                    capacity++;
                }
            }
            List<DateTime> fullRooms = new List<DateTime>();
            Dictionary<DateTime, int> unavailableCounter = new Dictionary<DateTime, int>();
            foreach (IReservation r in reservationList)
            {
                if (r.RoomType == roomType) //only gets rooms of selected room type
                {
                    for (DateTime i = r.StartDate; i <= r.EndDate; i = i.AddDays(1)) //itterates through startDate till endDate
                    {
                        //adds to dictionary how many rooms are occupied for a specific date
                        if (unavailableCounter.ContainsKey(i))
                        {
                            unavailableCounter[i]++;

                        }
                        else
                        {
                            unavailableCounter.Add(i, 1);
                        }
                        //if the room has reached capacity, add it to the full rooms list
                        if (unavailableCounter[i] == capacity)
                        {
                            fullRooms.Add(i);
                        }
                    }
                }
            }

            foreach (IUnavailableRoom room in UnavailableRoomList)
            {
                if (room.Type == roomType)
                {
                    for (DateTime i = room.Start; i <= room.End; i = i.AddDays(1)) //itterates through startDate till endDate
                    {
                        //adds to dictionary how many rooms are occupied for a specific date
                        if (unavailableCounter.ContainsKey(i))
                        {
                            unavailableCounter[i]++;

                        }
                        else
                        {
                            unavailableCounter.Add(i, 1);
                        }
                        //if the room has reached capacity, add it to the full rooms list
                        if (unavailableCounter[i] == capacity)
                        {
                            fullRooms.Add(i);
                        }
                    }
                }
            }
            return fullRooms;
        }

        public Dictionary<int, int> GetRoomAvailability(DateTime date)
        {

            refreshUnavailableRoomList();
            Dictionary<int, int> roomTypesInUse = new Dictionary<int, int>();
            int id = 1;
            foreach (string rt in RoomTypeList)
            {
                roomTypesInUse.Add(id, 0);
                id++;
            }

            foreach (IReservation r in ReservationList)
            {
                for (DateTime i = r.StartDate; i <= r.EndDate; i = i.AddDays(1))
                {
                    if (i == date)
                    {
                        roomTypesInUse[r.RoomType]++;
                        break;
                    }
                }
            }
            foreach (IUnavailableRoom room in UnavailableRoomList)
            {
                for (DateTime i = room.Start; i <= room.End; i = i.AddDays(1))
                {
                    if (i == date)
                    {
                        roomTypesInUse[room.Type]++;
                        break;
                    }
                }
            }
            return roomTypesInUse;
        }
        public bool UpdateMStock(IMoveStock stock)
        {
            try
            {

                dataLayer.updateMStockDB(stock);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public void insertClean(int no, DateTime date)
        {
            try
            {
                string name = dataLayer.findName(no);

                ICleaningRooms cleaningRooms = CleaningRoomFactory.GetCleaningRooms(no, name, date);
                toBeCleanedList.Add(cleaningRooms);
                dataLayer.addCleanRoomtoDB(cleaningRooms);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());

            }

        }

        public bool recordCovidInfo(string fName, string lName, string phoneNo)
        {
            DateTime currentTime = DateTime.Now;
            ICovidInfo info = CovidInfoFactory.GetCovidInfo(fName, lName, phoneNo, currentTime);
            if (DataLayer.addCovidInfo(info))
            {
                return true;
            }
            return false;
        }

        public bool renewMember(int id, string memberType, int monthsToSubscribe)
        {
            ILeisureMember leisureMember = currentMember;
            DateTime newDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            leisureMember.ExpireDate = newDate.AddMonths(monthsToSubscribe);
            leisureMember.AreaType = memberType;
            bool success = DataLayer.editLeisureMember(leisureMember);
            if (success)
            {
                currentMember = leisureMember;
                return true;
            }
            return false;
        }

        public bool roomCleaned(int roomID)
        {
            bool success = DataLayer.deleteCleanedRoom(roomID);
            if (success)
            {
                refreshToBeCleanedList();
            }
            return success;
        }

        public bool replenishCart(List<IStock> replenishList)
        {
            foreach (IStock stock in replenishList)
            {
                bool success = DataLayer.editStock(stock);
                if (!success)
                {
                    return success;
                }
            }
            return true;
        }
        public void RecordClean(string list, string comment)
        {
            try
            {
                int employeeno = currentUser.EmpNo;

                dataLayer.addCleantoDB(employeeno, list, comment);

            }
            catch
            {


            }
        }
    }
}