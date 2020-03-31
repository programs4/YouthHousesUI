using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Surveys_Default : System.Web.UI.Page
{
    Tools.Tables tableName = Tools.Tables.V_Surveys;
    Dictionary<string, object> filterDictionary = new Dictionary<string, object>();

    private void BindDList()
    {
        dListSurveysStatus.DataSource = DALCL.GetSurveysStatus();
        dListSurveysStatus.DataBind();
        dListSurveysStatus.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindSurveys()
    {
        grdSurveys.DataSource = null;
        grdSurveys.DataBind();

        pnlFilter.ControlsBind(filterDictionary, tableName);

        int surveysId;
        int.TryParse(txtSurveysId.Text, out surveysId);
        if (surveysId == 0)
        {
            surveysId = -1;
        }

        filterDictionary = new Dictionary<string, object>()
        {
            {"Id",surveysId},
            {"LangsId",Langs.Id},
            {"SurveysStatusId",int.Parse(dListSurveysStatus.SelectedValue)}
        };

        int pageNumber;
        int rowNumber = 50;

        if (!int.TryParse(ConfigL._Route("pagenum", "1"), out pageNumber))
        {
            pageNumber = 1;
        }

        HdnPageNumber.Value = pageNumber.ToString();

        DALC.DataTableResult surveysResult = DALC.GetFilterList(tableName, filterDictionary, pageNumber, rowNumber);

        if (surveysResult.Count == -1)
        {
            return;
        }

        lblCount.Text = string.Format("Axtarış üzrə nəticə: {0}", surveysResult.Count.ToString());

        int totalCount = surveysResult.Count % rowNumber > 0 ? (surveysResult.Count / rowNumber) + 1 : surveysResult.Count / rowNumber;
        hdnTotalCount.Value = totalCount.ToString();

        pnlPager.Visible = surveysResult.Count > rowNumber;

        grdSurveys.DataSource = surveysResult.Dt;
        grdSurveys.DataBind();

    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Sual_cavablar))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "SORĞULAR";
            BindDList();
            BindSurveys();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsBind(filterDictionary, tableName, "", true);
        ConfigL.RedirectURL(string.Format("/{0}/modules/surveys/1", Langs.Name));
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlFilter.ControlsClear();
        btnFilter_Click(null, null);
    }

}