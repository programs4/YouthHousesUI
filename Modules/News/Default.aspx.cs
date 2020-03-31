using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_News_Default : System.Web.UI.Page
{
    Tools.Tables TableName = Tools.Tables.News;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        DListNewsStatus.DataSource = DALC.GetDataTable("*", Tools.Tables.NewsLangsStatus);
        DListNewsStatus.DataBind();
        DListNewsStatus.Items.Insert(0, new ListItem("--", "-1"));  
    }

    private void BindNews()
    {
        RptNews.DataSource = null;
        RptNews.DataBind();

        PnlFilter.ControlsBind(FilterDictionary, TableName);

        int NewsID;
        int.TryParse(TxtNewsID.Text, out NewsID);
        if (NewsID == 0)
        {
            NewsID = -1;
        }

        FilterDictionary = new Dictionary<string, object>()
        {
           
            {"Id",NewsID},
            {"LangsId",Langs.Id},           
            {"OrganizationsId",DALCL._Login.organizationsId},
            {"OrganizationsLangsId",Langs.Id },
            {"NewsLangsStatusId",int.Parse(DListNewsStatus.SelectedValue) }
        };

        int PageNum;
        int RowNumber = 50;

        if (!int.TryParse(ConfigL._Route("pagenum", "1"), out PageNum))
        {
            PageNum = 1;
        }       

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult NewsResult = DALC.GetFilterList(Tools.Tables.V_News, FilterDictionary, PageNum, RowNumber);

        if (NewsResult.Count == -1)
        {
            return;
        }

        LblCount.Text = string.Format("Axtarış üzrə nəticə: {0}", NewsResult.Count.ToString());

        int Total_Count = NewsResult.Count % RowNumber > 0 ? (NewsResult.Count / RowNumber) + 1 : NewsResult.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = NewsResult.Count > RowNumber;

        RptNews.DataSource = NewsResult.Dt;
        RptNews.DataBind();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Xəbərlər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "XƏBƏRLƏR";
            BindDList();
            BindNews();
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlFilter.ControlsBind(FilterDictionary, TableName, "", true);
        ConfigL.RedirectURL(string.Format("/{0}/modules/news/1", Langs.Name));
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        PnlFilter.ControlsClear();
        BtnFilter_Click(null, null);
    }

    protected void RptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal LtrStart = (Literal)e.Item.FindControl("LtrStart");
            Literal LtrEnd = (Literal)e.Item.FindControl("LtrEnd");

            LtrStart.Visible = e.Item.ItemIndex % 2 == 0;
            LtrEnd.Visible = (e.Item.ItemIndex - 1) % 2 == 0;
        }
    }
}