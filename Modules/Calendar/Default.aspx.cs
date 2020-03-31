using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Calendar_Default : System.Web.UI.Page
{
    private string[] MonthList = { "Yanvar", "Fevral", "Mart", "Aprel", "May", "İyun", "İyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
    private string[] MonthListShortname = { "Yan.", "Fev.", "Mar.", "Apr.", "May.", "İyn.", "İyl.", "Avq.", "Sen.", "Okt.", "Noy.", "Dek." };
    private void ICalendarDataBind(int selectedMonth, int selectedYear)
    {
        DateTime selectedDate = new DateTime(selectedYear, selectedMonth, 1);

        DlistMonth.SelectedValue = selectedMonth.ToString();
        DlistYear.SelectedValue = selectedYear.ToString();

        LnkBtnPrev.Text = MonthListShortname[selectedDate.AddMonths(-1).Month - 1] + " " + selectedDate.AddMonths(-1).Year;
        LnkBtnNext.Text = MonthListShortname[selectedDate.AddMonths(1).Month - 1] + " " + selectedDate.AddMonths(1).Year;

        int weekDay = (int)selectedDate.DayOfWeek;
        if (weekDay == 0)
            weekDay = 7;

        selectedDate = selectedDate.AddDays((weekDay - 1) * -1);

        DataTable dtEvent = DALC.GetDataTableBySqlCommand("Select Id,OrgName,OrgShortName,Title,StartDate,CONVERT(varchar, StartDate, 104) as StartDateFilter From V_Calendar Where IsActive=1 and IsPublic=1 and (CalendarTypesId=@CalendarTypesId or @CalendarTypesId=0) and(CalendarOganizationsId=@CalendarOganizationsId or @CalendarOganizationsId=0) Order by StartDate asc", "CalendarTypesId,CalendarOganizationsId", new object[] { DlistTypes.SelectedValue._ToInt32(), DlistOrganizations.SelectedValue._ToInt32() });

        LtrCalenderContent.Text = "<tr>";

        for (int i = 0; i < 50; i++)
        {

            string className = "";

            if (selectedDate.DayOfWeek == DayOfWeek.Sunday || selectedDate.DayOfWeek == DayOfWeek.Saturday)
            {
                className = "weekend";
            }

            if (selectedDate.Month != selectedMonth)
            {
                className += " old";
            }

            if (selectedDate.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"))
            {
                className = "today";
            }

            string addEvent = $"ondblclick=\"location.href='/az/modules/calendarmanagment/add/0/?selectedDate={selectedDate.ToString("dd.MM.yyyy")}'\"";

            if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Kalendar_idarəetmə))
                addEvent = "";

            LtrCalenderContent.Text += $"<td class=\"{className}\" {addEvent}>";
            LtrCalenderContent.Text += $"<span>{ selectedDate.Day.ToString()}</span>";

            if (dtEvent != null)
            {
                DataRow[] dataRowsEvents = dtEvent.Select($"StartDateFilter='{selectedDate.ToString("dd.MM.yyyy")}'");

                if (dataRowsEvents.Length > 0)
                {
                    LtrCalenderContent.Text += "<ul class=\"agenda\">";
                    foreach (DataRow item in dataRowsEvents)
                    {
                        LtrCalenderContent.Text += $"<li onClick=\"document.getElementById('HiddenDataId').value={item["Id"]};document.getElementById('BtnDetail').click();\"><span>{ item["StartDate"]._ToDateTime().ToString("HH:mm")}:</span> { item["Title"]} ({item["OrgShortName"]})</li>";
                    }
                    LtrCalenderContent.Text += "</ul>";
                }
            }

            LtrCalenderContent.Text += "</td>";

            //Həftə bitti
            if (selectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                LtrCalenderContent.Text += "</tr><tr>";
            }

            selectedDate = selectedDate.AddDays(1);

            if (selectedDate.Month != selectedMonth && selectedDate.DayOfWeek == DayOfWeek.Monday)
            {
                break;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Kalendar))
        {
            ConfigL.RedirectError();
            return;
        }

        ((Literal)Master.FindControl("LtrTitle")).Text = "KALENDAR";

        if (!IsPostBack)
        {
            DlistOrganizations.DataSource = DALC.GetDataTable("Id,Name", Tools.Tables.CalendarOganizations, "Where IsActive=1 Order by Name asc");
            DlistOrganizations.DataBind();
            DlistOrganizations.Items.Insert(0, new ListItem("--", "0"));

            DlistTypes.DataSource = DALC.GetDataTable("Id,Name", Tools.Tables.CalendarTypes, "Where IsActive=1 Order by Priority asc");
            DlistTypes.DataBind();
            DlistTypes.Items.Insert(0, new ListItem("--", "0"));

            for (int i = 0; i < MonthList.Length; i++)
            {
                DlistMonth.Items.Add(new ListItem(MonthList[i], (i + 1).ToString()));
            }

            for (int i = 2015; i < DateTime.Now.Year + 5; i++)
            {
                DlistYear.Items.Add(i.ToString());
            }

            LnkToday.Text = $"Bugün ({DateTime.Now.ToString("dd.MM.yyyy")})";

            ICalendarDataBind(DateTime.Now.Month, DateTime.Now.Year);
        }
    }

    protected void LnkBtnNext_Click(object sender, EventArgs e)
    {
        DateTime currentDate = new DateTime(DlistYear.SelectedValue._ToInt16(), DlistMonth.SelectedValue._ToInt16(), 1).AddMonths(1);
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void LnkBtnPrev_Click(object sender, EventArgs e)
    {
        DateTime currentDate = new DateTime(DlistYear.SelectedValue._ToInt16(), DlistMonth.SelectedValue._ToInt16(), 1).AddMonths(-1);
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void DlistMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime currentDate = new DateTime(DlistYear.SelectedValue._ToInt16(), DlistMonth.SelectedValue._ToInt16(), 1);
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void DlistYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime currentDate = new DateTime(DlistYear.SelectedValue._ToInt16(), DlistMonth.SelectedValue._ToInt16(), 1);
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void LnkToday_Click(object sender, EventArgs e)
    {
        DateTime currentDate = DateTime.Now;
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void DlistOrganizations_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime currentDate = new DateTime(DlistYear.SelectedValue._ToInt16(), DlistMonth.SelectedValue._ToInt16(), 1);
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void DlistTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime currentDate = new DateTime(DlistYear.SelectedValue._ToInt16(), DlistMonth.SelectedValue._ToInt16(), 1);
        ICalendarDataBind(currentDate.Month, currentDate.Year);
    }

    protected void BtnDetail_Click(object sender, EventArgs e)
    {
        DataTable dtDetail = DALC.GetDataTable("*", Tools.Tables.V_Calendar, "Id", HiddenDataId.Value, "");

        if (dtDetail == null || dtDetail.Rows.Count < 1)
        {
            return;
        }

        lnkEdit.Visible = false;
        LblOrgname.Text = dtDetail._Rows("OrgName") + " (" + dtDetail._Rows("OrgShortname") + ")";
        LblTitle.Text = dtDetail._Rows("Title");
        LblDescription.Text = dtDetail._Rows("Description");
        LblStartDate.Text = dtDetail._RowsDatetime("StartDate").ToString("dd.MM.yyyy HH:mm");
        LblEndDate.Text = dtDetail._RowsDatetime("EndDate").ToString("dd.MM.yyyy HH:mm");
        LblCreatedDate.Text = dtDetail._RowsDatetime("CreatedDate").ToString("dd.MM.yyyy HH:mm");

        if (dtDetail._RowsInt("CalendarOganizationsId") == DALCL._Login.calendarOganizationsId)
        {
            if (DALCL.CheckPermission(Tools.AdministratorsMenu.Kalendar_idarəetmə))
            {
                lnkEdit.Visible = true;
            }
        }

        PnlModalDialog.Style.Add("padding-top", "10%");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "JqueryAlert", "$('#myModal').modal('show');", true);
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/calendarmanagment/edit/{HiddenDataId.Value}");
    }
}