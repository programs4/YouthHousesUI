using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Reports_Default : System.Web.UI.Page
{

    Tools.Tables tableName = Tools.Tables.V_Administrators;
    Dictionary<string, object> filterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        dListOrganizations.Items.Add(new ListItem(DALCL._Login.organizationsName, DALCL._Login.organizationsId.ToString()));
        dListOrganizations.Enabled = false;
    }       

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Hesabatlar))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "HESABATLAR";

            BindDList();
            btnFilter_Click(null,null);
        }
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {       
        try
        {
            PnlContent.Controls.Add(Page.LoadControl($"/modules/reports/usercontrols/{dlistReportsType.SelectedValue}.ascx"));
        }
        catch (Exception er)
        {
            DALC.ErrorLog("Report modules user control binding catch error: " + er.Message);
            ConfigL.RedirectError();
            return;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsClear();
        btnFilter_Click(null, null);
    }
}