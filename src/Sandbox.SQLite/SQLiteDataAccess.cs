namespace Sandbox.SQLite
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;
    using Dapper;
    using Sandbox.VisualBasic.Library;

    public class SQLiteDataAccess
    {
        public static List<Person> LoadPeople()
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = conn.Query<Person>("SELECT * FROM Person;", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SavePerson(Person person)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameter = new DynamicParameters();

                conn.Execute("INSERT INTO Person (FirstName, LastName) values (@FirstName, @LastName)", person);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
