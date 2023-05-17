using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLayer
{
    public interface IAccessHandler
    {
        bool addNewUser(string name, string lname, string address, string email, int HPhone, int MPhone, string NextToKin, int NextToKinPhoneNo, string NextToKinRel, string PPSN, string username, string password, string EmpType);
        BusinessEntities.Staff CurrentUser { get; set; }
        BusinessEntities.Staff Currentstaff { get; set; }
        BusinessEntities.Reservation CurrentReservation { get; set; }
        BusinessEntities.ILeisureMember CurrentMember { get; set; }
        DataAccessLayer.IDataLayer DataLayer { get; set; }
        bool deleteUser(IStaff user);
        bool deleteReservation(IReservation reservation);
        bool editUser(IStaff user);
        bool ChangeMemberStatus(ILeisureMember theMember, byte status);
        bool DeleteMember(ILeisureMember leisureMember);
        bool ChangeGuestStatus(IGuest theGuest, bool status);
        bool recordCovidInfo(string fName, string lName, string phoneNo);
        bool editRoom(IRoom room);
        bool editRoomUnavailability(IRoom room, string s, string e);
        bool editReservation(IReservation r);
        bool addCommonItemDB(IStock s);
        bool removeCommonItemDB(IStock s);
        string getUserTypeForCurrentuser();
        bool Login(string name, string password);
        void LogOut();
        void tearDown();
        bool addNewOrder(string orderID, string ItemID, string Item_name, int Quantity);
        bool addNewBarSale(string orderID, string ItemID, string Item_name, int Quantity);
        bool roomCleaned(int roomID);
        void RecordClean(string list, string comment);
        bool changeRoomPricing(IRoomPrice roomPrice);
        List<DateTime> GetFullRooms(int roomType);
        Dictionary<int,int> GetRoomAvailability(DateTime date);
        bool checkOutGuest();
        bool addNewReservation(string fName, string lName, DateTime arrivalD, DateTime departD, string address1, string address2, string address3, string email, string PhoneNo, string SpecialRequest, int roomType, int adultNo, int childernNo);
        int getmaxresid();
        System.Collections.ArrayList UserList { get; }
        System.Collections.ArrayList RoomTypeList { get; }
        System.Collections.ArrayList ReservationList { get; }
        System.Collections.ArrayList OrderList { get; }
        System.Collections.ArrayList BarSaleList { get; }
        System.Collections.ArrayList UnavailableRoomList { get; }

        bool renewMember(int id, string memberType, int monthsToSubscribe);

        System.Collections.ArrayList GuestList { get; }
        System.Collections.ArrayList MemberList { get; }
        System.Collections.ArrayList ToBeCleanedList { get; }
        System.Collections.ArrayList RoomPricingList { get; }
        bool checkInGuest(string fName, string lName, bool isAdult, string roomNo, string reservationNo);
        bool addNewMember(string fname, string lname, DateTime date, int age, string medicalConditions, string email, string phone, string areaType);
        System.Collections.ArrayList roomList { get; }
        DateTime endDate { get; set; }
        DateTime startDate { get; set; }
        string roomType { get; set; }
        System.Collections.ArrayList StockList { get; }
        System.Collections.ArrayList MoveStockList { get; }
        System.Collections.ArrayList BarStockList { get; }
        System.Collections.ArrayList Leisure_CleanList { get; }
        System.Collections.ArrayList CommonItemsList { get; }

        bool UpdateMStock(IMoveStock stock);
        void insertClean(int no, DateTime date);
        System.Collections.ArrayList refreshUserList();
        System.Collections.ArrayList refreshRoomList();
        System.Collections.ArrayList refreshReservationList();
        System.Collections.ArrayList refreshOrderList();
        bool replenishCart(List<IStock> replenishList);
        System.Collections.ArrayList refreshBarSaleList();
        System.Collections.ArrayList refreshMemberList();
        System.Collections.ArrayList refreshGuestList();
        System.Collections.ArrayList refreshRoomTypeList();
        System.Collections.ArrayList refreshToBeCleanedList();
        System.Collections.ArrayList refreshStockList();
        System.Collections.ArrayList refreshMoveStockList();
        System.Collections.ArrayList refreshRoomPricingList();
        System.Collections.ArrayList refreshBarStockList();
        System.Collections.ArrayList refreshUnavailableRoomList();
        System.Collections.ArrayList refreshLeisureCleanList();
        System.Collections.ArrayList refreshCommonItemsList();
    }
}
