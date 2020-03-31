using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Services_Operations_Default : System.Web.UI.Page
{
    int _serviceId;
    string _operationsType;
    int _servicesTypesId;

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
            ConfigL.RedirectURL($"/{Langs.Name}/modules/services");
            return;
        }

        txtTitle.Text = dt._Rows("Title");
        txtNameLangs.Text = dt._Rows("Name");
        txtDescriptionLangs.Text = dt._Rows("Description");
        txtMinMinute.Text = dt._Rows("MinMinute");
        txtDailyUse.Text = dt._Rows("DailyUse");
        txtUseBetweenHour.Text = dt._Rows("UseBetweenHour");
        txtExpireDate.Text = dt._RowsDatetime("ExpireDate").ToString("dd.MM.yyyy");
        dListStatus.SelectedValue = dt._RowsBoolean("IsActive") ? "1" : "0";
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Yeni_Xidmətlər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _serviceId = ConfigL._Route("id", "-1")._ToInt32();
        _operationsType = ConfigL._Route("type", "add");
        int.TryParse(ConfigL._Route("servicestypesid"), out _servicesTypesId);

        if (_operationsType == "edit")
        {
            txtTitle.Enabled = false;
        }

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "XİDMƏTLƏR";

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

        if (string.IsNullOrEmpty(txtMinMinute.Text))
        {
            ConfigL.BorderColor(txtMinMinute);
            return;
        }

        if (string.IsNullOrEmpty(txtDailyUse.Text))
        {
            ConfigL.BorderColor(txtDailyUse);
            return;
        }

        if (string.IsNullOrEmpty(txtUseBetweenHour.Text))
        {
            ConfigL.BorderColor(txtUseBetweenHour);
            return;
        }

        DateTime dateTime;

        if (string.IsNullOrEmpty(txtExpireDate.Text) || !Config.DateFormat(txtExpireDate.Text, out dateTime))
        {
            ConfigL.BorderColor(txtExpireDate);
            return;
        }

        if (dListStatus.SelectedValue == "-1")
        {
            ConfigL.BorderColor(dListStatus);
            return;
        }

        if (!Enum.IsDefined(typeof(Tools.ServicesTypes), _servicesTypesId))
        {
            ConfigL.MsgBoxAjax("Xidmət növü düzgün deyil!");
            return;
        }

        DALC.Transaction transaction = new DALC.Transaction();
        int check;
        var dictionaryServices = new Dictionary<string, object>()
        {
            {"MinMinute", txtMinMinute.Text},
            {"DailyUse", txtDailyUse.Text},
            {"UseBetweenHour", txtUseBetweenHour.Text},
            {"ExpireDate", dateTime},
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
            dictionaryServices.Add("ServicesTypesId", _servicesTypesId);
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

        ConfigL.MsgBoxAjax(Config._AlertMessages.Success, $"/{Langs.Name}/modules/services/{_servicesTypesId}");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/services/{_servicesTypesId}");
    }

}