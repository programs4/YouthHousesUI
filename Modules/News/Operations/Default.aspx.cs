using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthHousesLibrary;

public partial class Modules_News_Operations_Default : System.Web.UI.Page
{
    string _allowTypes = "-gif-jpg-jpeg-bmp-png-";
    int _newsId = 0;
    string _operations = "";
    string _countCacheName = "NewsCount" + Langs.Name;

    void BindDetailsControls()
    {
        DListNewsTypes.DataSource = DALCL.GetNewsTypes();
        DListNewsTypes.DataBind();
        DListNewsTypes.Items.Insert(0, new ListItem("--", "-1"));

        DListStatus.DataSource = DALCL.GetNewsLangsStatus();
        DListStatus.DataBind();
        DListStatus.Items.Insert(0, new ListItem("--", "-1"));
    }

    void BindNewsDetails(int newsId)
    {
        BindDetailsControls();

        DataTable dt = DALCL.GetNewsByID(newsId);

        if (dt == null)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }

        if (dt.Rows.Count < 1)
        {
            MultiView1.ActiveViewIndex = 1;
            return;
        }

        DataTable dtLangs = DALCL.GetAllLangs();
        if (dtLangs == null)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }
        string langs = dt._Rows("Langs");
        for (int i = 0; i < dtLangs.Rows.Count; i++)
        {
            ltrLangs.Text += $"<a href=\"/{dtLangs._Rows("Name", i)}/modules/news/operations/edit/{ConfigL._Route("id")}\">" +
                                    $"<img src=\"/images/langs/{(("," + langs + ",").IndexOf("," + dtLangs._Rows("Id", i) + ",") > -1 ? $"{dtLangs._Rows("Name", i)}-lang" : $"{dtLangs._Rows("Name", i)}-lang-d")}.png\"/></a>";

        }

        DListNewsTypes.SelectedValue = dt._Rows("NewsTypesId");
        TxtTitle.Text = dt._Rows("Title");
        TxtTitleSub.Text = dt._Rows("SubTitle");
        LblSimvolCount.Text = "- simvol sayı : 300 / " + TxtTitleSub.Text.Length._ToString();

        TxtShowDate.Text = dt._RowsDatetime("ShowDate").ToString("dd.MM.yyyy HH:mm");

        txtContent.Text = dt._Rows("ContentText");

        ImgMedium.ImageUrl = $"{dt._Rows("Path")}?{((DateTime)dt._RowsObject("UpdatedDate")).Ticks.ToString()}";

        DListStatus.SelectedValue = dt._Rows("NewsLangsStatusId");

        //Update-də istifadə olunur
        BtnSave.CommandArgument = dt._Rows("NewsLangsId");

        BindImgDetails();
    }

    void BindImgDetails()
    {
        RptImageList.DataSource = DALCL.GetNewsFilesByID(_newsId);
        RptImageList.DataBind();
    }

    void BindNewsAdd()
    {
        BindDetailsControls();

        TxtShowDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
    }

    public bool IsValidation()
    {
        if (FlUpCkEdit.HasFile)
        {
            ConfigL.MsgBoxAjax("Mətnə aid əlavə şəkillər seçili olduğu halda xəbəri təsdiqləmək olmaz.");
            return false;
        }

        if (TxtTitle.Text.Length < 5)
        {
            ConfigL.MsgBoxAjax("Əsas başlığı yazın.");
            return false;
        }

        if (TxtTitle.Text.Length > 300)
        {
            ConfigL.MsgBoxAjax("Əsas başlıq ən çox 300 simvoldan ibarət ola bilər.");
            return false;
        }

        if (TxtTitleSub.Text.Length > 300)
        {
            ConfigL.MsgBoxAjax("Alt başlıq ən çox 300 simvoldan ibarət ola bilər.");
            return false;
        }

        if (txtContent.Text.Length < 100)
        {
            ConfigL.MsgBoxAjax("Mətn ən az 100 simvoldan ibarət olmalıdır.");
            return false;
        }

        if (_operations == "add")
        {
            //Xeberin esas shəklini mutleq secmelidir
            if (FlUpMain.HasFile)
            {
                if (_allowTypes.IndexOf("-" + System.IO.Path.GetExtension(FlUpMain.FileName).Trim('.').ToLower() + "-") < 0)
                {
                    ConfigL.MsgBoxAjax("Xəbərə aid əsas şəkilin formatı uyğun gəlmir.");
                    return false;
                }
            }
            else
            {
                ConfigL.MsgBoxAjax("Xəbərə aid əsas şəkli seçin.");
                return false;
            }
        }

        if (DListStatus.SelectedValue == "-1")
        {
            ConfigL.MsgBoxAjax("Xəbərin statusunu seçin.");
            return false;
        }

        return true;
    }

    public void LoadScript()
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "calendar", "$(\".datepicker\").datetimepicker({format: 'dd.mm.yyyy hh:ii',use24hours: true});", true);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (DALCL._Login == null)
        {
            ConfigL.RedirectLogin();
            return;
        }

        if (!DALCL.CheckPermission(Tools.AdministratorsMenu.Xəbərlər))
        {
            ConfigL.RedirectError();
            return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //CkContentText.Language = Langs.Name;
        _operations = ConfigL._Route("type", "add");
        _newsId = ConfigL._Route("id", "-1")._ToInt32();
        LoadScript();
        if (!IsPostBack)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "XƏBƏRLƏR";
            if (_operations == "add")
            {
                BindNewsAdd();
            }
            else if (_operations == "edit")
            {
                if (_newsId == -1)
                {
                    ConfigL.RedirectURL(string.Format("/{0}/modules/news", Langs.Name));
                    return;
                }
                BindNewsDetails(_newsId);
            }
        }
    }

    protected void BtnAddFile_Click(object sender, EventArgs e)
    {
        string fileName = "";
        string ImgHtml = "";
        string FileType = "";

        DateTime Date = DateTime.Now;
        string path = "/uploads/contents/";

        HttpFileCollection Files = Request.Files;

        for (int i = 0; i < Files.Count - 2; i++)
        {
            if (Files[i].ContentLength > 0)
            {
                FileType = System.IO.Path.GetExtension(Files[i].FileName).Trim('.');
                if (_allowTypes.IndexOf("-" + FileType.ToLower() + "-") > -1)
                {
                    if (!Files[i].CheckFileContentLength(50))
                        continue;

                    fileName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "_" + i.ToString();
                    path = $"{path}{fileName}.{FileType}";
                    ImgHtml += "<img src=\"" + path + "\" alt=\"news-detail\"/><br/><br/>";

                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Files[i].InputStream);

                    ImageResize.ImgResize(path, bmp.Width, bmp.Height, Files[i].InputStream, 95L);

                    DALCL.InsertFileUpload(
                        (int)Tools.FilesFolders.Xəbərlər,
                        (int)(Tools.FilesTypes)Enum.Parse(typeof(Tools.FilesTypes), FileType),
                        path,
                        fileName,
                        Files[i].ContentLength);
                }
            }
        }

        txtContent.Text += ImgHtml;
    }

    protected void BtnAddEmbed_Click(object sender, EventArgs e)
    {
        if (TxtVideoEmbed.Text.Trim().Length > 10)
        {
            txtContent.Text += TxtVideoEmbed.Text;
        }
        TxtVideoEmbed.Text = "";
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidation())
        {
            return;
        }

        DateTime ShowDate;
        if (!DateTime.TryParse(TxtShowDate.Text, out ShowDate))
        {
            ConfigL.MsgBoxAjax("Xəbərin tarixini düzgün daxil edin!");
            return;
        }

        DALC.Transaction transaction = new DALC.Transaction();
        int check;
        var dictionaryNews = new Dictionary<string, object>()
        {
            {"NewsTypesId",int.Parse(DListNewsTypes.SelectedValue)},
            {"Title", TxtTitle.Text.ClearChar13()},
            {"IsActive", 1},
            {"ShowDate", ShowDate}
        };

        var dictionaryNewsLangs = new Dictionary<string, object>()
        {
            {"NewsLangsStatusId", DListStatus.SelectedValue},
            {"Title",TxtTitle.Text },
            {"SubTitle", TxtTitleSub.Text},
            {"ContentText", txtContent.Text},
            {"UpdatedDate", DateTime.Now},
        };

        if (_operations == "add")
        {
            dictionaryNews.Add("OrganizationsId", DALCL._Login.organizationsId);
            //dictionaryNews.Add("CreatedDate", DateTime.Now);

            int resultId = DALC.InsertDatabase(Tools.Tables.News, dictionaryNews, transaction);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            _newsId = resultId;

            dictionaryNewsLangs.Add("NewsId", resultId);
            dictionaryNewsLangs.Add("LangsId", Langs.Id);
            dictionaryNewsLangs.Add("VisitorCount", 0);
            dictionaryNewsLangs.Add("CreatedDate", DateTime.Now);


            resultId = DALC.InsertDatabase(Tools.Tables.NewsLangs, dictionaryNewsLangs, transaction, true);
            if (resultId < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }
        else if (_operations == "edit")
        {
            dictionaryNews.Add("WhereId", _newsId);

            check = DALC.UpdateDatabase(Tools.Tables.News, dictionaryNews, transaction);
            if (check < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }

            dictionaryNewsLangs.Add("WhereId", int.Parse(BtnSave.CommandArgument));

            check = DALC.UpdateDatabase(Tools.Tables.NewsLangs, dictionaryNewsLangs, transaction, true);
            if (check < 1)
            {
                ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
                return;
            }
        }

        string fileName = "";
        string path = "/uploads/news";
        string fileType = "";
        int contentLenght;
        HttpFileCollection Files = Request.Files;

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("NewsFilesTypesId", typeof(int));
        dtFiles.Columns.Add("FilesFoldersId", typeof(int));
        dtFiles.Columns.Add("FilesTypesId", typeof(int));
        dtFiles.Columns.Add("Path", typeof(string));
        dtFiles.Columns.Add("Name", typeof(string));
        dtFiles.Columns.Add("ContentLength", typeof(int));

        DataRow dr;

        for (int i = 1; i < Files.Count; i++)
        {
            if (Files[i].ContentLength > 0)
            {
                fileType = System.IO.Path.GetExtension(Files[i].FileName).Trim('.').ToLower();
                if (_allowTypes.IndexOf($"-{fileType}-") > -1)
                {
                    if (!Files[i].CheckFileContentLength(50))
                        continue;

                    contentLenght = Files[i].ContentLength;
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Files[i].InputStream);

                    string[] folderList = null;

                    fileName = $"{_newsId}_{(i - 1).ToString()}_{DateTime.Now.ToString("ddMMyyyyHHmmssfff")}.{fileType}";
                    if (i == 1)
                    {
                        //Boyuk original sekil nece var ama azmaz keyfiyyeti ile oynuyaq.
                        ImageResize.ImgResize($"{path}/main/{fileName}", bmp.Width, bmp.Height, Files[i].InputStream, 90L);
                        folderList = new string[] { "main" };
                    }
                    else
                    {
                        ImageResize.ImgResize($"{path}/other/{fileName}", bmp.Width, bmp.Height, Files[i].InputStream, 90L);

                        folderList = new string[] { "other" };
                    }

                    foreach (string item in folderList)
                    {
                        dr = dtFiles.NewRow();
                        switch (item)
                        {
                            case "main":
                                dr["NewsFilesTypesId"] = (int)Tools.NewsFilesTypes.Əsas_şəkil;
                                break;
                            case "other":
                                dr["NewsFilesTypesId"] = (int)Tools.NewsFilesTypes.Digər_şəkillər;
                                break;
                            default:
                                break;
                        }

                        dr["FilesFoldersId"] = (int)Tools.FilesFolders.Xəbərlər;
                        dr["FilesTypesId"] = (int)(Tools.FilesTypes)Enum.Parse(typeof(Tools.FilesTypes), fileType);
                        dr["Path"] = $"{path}/{item}/{fileName}";
                        dr["Name"] = fileName;
                        dr["ContentLength"] = contentLenght;

                        dtFiles.Rows.Add(dr);
                    }
                }
            }
        }

        DALC.ExecuteProcedure("InsertNewsFiles", "NewsId,ImportTableNewsFiles,CreatedDate", new object[] { _newsId, dtFiles, DateTime.Now });

        int NewsPageNum = 1;
        if (Session["NewsPageID"] != null)
            NewsPageNum = int.Parse(Session["NewsPageID"].ToString());

        ConfigL.RedirectURL(string.Format("/{0}/modules/news/{1}", Langs.Name, NewsPageNum));
    }

    protected void LnkImgDeleted_Command(object sender, CommandEventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            new System.IO.FileInfo(Server.MapPath(e.CommandArgument._ToString())).Delete();
            new System.IO.FileInfo(Server.MapPath(e.CommandArgument._ToString().Replace("othersmall", "other"))).Delete();
            DALCL.DeleteFiles(Tools.Tables.NewsFiles, int.Parse(e.CommandName));
            BindImgDetails();
        }
        catch
        {
            ConfigL.MsgBoxAjax("Şəkil silinə bilmədi.");
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        int NewsPageNum = 1;
        if (Session["NewsPageID"] != null)
            NewsPageNum = int.Parse(Session["NewsPageID"].ToString());

        ConfigL.RedirectURL(string.Format("/{0}/adminn/tools/news/{1}", Langs.Name, NewsPageNum));
    }

    protected void lnkTranslation_Click(object sender, EventArgs e)
    {
        //Id-ə görə əgər News table-inda varsa tərcümə üçün insert edek
        if (!DALCL.CheckNewsById(_newsId))
        {
            ConfigL.MsgBoxAjax("Tərcümə üçün xəbər mövcud deyil");
            return;
        }

        var dictionaryNewsLangs = new Dictionary<string, object>()
        {
            {"NewsId",_newsId },
            {"LangsId", Langs.Id},
            {"NewsLangsStatusId", (int)Tools.NewsLangsStatus.Hazırlanır},
            {"Title", ""},
            {"Subtitle", ""},
            {"ContentText", ""},
            {"VisitorCount", 0},
            {"UpdatedDate", DateTime.Now},
            {"CreatedDate", DateTime.Now},
        };

        int resultId = DALC.InsertDatabase(Tools.Tables.NewsLangs, dictionaryNewsLangs);
        if (resultId < 1)
        {
            ConfigL.MsgBoxAjax(Config._AlertMessages.Error);
            return;
        }
        MultiView1.ActiveViewIndex = 0;
    }
}