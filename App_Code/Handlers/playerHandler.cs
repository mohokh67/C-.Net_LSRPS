using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.SessionState;
using WebMatrix.Data;

/// <summary>
/// Summary description for playerHandler
/// </summary>
public class playerHandler : IHttpHandler, IReadOnlySessionState
{

    public bool IsReusable
    {
        get { return false; }
    }

    private static string createHashPassword(string unHashedPassword)
    {
        //hash the password for more security 
        //it adds salt automaticly
        return Crypto.HashPassword(unHashedPassword);
    }

    public void ProcessRequest(HttpContext context)
    {
        //Cross Site Request Forgery
        AntiForgery.Validate();
        var mode = context.Request.Form["mode"];
        var id = context.Request.Form["playerId"];
        var username = context.Request.Form["username"];
        var password = context.Request.Form["password"];
        var password2 = context.Request.Form["password2"];
        var email = context.Request.Form["email"];
        var birthday = context.Request.Form["birthday"];
        //used in JS delete player
        var resourceItem = context.Request.Form["resourceItem"];


        if (mode == "delete")
        {
            //if (WebPlayer.isAdmin || WebPlayer.Username == username)
            if (WebPlayer.isAdmin) //only admin user can delete users
            {
                //use resourseItem for Ajax and username for normal actions
                DeleteUser(username ?? resourceItem);
            }
            
        }
        else
        {

            if (password != password2)
            {
                //password and confirmatin must be the same at each time, for edit and new 
                throw new Exception("Passwords do not match.");

            }

            if (string.IsNullOrWhiteSpace(email))
            {
                //However, I checked the email text box by 'HTML INPUT ATTRIBUTE' and It should fill in corrct format
                //But I will check again in case of any problem
                throw new Exception("Email can not be blank.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                //However, I checked the username text box by 'HTML INPUT ATTRIBUTE' and It is required
                //But I will check again in case of any problem
                throw new Exception("Username can not be blank.");
            }

            if (mode == "edit")
            {

                if (WebPlayer.isAdmin || WebPlayer.Username == username)
                {
                    username = username.ToLower();
                    email = email.ToLower();
                    EditUser(Convert.ToInt32(id), username, password, email, birthday);
                }
                
                
            }
            else if (mode == "new")
            {
                if (WebPlayer.IsAuthenticated)
                {
                    throw new Exception("Username can not be blank.");
                }

                username = username.ToLower();
                email = email.ToLower();
                CreatUser(username, password, email, birthday, 0, 0, 0);
            }
        }

        if (string.IsNullOrEmpty(resourceItem))
        {
            context.Response.Redirect("/player/");
        }
        

    }

    private void EditUser(int id, string username, string password, string email, string birthday)
    {
        var result = PlayerRepository.Get(id);

        if (result == null)
        {
            //throw new HttpException(410, "You are already registered loged in!");
        }

        //updating password if ONLY the user put some thing in password text box
        //Initilize updatedPassword with current password
        var updatedPassword = result.Password;
        if (!string.IsNullOrWhiteSpace(password))
        {
            updatedPassword = createHashPassword(password);
        }

        //We do not need (Or user is not allowed to chenge the username)
        //So we use the current username for updating
        PlayerRepository.Edit(id, result.Username, updatedPassword, email, birthday);
        //update the session value specially for changin the DOB 
        WebPlayer.Login(WebPlayer.Username);
    }

    //Not used
    private static void CreatUser(string username, string password, string email, string birthday, int win, int lost, int draw)
    {

        if (string.IsNullOrWhiteSpace(password))
        {
            //However, I checked the email text box by 'HTML INPUT ATTRIBUTE' and It is required
            //But I will check again in case of any problem
            //throw new Exception("Password can not be blank.");
        }
       

        var result = PlayerRepository.Get(username);

        if (result != null)
        {
            //throw new HttpException(409, "Username is already in use! try another one");
            //Session["ErrorCode"] = "Username is already in use! try another one";
            //RegisterError("Username is already in use! try another one");
            //ErrorMessage = "Username is already in use! try another one";
        }
        else
        {
            PlayerRepository.Add(username, createHashPassword(password), email, birthday, 0, 0, 0);
        }
       
    }

    private void DeleteUser(string username)
    {
        PlayerRepository.Remove(username);
        //need layout page
    }

    public static void UpdateStatistic(int gameResult, string username)
    {
        var player = PlayerRepository.Get(username);

        if (player == null)
        {
            //throw new HttpException(404, "Player does not exist");
        }
        else
        {
            string increaseColumn = increaseColumnSQL(gameResult);
            PlayerRepository.UpdateStatistic(increaseColumn, username);
        }
        

    }

    private static string increaseColumnSQL(int gameResult)
    {
        var increaseColumn = string.Empty;
        switch (gameResult)
        {

            case -1:
                increaseColumn = "[lost] = lost+1";
                break;

            case 0:
                increaseColumn = "[draw] = draw+1";
                break;

            case 1:
                increaseColumn = "[win] = win+1";
                break;
        }

        return increaseColumn;
    }

}