using YouthHousesLibrary;
using System;
using System.Web.UI;
using System.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Router ilə gəlmədikdə main ataq.
        if (string.IsNullOrEmpty(Langs.Name))
        {
            ConfigL.RedirectURL("/az/modules");
            return;
        }

        pnlMain.Style.Clear();

        if (!IsPostBack)
        {
            ltrOperatorName.Text = DALCL._Login.fullname;

            DataTable Dt = DALCL.GetAdministratorsMenu(0);
            DataTable DtSub = new DataTable();

            RptLangs.DataSource = DALCL.GetAllLangs();
            RptLangs.DataBind();

            foreach (DataRow Dr in Dt.Rows)
            {
                DtSub = DALCL.GetAdministratorsMenu(Dr["ID"]._ToInt16());

                if (DtSub.Rows.Count < 1)
                {
                    LtrLeftMenu.Text += string.Format(
                                     @"<li onclick=""location.href='/{0}/modules/{1}'; return false;"">
                                     <a href=""/{0}/modules/{1}""> 
                                         <img src=""/images/leftmenu/{2}.png""/>{3}
                                     </a>
                                 </li>", Langs.Name, Dr["PageName"], Dr["ID"], Dr["Name"]);
                }
                else
                {
                    LtrLeftMenu.Text += string.Format(@"<li class=""has-dropdown"">
                                                        <img src=""/images/leftmenu/{0}.png""/>{1}
                                                    </li>", Dr["ID"], Dr["Name"]);


                    LtrLeftMenu.Text += @"<ul class=""dropdown"">";
                    foreach (DataRow DrSub in DtSub.Rows)
                    {
                        LtrLeftMenu.Text += string.Format(@"<li><a href=""/{0}/modules/{1}"">{2}</a></li>", Langs.Name, DrSub["PageName"], DrSub["Name"]);
                    }
                    LtrLeftMenu.Text += "</ul>";
                }
            }
        }
    }
}
