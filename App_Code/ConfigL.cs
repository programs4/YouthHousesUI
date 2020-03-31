using YouthHousesLibrary;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static YouthHousesLibrary.Config;
using System;

public static class ConfigL
{
    public static int DefaultPageRowCount = 20;

    public static string _Route(string KeyName, string CatchValue = "")
    {
        try
        {
            Page P = (Page)HttpContext.Current.Handler;
            return P.RouteData.Values[KeyName].ToString().ToLower();
        }
        catch
        {
            return CatchValue;
        }
    }

    //Get WebConfig.config App Key
    public static string _GetAppSettings(string KeyName)
    {
        return ConfigurationManager.AppSettings[KeyName];
    }

    //Get Querystring
    public static string _GetQueryString(string KeyName)
    {
        return HttpContext.Current.Request.QueryString[KeyName]._ToString();
    }

    //Ajax error message
    public static void MsgBoxAjax(string Text, string Href = "")
    {
        if (!string.IsNullOrEmpty(Href))
        {
            Href = "location.href = '" + Href + "';";
        }

        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(P, P.Page.GetType(), "PopupScript", string.Format("window.focus(); alert('{0}');{1}", Text, Href), true);
    }

    //Səhifəni yönləndirək:
    public static void RedirectURL(string GetUrl, bool EndResponse = false)
    {
        HttpContext.Current.Response.Redirect(GetUrl, EndResponse);
        HttpContext.Current.Response.End();
    }

    public static DateTime? DatetimeSplitParse(string DateTime)
    {
        try
        {
            string[] Date = Config.Split(DateTime, ' ', 0).Split('.');
            string[] Time = Config.Split(DateTime, ' ', 1).Split(':');

            return new DateTime(Date[2]._ToInt16(), Date[1]._ToInt16(), Date[0]._ToInt16(), Time[0]._ToInt16(), Time[1]._ToInt16(), 0);
        }
        catch
        {
            return null;
        }
    }

    public static void RedirectLogin()
    {
        HttpContext.Current.Response.Redirect(string.Format("~/?return={0}", HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString())), false);
        HttpContext.Current.Response.End();
    }

    public static void RedirectError()
    {
        HttpContext.Current.Response.Redirect("~/error", false);
        HttpContext.Current.Response.End();
    }

    public static string LangsURL(this object Lang, string MoreURL = "")
    {
        if (MoreURL.Contains("default.aspx"))
            return string.Format("{0}/index", Lang);

        return string.Format("{0}/{1}", Lang, MoreURL.Substring(4));
    }

    public static bool CheckFileContentLength(this HttpPostedFile PostedFile, int MaxMB = 10)
    {
        if ((PostedFile.ContentLength / 1024) > MaxMB * 1000)
            return false;
        else
            return true;
    }

    public static void Modal(string Type = "show", string ModalName = "Modal")
    {
        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(P, P.GetType(), "JqueryModal", string.Format("$('#{0}').modal('{1}');", ModalName, Type), true);
    }

    public static void Calendar()
    {
        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterClientScriptBlock(P, P.GetType(), "calendar", "$('.form_datetime').datetimepicker({format: 'dd.mm.yyyy',language: 'en',weekStart: 1,todayBtn: 1,autoclose: 1,todayHighlight: 1,startView: 2,minView: 2,forceParse: 0});", true);
    }

    public static void ControlsClear(this Control Panel)
    {
        foreach (Control item in Panel.Controls)
        {
            if (item.HasControls())
            {
                ControlsClear(item);
            }

            if (item is TextBox)
            {
                (item as TextBox).Text = "";
            }
            else if (item is DropDownList)
            {
                (item as DropDownList).SelectedIndex = 0;
            }
            else if (item is ListBox)
            {
                ListBox LitBx = item as ListBox;
                for (int i = 0; i < LitBx.Items.Count; i++)
                {
                    LitBx.Items[i].Selected = false;
                }
            }
            else if (item is CheckBox)
            {
                (item as CheckBox).Checked = false;
            }
            else if (item is Image)
            {
                (item as Image).ImageUrl = "/uploads/products/0.jpg";
            }
            else if (item is Label)
            {
                (item as Label).Text = "";
            }
        }
    }

    public static void ControlsBind(this Control Panel, Dictionary<string, object> FilterDictionary, Tools.Tables TableName, string addSessionName = "", bool ClearSession = false)
    {
        string SessionName = $"{TableName._ToString()}{addSessionName}";
        if (ClearSession)
        {
            HttpContext.Current.Session[SessionName] = null;
        }

        if (HttpContext.Current.Session[SessionName] != null)
        {
            if (FilterDictionary.Count < 1)
            {
                FilterDictionary = (Dictionary<string, object>)HttpContext.Current.Session[SessionName];
            }
            foreach (Control item in Panel.Controls)
            {
                if (item.HasControls())
                {
                    ControlsBind(item, FilterDictionary, TableName, addSessionName);
                }

                try
                {
                    if (item is TextBox)
                    {
                        TextBox Txt = item as TextBox;
                        Txt.Text = FilterDictionary[Txt.ID]._ToString();
                    }
                    else if (item is DropDownList)
                    {
                        DropDownList DList = item as DropDownList;
                        DList.SelectedValue = FilterDictionary[DList.ID]._ToString();
                    }
                    else if (item is ListBox)
                    {
                        ListBox LstBx = item as ListBox;
                        for (int i = 0; i < LstBx.Items.Count; i++)
                        {
                            ListItem Item = LstBx.Items[i];
                            if (FilterDictionary[LstBx.ID]._ToString().IndexOf(Item.Value) > -1)
                            {
                                Item.Selected = true;
                            }
                        }
                    }
                    else if (item is CheckBox)
                    {
                        CheckBox Check = item as CheckBox;
                        Check.Checked = (bool)FilterDictionary[Check.ID];
                    }
                }
                catch
                {
                    HttpContext.Current.Session[SessionName] = null;
                    FilterDictionary.Clear();
                    ControlsBind(Panel, FilterDictionary, TableName);
                }
            }
        }
        else
        {
            foreach (Control item in Panel.Controls)
            {
                if (item.HasControls())
                {
                    ControlsBind(item, FilterDictionary, TableName, addSessionName, true);
                }

                if (item is TextBox)
                {
                    TextBox Txt = item as TextBox;
                    FilterDictionary.Add(Txt.ID, Txt.Text);
                }
                else if (item is DropDownList)
                {
                    DropDownList DList = item as DropDownList;
                    FilterDictionary.Add(DList.ID, DList.SelectedValue);
                }
                else if (item is ListBox)
                {
                    ListBox LstBx = item as ListBox;
                    StringBuilder Values = new StringBuilder();
                    for (int i = 0; i < LstBx.Items.Count; i++)
                    {
                        ListItem Item = LstBx.Items[i];
                        if (Item.Selected)
                        {
                            Values.Append(Item.Value + ",");
                        }
                    }
                    FilterDictionary.Add(LstBx.ID, Values._ToString().Trim(','));
                }
                else if (item is CheckBox)
                {
                    CheckBox Check = item as CheckBox;
                    FilterDictionary.Add(Check.ID, Check.Checked);
                }

            }
            HttpContext.Current.Session[SessionName] = FilterDictionary;
        }
    }

    public static void BorderColor(Control control)
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

    public static void ClearBorderColor(Control control)
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

}
