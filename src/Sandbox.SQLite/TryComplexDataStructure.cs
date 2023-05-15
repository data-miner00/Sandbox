namespace Sandbox.SQLite
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Sandbox.Library.VB;

    public class DataTransferObject
    {
        public string UserId { get; set; }

        public string Favorite { get; set; }
    }

    public class TryComplexDataStructure
    {
        public static List<User> LoadUser()
        {
            using (SQLiteConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<User>("SELECT * FROM Users;");

                foreach (var user in output.ToList())
                {
                    var favourites = new List<string>();
                    var o = connection.Query<DataTransferObject>("SELECT * FROM Favorites WHERE UserId = " + user.Id);
                    if (o != null)
                    {
                        favourites.AddRange(o.Select(x => x.Favorite));
                        user.Favorites = favourites;
                    }
                }

                return output.ToList();
            }
        }

        public static void SaveUser(User user)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Execute("INSERT INTO Users (Username, Password) values (@Username, @Password);", user);

                foreach (var fav in user.Favorites)
                {
                    conn.Execute("INSERT INTO Favorites (UserId, Favorite) values (@UserId, @Favorite);", new { UserId = 1, Favorite = fav, });
                }
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
