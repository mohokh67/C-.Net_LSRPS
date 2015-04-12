<%@ Page Language="C#" %>

<script runat="server">
  protected HttpException ex = null;

  protected void Page_Load(object sender, EventArgs e)
  {
      // Log the exception and notify system operators
      ex = new HttpException("HTTP 409");
 
  }
</script>

<HTML>
<HEAD>
<TITLE></TITLE>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 7.0">
</HEAD>
<BODY>
     <b>Custom Error page!</b>
     <br>
     You have been redirected here from the <customErrors> section of the 
     Web.config file.
</BODY>
</HTML>