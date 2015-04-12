using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.SessionState;

/// <summary>
/// Summary description for WebPlayer
/// </summary>
public class WebPlayer
{

    private static HttpSessionState Session
    {
        get
        {
            return HttpContext.Current.Session;
        }
    }
 
    public static bool Authenticate(string username, string password)
    {
        var player = PlayerRepository.Get(username);

        if (player == null)
        {
            return false;
        }

        return Crypto.VerifyHashedPassword((string)player.Password, password);
    }

    public static void Login(string username)
    {
        var player = PlayerRepository.Get(username);

        if (player == null)
        {
            return;
        }

        SetupSession(player);
    }

    public static bool AuthenticateAndLogin(string username, string password)
    {
        var player = PlayerRepository.Get(username);

        if (player == null)
        {
            return false;
        }

        var verified = Crypto.VerifyHashedPassword((string)player.Password, password);
        if (!verified)
        {
            return false;
        }

        SetupSession(player);
        return true;

    }

    public static string UserStatistics(string username)
    {
        var player = PlayerRepository.Get(username);

        if (player == null)
        {
            return string.Empty;
        }

        string userstat = "Dear <b>" + username;
        userstat += "</b>, You have played " + (player.win + player.lost + player.draw) + " times, ";
        userstat += "You have won " + player.win + " times, lost " + player.lost + " times, drawn " + player.draw + " times.";
        return userstat;

    }

    public static bool UpdateStatistic(string username, string password)
    {
        var player = PlayerRepository.Get(username);

        if (player == null)
        {
            return false;
        }

        var verified = Crypto.VerifyHashedPassword((string)player.Password, password);
        if (!verified)
        {
            return false;
        }

        SetupSession(player);
        return true;

    }

    private static void SetupSession(dynamic player)
    {
        //var session = HttpContext.Current.Session;
        Session["playerId"] = (int)player.Id;
        Session["username"] = (string)player.Username;
        Session["birthday"] = (string)player.birthday;
    }

    public static int PlayerId
    {
        get{
            var value = Session["playerId"];
            if (value == null)
            {
                //there is no user id = -1 , so it means user does not exist
                return - 1;
            }
            return (int)value;
        }
    }

    public static string Username
    {
        get
        {
            var value = Session["username"];
            if (value == null)
            {
                //there is no null username , so it means user does not exist
                return string.Empty;
            }
            return (string)value;
        }
    }

    public static string Birthday
    {
        get
        {
            var value = Session["birthday"];
            if (value == null)
            {
                //there is no null username , so it means user does not exist
                return string.Empty;
            }
            return (string)value;
        }
    }

   //not needed ...
    public static string updatedBirthday
    {
        get
        {
            var value = Session["birthday"];
            if (value == null)
            {
                //there is no null username , so it means user does not exist
                return string.Empty;
            }
            return (string)value;
        }
    }

    public static bool IsAuthenticated
    {
        get
        {
            return !string.IsNullOrEmpty(Username);
        }
    }

    public static bool IsBirthday
    {
        get
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            string pattern = "-";
            var splitedTodaye = System.Text.RegularExpressions.Regex.Split(today, pattern);


            pattern = "-";
            var splitedBirthday = System.Text.RegularExpressions.Regex.Split(Birthday, pattern);
            
            if(Convert.ToInt32(splitedBirthday[1]) == Convert.ToInt32(splitedTodaye[1]) &&
               Convert.ToInt32(splitedBirthday[2]) == Convert.ToInt32(splitedTodaye[2])
                ) {
                return true;
                }
            else
            {
                return false;
            }
    
        }
    }

    public static bool isAdmin
    {
        get
        {
            if (Username == "administrator")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}