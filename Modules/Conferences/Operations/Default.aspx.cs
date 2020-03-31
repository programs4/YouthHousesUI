using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Conferences_Operations_Default : System.Web.UI.Page
{
    int _serviceId;
    string _operationsType;

    private void BindServicesDetails()
    {
        DataTable dt = new DataTable();
        dt = DALCL.GetServicesById(_serviceId);

        if (dt == null)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        if (dt.Rows.Count < 1)
        {
            ConfigL.RedirectURL($"/{Langs.Name}/modules/conferences");
            return;
        }

        txtTitle.Text = dt._Rows("Title");
        txtNameLangs.Text = dt._Rows("Name");
        txtDescriptionLangs.Text = dt._Rows("Description");
        dListStatus.SelectedValue = dt._RowsBoolean("IsActive") ? "1" : "0";
    }

    private void BindGrdServicesVotesTypesRelations()
    {
        grdServicesVotesTypes.DataSource = DALCL.GetServicesVotesTypesRelations(_serviceId);
        grdServicesVotesTypes.DataBind();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Tədbirlər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _serviceId = ConfigL._Route("id", "-1")._ToInt32();
        _operationsType = ConfigL._Route("type", "add");

        if (_operationsType == "edit")
        {
            txtTitle.Enabled = false;
            pnlServicesVotesTypes.Visible = true;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "TƏDBİRLƏR";
            BindGrdServicesVotesTypesRelations();

            if (_operationsType == "add")
            {

            }
            else
            {
                BindServicesDetails();
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

        if (string.IsNullOrEmpty(txtNameLangs.Text))
        {
            ConfigL.BorderColor(txtNameLangs);
            return;
        }

        if (dListStatus.SelectedValue == "-1")
        {
            ConfigL.BorderColor(dListStatus);
            return;
        }

        DALC.Transaction transaction = new DALC.Transaction();
        int check;
        var dictionaryServices = new Dictionary<string, object>()
        {
            {"ExpireDate", DateTime.Now.AddYears(1)},
            {"IsActive",int.Parse(dListStatus.SelectedValue)}
        };

        DataTable dtServicesLangs = new DataTable();
        dtServicesLangs.Columns.Add("ServicesId", typeof(int));
        dtServicesLangs.Columns.Add("LangsId", typeof(int));
        dtServicesLangs.Columns.Add("Name", typeof(string));
        dtServicesLangs.Columns.Add("Description", typeof(string));
        dtServicesLangs.Columns.Add("CreatedDate", typeof(DateTime));

        if (_operationsType == "add")
        {
            dictionaryServices.Add("MinMinute", 0);
            dictionaryServices.Add("DailyUse", 255);
            dictionaryServices.Add("UseBetweenHour", 0);
            dictionaryServices.Add("ServicesTypesId", (int)Tools.ServicesTypes.Tədbirlər);
            dictionaryServices.Add("Title", txtTitle.Text);

            int resultId = DALC.InsertDatabase(Tools.Tables.Services, dictionaryServices, transaction);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            _serviceId = resultId;

            DataRow dr;
            foreach (Tools.Langs langs in (Tools.Langs[])Enum.GetValues(typeof(Tools.Langs)))
            {
                dr = dtServicesLangs.NewRow();
                dr["ServicesId"] = _serviceId;
                dr["LangsId"] = (int)langs;
                dr["Name"] = txtNameLangs.Text;
                dr["Description"] = txtDescriptionLangs.Text;
                dr["CreatedDate"] = DateTime.Now;
                dtServicesLangs.Rows.Add(dr);
            }

            resultId = DALC.InsertBulk(Tools.Tables.ServicesLangs, dtServicesLangs, transaction, true);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            var dictionaryServicesOrganizations = new Dictionary<string, object>()
            {
                {"Barcode",Guid.NewGuid() },
                {"OrganizationsId",DALCL._Login.organizationsId },
                {"ServicesId",_serviceId},
                {"IsActive",true },
                {"CreatedDate",DateTime.Now },
            };

            resultId = DALC.InsertDatabase(Tools.Tables.ServicesOrganizations, dictionaryServicesOrganizations);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }
        else if (_operationsType == "edit")
        {
            dictionaryServices.Add("WhereId", _serviceId);

            check = DALC.UpdateDatabase(Tools.Tables.Services, dictionaryServices, transaction);
            if (check < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            var dictionaryServicesLangs = new Dictionary<string, object>()
            {
                {"Name", txtNameLangs.Text},
                {"Description", txtDescriptionLangs.Text},
                {"WhereServicesId", _serviceId},
                {"WhereLangsId", Langs.Id},
            };

            check = DALC.UpdateDatabase(Tools.Tables.ServicesLangs, dictionaryServicesLangs, transaction, true);
            if (check < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        ConfigL.MsgBoxAjax(Config._AlertMessages.Success, $"/{Langs.Name}/modules/conferences/operations/edit/{_serviceId}");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/conferences");
    }

    protected void chkServicesVotesTypes_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox check = sender as CheckBox;
        string isActive = check.Attributes["data-isactive"];
        int servicesVotesTypesId = int.Parse(check.Attributes["data-id"]);

        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        int result = 0;
        if (string.IsNullOrEmpty(isActive))
        {
            dictionary.Add("ServicesId", _serviceId);
            dictionary.Add("ServicesVotesTypesId", servicesVotesTypesId);
            dictionary.Add("IsActive", true);

            result = DALC.InsertDatabase(Tools.Tables.ServicesVotesTypesRelations, dictionary);
        }
        else
        {
            dictionary.Add("IsActive", check.Checked);
            dictionary.Add("WhereId", int.Parse(check.Attributes["data-servicesvotestypesrelationsid"]));
            result = DALC.UpdateDatabase(Tools.Tables.ServicesVotesTypesRelations, dictionary);
        }

        if (result < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        BindGrdServicesVotesTypesRelations();
    }
}