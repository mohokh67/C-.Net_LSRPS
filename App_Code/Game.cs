using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Game
/// </summary>
public class Game
{
    public static int computerChoice
    {
        get
        {
            Random random = new Random();
            return random.Next(0, 5);
        }

    }

    public static string UserFriendlyResult(int result)
    {
        var game_result = string.Empty;
        switch (result)
        {

            case -1:
                game_result = "Lost";
                break;


            case 0:
                game_result = "Drawn";
                break;



            case 1:
                game_result = "Won";
                break;
        }

        return game_result;
    }

    private string ChangeToWord2(int userChoice)
    {

        string[] gameOption = new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
        return gameOption[userChoice];

    }

    public static int CalculateResult(int UserChoice, int ComputerChoice)
    {
        var game_result = string.Empty;
        int[,] RPSLS_Array = new int[,]  {
	                                    {0,-1,1,1,-1},
	                                    {1,0,-1,-1,1},
	                                    {-1,1,0,1,-1},
	                                    {-1,1,-1,0,1},
	                                    {1,-1,1,-1,0}
                                     };

        int result = RPSLS_Array[UserChoice, ComputerChoice];
        return result;
    }
    
    public static string ChangeToWord(int userChoice)
    {

        var result = string.Empty;
        switch (userChoice)
        {

            case 0:
                result = "Rock";
                break;

            case 1:
                result = "Paper";
                break;

            case 2:
                result = "Scissors";
                break;

            case 3:
                result = "Lizard";
                break;

            case 4:
                result = "Spock";
                break;

        }
        return result;
    }

}