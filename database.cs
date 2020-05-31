using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalStore
{
    public class Database
    {
        private string connectionString = @"Data Source=JACKLAPTOP\SQLEXPRESS02;Initial Catalog=Movies;Integrated Security=True;";

        public bool CreateEntry(string tableName, string parameterDefine, string[] parameterAssign, string[] insertArr)
        {
            // Define DB connection
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                // Define createStatement
                string createStatement = $@"INSERT INTO {tableName} VALUES ({parameterDefine})";

                // Define SQL Command
                SqlCommand createData = new SqlCommand(createStatement, Con);

                if (parameterAssign.Length == insertArr.Length)
                {
                    for (int i = 0; i < parameterAssign.Length; i++)
                    {
                        createData.Parameters.AddWithValue(parameterAssign[i], insertArr[i]);
                    }
                }

                // Open DB Connection
                Con.Open();

                // Run SQL Command - Will return how many rows were effected
                int returnValue = createData.ExecuteNonQuery();

                // Close DB Connection
                Con.Close();

                if (returnValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public DataView ReadEntries(string selectFields, string tableName, string whereClause)
        {
            // 1
            // Open connection
            using (SqlConnection Con = new SqlConnection(
                connectionString))
            {
                Con.Open();
                // Create new DataAdapter
                using (SqlDataAdapter adapter = new SqlDataAdapter(
                    $@"SELECT {selectFields} FROM {tableName} {whereClause}", Con))
                {
                    // Use DataAdapter to fill DataTable
                    DataTable tempTable = new DataTable();
                    adapter.Fill(tempTable);

                    // Create DataView and fill with DataTable
                    DataView returnView = new DataView(tempTable);

                    // Return DataView
                    return returnView;
                }
            }
        }

        public bool UpdateEntry(string tableName, string setFields, string whereClause, string[] parameterAssign, string[] updateArr)
        {
            // Define DB connection
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                // Define Query Statement
                string updateStatement = $@"UPDATE {tableName} SET {setFields} {whereClause}";

                // Define SQL Command
                SqlCommand updateData = new SqlCommand(updateStatement, Con);

                if (parameterAssign.Length == updateArr.Length)
                {
                    for (int i = 0; i < parameterAssign.Length; i++)
                    {
                        updateData.Parameters.AddWithValue(parameterAssign[i], updateArr[i]);
                    }
                }

                // Open DB Connection
                Con.Open();

                // Run SQL Command - Will return how many rows were effected
                int returnValue = updateData.ExecuteNonQuery();

                // Close DB Connection
                Con.Close();

                if (returnValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteEntry(string tableName, string whereClause, string ID)
        {
            // Define deleteStatement
            string deleteStatement = $@"DELETE FROM {tableName} {whereClause}";

            // Define DB Connection
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                // Define SQL Command
                SqlCommand deleteData = new SqlCommand(deleteStatement, Con);

                // Assign Parameters
                deleteData.Parameters.AddWithValue("@ID", ID);

                // Opens Connection to DB
                Con.Open();

                // Executes SQL Command - Will return how many rows were effected
                int returnValue = deleteData.ExecuteNonQuery();

                // Closes Connection to DB
                Con.Close();

                if (returnValue == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string TestConnection()
        {
            // Define DB Connection
            using (SqlConnection Con = new SqlConnection(connectionString))
            {
                // Define queryString
                string queryString = "SELECT MovieIDFK FROM RentedMovies WHERE RMID=1";
                // Define runQuery
                SqlCommand runQuery = new SqlCommand(queryString, Con);
                // Open Connection to DB
                Con.Open();
                // Executes SQL Command
                string result = runQuery.ExecuteScalar().ToString();
                // Close Connection to DB
                Con.Close();
                // Return Result
                return result;
            }
        }
    }
}
