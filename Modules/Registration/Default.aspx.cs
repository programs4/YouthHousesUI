using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Registration_Default : System.Web.UI.Page
{
    Tools.Tables tableName = Tools.Tables.V_Users;
    Dictionary<string, object> filterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        dListSocialStatus.DataSource = DALCL.GetUsersSocialStatusAll();
        dListSocialStatus.DataBind();
        dListSocialStatus.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindUsers()
    {
        rptUsers.DataSource = null;
        rptUsers.DataBind();

        pnlFilter.ControlsBind(filterDictionary, tableName);

        int usersId;
        int.TryParse(txtUsersId.Text, out usersId);
        if (usersId == 0)
        {
            usersId = -1;
        }

        filterDictionary = new Dictionary<string, object>()
        {

            {"Id",usersId},
            {"OrganizationsId",DALCL._Login.organizationsId},
            {"UsersSocialStatusLangsId",(int)Tools.Langs.AZ},
            {"UsersSocialStatusId",int.Parse(dListSocialStatus.SelectedValue) },
            {"UsersGendersLangsId",(int)Tools.Langs.AZ},
        };

        int pageNumber;
        int rowNumber = 50;

        if (!int.TryParse(ConfigL._Route("pagenum", "1"), out pageNumber))
        {
            pageNumber = 1;
        }

        hdnPageNumber.Value = pageNumber.ToString();

        DALC.DataTableResult usersResult = DALC.GetFilterList(tableName, filterDictionary, pageNumber, rowNumber);

        if (usersResult.Count == -1)
        {
            return;
        }

        lblCount.Text = $"Axtarış üzrə nəticə: {usersResult.Count.ToString()}";

        int totalCount = usersResult.Count % rowNumber > 0 ? (usersResult.Count / rowNumber) + 1 : usersResult.Count / rowNumber;
        hdnTotalCount.Value = totalCount.ToString();

        pnlPager.Visible = usersResult.Count > rowNumber;

        rptUsers.DataSource = usersResult.Dt;
        rptUsers.DataBind();
    }

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "QEYDİYYAT";
            BindDList();
            BindUsers();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsBind(filterDictionary, tableName, "", true);
        ConfigL.RedirectURL(string.Format("/{0}/modules/news/1", Langs.Name));
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsClear();
        BtnFilter_Click(null, null);
    }

    protected void RptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Literal LtrStart = (Literal)e.Item.FindControl("LtrStart");
            //Literal LtrEnd = (Literal)e.Item.FindControl("LtrEnd");

            //LtrStart.Visible = e.Item.ItemIndex % 2 == 0;
            //LtrEnd.Visible = (e.Item.ItemIndex - 1) % 2 == 0;
        }
    }

}