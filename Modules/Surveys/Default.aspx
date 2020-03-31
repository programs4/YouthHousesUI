<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Surveys_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Panel ID="pnlFilter" runat="server" CssClass="filter">
        <div class="row">
            <div class="col-md-2">
                №:
                <asp:TextBox ID="txtSurveysId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Status:
                <asp:DropDownList ID="dListSurveysStatus" runat="server" DataValueField="ID" DataTextField="Name" CssClass="form-control">                    
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                <asp:Button ID="btnFilter" runat="server" Width="65%" Height="40px" CssClass="op-btn op-btn-gray" Text="Axtar" OnClick="btnFilter_Click" />
                <asp:Button ID="btnClear" runat="server" Width="33%" Height="40px" CssClass="op-btn op-btn-default" Text="Təmizlə" OnClick="btnClear_Click" />
            </div>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-md-6">
            <asp:Panel ID="PnlAddSurveys" runat="server" CssClass="addNews">
                <a href='<%=$"/{Langs.Name}/modules/surveys/operations/add/0" %>'>
                    <img class="alignMiddle" src="/images/add.png" />
                    YENİ SORĞU
                </a>
            </asp:Panel>
        </div>
        <div class="col-md-6 text-right">
            <br />
            <asp:Label ID="lblCount" runat="server"></asp:Label>
        </div>
    </div>
    <br />
    <asp:GridView ID="grdSurveys" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
        <Columns>
            <asp:TemplateField HeaderText="S/s">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:BoundField DataField="Question" HeaderText="Sual">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>

            <asp:BoundField DataField="VotesCount" HeaderText="Cavab sayı">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText="Tarix">
                <ItemTemplate>
                    <%#((DateTime)Eval("CreatedDate")).ToString("dd.MM.yyyy")%>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:TemplateField>

            <asp:BoundField DataField="SurveysStatusName" HeaderText="Statusu">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText="Düzəliş">
                <ItemTemplate>
                    <a href="/<%=Langs.Name%>/modules/surveys/operations/edit/<%#Eval("Id")%>">
                        <span class="glyphicon glyphicon-edit"></span>
                        Düzəliş
                    </a>
                </ItemTemplate>
                <ItemStyle Width="100px" Font-Size="11pt" />
            </asp:TemplateField>

        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <EmptyDataTemplate>
            <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                Məlumat tapılmadı.
            </div>
        </EmptyDataTemplate>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle Font-Bold="True" BackColor="WhiteSmoke" Height="40px" />
        <PagerSettings PageButtonCount="20" />
        <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
        <RowStyle Height="45px" HorizontalAlign="Center" Font-Bold="False" CssClass="gridRowHover" />
        <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>

    <asp:Panel ID="pnlPager" CssClass="pager_top row pagination-row" Style="text-align: center;" runat="server">
        <ul class="pagination bootpag"></ul>
    </asp:Panel>

    <asp:HiddenField ID="hdnTotalCount" ClientIDMode="Static" runat="server" />
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
                window.location.href = '/<%=Langs.Name%>/modules/surveys/' + num;
                }).find('.pagination');
        }

        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        });

    </script>
</asp:Content>

