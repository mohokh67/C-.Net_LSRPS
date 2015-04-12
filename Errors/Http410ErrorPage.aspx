<%@ Page Language="C#" %>

<script runat="server">
  protected HttpException ex = null;

  protected void Page_Load(object sender, EventArgs e)
  {
      // Log the exception and notify system operators
      ex = new HttpException("HTTP 410");
  
    
  }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
  <title>HTTP 410 Error Page</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <h2>
      HTTP 410</h2><br />
    <br />
    Return to the <a href='Default.cshtml'> Default Page</a>
      
  </div>
  </form>
</body>
</html>