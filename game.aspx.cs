using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class game : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var message = string.Empty;
        
        if (!WebPlayer.IsAuthenticated) { Response.Redirect("~/login/"); }
        if (WebPlayer.IsBirthday) { birthdayLabel.Text = "Happy birthday " + WebPlayer.Username; }
        userInfo.Text = WebPlayer.UserStatistics(WebPlayer.Username);
        
        var editUserLink = "~/player/edit/" + WebPlayer.Username + "/";
        editlink.NavigateUrl = editUserLink;
        logoutlink.NavigateUrl = "~/logout/";
        homelink.NavigateUrl = "~/";
        gamelink.NavigateUrl = "~/game.aspx";
        fullstatlink.NavigateUrl = "~/player/";

        
    }

    protected void ImageMap1_Click1(object sender, ImageMapEventArgs e)
    {
       
        int computer = Game.computerChoice;
        var game_result = string.Empty;        
        int user_number = Convert.ToInt32(e.PostBackValue);        
        
        
        int rawResult = Game.CalculateResult(user_number, computer);
        //update databse 
        playerHandler.UpdateStatistic(rawResult, WebPlayer.Username);
        userInfo.Text = WebPlayer.UserStatistics(WebPlayer.Username);


        game_result = Game.UserFriendlyResult(rawResult);

        gameResult.Text = 
            "You clicked the " + Game.ChangeToWord(user_number) + " and computer chose " + Game.ChangeToWord(computer) +
            " and it means you: " + game_result;

    }


}