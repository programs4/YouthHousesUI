<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_News_Operations_Default" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Import Namespace="YouthHousesLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link href="/adminn/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/adminn/js/jquery-ui.js"></script>
    <link href="/adminn/css/bootstrap-datetimepicker.min.css" rel="stylesheet" media="screen">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
        <asp:View ID="View1" runat="server">

            <div class="row">
                <div class="col-md-6">
                    Xəbərin növü :
             <asp:DropDownList ID="DListNewsTypes" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <div class="lang-bar-2" style="margin-top: 20px">
                        <asp:Literal ID="ltrLangs" runat="server"></asp:Literal>
                        <%--  <a href='<%=$"/az/modules/news/operations/edit/{ConfigL._Route("lang")}" %>'>
                            <img src='<%=string.Format("/images/langs/{0}.png",($",{12},"._ToString().IndexOf(",10,")>-1)?"az-lang":"az-lang-d") %>' alt="icon" />
                        </a>

                        <a href='<%=$"/en/modules/news/operations/edit/{ConfigL._Route("lang")}" %>'>
                            <img src='<%#string.Format("/images/langs/{0}.png",$",{Eval("Langs")},"._ToString().IndexOf(",20,")>-1?"en-lang":"en-lang-d") %>' alt="icon" />
                        </a>

                        <a href='<%=$"/ru/modules/news/operations/edit/{ConfigL._Route("lang")}" %>'>
                            <img src='<%#string.Format("/images/langs/{0}.png",$",{Eval("Langs")},"._ToString().IndexOf(",30,")>-1?"ru-lang":"ru-lang-d") %>' alt="icon" />
                        </a>--%>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    Əsas başlıq :
            <asp:TextBox ID="TxtTitle" CssClass="form-control" runat="server" TextMode="MultiLine" ForeColor="Black" Rows="2"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    Alt başlıq :
            <asp:TextBox ID="TxtTitleSub" CssClass="form-control" runat="server" ForeColor="#666666" Rows="3" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                    <asp:Label ID="LblSimvolCount" runat="server" Text="- simvol sayı : 300 / 0" ClientIDMode="Static"></asp:Label>
                </div>
            </div>

            <div class="row" style="margin-top: 30px">
                <div class="col-md-12">
                    Mətn:
                    <asp:TextBox ID="txtContent" runat="server" ClientIDMode="Static" CssClass="form-control" ForeColor="#666666" Height="300px" TextMode="MultiLine"></asp:TextBox>
                    <br />
                </div>
            </div>

            <div style="display: none">
                <div class="row" style="margin-top: 30px">
                    <div class="col-md-6">
                        Mətnə şəkil əlavə et:
        <asp:FileUpload ID="FlUpCkEdit" runat="server" AllowMultiple="true" CssClass="form-control displayInline" />
                    </div>
                    <div class="col-md-2 pull-left">
                        <br />
                        <asp:Button ID="BtnAddFile" runat="server" CssClass="btn btn-default" CommandName="Edit" Height="35px" Text="Əlavə et" OnClick="BtnAddFile_Click" OnClientClick="if(loginControl()=='Success'){this.style.visibility='hidden';document.getElementById('load1').style.display='';}else{return false;}" />
                        <div id="load1" class="floatLeft" style="display: none">
                            <img src="/assets/img/admin/other/loading.gif" alt="gif-load" />
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 30px">
                    <div class="col-md-6">
                        Video embed:
        <asp:TextBox ID="TxtVideoEmbed" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <br />
                        <asp:Button ID="BtnAddEmbed" runat="server" CssClass="btn btn-default" CommandName="Edit" Height="35px" Text="Əlavə et" OnClientClick="document.getElementById('load2').style.display='';" OnClick="BtnAddEmbed_Click" />
                        <div id="load2" class="floatLeft" style="display: none">
                            <img src="/assets/img/admin/other/loading.gif" alt="gif-load" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="PnlMainImage" runat="server">
                <div class="row" style="margin-top: 30px">
                    <div class="col-md-6">
                        <asp:Panel ID="PnlFileUpload" runat="server">
                            Əsas şəkil:
            <asp:FileUpload ID="FlUpMain" runat="server" CssClass="form-control displayInline" onchange="readURL(this,'.imgSmall');" BackColor="#E1F4EE" />
                        </asp:Panel>
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="imgDetails">
                            <div class="imgContainer">
                                <asp:Image ID="ImgMedium" ImageUrl="/uploads/news/0.jpg" onerror="src='/uploads/0.jpg'" runat="server" CssClass="profImg imgSmall" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="row" style="margin-top: 30px">
                <div class="col-md-6">
                    Digər şəkillər:
        <asp:FileUpload ID="FlUpOther" runat="server" AllowMultiple="true" CssClass="form-control displayInline" />
                </div>
            </div>

            <asp:UpdatePanel ID="UpdtePanel2" runat="server">
                <ContentTemplate>
                    <div class="row" style="margin-top: 30px">
                        <div class="col-md-6">
                            <div class="row">
                                <asp:Repeater ID="RptImageList" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3">
                                            <div class="imgOther">
                                                <img class="" src='<%#Eval("Path")%>' alt="img-details" />
                                                <div class="container">
                                                    <asp:LinkButton ID="LnkImgDeleted" runat="server" CommandName='<%#Eval("Id") %>' CommandArgument='<%#Eval("Path")%>' OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');return false;" OnCommand="LnkImgDeleted_Command">Sil</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <div class="row">
                <div class="col-md-2">
                    Xəbərin göstərilmə tarixi:
                    <asp:TextBox ID="TxtShowDate" CssClass="form_datetime form-control" Height="35px" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2">
                    Xəbərin Statusu :
                    <asp:DropDownList ID="DListStatus" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

            <div class="row" style="margin-top: 30px">
                <div class="col-md-6">
                    <div class="operationBnt" id="operationBnt">
                        <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" Height="35px" Text="YADDA SAXLA" OnClientClick="if(loginControl()=='Success'){document.getElementById('operationBnt').style.display='none';document.getElementById('load3').style.display='';}else{return false;}" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-warning" Height="35px" Text="İMTİNA ET" OnClientClick="return confirm('Əminsinizmi');" OnClick="BtnCancel_Click" />
                    </div>
                    <div id="load3" style="display: none">
                        &nbsp; &nbsp;
        <img src="/assets/img/admin/other/loading.gif" alt="gif-load" />
                    </div>
                </div>
            </div>

        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="row">
                <div class="text-center" style="margin-top: 350px">
                    <asp:LinkButton ID="lnkTranslation" OnClick="lnkTranslation_Click" runat="server">
                        <img class="alignMiddle" src="/images/add.png" />
                        Tərcümə et
                    </asp:LinkButton>

                </div>
            </div>
        </asp:View>
    </asp:MultiView>

    <script type="text/javascript" src="/adminn/js/bootstrap-datetimepicker.min.js" charset="UTF-8"></script>

    <script type="text/javascript">
        function readURL(input, className) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $(className).attr('src', e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
        $(function () {
            $(".datepicker").datetimepicker({
                format: 'yyyy-mm-dd hh:ii',
                use24hours: true
            });
        });
    </script>

</asp:Content>

