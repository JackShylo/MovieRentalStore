using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalStore
{
    class Movies
    {
        Database moviesDatabase = new Database();
        private string tableName = "Movies";

        public bool CreateMovie(string[] insertArr)
        {
            // parameterDefine, parameterAssign & insertArr need to be the same length.
            string parameterDefine = "@Rating, @Title, @Year, @Rental_Cost, @Copies, @Plot, @Genre";
            string[] parameterAssign = { "@Rating", "@Title", "@Year", "@Rental_Cost", "@Copies", "@Plot", "@Genre" };
            return moviesDatabase.CreateEntry(tableName, parameterDefine, parameterAssign, insertArr);
        }

        public DataView ReadEntries()
        {
            string selectFields = "*";
            string whereClause = "";
            return moviesDatabase.ReadEntries(selectFields, tableName, whereClause);
        }

        public bool UpdateMovie(string[] updateArr)
        {
            // parameterDefine, parameterAssign & insertArr need to be the same length.
            string[] parameterAssign = { "@MovieID", "@Rating", "@Title", "@Year", "@Rental_Cost", "@Copies", "@Plot", "@Genre" };
            string setFields = "Rating=@Rating, Title=@Title, Year=@Year, Rental_Cost=@Rental_Cost, Copies=@Copies, Plot=@Plot, Genre=@Genre";
            string whereClause = "WHERE MovieID=@MovieID";
            return moviesDatabase.UpdateEntry(tableName, setFields, whereClause, parameterAssign, updateArr);
        }

        public bool DeleteMovie(string ID)
        {
            string whereClause = "WHERE CustID=@ID";
            return moviesDatabase.DeleteEntry(tableName, whereClause, ID);
        }
    }
}
