using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_Conferences_Default : System.Web.UI.Page
{
    private void BindServices()
    {
        DALC.DataTableResult result = new DALC.DataTableResult();
        var dictionary = new Dictionary<string, object>()
        {
            {"ServicesId",txtServicesId.Text},
            {"ServicesLangsId",Langs.Id},
            {"OrganizationsId",DALCL._Login.organizationsId },
            {"OrganizationsLangsId",(int)Tools.Langs.AZ},
            {"ServicesTypesId",(int)Tools.ServicesTypes.Tədbirlər },
            {"IsActive",int.Parse(dListServicesStatus.SelectedValue)},
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
            ((Literal)Master.FindControl("LtrTitle")).Text = "TƏDBİRLƏR";
            ltrAddServices.Text = "YENİ TƏDBİR";

            BindServices();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindServices();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtServicesId.Text = "";
        dListServicesStatus.SelectedIndex = 0;
        btnFilter_Click(null, null);
    }

    protected void lnkGenerateQRCode_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string data = lnk.CommandArgument;// "BA50D346-71E3-4146-8356-484685C98C39"; heleleik static qoyduq

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
}