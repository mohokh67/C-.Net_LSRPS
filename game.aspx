<%@ Page Language="C#" AutoEventWireup="true" CodeFile="game.aspx.cs" Inherits="game" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <link href="/Content/Site.css" rel="stylesheet" />
</head>
<body>
    <header>
        <div class="content-wrapper">
            <div class="float-left">
                <p class="site-title"><a href="/">Rock Paper Scissors Lizard Spock</a></p>
            </div>
            <div class="float-right">
                <nav>

                    <ul id="menu">
                        <li><asp:HyperLink ID="homelink" runat="server">Home</asp:HyperLink></li>
                        <li><asp:HyperLink ID="fullstatlink" runat="server">Full Statistics</asp:HyperLink></li>
                        <li><asp:HyperLink ID="gamelink" runat="server">Game</asp:HyperLink></li>
                        <li><asp:HyperLink ID="editlink" runat="server">Profile</asp:HyperLink></li>
                        <li><asp:HyperLink ID="logoutlink" runat="server">Logout</asp:HyperLink></li>

                    </ul>
                </nav>

            </div>
        </div>
    </header>
    <div id="body">

        <section class="content-wrapper main-content clear-fix">
            <center>
            <asp:Label ID="birthdayLabel" class="message-success" runat="server" Text=""></asp:Label> <br />
            <asp:Label ID="userInfo" runat="server" Text=""></asp:Label><br /><br />
            <asp:Label ID="startgame" runat="server" Text="">Choose one for play please.</asp:Label><br /><br />
            <form id="form1" runat="server">
                <div>
                    <asp:ImageMap ID="RPSLS_photo" runat="server" Height="400px" HotSpotMode="PostBack"
                        ImageUrl="~/Photo/RPSLS.png" OnClick="ImageMap1_Click1" Width="400px" BorderStyle="None">
                        <asp:CircleHotSpot AlternateText="Spock" PostBackValue="4" Radius="60" X="68" Y="170" />
                        <asp:CircleHotSpot AlternateText="Lizard" PostBackValue="3" Radius="60" X="120" Y="325" />
                        <asp:CircleHotSpot AlternateText="Scissors" PostBackValue="2" Radius="60" X="284" Y="325" />
                        <asp:CircleHotSpot AlternateText="Paper" PostBackValue="1" Radius="60" X="330" Y="170" />
                        <asp:CircleHotSpot AlternateText="Rock" PostBackValue="0" Radius="60" X="200" Y="70" />
                    </asp:ImageMap>



                    <div>
                        <asp:Label ID="gameResult" runat="server"></asp:Label></div>

                </div>
            </form>
                </center>
        </section>
    </div>
    <footer>
        <div class="content-wrapper">
            <div class="float-left">

                <p>
                    <a href="/information/">How to play</a>&nbsp;&nbsp;&nbsp;
                    <a href="/projectinfo/">Coursework Information</a>
                </p>
                <p>This project required .Net Framework 4.5</p>
            </div>
        </div>
    </footer>
</body>
</html>
