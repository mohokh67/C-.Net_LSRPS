using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using WebMatrix.Data;

/// <summary>
/// Summary description for Player
/// </summary>
public class Player
{
    private static WebPageRenderingBase Page
    {
        get
        {
            return WebPageContext.Current.Page;
        }
    }

    public static string Mode
    {
        get
        {
            if (Page.UrlData.Any())
            {
                return Page.UrlData[0].ToLower();
            }
            return string.Empty;
        }
    }

    public static string Username
    {
        get
        {
            if (Mode != "new")
            {
                return Page.UrlData[1];
            }
            return string.Empty;
        }
    }

    public static dynamic Current
    {
        get
        {
            var result = PlayerRepository.Get(Username);
            return result ?? CreatePlayerObject();
        }

    }

    private static dynamic CreatePlayerObject()
    {
        dynamic thisUser = new ExpandoObject();

        thisUser.Id       = 0;
        thisUser.win      = 0;
        thisUser.lost     = 0;
        thisUser.draw     = 0;
        thisUser.Username = string.Empty;
        thisUser.Password = string.Empty;
        thisUser.Email    = string.Empty;
        thisUser.Birthday = string.Empty;

        return thisUser;

    }



}