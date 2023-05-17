using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using BusinessEntities;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class IDatalayer : IDataLayer
    {

        private SqlConnection con;
        DataSet ds;
        SqlDataAdapter da;
        int totUsers;
        SqlCommandBuilder cb;
        private int DBType = 1;
        private static SqlDataReader reader;
        

        public string GetDBType()
        {
            switch(DBType)
            {
                case 1:
                    return "Connected to LIT";
                default:
                    return "Connected to local";
            }

        }

        private static IDataLayer dataLayerSingletonInstance;
        static readonly object padlock = new object();

        #region Constructors
        public static IDataLayer GetInstance()
        {
            lock (padlock)
            {
                if (dataLayerSingletonInstance == null)
                {
                    dataLayerSingletonInstance = new IDatalayer();
                }
                return dataLayerSingletonInstance;
            }
        }
        private IDatalayer()
        {
            openConnection();
        }
        #endregion
        public void openConnection()
        {
            con = new SqlConnection();
            con.ConnectionString = DatabaseType(DBType);
            try
            {
                con.Open();
                //MessageBox.Show("Database Open");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Please connect with a VPN: ", ex.ToString());
                con.Close();
                Environment.Exit(0); //Force the application to close
            }
        }

        public static string DatabaseType(int type)
        {
            switch (type)
            {
                case 1:
                    //LIT DB
                    return @"Data Source=SQL5.student.litdom.lit.ie\TEAM1,60151;Initial Catalog=mydb;Persist Security Info=True;User ID=K00235601;Password=$QLT3AM01";

                case 2:
                    //Lukasz local DB
                    return @"Data Source=localhost\MSSQLSERVER01;Initial Catalog=mydb;Integrated Security=True";
                case 3:
                    //Brian Local DB
                    return @"Data Source=localhost\MSSQLSERVER01;Initial Catalog=mydb;Integrated Security=True";
                case 4:
                    //aoife Local DB
                    return @"Data Source=laptop-cni60eep;Initial Catalog=mybd;Intrated Security=True";
                case 5:
                    //Calum Local DB
                    return @"Data Source=localhost;Initial Catalog=mydb;Integrated Security=True";
                default:
                    throw new Exception();
            }
        }

        public void displayConStat()
        {
            MessageBox.Show(string.Concat("Client connection ID: ", con.ClientConnectionId, "\nConnection string: ", con.ConnectionString, "\nDatabase: ", con.Database, "\nSQL server instance: ", con.DataSource, "\nSQL server version: ", con.ServerVersion)); 
        }

        public void closeConnection()
        {
            con.Close();
        }

        public SqlConnection getConnection()
        {
            return con;
        }

        public ArrayList getAllUsers()
        {
            ArrayList StaffList = new ArrayList();

            try
            {
                string query = "Select* from[mydb].[dbo].[employee];";
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;


                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                //Checks if the query returned rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IStaff staff = StaffFactory.GetStaff(Convert.ToInt32(reader["Employee_No"]), reader["Employee_Name"].ToString(),
                                        reader["Employee_LName"].ToString(), reader["Employee_Address"].ToString(), reader["Employee_Email"].ToString(), Convert.ToInt32(reader["Employee_HomePhone"].ToString()),
                                            Convert.ToInt32(reader["Employee_MobilePhone"].ToString()), reader["Employee_NextToKin"].ToString(), Convert.ToInt32(reader["Employee_NextToKinPhoneNo"]),
                                            reader["Employee_NextToKinRelationship"].ToString(), reader["Employee_PPSN"].ToString(), reader["Employee_UserName"].ToString(), reader["Employee_Password"].ToString(), reader["Employee_Type"].ToString());

                        StaffList.Add(staff);
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                    MessageBox.Show("ERROR RETREIVING USERS FROM DB: ", ex.ToString());
            }

            reader.Close();
            return StaffList;
        }

        public void addNewUserToDB(IStaff theStaff)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[employee];";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Fill(ds, "employee");
                totUsers = ds.Tables["employee"].Rows.Count;
                DataRow dRow = ds.Tables["employee"].NewRow();
                dRow[0] = theStaff.EmpNo;
                dRow[1] = theStaff.Name;
                dRow[2] = theStaff.LName;
                dRow[3] = theStaff.Address;
                dRow[4] = theStaff.Email;
                dRow[5] = theStaff.HPhone;
                dRow[6] = theStaff.Mphone;
                dRow[7] = theStaff.NextToKin;
                dRow[8] = theStaff.NextToKinPhoneNo;
                dRow[9] = theStaff.NextToKinRel;
                dRow[10] = theStaff.PPSN;
                dRow[11] = theStaff.UserName;
                dRow[12] = theStaff.Password;
                dRow[13] = theStaff.EmployeeType;

                ds.Tables["employee"].Rows.Add(dRow);
                da.Update(ds, "employee");
            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                MessageBox.Show("ERROR ADDING USER TO DB", ex.ToString());
            }


        }

        public void addNewOrderToDB(IOrder theOrder)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[orders];";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Fill(ds, "orders");
                totUsers = ds.Tables["orders"].Rows.Count;
                DataRow dRow = ds.Tables["orders"].NewRow();
                dRow[0] = theOrder.Order_No;
                dRow[1] = theOrder.Item_ID;
                dRow[2] = theOrder.Item_Name; ;
                dRow[3] = theOrder.Quantity;

                ds.Tables["orders"].Rows.Add(dRow);
                da.Update(ds, "orders");
            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                MessageBox.Show("ERROR ADDING ORDER TO DB", ex.ToString());
            }


        }
        public bool deleteUserFromDB(IStaff theStaff)
        {

            try
            {
                ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[employee];";
                da = new SqlDataAdapter(sql, con);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                cb = new SqlCommandBuilder(da);  //Generates
                da.Fill(ds, "employee");
                DataRow findRow = ds.Tables["employee"].Rows.Find(theStaff.EmpNo);
                if (findRow != null)
                {
                    findRow.Delete();
                }
                da.Update(ds, "employee");
            }
            catch (System.Exception ex)
            {
                if (getConnection().ToString() == "Open")
                    closeConnection();
                MessageBox.Show("ERROR DELETING USER FROM DATABASSE", ex.ToString());
            }
            return true;
        }

        public bool editRoomUnavailability(IRoom room, string s, string e)
        {

            string query = "UPDATE [mydb].[dbo].[roomUnavailable] SET StartDate = '" + e + "', EndDate = '" + s + "' WHERE roomID = " + room.RoomNo + ";";
            //MessageBox.Show(query);

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }

                reader.Close();
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool editUserInDB(IStaff theStaff)
        {
            string query = "UPDATE [mydb].[dbo].[employee] SET Employee_Name = '" + theStaff.Name + "', Employee_LName = '" + theStaff.LName +
                 "', Employee_Address = '" + theStaff.Address + "', Employee_Email = '" + theStaff.Email +
                 "', Employee_HomePhone = '" + theStaff.HPhone + "', Employee_MobilePhone = '" + theStaff.Mphone +
                 "', Employee_NextToKin = '" + theStaff.NextToKin +
                 "', Employee_NextToKinPhoneNo = '" + theStaff.NextToKinPhoneNo +
                 "', Employee_NextToKinRelationship = '" + theStaff.NextToKinRel +
                 "', Employee_PPSN = '" + theStaff.PPSN + "', Employee_UserName = '" + theStaff.UserName +
                 "', Employee_Password = '" + theStaff.Password + "', Employee_Type = '" + theStaff.EmployeeType + "'" +
                 "WHERE Employee_No = '" + theStaff.EmpNo + "';";

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR EDITING USER IN DB", ex.ToString());
                reader.Close();
                return false;
            }


        }

        public bool editRoomInDB(IRoom room)
        {
            string query = "UPDATE [mydb].[dbo].[room] SET Room_Type = '" + room.RoomType +
                 "', Status = '" + room.Status +
                 "', Availability = '" + room.Availability +
                 "' WHERE Room_No = '" + room.RoomNo + "';";

            //MessageBox.Show(query);


            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR EDITING ROOM IN DB", ex.ToString());
                reader.Close();
            }

            return false;



        }

        public ArrayList GetAllRooms()
        {
            ArrayList RoomList = new ArrayList();
            try
            {
                string query = "Select * from [mydb].[dbo].[room] LEFT JOIN roomType ON roomType.ID = room.Room_Type LEFT JOIN roomUnavailable ON roomUnavailable.roomID = room.Room_No;";
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;

                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IRoom room = new Room(Convert.ToInt32(reader["Room_No"]), Convert.ToInt32(reader["Room_Type"]), reader["Status"].ToString(), reader["Availability"].ToString(), Convert.ToInt32(reader["Pricing"].ToString()), (reader["StartDate"].ToString() == "") ? "01/01/0001" : reader["StartDate"].ToString(), (reader["EndDate"].ToString() == "") ? "01/01/0001" : reader["EndDate"].ToString());
                        RoomList.Add(room);
                    }
                }

                reader.Close();

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                {
                    MessageBox.Show(ex.ToString());
                }
                reader.Close();
            }
            return RoomList;
        }

        public ArrayList getAllReservations()
        {
            ArrayList ReservationList = new ArrayList();

            string query = "SELECT * FROM reservation LEFT JOIN roomType ON roomType.ID = reservation.roomType; ";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            //Execute the query and the reader will read shit from the DB
            reader = commandDB.ExecuteReader();

            //Checks if the query returned rows
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["CheckOutDate"] == DBNull.Value)
                    {
                        //if (Convert.ToDateTime(reader["CheckOutDate"]) != Convert.ToDateTime("01/01/0001 00:00:00") && Convert.ToDateTime(reader["CheckOutDate"]) > DateTime.Now)
                        //{
                            if (reader.IsDBNull(16))
                            {
                                IReservation reservation = ReservationFactory.GetReservation(Convert.ToInt32(reader["ReservationNo"]), reader["FirstName"].ToString(),
                                                            reader["LastName"].ToString(), Convert.ToDateTime(reader["StartDate"]), Convert.ToDateTime(reader["EndDate"]), Convert.ToInt32(reader["Status"]), reader["Email"].ToString(),
                                                            reader["Address1"].ToString(), reader["Address2"].ToString(), reader["Address3"].ToString(), reader["PhoneNo"].ToString(), reader["SpecialRequest"].ToString(),
                                                            Convert.ToInt32(reader["NoChildren"]), Convert.ToInt32(reader["NoAdults"]), Convert.ToInt32(reader["roomType"]));
                                ReservationList.Add(reservation);
                            }
                            else if (reader.IsDBNull(7))
                            {
                                IReservation reservation = ReservationFactory.GetReservation(Convert.ToInt32(reader["ReservationNo"]), reader["FirstName"].ToString(),
                                                           reader["LastName"].ToString(), Convert.ToDateTime(reader["StartDate"]), Convert.ToDateTime(reader["EndDate"]), Convert.ToInt32(reader["Status"]), reader["Email"].ToString(),
                                                           reader["Address1"].ToString(), reader["Address2"].ToString(), reader["Address3"].ToString(), reader["PhoneNo"].ToString(), reader["SpecialRequest"].ToString(),
                                                           Convert.ToInt32(reader["NoChildren"]), Convert.ToInt32(reader["NoAdults"]), Convert.ToInt32(reader["roomType"]), Convert.ToDateTime(reader["CheckInDate"]), Convert.ToInt32(reader["roomNo"]));
                                ReservationList.Add(reservation);
                            }
                            else
                            {
                                IReservation reservation = ReservationFactory.GetReservation(Convert.ToInt32(reader["ReservationNo"]), reader["FirstName"].ToString(),
                                                reader["LastName"].ToString(), Convert.ToDateTime(reader["StartDate"]), Convert.ToDateTime(reader["EndDate"]), Convert.ToInt32(reader["Status"]),
                                                Convert.ToDateTime(reader["CheckInDate"]), Convert.ToDateTime(reader["CheckOutDate"]), reader["Email"].ToString(),
                                                reader["Address1"].ToString(), reader["Address2"].ToString(), reader["Address3"].ToString(), reader["PhoneNo"].ToString(), reader["SpecialRequest"].ToString(),
                                                Convert.ToInt32(reader["NoChildren"]), Convert.ToInt32(reader["NoAdults"]), Convert.ToInt32(reader["roomNo"]), Convert.ToInt32(reader["roomType"]));
                                ReservationList.Add(reservation);
                            }
                        //}
                    }


                }
            }
            reader.Close();

            return ReservationList;
        }

        public int getmaxresid()
        {
            int max = 0;
            try
            {
                string query = "SELECT MAX(ReservationNo) as max FROM reservation;";
                
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;

                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        max = Convert.ToInt32(reader["max"]);
                        //return max;
                    }
                }

                reader.Close();

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                {
                    MessageBox.Show(ex.ToString());
                }
                reader.Close();
            }
            return max;
        }

        public bool CheckInGuest(string fName, string lName, bool isAdult, string room, string reservationNo)
        {
            int boolConversion = 0;
            if (isAdult)
            {
                boolConversion = 1;
            }
            string query = "INSERT INTO guest(FirstName,LastName,IsAdult,Reservation,InLeisureCentre) VALUES('" + fName + "','" + lName + "'," + boolConversion + "," + reservationNo + ",0)";
            SqlCommand command = new SqlCommand(query, con);
            command.CommandTimeout = 60;
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: DATABASE ERROR", ex.ToString());
                reader.Close();
                return false;
            }
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            command.CommandText = "UPDATE reservation SET roomNo = " + room + ", Status = 1, CheckInDate = '" + sqlFormattedDate + "' WHERE ReservationNo = " + reservationNo;
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR: DATABASE ERROR", ex.ToString());
                reader.Close();
                return false;
            }
            command.CommandText = "UPDATE room SET Status = 'Not-Vacant' WHERE Room_No = " + room;
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR: DATABASE ERROR", ex.ToString());
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }

        public void addReservationDB(IReservation reservation)
        {
            int id = 0;
            string query = "SELECT ID FROM roomType WHERE ID = '" + reservation.RoomType + "'";
            SqlCommand command = new SqlCommand(query, con);
            command.CommandTimeout = 60;
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: DATABASE ERROR", ex.ToString());
                reader.Close();
                return;
            }




            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[reservation];";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Fill(ds, "reservation");
                totUsers = ds.Tables["reservation"].Rows.Count;
                DataRow dRow = ds.Tables["reservation"].NewRow();

                //dRow[0] = reservation.ReservationNo;
                dRow[1] = reservation.Name;
                dRow[2] = reservation.LName;
                dRow[3] = reservation.StartDate;
                dRow[4] = reservation.EndDate;
                dRow[5] = reservation.Status;
                // dRow[6] = reservation.CheckInDate;
                //dRow[7] = reservation.CheckOutDate;
                dRow[8] = reservation.Email;
                dRow[9] = reservation.Address1;
                dRow[10] = reservation.Address2;
                dRow[11] = reservation.Address3;

                dRow[12] = reservation.Phone;
                dRow[13] = reservation.SpecialReq;

                dRow[14] = reservation.NoChildren;
                dRow[15] = reservation.NoAdults;
                //   dRow[16] = reservation.RoomNo;
                dRow[17] = id;


                ds.Tables["reservation"].Rows.Add(dRow);
                da.Update(ds, "reservation");
            }
            catch (System.Exception)
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                //Environment.Exit(0);
            }
        }
        public bool deleteReservationFromDB(IReservation theReservation)
        {

            string query = "DELETE FROM reservation WHERE ReservationNo =" + theReservation.ReservationNo ;
            try
            {
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;
                reader = commandDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }
            return true;
        }

        public ArrayList getAllOrders()
        {
            ArrayList ordersList = new ArrayList();
            try
            {
                string query = "Select* from[mydb].[dbo].[orders];";
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;

                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                //Checks if the query returned rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IOrder neworder = OrdersFactory.GetOrder(reader["Order_no"].ToString(), reader["Item_ID"].ToString(),
                                        reader["Item_Name"].ToString(), Convert.ToInt32(reader["Quantity"]));

                        ordersList.Add(neworder);
                    }
                }
                reader.Close();
            }
            catch (System.Exception)
            {
                //if (con.State.ToString() == "Open");
                reader.Close();
            }
            return ordersList;


        }
        public void addLeisureMemberDB(ILeisureMember member)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[leisureMember];";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Fill(ds, "leisureMember");
                totUsers = ds.Tables["leisureMember"].Rows.Count;
                DataRow dRow = ds.Tables["leisureMember"].NewRow();
                dRow[0] = member.ID;
                dRow[1] = member.FirstName;
                //MessageBox.Show(member.FirstName);
                dRow[2] = member.LastName;
                //MessageBox.Show(member.LastName);
                dRow[3] = member.ExpireDate;
                //MessageBox.Show(member.ExpireDate.ToString());
                dRow[4] = member.Age;
                //MessageBox.Show(member.Age.ToString());
                dRow[5] = member.MedicalConditions;
                //MessageBox.Show(member.MedicalConditions);
                dRow[6] = member.Email;
                //MessageBox.Show(member.Email);
                dRow[7] = member.Phone;
                //MessageBox.Show(member.Phone);
                dRow[8] = member.InLeisure;
                //MessageBox.Show(member.InLeisure.ToString());
                dRow[9] = member.AreaType;
                //MessageBox.Show(member.AreaType);
                ds.Tables["leisureMember"].Rows.Add(dRow);
                da.Update(ds, "leisureMember");

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                MessageBox.Show("ERROR  Adding Member  to DATABASE", ex.ToString());
            }
        }


        public ArrayList getAllMembers()
        {
            ArrayList memberList = new ArrayList();
            try
            {
                string query = "Select * from [mydb].[dbo].[leisureMember];";
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;

                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                //Checks if the query returned rows

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ILeisureMember newMember = LeisureMemberFactory.GetMember(Convert.ToInt32(reader["ID"]), reader["FirstName"].ToString(),
                                        reader["LastName"].ToString(), Convert.ToDateTime(reader["ExpiryDate"]), Convert.ToInt32(reader["Age"]),
                                        reader["MedicalConditions"].ToString(), reader["Email"].ToString(), reader["Phone"].ToString(), Convert.ToByte(reader["InLeisureCentre"]),
                                          reader["AreaType"].ToString());

                        memberList.Add(newMember);
                    }
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {

                MessageBox.Show("ERROR READING MEMBERS FROM DB", ex.ToString());
                reader.Close();
            }
            return memberList;
        }

        public ArrayList getAllGuests()
        {
            ArrayList GuestList = new ArrayList();
            try
            {
                string query = "SELECT * FROM guest; ";
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;

                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                //Checks if the query returned rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IGuest guest = GuestFactory.GetGuest(Convert.ToInt32(reader["ID"]), reader["FirstName"].ToString(), reader["LastName"].ToString(), Convert.ToBoolean(reader["IsAdult"]), Convert.ToInt32(reader["Reservation"]), Convert.ToBoolean(reader["InLeisureCentre"]));
                        GuestList.Add(guest);
                    }
                }
                reader.Close();
            }
            catch (System.Exception e)
            {
                if (con.State.ToString() == "Open")
                    MessageBox.Show("get guests complete or error " + e); //Force the application to close
                reader.Close();

            }
            return GuestList;
        }

        public bool editReservation(IReservation r)
        {
            string query = "SELECT roomType FROM reservation LEFT JOIN roomType ON roomType.ID = reservation.roomType WHERE ReservationNo =" + r.ReservationNo;
            int roomTypeNo = 1;

            try
            {
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;
                reader = commandDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roomTypeNo = Convert.ToInt32(reader["roomType"]);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }

            DateTime sDate = r.StartDate;
            string f_sDate = sDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime eDate = r.EndDate;
            string f_eDate = eDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime ciDate = r.CheckInDate;
            string f_ciDate = ciDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime coDate = r.CheckOutDate;
            string f_coDate = coDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (f_ciDate == "0001-01-01 00:00:00.000" && f_coDate == "0001-01-01 00:00:00.000")
            {
                query = "UPDATE reservation " +
                     "SET FirstName = '" + r.Name + "', LastName ='" + r.LName + "', StartDate = '" + f_sDate + "', EndDate = '" + f_eDate + "', Status = " + r.Status +  "', Email = '" + r.Email + "', Address1 = '" + r.Address1 + "',Address2 ='" + r.Address2 + "', Address3 ='" + r.Address3 + "',PhoneNo='" + r.Phone + "',SpecialRequest='" + r.SpecialReq + "',NoChildren='" + r.NoChildren + "', NoAdults=" + r.NoAdults + ",roomNo=" + r.RoomNo + ",roomType=" + roomTypeNo +
                     " WHERE ReservationNo = " + r.ReservationNo;
            }
            else if (f_ciDate != "0001-01-01 00:00:00.000" && f_coDate == "0001-01-01 00:00:00.000")
            {
                query = "UPDATE reservation " +
                     "SET FirstName = '" + r.Name + "', LastName ='" + r.LName + "', StartDate = '" + f_sDate + "', EndDate = '" + f_eDate + "', Status = " + r.Status + ", CheckInDate = '" + f_ciDate +  "', Email = '" + r.Email + "', Address1 = '" + r.Address1 + "',Address2 ='" + r.Address2 + "', Address3 ='" + r.Address3 + "',PhoneNo='" + r.Phone + "',SpecialRequest='" + r.SpecialReq + "',NoChildren='" + r.NoChildren + "', NoAdults=" + r.NoAdults + ",roomNo=" + r.RoomNo + ",roomType=" + roomTypeNo +
                     " WHERE ReservationNo = " + r.ReservationNo;
            }
            else
            {
                query = "UPDATE reservation " +
                     "SET FirstName = '" + r.Name + "', LastName ='" + r.LName + "', StartDate = '" + f_sDate + "', EndDate = '" + f_eDate + "', Status = " + r.Status + ", CheckInDate = '" + f_ciDate + "', CheckOutDate = '" + f_coDate + "', Email = '" + r.Email + "', Address1 = '" + r.Address1 + "',Address2 ='" + r.Address2 + "', Address3 ='" + r.Address3 + "',PhoneNo='" + r.Phone + "',SpecialRequest='" + r.SpecialReq + "',NoChildren='" + r.NoChildren + "', NoAdults=" + r.NoAdults + ",roomNo=" + r.RoomNo + ",roomType=" + roomTypeNo +
                     " WHERE ReservationNo = " + r.ReservationNo;
            }
            roomTypeNo = r.RoomType;

            
            //MessageBox.Show(query);
            try
            {
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;
                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                    }
                }
                reader.Close();
            }
            catch (System.Exception)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }

        public bool deleteGuests(List<IGuest> guests)
        {
            foreach (IGuest g in guests)
            {
                string query = "DELETE FROM guest WHERE ID =" + g.GuestID;
                try
                {
                    SqlCommand commandDB = new SqlCommand(query, con);
                    commandDB.CommandTimeout = 60;
                    reader = commandDB.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                        }
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                    return false;
                }
            }
            return true;
        }

        public bool ChangeMemberStatus(ILeisureMember theMember, byte status)
        {
            string query = "UPDATE [mydb].[dbo].[leisureMember] SET InLeisureCentre = '" + status +
                "' WHERE ID = '" + theMember.ID + "';";


            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                MessageBox.Show("Success");
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR EDITING MEMBER STATUS IN DB", ex.ToString());
                reader.Close();
                return false;
            }
        }

        public ArrayList getAllRoomTypes()
        {
            string query = "SELECT [mydb].[dbo].[roomType].Name FROM [mydb].[dbo].[roomType]";
            SqlCommand command = new SqlCommand(query, con);
            command.CommandTimeout = 60;
            ArrayList roomTypeList = new ArrayList();
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roomTypeList.Add(reader["Name"].ToString());

                    }

                }

                reader.Close();
            }
            catch (Exception)
            {
                reader.Close();

            }
            foreach (var item in roomTypeList)
            {
                Console.WriteLine(item);
            }
            return roomTypeList;
        }

        public bool ChangeGuestStatus(IGuest theGuest, bool status)
        {
            string query = "UPDATE [mydb].[dbo].[guest] SET InLeisureCentre = '" + Convert.ToByte(status) +
               "' WHERE ID = '" + theGuest.GuestID + "';";


            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                MessageBox.Show("Success");
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR EDITING GUEST STATUS IN DB", ex.ToString());
                reader.Close();
                return false;
            }
        }
        public ArrayList getToBeCleanedList()
        {
            ArrayList toBeCleanedList = new ArrayList();
            string query = "SELECT room.Room_No, roomType.Name, DayToClean FROM (cleaningSchedule INNER JOIN room ON cleaningSchedule.Room_No = room.Room_No) INNER JOIN roomType ON roomType.ID = room.Room_Type;";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            try
            {
                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                //Checks if the query returned rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(2))
                        {

                        }
                        else
                        {
                            ICleaningRooms newroom = CleaningRoomFactory.GetCleaningRooms(Convert.ToInt32(reader["Room_No"]), reader["Name"].ToString(),
                            Convert.ToDateTime(reader["DayToClean"]));
                            toBeCleanedList.Add(newroom);
                        }
                    }
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                    MessageBox.Show("ERROR READING ALL CLEANING ROOMS IN DB", ex.ToString());
                reader.Close();
            }
            return toBeCleanedList;


        }

        //stock array
        public ArrayList getAllStock()
        {

            ArrayList StockList = new ArrayList();
            string query = "Select * from [mydb].[dbo].[stock] LEFT JOIN locationStock ON locationStock.ID = stock.LocationID;";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            //MessageBox.Show(query);
            try
            {
                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(4))
                        {
                            IStock stock = new Stock(Convert.ToInt32(reader["ID"].ToString()), reader["NameStock"].ToString(), reader["Type"].ToString(), Convert.ToInt32(reader["Amount"].ToString()), reader["Size"].ToString(), reader["Location"].ToString());
                            StockList.Add(stock);
                        }
                        else
                        {
                            IStock stock = new Stock(Convert.ToInt32(reader["ID"].ToString()), reader["NameStock"].ToString(), reader["Type"].ToString(), Convert.ToInt32(reader["Amount"].ToString()), Convert.ToDouble(reader["Price"].ToString()), reader["Size"].ToString(), reader["Location"].ToString());
                            StockList.Add(stock);
                        }


                    }
                }

                reader.Close();

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                {
                    MessageBox.Show("ERROR READING STOCK" + ex);
                }



            }
            return StockList;

        }
        public ArrayList getAllMoveStock()
        {

            ArrayList MoveStockList = new ArrayList();
            string query = "Select moveStock.ID,NameStock,Type,Size,Bar,Storage,Amount from [mydb].[dbo].[moveStock] LEFT JOIN stock ON stock.ID = movestock.StockID;";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            //MessageBox.Show(query);

            try
            {

                reader = commandDB.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {



                        IMoveStock moveStock = new MoveStock(Convert.ToInt32(reader["ID"].ToString()), reader["NameStock"].ToString(), reader["Type"].ToString(), reader["Size"].ToString(), Convert.ToInt32(reader["Bar"].ToString()), Convert.ToInt32(reader["Storage"].ToString()), Convert.ToInt32(reader["Amount"].ToString()));
                        MoveStockList.Add(moveStock);




                    }
                }

                reader.Close();

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                {
                    MessageBox.Show("ERROR Move STOCK" + ex);
                }


            }
            return MoveStockList;

        }
        public bool updateMStockDB(IMoveStock stock)
        {
            string query = "UPDATE [mydb].[dbo].[movestock] SET Bar = " + stock.InBar + ", Storage = " + stock.InStorage +
                "WHERE movestock.ID = " + stock.ID + ";";

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            try
            {
                reader = commandDB.ExecuteReader();



                reader.Close();
                return true;

            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR EDITING MoveStock" + ex.ToString());

            }
            return false;



        }
        public ArrayList getAllBarStock()
        {

            ArrayList BarStockList = new ArrayList();
            string query = "Select * from [mydb].[dbo].[stock] LEFT JOIN locationStock ON locationStock.ID = stock.LocationID WHERE stock.LocationID = 1;";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            //MessageBox.Show(query);
            try
            {
                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(4))
                        {
                            IStock stock = new Stock(Convert.ToInt32(reader["ID"].ToString()), reader["NameStock"].ToString(), reader["Type"].ToString(), Convert.ToInt32(reader["Amount"].ToString()), reader["Size"].ToString(), reader["Location"].ToString());
                            BarStockList.Add(stock);
                        }
                        else
                        {
                            IStock stock = new Stock(Convert.ToInt32(reader["ID"].ToString()), reader["NameStock"].ToString(), reader["Type"].ToString(), Convert.ToInt32(reader["Amount"].ToString()), Convert.ToDouble(reader["Price"].ToString()), reader["Size"].ToString(), reader["Location"].ToString());
                            BarStockList.Add(stock);
                        }


                    }
                }

                reader.Close();

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                {
                    MessageBox.Show("ERROR READING STOCK" + ex);
                }



            }
            return BarStockList;

        }
        public ArrayList getAllCommonItems()
        {

            ArrayList CommonItemList = new ArrayList();
            ArrayList BarStockList = getAllBarStock();
            string query = "Select * from [mydb].[dbo].[commonItems] LEFT JOIN locationStock ON locationStock.ID = commonItems.LocationID AND commonItems.LocationID = 1;";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            //MessageBox.Show(query);
            try
            {
                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(4))
                        {
                            foreach (Stock s in BarStockList)
                            {
                                if (s.StockID == Convert.ToInt32(reader["ID"].ToString()) && s.Size == reader["Size"].ToString() && s.NameStock == reader["NameStock"].ToString())
                                {
                                    CommonItemList.Add(s);
                                }
                            }  
                        }
                        else
                        {
                            foreach (Stock s in BarStockList)
                            {
                                if (s.StockID == Convert.ToInt32(reader["ID"].ToString()) && s.Size == reader["Size"].ToString() && s.NameStock == reader["NameStock"].ToString())
                                {
                                    CommonItemList.Add(s);
                                }
                            }
                        }


                    }
                }

                reader.Close();

            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                {
                    MessageBox.Show("ERROR READING COMMON ITEMS" + ex);
                }



            }
            return CommonItemList;

        }
        public ArrayList getAllBarSales()
        {
            ArrayList barSaleList = new ArrayList();
            try
            {
                string query = "Select* from[mydb].[dbo].[barSales];";
                SqlCommand commandDB = new SqlCommand(query, con);
                commandDB.CommandTimeout = 60;

                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                //Checks if the query returned rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IBarSale newBarSale = BarSaleFactory.GetBarSale(reader["Order_no"].ToString(), reader["Item_ID"].ToString(),
                                        reader["Item_Name"].ToString(), Convert.ToInt32(reader["Quantity"]));

                        barSaleList.Add(newBarSale);
                    }
                }
                reader.Close();
            }
            catch (System.Exception)
            {
                //if (con.State.ToString() == "Open");
                reader.Close();
            }
            return barSaleList;


        }
        public ArrayList getAllCleanList()
        {

            ArrayList LeisureCleaning = new ArrayList();

            string query = "SELECT Reference_No, Employee_Name, AreasCleaned, Comment, DateDone FROM leisureCleaning left JOIN Employee ON Employee.Employee_No = leisureCLeaning.Employee_No; ";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            //Execute the query and the reader will read shit from the DB
            reader = commandDB.ExecuteReader();
            
                while (reader.Read())
                {

                    ILeisureCleaning Clean = LeisureCleaningFactory.GetLeisureCleaning(Convert.ToInt32(reader["Reference_No"]), reader["Employee_Name"].ToString(), reader["AreasCLeaned"].ToString(), reader["Comment"].ToString(), Convert.ToDateTime(reader["DateDone"]));
                    LeisureCleaning.Add(Clean);
                }
            
            reader.Close();
            return LeisureCleaning;



        }
        public void addNewBarSaleToDB(IBarSale theBarSale)
        {

            try
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[barSales];";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Fill(ds, "barSales");
                totUsers = ds.Tables["barSales"].Rows.Count;
                DataRow dRow = ds.Tables["barSales"].NewRow();
                dRow[0] = theBarSale.Order_No;
                dRow[1] = theBarSale.Item_ID;
                dRow[2] = theBarSale.Item_Name; ;
                dRow[3] = theBarSale.Quantity;

                ds.Tables["barSales"].Rows.Add(dRow);
                da.Update(ds, "barSales");

                string query1 = "UPDATE [mydb].[dbo].[stock] SET Amount = Amount - " + theBarSale.Quantity + "WHERE stock.ID = " + theBarSale.Item_ID + ";";

                SqlCommand commandDB = new SqlCommand(query1, con);
                commandDB.CommandTimeout = 60;

                try
                {
                    reader = commandDB.ExecuteReader();



                    reader.Close();

                }

                catch (Exception ex)
                {
                    MessageBox.Show("ERROR EDITING MoveStock" + ex.ToString());

                }
            }
            catch (System.Exception ex)
            {
                if (con.State.ToString() == "Open")
                    con.Close();
                MessageBox.Show("ERROR ADDING ORDER TO DB", ex.ToString());
            }


        }
        public string findName(int no)
        {
            string query = "SELECT Name  FROM [mydb].[dbo].[room]  INNER JOIN roomType ON roomType.ID = Room.Room_Type WHERE Room_No = " + no + "; ";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            string id = "";
            try
            {
                reader = commandDB.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        id = reader["Name"].ToString();


                    }
                }
                reader.Close();
                return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return id;
            }


        }
        public bool deleteMember(ILeisureMember leisureMember)
        {
            try
            {
                ds = new DataSet();
                string sql = "SELECT * From [mydb].[dbo].[leisureMember];";
                da = new SqlDataAdapter(sql, con);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                cb = new SqlCommandBuilder(da);  //Generates
                da.Fill(ds, "leisureMember");
                DataRow findRow = ds.Tables["leisureMember"].Rows.Find(leisureMember.ID);
                if (findRow != null)
                {
                    findRow.Delete();
                }
                da.Update(ds, "leisureMember");
            }
            catch (System.Exception ex)
            {
                if (getConnection().ToString() == "Open")
                    closeConnection();
                MessageBox.Show("ERROR DELETING MEMBER", ex.ToString());
            }
            return true;
        }

        public bool addCovidInfo(ICovidInfo info)
        {
            string sqlFormattedDate = info.Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string query = "INSERT INTO covidInfo(FirstName,LastName,Phone,DateRecorded)" +
                "VALUES('" + info.FirstName + "','" + info.LastName + "','" + info.PhoneNo + "','" + sqlFormattedDate + "')";

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }

            catch (Exception)
            {
                reader.Close();
                return false;
            }

        }

        public bool editLeisureMember(ILeisureMember leisureMember)
        {
            string sqlFormattedDate = leisureMember.ExpireDate.ToString("yyyy-MM-dd");

            string query = "UPDATE leisureMember " +
                "SET FirstName = '" + leisureMember.FirstName + "', LastName = '" + leisureMember.LastName + "', ExpiryDate = '" + sqlFormattedDate + "', MedicalConditions = '" + leisureMember.MedicalConditions + "', Email = '" + leisureMember.Email + "', Phone = '" + leisureMember.Phone + "', InLeisureCentre = " + leisureMember.InLeisure + ", AreaType = '" + leisureMember.AreaType + "', Age = " + leisureMember.Age +
                "WHERE ID = " + leisureMember.ID;
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }

            catch (Exception)
            {
                MessageBox.Show("ERROR EDITING MEMBER STATUS IN DB");
                reader.Close();
                return false;
            }
        }
        public ArrayList getRoomPricing()
        {
            string query = "SELECT [mydb].[dbo].[roomType].Name, [mydb].[dbo].[roomType].Pricing FROM [mydb].[dbo].[roomType]";
            SqlCommand command = new SqlCommand(query, con);
            command.CommandTimeout = 60;
            ArrayList roomPriceList = new ArrayList();
            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IRoomPrice roomPrice = new RoomPrice(reader["Name"].ToString(), Convert.ToDouble(reader["Pricing"]));
                        roomPriceList.Add(roomPrice);

                    }

                }

                reader.Close();
            }
            catch (Exception)
            {
                reader.Close();

            }
            return roomPriceList;
        }

        public bool ChangeRoomPricing(IRoomPrice roomPrice)
        {
            string query = "UPDATE [mydb].[dbo].[roomType] SET Pricing = '" + roomPrice.Pricing +
                "' WHERE Name = '" + roomPrice.RoomName + "';";

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                MessageBox.Show("Success");
                return true;
            }

            catch (Exception ex)
            {

                MessageBox.Show("ERROR EDITING MEMBER STATUS IN DB", ex.ToString());
                reader.Close();
                return false;
            }

        }
        public bool addCleanRoomtoDB(ICleaningRooms cleaningRooms)
        {
            string sqlFormattedDate = cleaningRooms.CheckOutDate.ToString("yyyy-MM-dd");
            string query = "INSERT INTO cleaningSchedule(Room_No, DayToClean) VALUES(" + cleaningRooms.Room_No + ",'" + sqlFormattedDate + "')";
            //  MessageBox.Show(cleaningRooms.Room_No.ToString() + cleaningRooms.CheckOutDate.ToString());
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();
                //  MessageBox.Show("Executed");
                while (reader.Read())
                {
                }
                //  MessageBox.Show("Success");
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                reader.Close();
                return false;
            }
        }

        public ArrayList getUnavailableRooms()
        {
            string query = "SELECT roomID, Room_Type, StartDate, EndDate FROM roomUnavailable INNER JOIN room ON roomUnavailable.roomID = room.Room_No;";

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                ArrayList list = new ArrayList();
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                    DateTime s;
                    DateTime e;
                    try { s = Convert.ToDateTime(reader["StartDate"]); }
                    catch { s = Convert.ToDateTime("01/01/0001"); }

                    try { e = Convert.ToDateTime(reader["EndDate"]); }
                    catch { e = Convert.ToDateTime("01/01/0001"); }
                    list.Add(UnavailableRoomFactory.GetUnavailableRoom(Convert.ToInt32(reader["roomID"]), Convert.ToInt32(reader["Room_Type"]), s, e));
                }

                reader.Close();
                return list;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }

        public bool deleteCleanedRoom(int roomID)
        {
            string query = "DELETE FROM cleaningSchedule WHERE Room_No = " + roomID;
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try {
                reader = commandDB.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            } 
            catch (Exception) {
                return false;
            }
        }

        public bool editStock(IStock stock)
        {
            string query = "UPDATE stock SET NameStock = '" + stock.NameStock +"', Type = '" + stock.Type + "', Amount = " + stock.Amount + ", Price = " + stock.Price + ", Size = '" + stock.Size + "'" +
                " WHERE ID = " + stock.StockID + ";";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            
            try
            {
                reader = commandDB.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                reader.Close();
                return false;
            }
        }
        public bool addCleantoDB(int employeeno, string list, string comment)
        {
            DateTime date = DateTime.Now;
            string sqlFormattedDate = date.ToString("yyyy-MM-dd");
            string query = "INSERT INTO leisureCleaning(Employee_No, AreasCleaned,Comment,DateDone) VALUES(" + employeeno + ",'" + list + "','" + comment + "','" + sqlFormattedDate + "')";
            //  MessageBox.Show(cleaningRooms.Room_No.ToString() + cleaningRooms.CheckOutDate.ToString());

            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;
            try
            {
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                reader.Close();
                return false;
            }
        }
        public bool addCommonItemDB(IStock stock)
        {
            if (stock.Location == "BarStock")
            {
                stock.Location = "1";
            }    
            string query = "INSERT INTO commonItems(ID,NameStock,Type,Amount,Price,Size,LocationID) VALUES('" + stock.StockID + "','" + stock.NameStock + "','" + stock.Type + "', " + stock.Amount + ", " + stock.Price + ", '" + stock.Size + "', " + stock.Location + ");";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            try
            {
                reader = commandDB.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                reader.Close();
                return false;
            }
        }
        public bool removeCommonItemDB(IStock stock)
        {
            //if (stock.Location == "BarStock")
            //{
            //    stock.Location = "1";
            //}
            string query = "DELETE FROM commonItems WHERE ID=" + stock.StockID + " AND NameStock = '" + stock.NameStock + "' AND Type = '" + stock.Type + "' AND Amount = " + stock.Amount + " AND Price = " + stock.Price + " AND Size = '" + stock.Size + "';";
            SqlCommand commandDB = new SqlCommand(query, con);
            commandDB.CommandTimeout = 60;

            try
            {
                reader = commandDB.ExecuteReader();
                while (reader.Read())
                {
                }
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                reader.Close();
                return false;
            }
        }
    }
}
