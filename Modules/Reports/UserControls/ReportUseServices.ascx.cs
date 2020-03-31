using System;
using System.Data;
using System.Web.UI.WebControls;
using YouthHousesLibrary;


public partial class Modules_Reports_UserControls_ReportUseServices : System.Web.UI.UserControl
{
    private void BindReports()
    {
        DateTime fromDate;
        DateTime toDate;

        DropDownList dListOrganizations = (DropDownList)this.Parent.FindControl("dListOrganizations");
        TextBox TxtStartDt = (TextBox)this.Parent.FindControl("TxtStartDt");
        TextBox TxtEndDt = (TextBox)this.Parent.FindControl("TxtEndDt");


        if (!Config.DateFormat(TxtStartDt.Text, out fromDate))
        {
            fromDate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1));
        }

        if (!Config.DateFormat(TxtEndDt.Text, out toDate))
        {
            toDate = DateTime.Now;
        }

        string date1 = ((DateTime)fromDate).ToString("yyyyMMdd");
        string date2 = ((DateTime)toDate).ToString("yyyyMMdd");

        TxtStartDt.Text = ((DateTime)fromDate).ToString("dd.MM.yyyy");
        TxtEndDt.Text = ((DateTime)toDate).ToString("dd.MM.yyyy");

        DataTable dtReports = new DataTable();
        string servicesSessionsStatus = $"{(int)Tools.ServicesSessionsStatus.Qiymətləndirilən},{(int)Tools.ServicesSessionsStatus.Qiymətləndirilməyən}";
        dtReports = DALC.GetDataTableBySqlCommand("ReportUseServices",
                                                   "LangsId,OrganizationsID,ServicesSessionsStatusId,Date1,Date2",
                                                   new object[] { (int)Tools.Langs.AZ, dListOrganizations.SelectedValue, servicesSessionsStatus, date1, date2 }, CommandType.StoredProcedure);

        grdReports.DataSource = dtReports;
        grdReports.DataBind();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        BindReports();
    }
}