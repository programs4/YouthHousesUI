using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Administrators_Default : System.Web.UI.Page
{
    Tools.Tables tableName = Tools.Tables.V_Administrators;
    Dictionary<string, object> filterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        //dListOrganizations.DataSource = DALCL.GetAllOrganizations();
        //dListOrganizations.DataBind();
        //dListOrganizations.Items.Insert(0, new ListItem("--", "-1"));

        dListOrganizations.Items.Add(new ListItem(DALCL._Login.organizationsName, DALCL._Login.organizationsId.ToString()));
        dListOrganizations.Enabled = false;

        dListAdministratorsStatus.DataSource = DALCL.GetAdministratorsStatus();
        dListAdministratorsStatus.DataBind();
        dListAdministratorsStatus.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindAdministrators()
    {
        grdAdministrators.DataSource = null;
        grdAdministrators.DataBind();

        pnlFilter.ControlsBind(filterDictionary, tableName);

        int administratorsId;
        int.TryParse(txtAdministratorsId.Text, out administratorsId);
        if (administratorsId == 0)
        {
            administratorsId = -1;
        }

        filterDictionary = new Dictionary<string, object>()
        {
            {"Id",administratorsId},
            {"OrganizationsId",int.Parse(dListOrganizations.SelectedValue)},
            {"OrganizationsLangsId",(int)Tools.Langs.AZ},
            {"AdministratorsStatusId",int.Parse(dListAdministratorsStatus.SelectedValue)}
        };

        int pageNumber;
        int rowNumber = 50;

        if (!int.TryParse(ConfigL._Route("pagenum", "1"), out pageNumber))
        {
            pageNumber = 1;
        }

        HdnPageNumber.Value = pageNumber.ToString();

        DALC.DataTableResult administratorsResult = DALC.GetFilterList(tableName, filterDictionary, pageNumber, rowNumber);

        if (administratorsResult.Count == -1)
        {
            return;
        }

        lblCount.Text = string.Format("Axtarış üzrə nəticə: {0}", administratorsResult.Count.ToString());

        int totalCount = administratorsResult.Count % rowNumber > 0 ? (administratorsResult.Count / rowNumber) + 1 : administratorsResult.Count / rowNumber;
        hdnTotalCount.Value = totalCount.ToString();

        pnlPager.Visible = administratorsResult.Count > rowNumber;

        grdAdministrators.DataSource = administratorsResult.Dt;
        grdAdministrators.DataBind();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.İdarəçilər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "İDARƏÇİLƏR";
            BindDList();
            BindAdministrators();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsBind(filterDictionary, tableName, "", true);
        ConfigL.RedirectURL(string.Format("/{0}/modules/administrators/1", Langs.Name));
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsClear();
        btnFilter_Click(null, null);
    }
}