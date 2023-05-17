using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using BusinessEntities;
using BusinessLayer;

namespace DataAccessLayer
{
    public static class Database_Handler_SQL
    {
        public static List<Staff> Read(string query)
        {

            SqlConnection DBConnection = new SqlConnection(Global.connectionString);

            SqlCommand commandDB = new SqlCommand(query, DBConnection);
            commandDB.CommandTimeout = 60;
            SqlDataReader reader;
            List<Staff> row = new List<Staff>();


            //Open DB connection
            DBConnection.Open();
            //Execute the query and the reader will read shit from the DB
            reader = commandDB.ExecuteReader();

            //Checks if the query returned rows
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int no = Convert.ToInt32(reader["Employee_No"]);
                    string name = reader["Employee_Name"].ToString();
                    string Lname = reader["Employee_LName"].ToString();
                    string address = reader["Employee_Address"].ToString();
                    string email = reader["Employee_Email"].ToString();
                    int HPhone = Convert.ToInt32(reader["Employee_HomePhone"].ToString());
                    int MPhone = Convert.ToInt32(reader["Employee_MobilePhone"].ToString());
                    string nextToKin = reader["Employee_NextToKin"].ToString();
                    int nextToKinPhoneNo = Convert.ToInt32(reader["Employee_NextToKinPhoneNo"]);
                    string nextToKinRelationship = reader["Employee_NextToKinRelationship"].ToString();
                    string PPSN = reader["Employee_PPSN"].ToString();
                    string empType = reader["Employee_Type"].ToString();

                    Staff newStaff = AccessHandler.Make_Employee(no, name, Lname, address, email, HPhone, MPhone,
                    nextToKin, nextToKinPhoneNo, nextToKinRelationship, PPSN, empType);

                    row.Add(newStaff);

                    Update("UPDATE employee SET Employee_Type = Reception manager WHERE Employee_No = 34655422326");
                }
            }

            DBConnection.Close();
            return row;

        }


        public static bool Write(string query)
        {
            SqlConnection DBConnection = new SqlConnection(Global.connectionString);

            SqlCommand commandDB = new SqlCommand(query, DBConnection);
            commandDB.CommandTimeout = 60;
            SqlDataReader reader;
            NameValueCollection row = new NameValueCollection();
            //Open DB connection
            DBConnection.Open();
            //Execute the query and the reader will read shit from the DB
            reader = commandDB.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                }

                DBConnection.Close();
                return true;
            }

            catch (Exception ex)
            {
                WriteLine("Encountered error: " + ex.ToString());
                return false;
            }

        }

        public static bool Update(string query)
        {
            SqlConnection DBConnection = new SqlConnection(Global.connectionString);

            SqlCommand commandDB = new SqlCommand(query, DBConnection);
            commandDB.CommandTimeout = 60;
            SqlDataReader reader;
            NameValueCollection row = new NameValueCollection();
            try
            {
                //Open DB connection
                DBConnection.Open();
                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }

                DBConnection.Close();
                return true;
            }

            catch (Exception ex)
            {
                WriteLine("Encountered error: " + ex.ToString());
                return false;
            }
        }

        public static bool Delete(string query)
        {

            SqlConnection DBConnection = new SqlConnection(Global.connectionString);

            SqlCommand commandDB = new SqlCommand(query, DBConnection);
            commandDB.CommandTimeout = 60;
            SqlDataReader reader;
            NameValueCollection row = new NameValueCollection();


            try
            {
                //Open DB connection
                DBConnection.Open();
                //Execute the query and the reader will read shit from the DB
                reader = commandDB.ExecuteReader();

                while (reader.Read())
                {
                }

                DBConnection.Close();
                return true;
            }

            catch (Exception ex)
            {
                WriteLine("Encountered error: " + ex.ToString());
                return false;
            }
        }

        public static bool CheckDatabaseExists()
        {
            bool isExist = false;
            string cmdText = "SELECT TOP 1 * FROM [mydb].[dbo].[employee];";
            using (SqlConnection con = new SqlConnection(Global.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        isExist = reader.HasRows;
                    }
                }
                con.Close();
            }
            return isExist;
        }


    }
}
