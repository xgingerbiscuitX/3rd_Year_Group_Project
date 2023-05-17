using BusinessEntities;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;


namespace fakeDalUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginTestDelay()
        {
            ArrayList userList = new ArrayList();
            IDataLayer _Datalayer; 
            _Datalayer = IDatalayer.GetInstance();
            userList = _Datalayer.getAllUsers();

            string username = "Lukasz4282";
            string password = "awesome123";

            Staff CurrentUser = null;

            foreach (Staff staff in userList)
            {
                //MessageBox.Show("What is being checked: " + staff.UserName + " - " + staff.Password + "\nWhat You typed in: " + username + " - " + password);
                if (username == staff.UserName && Crypt.Encrypt(password) == staff.Password)
                {
                    CurrentUser = staff;
                }
            }

            Assert.IsNotNull(CurrentUser);
        }
    }
}
