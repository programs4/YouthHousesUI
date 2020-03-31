<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="YouthHousesLibrary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>YouthHouses UI</title>
    <link rel="shortcut icon" href="/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="/css/customscroll.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/styles.css" />
</head>
<body>
    <section class="loginHolder">
        <div class="container-fluid" style="height: 100%;">
            <div class="row" style="height: 100%;">
                <div class="col-lg-4 col-lg-offset-4 vMiddle col-md-6 col-md-offset-3">
                    <div class="loginHead" onclick="location.href='/'">
                        <img src="/images/logo.png" class="vMiddle" />
                    </div>
                    <div class="loginBody">
                        <form id="AspnetForm" runat="server">
                            <br />
                            <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" placeholder="İstifadəçi adı"></asp:TextBox><br />
                            <br />
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Şifrə" TextMode="Password"></asp:TextBox><br />
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="DAXİL OL" CssClass="identity-button" OnClick="btnSubmit_Click" OnClientClick="this.style.display='none';" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script type="text/javascript" src="/js/jquery.js"></script>
    <script type="text/javascript" src="/js/jquery-ui.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/customscroll.min.js"></script>
    <script type="text/javascript" src="/Js/jquery-mask.js"></script>
</body>
</html>
