using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_CalendarManagment_Default : System.Web.UI.Page
{
    int _calendarId = 0;
    string _operationsType = "";

    private void BindList()
    {
        dListCalendarTypes.DataSource = DALCL.GetCalendarTypes();
        dListCalendarTypes.DataBind();
        dListCalendarTypes.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindCalendar()
    {
        DataTable dt = DALCL.GetCalendarById(_calendarId);

        if (dt == null || dt.Rows.Count < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        if (DALCL._Login.calendarOganizationsId != dt._RowsInt("CalendarOganizationsId"))
        {
            Response.Redirect("/az/modules/calendar");
            return;
        }

        dListCalendarTypes.SelectedValue = dt._Rows("CalendarTypesId");
        txtTitle.Text = dt._Rows("Title");
        txtDescription.Text = dt._Rows("Description");
        txtStartDate.Text = dt._RowsDatetime("StartDate").ToString("dd.MM.yyyy HH:mm");
        txtEndDate.Text = dt._RowsDatetime("EndDate").ToString("dd.MM.yyyy HH:mm");
        dListStatus.SelectedValue = dt._RowsBoolean("IsActive") ? "1" : "0";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Təşkilatlar))
        {
            ConfigL.RedirectError();
            return;
        }

        if (DALCL._Login.calendarOganizationsId < 1)
        {
            MultiView1.ActiveViewIndex = 1;
            return;
        }

        _calendarId = ConfigL._Route("id", "-1")._ToInt32();
        _operationsType = ConfigL._Route("type", "add");

        if (!IsPostBack)
        {
            if (ConfigL._GetQueryString("selectedDate").Length > 0)
            {
                txtStartDate.Text = ConfigL._GetQueryString("selectedDate") + " 09:00";
            }

            ((Literal)Master.FindControl("LtrTitle")).Text = "KALENDAR İDARƏETMƏ";

            BindList();

            if (_operationsType != "add")
            {
                BindCalendar();
            }
            else
            {
                dListStatus.SelectedValue = "1";
                dListStatus.Enabled = false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ConfigL.ClearBorderColor(pnlControls);
        if (dListCalendarTypes.SelectedValue == "-1")
        {
            ConfigL.BorderColor(dListCalendarTypes);
            return;
        }

        if (string.IsNullOrEmpty(txtTitle.Text))
        {
            ConfigL.BorderColor(txtTitle);
            return;
        }

        DateTime? dateTimeStart = ConfigL.DatetimeSplitParse(txtStartDate.Text);
        DateTime? dateTimeEnd = ConfigL.DatetimeSplitParse(txtEndDate.Text);

        if (dateTimeStart == null)
        {
            ConfigL.BorderColor(txtStartDate);
            return;
        }

        if (dateTimeEnd == null)
        {
            ConfigL.BorderColor(txtEndDate);
            return;
        }

        if (dListStatus.SelectedValue == "-1")
        {
            ConfigL.BorderColor(dListStatus);
            return;
        }

        var dictionary = new Dictionary<string, object>()
        {
            {"CalendarTypesId",int.Parse(dListCalendarTypes.SelectedValue) },
            {"Title", txtTitle.Text},
            {"Description", txtDescription.Text},
            {"StartDate", dateTimeStart},
            {"EndDate", dateTimeEnd},
            {"IsActive",int.Parse(dListStatus.SelectedValue) },
        };

        if (_operationsType == "add")
        {
            dictionary.Add("AdministratorsID", DALCL._Login.administratorsId);
            dictionary.Add("CalendarOganizationsId", DALCL._Login.calendarOganizationsId);
            dictionary.Add("CreatedDate", DateTime.Now);
            dictionary.Add("CreatedIP", Request.UserHostAddress.IPToInteger());

            int result = DALC.InsertDatabase(Tools.Tables.Calendar, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
            _calendarId = result;
        }
        else
        {
            dictionary.Add("WhereId", _calendarId);

            int result = DALC.UpdateDatabase(Tools.Tables.Calendar, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        ConfigL.MsgBoxAjax(Config._AlertMessages.Success, $"/{Langs.Name}/modules/calendar");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/calendar");
    }

}