using DBAccess.Models;
using System.Data.Entity;
using System.Data.SQLite;

namespace DBAccess
{
    public interface IDB_Manager
    {
        TaxCalculatorParameters ReadData(string conStr,string city);
    }
}