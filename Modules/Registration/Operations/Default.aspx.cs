using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Registration_Operations_Default : System.Web.UI.Page
{
    string _allowTypes = "-gif-jpg-jpeg-bmp-png-";
    int _usersId = 0;
    string _operationsType = "";

    private void BindList()
    {
        dListSocialStatus.DataSource = DALCL.GetUsersSocialStatusActive();
        dListSocialStatus.DataBind();
        dListSocialStatus.Items.Insert(0, new ListItem("--", "-1"));

        dListGender.DataSource = DALCL.GetUsersGenders();
        dListGender.DataBind();
        dListGender.Items.Insert(0, new ListItem("--", "-1"));

        dListStatus.DataSource = DALCL.GetUsersStatus();
        dListStatus.DataBind();
        dListStatus.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindUsers()
    {
        DataTable dt = new DataTable();
        dt = DALCL.GetUsersById(_usersId);

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

        dListSocialStatus.SelectedValue = dt._Rows("UsersSocialStatusId");
        txtDocumentNumber.Text = dt._Rows("DocumentNumber");
        txtName.Text = dt._Rows("Name");
        txtSurname.Text = dt._Rows("Surname");
        txtPatronymic.Text = dt._Rows("Patronymic");
        txtUsername.Text = dt._Rows("Username");
        txtBirthDate.Text = dt._RowsDatetime("BirthDate").ToString("dd.MM.yyyy");
        dListGender.SelectedValue = dt._Rows("UsersGendersId");
        txtAddress.Text = dt._Rows("Address");
        txtEmail.Text = dt._Rows("Email");
        txtContact.Text = dt._Rows("Contact");
        txtDescription.Text = dt._Rows("Description");
        dListStatus.SelectedValue = dt._Rows("UsersStatusId");
        imgUser.ImageUrl = $"/uploads/users/{dt._Rows("Id")}_{dt._Rows("DocumentNumber")}.jpg";
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

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Qeydiyyat))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _usersId = ConfigL._Route("id", "-1")._ToInt32();
        _operationsType = ConfigL._Route("type", "add");

        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "QEYDİYYAT";
            BindList();

            if (_operationsType == "add")
            {
                pnlPassword.Visible = true;
            }
            else
            {
                BindUsers();
                pnlPassword.Visible = false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ClearBorderColor(pnlControls);

        if (dListSocialStatus.SelectedValue == "-1")
        {
            BorderColor(dListSocialStatus);
            return;
        }

        if (string.IsNullOrEmpty(txtDocumentNumber.Text))
        {
            BorderColor(txtDocumentNumber);
            return;
        }

        if (!DALCL.CheckDocumentNumber(_usersId, txtDocumentNumber.Text))
        {
            BorderColor(txtDocumentNumber);
            ConfigL.MsgBoxAjax("Sənəd nömrəsi üzrə qeydiyyat mövcuddur.");
            return;
        }

        if (string.IsNullOrEmpty(txtName.Text))
        {
            BorderColor(txtName);
            return;
        }

        if (string.IsNullOrEmpty(txtSurname.Text))
        {
            BorderColor(txtSurname);
            return;
        }

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
                ConfigL.MsgBoxAjax("İstifadəçi adında yalnız latın əlifbasından istifadə olunmalıdır!");
                return;
            }

            if (!DALCL.CheckUsername(txtUsername.Text))
            {
                BorderColor(txtUsername);
                ConfigL.MsgBoxAjax("İstifadəçi adı bazada mövcuddur!");
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                BorderColor(txtPassword);
                return;
            }

            if (txtPassword.Text.Length < 4)
            {
                BorderColor(txtPassword);
                ConfigL.MsgBoxAjax("Şifrə adı 4 simvoldan kiçik olmamalıdır!");
                return;
            }

            if (txtPassword.Text != txtPasswordRepeat.Text)
            {
                BorderColor(txtPassword);
                BorderColor(txtPasswordRepeat);
                ConfigL.MsgBoxAjax("Şifrələr uyğun gəlmir!");
                return;
            }
        }

        DateTime dateTime;
        if (!Config.DateFormat(txtBirthDate.Text, out dateTime))
        {
            BorderColor(txtBirthDate);
            return;
        }

        if (!txtEmail.Text.IsEmail())
        {
            BorderColor(txtEmail);
            return;
        }

        if (dListStatus.SelectedValue == "-1")
        {
            BorderColor(dListStatus);
            return;
        }

        var dictionary = new Dictionary<string, object>()
        {
            {"UsersSocialStatusId",int.Parse(dListSocialStatus.SelectedValue) },
            {"UsersStatusId",int.Parse(dListStatus.SelectedValue) },
            {"DocumentNumber", txtDocumentNumber.Text},
            {"Name", txtName.Text },
            {"Surname", txtSurname.Text },
            {"Patronymic",txtPatronymic.Text },
            {"BirthDate",dateTime },
            {"UsersGendersId",int.Parse(dListGender.SelectedValue) },
            {"Address",txtAddress.Text },
            {"Email", txtEmail.Text },
            {"Contact",txtContact.Text },
            {"Description",txtDescription.Text },
        };

        if (_operationsType == "add")
        {
            dictionary.Add("AdministratorsId", DALCL._Login.administratorsId);
            dictionary.Add("SessionKey", DBNull.Value);
            dictionary.Add("Username", txtUsername.Text);
            dictionary.Add("Password", txtPassword.Text.SHA1Special());
            dictionary.Add("CreatedDate", DateTime.Now);
            dictionary.Add("CreatedIP", Request.UserHostAddress.IPToInteger());

            int result = DALC.InsertDatabase(Tools.Tables.Users, dictionary);
            if (result < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
            _usersId = result;
        }
        else
        {
            dictionary.Add("WhereId", _usersId);

            int result = DALC.UpdateDatabase(Tools.Tables.Users, dictionary);
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
                ImageResize.ImgResize($"/uploads/users/{_usersId}_{txtDocumentNumber.Text}.jpg", bmp.Width, bmp.Height, flUpImg.PostedFile.InputStream, 95L);
            }
        }

        ConfigL.MsgBoxAjax(Config._AlertMessages.Success, $"/{Langs.Name}/modules/registration");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ConfigL.RedirectURL($"/{Langs.Name}/modules/registration");
    }
}