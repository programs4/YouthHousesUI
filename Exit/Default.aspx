﻿<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Clear();
        ConfigL.RedirectURL("/");
    }
</script>


