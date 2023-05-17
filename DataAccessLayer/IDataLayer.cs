using BusinessEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataLayer
    {
        void addNewUserToDB(IStaff theUser);
        string GetDBType();
        void displayConStat();
        bool deleteUserFromDB(IStaff theStaff);
        bool deleteReservationFromDB(IReservation theReservation);
        bool editUserInDB(IStaff theStaff);
        bool editRoomInDB(IRoom room);
        bool CheckInGuest(string fName, string lName, bool isAdult, string room, string reservationNo);
        void closeConnection();
        bool ChangeMemberStatus(ILeisureMember theMember, byte status);
        void addReservationDB(IReservation reservation);
        void addLeisureMemberDB(ILeisureMember member);
        bool addCommonItemDB(IStock stock);
        bool removeCommonItemDB(IStock stock);
        System.Collections.ArrayList getAllUsers();
        System.Collections.ArrayList getAllReservations();
        System.Data.SqlClient.SqlConnection getConnection();
        System.Collections.ArrayList GetAllRooms();
        System.Collections.ArrayList getAllOrders();
        System.Collections.ArrayList getAllMembers();
        System.Collections.ArrayList getToBeCleanedList();
        System.Collections.ArrayList getAllBarSales();
        System.Collections.ArrayList getRoomPricing();
        System.Collections.ArrayList getAllCommonItems();
        ArrayList getUnavailableRooms();
        int getmaxresid();
        void addNewOrderToDB(IOrder theOrder);
        void addNewBarSaleToDB(IBarSale theBarSale);
        bool ChangeGuestStatus(IGuest theGuest, bool status);
        ArrayList getAllGuests();
        bool editReservation(IReservation r);
        bool editRoomUnavailability(IRoom room, string s, string e);
        bool deleteGuests(List<IGuest> guests);
        void openConnection();
        System.Collections.ArrayList getAllRoomTypes();
        System.Collections.ArrayList getAllStock();
        System.Collections.ArrayList getAllMoveStock();
        System.Collections.ArrayList getAllBarStock();
        System.Collections.ArrayList getAllCleanList(); //Leisure CEnter
        bool updateMStockDB(IMoveStock stock);
        string findName(int no);
        bool deleteMember(ILeisureMember leisureMember);
        bool addCovidInfo(ICovidInfo info);
        bool editLeisureMember(ILeisureMember leisureMember);
        bool ChangeRoomPricing(IRoomPrice roomPrice);
        bool addCleanRoomtoDB(ICleaningRooms cleaningRooms);
        bool deleteCleanedRoom(int roomID);
        bool editStock(IStock stock);
        bool addCleantoDB(int employeeno, string list, string comment);
        //ArrayList getAllCleanList();
    }
}
