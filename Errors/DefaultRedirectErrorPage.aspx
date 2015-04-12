<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultRedirectErrorPage.aspx.cs" Inherits="errors_Forbidden" %>

 <script runat="server">
    protected HttpException ex = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Log the exception and notify system operators
        ex = new HttpException("HTTP 404");


    }
</script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <div> Some thing wrong happen!</div>

   

     Return to the <a href="../default.cshtml" >Default Page</a>
      
</body>
</html>
