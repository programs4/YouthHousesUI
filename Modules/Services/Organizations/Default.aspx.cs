using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;


public partial class Modules_Services_Organizations_Default : System.Web.UI.Page
{
    private void BindDList()
    {
        dListServices.DataSource = DALCL.GetServices((int)Tools.Langs.AZ);
        dListServices.DataBind();
        dListServices.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void BindServicesOrganizations()
    {
        DALC.DataTableResult result = new DALC.DataTableResult();
        var dictionary = new Dictionary<string, object>()
        {
            {"OrganizationsLangsId",Langs.Id},
            {"ServicesTypesId",(int)Tools.ServicesTypes.Xidmətlər},
            {"ServicesLangsId",Langs.Id},
            {"IsActive",true},
        };

        result = DALC.GetFilterList(Tools.Tables.V_ServicesOrganizations, dictionary, 1, 500);

        if (result.Count == -1)
        {
            return;
        }

        grdSrvices.DataSource = result.Dt;
        grdSrvices.DataBind();

    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Xidmətlər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDList();
            BindServicesOrganizations();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var dictionaryServicesOrganizations = new Dictionary<string, object>()
        {
            {"Barcode",Guid.NewGuid() },
            {"OrganizationsId",DALCL._Login.organizationsId },
            {"ServicesId",int.Parse(dListServices.SelectedValue) },
            {"IsActive",true },
            {"CreatedDate",DateTime.Now },
        };

        int resultId = DALC.InsertDatabase(Tools.Tables.ServicesOrganizations, dictionaryServicesOrganizations);
        if (resultId < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        BindServicesOrganizations();
        BindDList();
        ConfigL.MsgBoxAjax(Config._AlertMessages.Success);
    }

    protected void lnkGenerateQRCode_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string data = lnk.CommandArgument;

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        Bitmap qrCodeImage = qrCode.GetGraphic(20);

        Response.Clear();
        Response.BufferOutput = false;
        string fileName = $"{data}.jpg";
        Response.ContentType = "application/jpeg";
        Response.AddHeader("content-disposition", $"attachment; filename={fileName}");

        using (MemoryStream stream = new MemoryStream())
        {
            qrCodeImage.Save(Response.OutputStream, ImageFormat.Jpeg);
        }
        Response.End();
    }

    protected void lnkDeleted_Click(object sender, EventArgs e)
    {
        var dictionaryServicesOrganizations = new Dictionary<string, object>()
        {
            {"IsActive",false},
            {"WhereId",int.Parse((sender as LinkButton).CommandArgument)}
        };

        int resultId = DALC.UpdateDatabase(Tools.Tables.ServicesOrganizations, dictionaryServicesOrganizations);
        if (resultId < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        BindServicesOrganizations();
        BindDList();
        ConfigL.MsgBoxAjax(Config._AlertMessages.Success);
    }

}