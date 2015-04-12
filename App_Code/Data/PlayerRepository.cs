using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WebMatrix.Data;

/// <summary>
/// Summary description for PlayerRepository
/// </summary>
public class PlayerRepository
{
    private static readonly string _connectionString = "RPSLS_Connection";
    
    /*
    public PlayerRepository()
	{
		
	}
    */

    public static dynamic Get(int id)
    {
        using (var db = Database.Open(_connectionString))
        {
            var sql = "SELECT * FROM [user] WHERE [id] = @0";
            return db.QuerySingle(sql, id);
        }
    }

    public static dynamic Get(string username)
    {
        using (var db = Database.Open(_connectionString))
        {
            var sql = "SELECT * FROM [user] WHERE [username] = @0";
            return db.QuerySingle(sql, username);
        }
    }

    public static IEnumerable<dynamic> GetAll( string where=null, string orderBy=null)
    {
        using (var db = Database.Open(_connectionString))
        {
            var sql = "SELECT *,([win]+[lost]+[draw]) AS [total] FROM [user]";
            if (!string.IsNullOrEmpty(where))
            {
                sql += " WHERE " + where;
            } 
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " ORDER BY " + orderBy;
            }
            return db.Query(sql);
        }
    }

    public static void Add(string username, string password, string email, string birthday, int win=0, int lost=0, int draw=0)
    {
        using (var db = Database.Open(_connectionString))
        {
           var sql = "INSERT INTO [user] ([username], [password], [email], [birthday], [win], [lost], [draw]) VALUES (@0, @1, @2, @3, @4, @5, @6)";
           
           
           db.Execute(sql, username, password, email, birthday, win, lost, draw);
        }
    }

    public static void Edit(int id, string username, string password, string email, string birthday)
    {
        using (var db = Database.Open(_connectionString))
        {
          var sql = "UPDATE [user] SET [username] = @0, [password] = @1, [email] = @2, [birthday] = @3 WHERE [id] = @4";
           db.Execute(sql, username, password, email, birthday, id);
        }
    }

    public static void UpdateStatistic(string increaseColumn, string username)
    {
        using (var db = Database.Open(_connectionString))
        {

            // increaseColumn = "[win] = win + 1"
            var sql = "UPDATE [user] SET " + increaseColumn + " WHERE [username] = @0";
            db.Execute(sql, username);
        }
    }

    public static void Remove(string username)
    {
        using (var db = Database.Open(_connectionString))
        {
            var sql = "DELETE FROM [user] WHERE [username] = @0";

            db.Execute(sql, username);
        }
    }
}