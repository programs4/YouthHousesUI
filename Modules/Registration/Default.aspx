<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Registration_Default" %>

<%@ Import Namespace="YouthHousesLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Panel ID="pnlFilter" runat="server" CssClass="filter">
        <div class="row">
            <div class="col-md-2">
                №:
                <asp:TextBox ID="txtUsersId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Status:
                <asp:DropDownList ID="dListSocialStatus" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <asp:Button ID="BtnFilter" runat="server" Width="65%" Height="40px" CssClass="op-btn op-btn-gray" Text="Axtar" OnClick="BtnFilter_Click" />
                <asp:Button ID="BtnClear" runat="server" Width="33%" Height="40px" CssClass="op-btn op-btn-default" Text="Təmizlə" OnClick="BtnClear_Click" />
            </div>
        </div>
    </asp:Panel>


    <div class="row">
        <div class="col-md-6">
            <asp:Panel ID="pnlAddUsers" runat="server" CssClass="addNews">
                <a href='<%=string.Format("/{0}/modules/registration/operations/add/0",Langs.Name) %>'>
                    <img class="alignMiddle" src="/images/add.png" />
                    YENİ İSTİFADƏÇİ
                </a>
            </asp:Panel>
        </div>
        <div class="col-md-6 text-right">
            <br />
            <asp:Label ID="lblCount" runat="server"></asp:Label>
        </div>
    </div>

    <div class="row tabs-content">
        <div class="col-md-12">
            <asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="RptNews_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-4 col-xl-4">
                        <a href='<%# string.Format("/{0}/modules/registration/operations/edit/{1}",Langs.Name, Eval("Id")) %>'>
                            <div class="person-card clearfix">
                                <div class="person-img">
                                    <img src='<%# $"/uploads/users/{Eval("Id")}_{Eval("DocumentNumber")}.jpg"%>' onerror="src='/uploads/0.jpg'">
                                </div>
                                <div class="person-info">
                                    <h4><%# $"{Eval("Name")} {Eval("Surname")}"%></h4>
                                    <span href='mailto:<%#Eval("Email")%>'><%#Eval("Email")%></span>
                                    <p>
                                        <label for="age">Yas: &nbsp;</label>
                                        <%# (DateTime.Now.Year-((DateTime)Eval("BirthDate")).Year) %>
                                    </p>
                                    <p>
                                        <label for="age">Cinsi: &nbsp;</label><%#Eval("UsersGendersName")%>
                                    </p>
                                    <p>
                                        <label for="age">Sosial statusu:&nbsp;</label><%#Eval("UsersSocialStatusName")%>
                                    </p>
                                    <p>
                                        <label for="age">Dərsdə iştirak saatı:&nbsp;</label>10 saat
                                    </p>
                                </div>
                        </a>
                        <a href='<%# string.Format("/{0}/modules/registration/listservicesused/{1}",Langs.Name, Eval("Id")) %>'>
                            <div class="flag">
                                <img src="/images/gift.png" alt="icon">
                                <p><%#Eval("UsedServicesCount")%></p>
                                <p>
                                   Say                           
                                </p>
                            </div>
                        </a>
                    </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:Panel ID="pnlPager" CssClass="pager_top row pagination-row" Style="text-align: center;" runat="server">
        <ul class="pagination bootpag"></ul>
    </asp:Panel>

    <asp:HiddenField ID="hdnTotalCount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />

    <script type="text/javascript">
        function GetPagination(t, p) {
            $('.pager_top').bootpag({
                total: t,
                page: p,
                maxVisible: 15,
                leaps: true,
                firstLastUse: true,
                first: '<span aria-hidden="true">&larr;</span>',
                last: '<span aria-hidden="true">&rarr;</span>',
                wrapClass: 'pagination',
                activeClass: 'active',
                disabledClass: 'disabled',
                nextClass: 'next',
                prevClass: 'prev',
                lastClass: 'last',
                firstClass: 'first',

            }).on("page", function (event, num) {
                window.location.href = '/<%=Langs.Name%>/modules/registration/' + num;
            }).find('.pagination');
        }

        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });

    </script>
</asp:Content>

