using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRentalStore;

namespace MovieRentalStoreUnitTest
{
    [TestClass]
    public class DatabaseUnitTests
    {
        Database myDatabase = new Database();
        [TestMethod]
        public void MockDBTest()
        {
            string expected = "356";
            string actual = myDatabase.TestConnection();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testCreateEntry_True()
        {
            string tableName = "UnitTesting";
            string parameterDefine = "@ID, @FirstName, @LastName, @Address, @Phone";
            string[] parameterAssign = { "@ID", "@FirstName", "@LastName", "@Address", "@Phone" };
            string[] insertArr = { "1", "Jack", "Kelly", "420 Colombo Street", "0800 83 83 83" };

            bool expected = true;
            bool actual = myDatabase.CreateEntry(tableName, parameterDefine, parameterAssign, insertArr);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testDeleteEntry_True()
        {
            string tableName = "UnitTesting";
            string whereClause = "WHERE ID=@ID";
            string ID = "1";

            bool expected = true;
            bool actual = myDatabase.DeleteEntry(tableName, whereClause, ID);

            Assert.AreEqual(expected, actual);
        }
    }
}
