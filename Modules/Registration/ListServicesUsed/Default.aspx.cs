using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Registration_ListServicesUsed_Default : System.Web.UI.Page
{
    int _usersId = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Qeydiyyat))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    private void BindReports()
    {
        lblUsersFullname.Text = DALCL.GetUsersById(_usersId)._Rows("Fullname");

        grdReports.DataSource = DALC.GetDataTableBySqlCommand
            (
                "GetServicesUsedByUsersId",
                "OrganizationsId,LangsId,UsersId",
                new object[] { DALCL._Login.organizationsId, (int)Tools.Langs.AZ, _usersId },
                CommandType.StoredProcedure
            );
        grdReports.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _usersId = ConfigL._Route("id", "-1")._ToInt32();
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "İSTİFADƏ ETDİYİ XİDMƏTLƏRİN SİYAHISI";

            BindReports();

        }
    }
}