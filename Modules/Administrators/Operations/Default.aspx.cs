using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Administrators_Operations_Default : System.Web.UI.Page
{
    string _allowTypes = "-gif-jpg-jpeg-bmp-png-";
    int _administratorsId = 0;
    string _operationsType = "";

    private void BindList()
    {
        dListOrganizations.DataSource = DALCL.GetActiveOrganizations();
        dListOrganizations.DataBind();
        dListOrganizations.Items.Insert(0, new ListItem("--", "-1"));

        dlistCalendar.DataSource = DALC.GetDataTableBySqlCommand("Select Id,Name from CalendarOganizations Where IsActive=1 and OrganizationsId=@OrganizationsId Order by Name asc", "OrganizationsId", new object[] { DALCL._Login.organizationsId });
        dlistCalendar.DataBind();
        dlistCalendar.Items.Insert(0, new ListItem("--", "-1"));

        dListOrganizations.SelectedValue = DALCL._Login.administratorsId.ToString();
        dListOrganizations.Enabled = false;

        dListAdministratorsStatusId.DataSource = DALCL.GetAdministratorsStatus();
        dListAdministratorsStatusId.DataBind();
        dListAdministratorsStatusId.Items.Insert(0, new ListItem("--", "-1"));

        grdAdministratorsMenu.DataSource = DALCL.GetAdministratorsMenu(0);
        grdAdministratorsMenu.DataBind();
    }

    private void BindAdministrators()
    {
        DataTable dt = DALCL.GetAdministratorsById(_administratorsId);

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

        dlistCalendar.SelectedValue = dt._Rows("CalendarOganizationsId");
        dListOrganizations.SelectedValue = dt._Rows("OrganizationsId");
        txtUsername.Text = dt._Rows("Username");
        txtFullName.Text = dt._Rows("Fullname");
        txtPosition.Text = dt._Rows("Position");
        txtPhone.Text = dt._Rows("Phone");
        txtEmail.Text = dt._Rows("Email");

        dListAdministratorsStatusId.SelectedValue = dt._Rows("AdministratorsStatusId");

        string list = $",{dt._Rows("AdministratorsMenuList")},";

        for (int i = 0; i < grdAdministratorsMenu.Rows.Count; i++)
        {
            ((CheckBox)grdAdministratorsMenu.Rows[i].Cells[1].Controls[1]).Checked = (list == ",*," || (list.IndexOf($",{grdAdministratorsMenu.DataKeys[i]["Id"]._ToString()},") > -1));
        }

        imgUser.ImageUrl = $"/uploads/administrators/{dt._Rows("Id")}.jpg";

        //Özü olanda permissionlarını dəyişə bilməsin.
        if (_administratorsId == DALCL._Login.administratorsId)
        {
            grdAdministratorsMenu.Enabled = false;
        }
    }

    private string CheckList()
    {
        string list = "";
        for (int i = 0; i < grdAdministratorsMenu.Rows.Count; i++)
        {
            CheckBox check = ((CheckBox)grdAdministratorsMenu.Rows[i].Cells[1].Controls[1]);
            if (check.Checked)
            {
                list += $"{check.Attributes["data-id"]},";
            }
        }

        return list.Trim(',');
    }

    private void BorderColor(Control control)
    {
        if (control is TextBox)
        {
            TextBox textBox = control as TextBox;
            textBox.BorderColor = System.Drawing.Color.Red;
        }
        else if (control is DropDownList)
        {
            DropDownList dropDown = control as DropDownList;
            dropDown.BorderColor = System.Drawing.Color.Red;
        }
    }

    private void ClearBorderColor(Control control)
    {
        foreach (Control item in control.Controls)
        {
            if (item.HasControls())
            {
                ClearBorderColor(item);
            }

            if (item is TextBox)
            {
                TextBox Txt = item as TextBox;
                Txt.BorderColor = System.Drawing.Color.Empty;
            }
            else if (item is DropDownList)
            {
                DropDownList dropDown = item as DropDownList;
                dropDown.BorderColor = System.Drawing.Color.Empty;
            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.İdarəçilər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _administratorsId = ConfigL._Route("id", "-1")._ToInt32();
        _operationsType = ConfigL._Route("type", "add");

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "QEYDİYYAT İDARƏÇİLƏR";
            BindList();

            if (_operationsType != "add")
            {
                BindAdministrators();
                txtUsername.Enabled = false;
            }
        }
    }

    protected void chkAdministratorsMenu_CheckedChanged(object sender, EventArgs e)
    {
        if (_operationsType == "edit")
        {
            string list = CheckList();

            var dictionary = new Dictionary<string, object>()
            {
                {"AdministratorsMenuList",list.Trim(',') },
                {"WhereId",_administratorsId }
            };

            int result = DALC.UpdateDatabase(Tools.Tables.Administrators, dictionary);

            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ClearBorderColor(pnlControls);

        if (dListOrganizations.SelectedValue == "-1")
        {
            BorderColor(dListOrganizations);
            return;
        }

        if (string.IsNullOrEmpty(txtFullName.Text))
        {
            BorderColor(txtFullName);
            return;
        }

        //if (string.IsNullOrEmpty(txtPosition.Text))
        //{
        //    BorderColor(txtPosition);
        //    return;
        //}

        if (_operationsType == "add")
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                BorderColor(txtUsername);
                return;
            }

            if (txtUsername.Text.Length < 4)
            {
                BorderColor(txtUsername);
                ConfigL.MsgBoxAjax("İstifadəçi adı 4 simvoldan kiçik olmamalıdır!");
                return;
            }

            Regex rgx = new Regex(@"^[a-zA-Z0-9áéíóú@#%&',.\s-]+$");
            if (!rgx.IsMatch(txtUsername.Text))
            {
                BorderColor(txtUsername);
                ConfigL.MsgBoxAjax("İstifadəçi adında uyğunsuzluq var!");
                return;
            }

            if (!DALCL.CheckAdministratorsUserName(txtUsername.Text))
            {
                BorderColor(txtUsername);
                ConfigL.MsgBoxAjax("İstifadəçi adı bazada mövcuddur!");
                return;
            }

            if (string.IsNullOrEmpty(txtPasswordUp.Text))
            {
                BorderColor(txtPasswordUp);
                return;
            }
        }

        if (txtPasswordUp.Text.Length > 0)
        {
            if (txtPasswordUp.Text.Length < 4)
            {
                BorderColor(txtPasswordUp);
                ConfigL.MsgBoxAjax("Şifrə 4 simvoldan kiçik olmamalıdır!");
                return;
            }

            if (txtPasswordUp.Text != txtPasswordUpRepeat.Text)
            {
                BorderColor(txtPasswordUp);
                BorderColor(txtPasswordUpRepeat);
                ConfigL.MsgBoxAjax("Şifrələr uyğun gəlmir!");
                return;
            }
        }

        if (string.IsNullOrEmpty(txtPhone.Text))
        {
            BorderColor(txtPhone);
            return;
        }

        if (!txtEmail.Text.IsEmail())
        {
            BorderColor(txtEmail);
            return;
        }

        if (dListAdministratorsStatusId.SelectedValue == "-1")
        {
            BorderColor(dListAdministratorsStatusId);
            return;
        }

        string list = CheckList();

        if (string.IsNullOrEmpty(list))
        {
            ConfigL.MsgBoxAjax("İcazə təyin edin.");
            return;
        }

        var dictionary = new Dictionary<string, object>()
        {
            {"OrganizationsId",int.Parse(dListOrganizations.SelectedValue)},
            {"CalendarOganizationsId",int.Parse(dlistCalendar.SelectedValue)},
            {"AdministratorsStatusId",int.Parse(dListAdministratorsStatusId.SelectedValue)},
            {"AdministratorsMenuList",list},
            {"Fullname", txtFullName.Text },
            {"Position",txtPosition.Text },
            {"Phone",txtPhone.Text },
            {"Email",txtEmail.Text }
        };

        if (txtPasswordUp.Text.Length > 0)
        {
            dictionary.Add("Password", txtPasswordUp.Text.SHA1Special());
        }

        if (_operationsType == "add")
        {
            dictionary.Add("Username", txtUsername.Text);
            dictionary.Add("CreatedDate", DateTime.Now);

            int result = DALC.InsertDatabase(Tools.Tables.Administrators, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
            _administratorsId = result;
        }
        else
        {
            dictionary.Add("WhereId", _administratorsId);

            int result = DALC.UpdateDatabase(Tools.Tables.Administrators, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        if (flUpImg.HasFile)
        {
            string FileType = System.IO.Path.GetExtension(flUpImg.PostedFile.FileName).Trim('.');
            if (_allowTypes.IndexOf("-" + FileType.ToLower() + "-") > -1)
            {
                if (!flUpImg.PostedFile.CheckFileContentLength(50))
                {
                    ConfigL.MsgBoxAjax("Faylın həcmi 50mb-dan çox olmamalıdır!");
                    return;
                }
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(flUpImg.PostedFile.InputStream);
                ImageResize.ImgResize($"/uploads/users/{_administratorsId}.jpg", bmp.Width, bmp.Height, flUpImg.PostedFile.InputStream, 95L);
            }
        }

        ConfigL.MsgBoxAjax(Config._AlertMessages.Success, $"/{Langs.Name}/modules/administrators");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/administrators");
    }


}
