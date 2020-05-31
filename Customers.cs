using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalStore
{
    class Customers
    {
        Database customersDatabase = new Database();
        // Global Variables
        private string tableName = "Customer";

        public bool CreateEntry(string[] insertArr)
        {
            string parameterDefine = "@FirstName, @LastName, @Address, @Phone";
            string[] parameterAssign = { "@FirstName", "@LastName", "@Address", "@Phone" };
            return customersDatabase.CreateEntry(tableName, parameterDefine, parameterAssign, insertArr);
        }

        public DataView ReadEntries()
        {
            string whereClause = "";
            string selectFields = "*";
            return customersDatabase.ReadEntries(selectFields, tableName, whereClause);
        }

        public bool UpdateEntry(string[] updateArr)
        {
            string[] parameterAssign = { "@CustID", "@FirstName", "@LastName", "@Address", "@Phone" };
            string whereClause = "WHERE CustID=@CustID";
            string setFields = "FirstName=@FirstName, LastName=@LastName, Address=@Address, Phone=@Phone";
            return customersDatabase.UpdateEntry(tableName, setFields, whereClause, parameterAssign, updateArr);
        }

        public bool DeleteEntry(string ID)
        {
            string whereClause = "WHERE CustID=@ID";
            return customersDatabase.DeleteEntry(tableName, whereClause, ID);
        }
    }
}
