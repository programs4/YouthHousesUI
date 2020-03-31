<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_News_Default" %>

<%@ Import Namespace="YouthHousesLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Panel ID="PnlFilter" runat="server" CssClass="filter">
        <div class="row">
            <div class="col-md-2">
                №:
                <asp:TextBox ID="TxtNewsID" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Status:
            <asp:DropDownList ID="DListNewsStatus" runat="server" CssClass="form-control" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <asp:Button ID="BtnFilter" runat="server" Width="65%" Height="40px" CssClass="op-btn op-btn-gray" Text="Axtar" OnClick="BtnFilter_Click" />
                <asp:Button ID="BtnClear" runat="server" Width="33%" Height="40px" CssClass="op-btn op-btn-default" Text="Təmizlə" OnClick="BtnClear_Click" />
            </div>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-md-6">
            <asp:Panel ID="PnlAddAplicatons" runat="server" CssClass="addNews">
                <a href='<%=string.Format("/{0}/modules/news/operations/add",Langs.Name) %>'>
                    <img class="alignMiddle" src="/images/add.png" />
                    YENİ XƏBƏR
                </a>
            </asp:Panel>
        </div>
        <div class="col-md-6 text-right">
            <br />
            <asp:Label ID="LblCount" runat="server"></asp:Label>
        </div>
    </div>

    <div class="row tabs-content">
        <div class="col-md-12">
            <asp:Repeater ID="RptNews" runat="server" OnItemDataBound="RptNews_ItemDataBound">
                <ItemTemplate>
                    <asp:Literal ID="LtrStart" runat="server">
                            <div class="row donation-inner-a-row">
                    </asp:Literal>
                    <div class="col-md-6">
                        <a href="<%# string.Format("/{0}/modules/news/operations/edit/{1}",Langs.Name, Eval("ID")) %>">
                            <div class="donation-item-a">
                                <img src='<%#Eval("Path") %>' onerror="src='/uploads/0.jpg'">
                                <%-- <h3><%#string.Format("{0} &nbsp;&nbsp;<span class=\"orange\">№ {1}</span>",Eval("CategoriesName"),Eval("ID"))%></h3>--%>
                                <p><%# Eval("Title") %></p>
                                <div class="newsTitleSub donation-infos">
                                    <%# Eval("SubTitle") %>
                                </div>
                                <div class="clearFix"></div>
                                <div class="lang-bar-2">

                                     <a href='<%#string.Format("/az/modules/news/operations/edit/{0}",Eval("ID")) %>'>
                                        <img src='<%#string.Format("/images/langs/{0}.png",($",{Eval("Langs")},"._ToString().IndexOf(",10,")>-1)?"az-lang":"az-lang-d") %>' alt="icon" />
                                    </a>

                                    <a href='<%#string.Format("/en/modules/news/operations/edit/{0}",Eval("ID")) %>'>
                                        <img src='<%#string.Format("/images/langs/{0}.png",$",{Eval("Langs")},"._ToString().IndexOf(",20,")>-1?"en-lang":"en-lang-d") %>' alt="icon" />
                                    </a>

                                    <a href='<%#string.Format("/ru/modules/news/operations/edit/{0}",Eval("ID")) %>'>
                                        <img src='<%#string.Format("/images/langs/{0}.png",$",{Eval("Langs")},"._ToString().IndexOf(",30,")>-1?"ru-lang":"ru-lang-d") %>' alt="icon" />
                                    </a>
                                </div>
                            </div>
                        </a>
                    </div>
                    <asp:Literal ID="LtrEnd" runat="server"></div></asp:Literal>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>


    <asp:Panel ID="PnlPager" CssClass="pager_top row pagination-row" Style="text-align: center;" runat="server">
        <ul class="pagination bootpag"></ul>
    </asp:Panel>

    <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />

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
                window.location.href = '/<%=Langs.Name%>/modules/news/' + num;
                }).find('.pagination');
        }

        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });

    </script>

</asp:Content>

