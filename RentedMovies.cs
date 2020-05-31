using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalStore
{
    class RentedMovies
    {
        Database rentedMoviesDatabase = new Database();

        public DataView ReadEntries(string selectFields, string tableName, string whereClause)
        {

            return rentedMoviesDatabase.ReadEntries(selectFields, tableName, whereClause);
        }

        public bool IssueMovie(string[] insertArr)
        {
            string tableName = "RentedMovies";
            // parameterDefine, parameterAssign & insertArr need to be the same length.
            string parameterDefine = "@MovieIDFK, @CustIDFK, @DateRented, @DateReturned";
            string[] parameterAssign = { "@MovieIDFK", "@CustIDFK", "@DateRented", "@DateReturned" };
            return rentedMoviesDatabase.CreateEntry(tableName, parameterDefine, parameterAssign, insertArr);
        }

        public bool ReturnMovie(string[] updateArr)
        {
            string tableName = "RentedMovies";
            string setFields = "DateReturned=@DateReturned";
            string whereClause = "WHERE RMID=@RMID";
            string[] parameterAssign = { "@DateReturned", "@RMID" };
            return rentedMoviesDatabase.UpdateEntry(tableName, setFields, whereClause, parameterAssign, updateArr);
        }
    }
}
