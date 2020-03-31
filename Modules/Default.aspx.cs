using System;
using System.Web.UI.WebControls;

public partial class Modules_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "XOŞ GƏLMİŞSİNİZ !";
        }
    }
}