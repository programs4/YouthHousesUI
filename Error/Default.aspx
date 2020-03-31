<%@ Page Language="C#" %>

<%@ Import Namespace="YouthHousesLibrary" %>

<!DOCTYPE html>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Login"] = null;
        Session.RemoveAll();
        Session.Clear();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>YouthHouses UI</title>
    <link rel="shortcut icon" href="/favicon.ico" />
    <link href="/Css/bootstrap.css" rel="stylesheet" />
    <link href="/Css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="aspnetForm" runat="server">
        <div style="margin: 20px">
            <div class="alert alert-danger" role="alert">
                <span class="sr-only">Error:</span>
                <%=Config._AlertMessages.Error %>
                <p><a href="/">İstifadəçi girişi</a></p>
            </div>
        </div>
    </form>
</body>
</html>
