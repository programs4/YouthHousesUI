<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Services_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Panel ID="PnlFilter" runat="server" CssClass="filter">
        <div class="row">
            <div class="col-md-2">
                №:
                <asp:TextBox ID="txtServicesId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-2">
                Status:
                <asp:DropDownList ID="dListServicesStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Aktiv"></asp:ListItem>
                    <asp:ListItem Value="0" Text="Deaktiv"></asp:ListItem>
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
            <asp:Panel ID="PnlAddServices" runat="server" CssClass="addNews">
                <a href='<%=$"/{Langs.Name}/modules/services/operations/{ConfigL._Route("servicestypesid")}/add/0" %>'>
                    <img class="alignMiddle" src="/images/add.png" />
                    <asp:Literal ID="ltrAddServices" runat="server"></asp:Literal>
                </a>
            </asp:Panel>
        </div>
        <div class="col-md-6 text-right">
            <br />
            <asp:Label ID="LblCount" runat="server"></asp:Label>
        </div>
    </div>
    <br />
    <asp:GridView ID="grdSrvices" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,Name" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
        <Columns>
            <asp:TemplateField HeaderText="S/s">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:BoundField DataField="Name" HeaderText="Adı">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>

            <asp:TemplateField HeaderText="Ən az">
                <ItemTemplate>
                    <%# $"{Eval("MinMinute")} dəq." %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Saat ərzində">
                <ItemTemplate>
                    <%# $"{Eval("UseBetweenHour")} dəfə" %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="85px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Gün ərzində">
                <ItemTemplate>
                    <%# $"{Eval("DailyUse")} dəfə" %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField>

            <asp:BoundField DataField="Description" HeaderText="Qeyd">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="400px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText="Statusu">
                <ItemTemplate>
                    <img style="width: 22px;" src='/images/IsActive_<%#(bool)Eval("IsActive")?"1":"0" %>.png' title='' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="QRCode">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkGenerateQRCode" CommandArgument='<%#Eval("Id")%>' OnClick="lnkGenerateQRCode_Click" runat="server">QRCode</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="100px" Font-Size="11pt" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Düzəliş">
                <ItemTemplate>
                    <a href="/<%=Langs.Name%>/modules/services/operations/<%#Eval("ServicesTypesId")%>/edit/<%#Eval("Id")%>">
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
</asp:Content>

