namespace Sandbox.SQLite;

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Sandbox.Library.VB;

public static class TryComplexDataStructure
{
    public record DataTransferObject(string UserId, string Favorite);

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
