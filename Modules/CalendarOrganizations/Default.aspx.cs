using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_CalendarOrganizations_Default : System.Web.UI.Page
{
    private void BindCalendar()
    {
        grdCalendarOrganizations.DataSource = DALCL.GetCalendarOrganizations();
        grdCalendarOrganizations.DataBind();
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

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "TƏŞKİLATLAR";
            BindCalendar();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ConfigL.ClearBorderColor(pnlControls);
        int calendarOganizationsId = int.Parse(btnSave.CommandArgument);
        if (string.IsNullOrEmpty(txtName.Text))
        {
            ConfigL.BorderColor(txtName);
            return;
        }

        if (!DALCL.CheckCalendarOrganizationsName(calendarOganizationsId, txtName.Text))
        {
            ConfigL.BorderColor(txtName);
            ConfigL.MsgBoxAjax("Təşkilatın adı bazada mövcuddur!");
            return;
        }

        if (string.IsNullOrEmpty(txtShortName.Text))
        {
            ConfigL.BorderColor(txtShortName);
            return;
        }

        if (txtShortName.Text.Length > 7)
        {
            ConfigL.BorderColor(txtShortName);
            ConfigL.MsgBoxAjax("Təşkilatın qısa adı maksimum 7 simvoldan ibarət ola bilər!");
            return;
        }

        if (!DALCL.CheckCalendarOrganizationsShortName(calendarOganizationsId, txtShortName.Text))
        {
            ConfigL.BorderColor(txtShortName);
            ConfigL.MsgBoxAjax("Təşkilatın qısa adı bazada mövcuddur!");
            return;
        }

        if (dListStatus.SelectedValue == "-1")
        {
            ConfigL.BorderColor(dListStatus);
            return;
        }

        var dictionary = new Dictionary<string, object>()
        {
            {"Name",txtName.Text },
            {"Shortname",txtShortName.Text },
            {"IsActive",int.Parse(dListStatus.SelectedValue) },
            {"IsPublic",int.Parse(DlistPublic.SelectedValue) },
        };

        if (btnSave.CommandArgument == "0")
        {
            dictionary.Add("OrganizationsId", DALCL._Login.organizationsId);
            dictionary.Add("CreatedDate", DateTime.Now);

            int result = DALC.InsertDatabase(Tools.Tables.CalendarOganizations, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }
        else
        {
            dictionary.Add("WhereId", calendarOganizationsId);

            int result = DALC.UpdateDatabase(Tools.Tables.CalendarOganizations, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        btnSave.Text = "ƏLAVƏ ET";
        txtName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        dListStatus.SelectedValue = "-1";
        DlistPublic.SelectedValue = "-1";

        btnSave.CommandArgument = "0";
        BindCalendar();
    }

    protected void lnlkEdit_Click(object sender, EventArgs e)
    {
        btnSave.Text = "DÜZƏLT";
        btnSave.CommandArgument = (sender as LinkButton).CommandArgument;

        DataTable dtDetail = DALCL.GetCalendarOrganizationsById(int.Parse(btnSave.CommandArgument));

        if (dtDetail == null || dtDetail.Rows.Count < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        txtName.Text = dtDetail._Rows("Name");
        txtShortName.Text = dtDetail._Rows("Shortname");
        dListStatus.SelectedValue = dtDetail._RowsBoolean("IsActive") ? "1" : "0";
        DlistPublic.SelectedValue = dtDetail._RowsBoolean("IsPublic") ? "1" : "0";
    }
}