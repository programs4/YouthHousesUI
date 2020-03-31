using YouthHousesLibrary;
using System;
using System.Data;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Content domain ilə açılmasın admin panel
        if (Request.Url.ToString().ToLower().IndexOf("http://contents.youthhouses.az/") > -1)
        {
            Response.Write("Content error...");
            Response.End();
            return;
        }

        Session["Login"] = null;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLogin.Text))
        {
            ConfigL.MsgBoxAjax("İstifadəçi adınızı daxil edin!");
            return;
        }

        if (string.IsNullOrEmpty(txtPassword.Text))
        {
            ConfigL.MsgBoxAjax("İstifadəçi şifrənizi daxil edin!");
            return;
        }

        DataTable dtLoginControl = DALC.GetDataTable("*", Tools.Tables.V_Administrators, "Username,Password,AdministratorsStatusId", new object[] { txtLogin.Text, txtPassword.Text.SHA1Special(), (int)Tools.AdministratorsStatus.Aktiv });

        if (dtLoginControl == null)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        if (dtLoginControl.Rows.Count < 1)
        {
            ConfigL.MsgBoxAjax("Giriş baş tutmadı!");
            return;
        }

        DALCL.Login loginInfo = new DALCL.Login();
        loginInfo.organizationsId = dtLoginControl._RowsInt("OrganizationsId");
        loginInfo.organizationsName = dtLoginControl._Rows("OrganizationsName");
        loginInfo.calendarOganizationsId = dtLoginControl._RowsInt("CalendarOganizationsId");
        loginInfo.administratorsId = dtLoginControl._RowsInt("ID");
        loginInfo.fullname = dtLoginControl._Rows("Fullname");
        loginInfo.permissions = dtLoginControl._Rows("AdministratorsMenuList");

        Session["Login"] = loginInfo;

        if (DALCL.CheckPermission(Tools.AdministratorsMenu.Kalendar))
        {
            ConfigL.RedirectURL("/az/modules/calendar");
        }
        else
        {
            ConfigL.RedirectURL("/az/modules/");
        }
    }
}