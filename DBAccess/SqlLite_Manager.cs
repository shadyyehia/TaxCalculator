using DBAccess.Models;
using System.Data.Entity;
using System.Data.SQLite;

namespace DBAccess
{
    public class SqlLite_Manager : IDB_Manager
    {

       

        SQLiteConnection CreateConnection(string conStr)
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection(conStr);
         // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        public TaxCalculatorParameters ReadData(string connectionStr, string city)
        {

            SQLiteConnection conn = CreateConnection(connectionStr);
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM TaxCalculatorParameters where city = '" + city.ToLower() +"'";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            TaxCalculatorParameters _params = new TaxCalculatorParameters();
            while (sqlite_datareader.Read())
            {
               _params.PeriodOfTimeWhenConsequentPasses = sqlite_datareader.GetInt32(1);
            }
            
            conn.Close();
            return _params;
        }
    }
}