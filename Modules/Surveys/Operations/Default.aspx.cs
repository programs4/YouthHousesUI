using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Surveys_Operations_Default : System.Web.UI.Page
{
    int _surveysId;
    string _operationsType;

    private void BindDList()
    {
        dListStatus.DataSource = DALCL.GetSurveysStatus();
        dListStatus.DataBind();
        dListStatus.Items.Insert(0, new ListItem("--", "-1"));

        //string list = DALCL.GetSurveysSubscriptions(_surveysId) + ",";
        //hdnSubscriptionsList.Value = list;

        //for (int i = 0; i < grdSurveysSubscriptions.Rows.Count; i++)
        //{
        //    if (list.IndexOf($",{grdSurveysSubscriptions.DataKeys[i]["Id"]._ToString()},") > -1)
        //    {
        //        ((CheckBox)grdSurveysSubscriptions.Rows[i].Cells[1].Controls[1]).Checked = true;
        //    }
        //}
    }

    private void BindGrdSurveysSubscriptions()
    {
        grdSurveysSubscriptions.DataSource = DALCL.GetSurveysSubscriptions(_surveysId);
        grdSurveysSubscriptions.DataBind();
    }

    private void BindSurveysDetails()
    {
        DataTable dt = new DataTable();
        dt = DALCL.GetSurveysById(_surveysId);

        if (dt == null)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        if (dt.Rows.Count < 1)
        {
            ConfigL.RedirectURL($"/{Langs.Name}/modules/registration");
            return;
        }

        txtTitle.Text = dt._Rows("Title");
        txtQuestions.Text = dt._Rows("Question");
        dListStatus.SelectedValue = dt._Rows("SurveysStatusId");
    }

    private void BindSurveysAnswers()
    {
        pnlAnswers.Visible = true;
        DALC.DataTableResult result = new DALC.DataTableResult();
        var dictionary = new Dictionary<string, object>()
        {
            {"SurveysId",_surveysId},
            {"LangsId",Langs.Id},
            {"IsActive",true }
        };

        result = DALC.GetFilterList(Tools.Tables.V_SurveysAnswers, dictionary, 1, 500, "T.*", "", "Order By Id asc");

        if (result.Count == -1)
        {
            return;
        }

        grdSurveysAnswers.DataSource = result.Dt;
        grdSurveysAnswers.DataBind();

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
        _surveysId = ConfigL._Route("id", "-1")._ToInt32();
        _operationsType = ConfigL._Route("type", "add");

        if (_operationsType == "edit")
        {

        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "SUAL CAVABLAR";
            BindDList();
            BindGrdSurveysSubscriptions();

            if (_operationsType == "add")
            {
                pnlAnswers.Visible = pnlSubscriptions.Visible = false;
            }
            else
            {
                txtTitle.Enabled = false;
                pnlAnswers.Visible = pnlSubscriptions.Visible = true;

                BindSurveysDetails();
                BindSurveysAnswers();
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ConfigL.ClearBorderColor(pnlControls);

        if (string.IsNullOrEmpty(txtTitle.Text))
        {
            ConfigL.BorderColor(txtTitle);
            return;
        }

        if (string.IsNullOrEmpty(txtQuestions.Text))
        {
            ConfigL.BorderColor(txtQuestions);
            return;
        }

        if (dListStatus.SelectedValue == "-1")
        {
            ConfigL.BorderColor(dListStatus);
            return;
        }

        if (DALCL.CheckSurveysQuestion(_surveysId, txtQuestions.Text))
        {
            ConfigL.BorderColor(txtQuestions);
            ConfigL.MsgBoxAjax("Bu sual istifadə olunur!");
            return;
        }

        DALC.Transaction transaction = new DALC.Transaction();
        int check;
        var dictionarySurveys = new Dictionary<string, object>()
        {
            {"SurveysStatusId", int.Parse(dListStatus.SelectedValue)}
        };

        DataTable dtSurveysLangs = new DataTable();
        dtSurveysLangs.Columns.Add("SurveysId", typeof(int));
        dtSurveysLangs.Columns.Add("LangsId", typeof(int));
        dtSurveysLangs.Columns.Add("Question", typeof(string));
        dtSurveysLangs.Columns.Add("CreatedDate", typeof(DateTime));
        dtSurveysLangs.Columns.Add("UpdatedDate", typeof(DateTime));

        if (_operationsType == "add")
        {
            dictionarySurveys.Add("OrganizationsId", DALCL._Login.organizationsId);
            dictionarySurveys.Add("Title", txtTitle.Text);
            dictionarySurveys.Add("VotesCount", 0);

            int resultId = DALC.InsertDatabase(Tools.Tables.Surveys, dictionarySurveys, transaction);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            _surveysId = resultId;
            Dictionary<string, object> dictionarySurveysSubscriptions = new Dictionary<string, object>();

            dictionarySurveysSubscriptions.Add("SurveysId", _surveysId);
            dictionarySurveysSubscriptions.Add("OrganizationsId", DALCL._Login.organizationsId);
            dictionarySurveysSubscriptions.Add("CreatedDate", DateTime.Now);
            dictionarySurveysSubscriptions.Add("IsActive", true);

            resultId = DALC.InsertDatabase(Tools.Tables.SurveysSubscriptions, dictionarySurveysSubscriptions);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            DataRow dr;
            foreach (Tools.Langs langs in (Tools.Langs[])Enum.GetValues(typeof(Tools.Langs)))
            {
                dr = dtSurveysLangs.NewRow();
                dr["SurveysId"] = _surveysId;
                dr["LangsId"] = (int)langs;
                dr["Question"] = txtQuestions.Text;
                dr["CreatedDate"] = DateTime.Now;
                dr["UpdatedDate"] = DateTime.Now;
                dtSurveysLangs.Rows.Add(dr);
            }

            resultId = DALC.InsertBulk(Tools.Tables.SurveysLangs, dtSurveysLangs, transaction, true);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
            ConfigL.MsgBoxAjax(Config._AlertMessages.Success, $"/{Langs.Name}/modules/surveys/operations/edit/{_surveysId}");
        }
        else if (_operationsType == "edit")
        {
            dictionarySurveys.Add("WhereId", _surveysId);

            check = DALC.UpdateDatabase(Tools.Tables.Surveys, dictionarySurveys, transaction);
            if (check < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            var dictionarySurveysLangs = new Dictionary<string, object>()
            {
                {"Question", txtQuestions.Text},
                {"UpdatedDate", DateTime.Now},
                {"WhereSurveysId", _surveysId},
                {"WhereLangsId", Langs.Id},
            };

            check = DALC.UpdateDatabase(Tools.Tables.SurveysLangs, dictionarySurveysLangs, transaction, true);
            if (check < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        ConfigL.MsgBoxAjax(Config._AlertMessages.Success);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/surveys");
    }

    protected void btnSaveAnswers_Click(object sender, EventArgs e)
    {
        ConfigL.ClearBorderColor(pnlAnswers);

        if (string.IsNullOrEmpty(txtAnswer.Text))
        {
            ConfigL.BorderColor(txtAnswer);
            return;
        }

        if (DALCL.CheckSurveysAnswers(_surveysId, txtAnswer.Text))
        {
            ConfigL.BorderColor(txtAnswer);
            ConfigL.MsgBoxAjax("Bu cavab istifadə olunur!");
            return;
        }

        var dictionaryAnswers = new Dictionary<string, object>()
        {
            {"Title",txtAnswer.Text},
        };

        int surveysAnswersId;
        int result;
        DALC.Transaction transaction = new DALC.Transaction();
        if (btnSaveAnswers.CommandArgument == "0")
        {
            dictionaryAnswers.Add("SurveysId", _surveysId);
            dictionaryAnswers.Add("VotesCount", 0);
            dictionaryAnswers.Add("IsActive", true);

            result = DALC.InsertDatabase(Tools.Tables.SurveysAnswers, dictionaryAnswers, transaction);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
            surveysAnswersId = result;


            DataTable dtAnswersLangs = new DataTable();
            dtAnswersLangs.Columns.Add("SurveysAnswersId", typeof(int));
            dtAnswersLangs.Columns.Add("LangsId", typeof(int));
            dtAnswersLangs.Columns.Add("Answer", typeof(string));
            dtAnswersLangs.Columns.Add("CreatedDate", typeof(DateTime));

            DataRow dr;
            foreach (Tools.Langs langs in (Tools.Langs[])Enum.GetValues(typeof(Tools.Langs)))
            {
                dr = dtAnswersLangs.NewRow();
                dr["SurveysAnswersId"] = surveysAnswersId;
                dr["LangsId"] = (int)langs;
                dr["Answer"] = txtAnswer.Text;
                dr["CreatedDate"] = DateTime.Now;
                dtAnswersLangs.Rows.Add(dr);
            }

            result = DALC.InsertBulk(Tools.Tables.SurveysAnswersLangs, dtAnswersLangs, transaction, true);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }
        else
        {
            surveysAnswersId = int.Parse(btnSaveAnswers.CommandArgument);

            dictionaryAnswers.Add("WhereId", surveysAnswersId);

            result = DALC.UpdateDatabase(Tools.Tables.SurveysAnswers, dictionaryAnswers, transaction);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            var dictionaryAnswersLangs = new Dictionary<string, object>()
            {
                {"Answer", txtAnswer.Text},
                {"CreatedDate", DateTime.Now},
                {"WhereSurveysAnswersId", surveysAnswersId},
                {"WhereLangsId", Langs.Id},
            };

            result = DALC.UpdateDatabase(Tools.Tables.SurveysAnswersLangs, dictionaryAnswersLangs, transaction, true);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        txtAnswer.Text = "";
        btnSaveAnswers.CommandArgument = "0";
        BindSurveysAnswers();
    }

    protected void lnkEditAnswer_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        btnSaveAnswers.CommandArgument = lnk.CommandArgument;
        txtAnswer.Text = lnk.CommandName;
    }

    protected void lnkDeletedAnswer_Click(object sender, EventArgs e)
    {
        var dictionary = new Dictionary<string, object>()
        {
            {"IsActive",false },
            {"WhereId",(sender as LinkButton).CommandArgument }
        };

        int result = DALC.UpdateDatabase(Tools.Tables.SurveysAnswers, dictionary);
        if (result < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        BindSurveysAnswers();
    }

    protected void chkSubscriptions_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox check = sender as CheckBox;
        string isActive = check.Attributes["data-isactive"];
        int organizationsId = int.Parse(check.Attributes["data-id"]);

        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        int result = 0;
        if (string.IsNullOrEmpty(isActive))
        {
            dictionary.Add("SurveysId", _surveysId);
            dictionary.Add("OrganizationsId", organizationsId);
            dictionary.Add("CreatedDate", DateTime.Now);
            dictionary.Add("IsActive", true);

            result = DALC.InsertDatabase(Tools.Tables.SurveysSubscriptions, dictionary);
        }
        else
        {
            dictionary.Add("IsActive", check.Checked);
            dictionary.Add("WhereId", int.Parse(check.Attributes["data-surveyssubscriptionsid"]));
            result = DALC.UpdateDatabase(Tools.Tables.SurveysSubscriptions, dictionary);
        }

        if (result < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        BindGrdSurveysSubscriptions();
    }
}