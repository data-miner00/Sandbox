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
    using Sandbox.VisualBasic.Library;

    public class TryComplexDataStructureWithEnum
    {
        public static List<Article> LoadArticles()
        {
            using (SQLiteConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<Article>("SELECT * FROM Articles;");

                return output.ToList();
            }
        }

        public static void SaveArticle(Article article)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Execute("INSERT INTO Articles (Title, Category) values (@Title, @Category);", article);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
