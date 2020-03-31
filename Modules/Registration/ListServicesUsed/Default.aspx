<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Registration_ListServicesUsed_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h4>
        <asp:Label ID="lblUsersFullname" runat="server" Text=""></asp:Label>
    </h4>
    <hr />
    <asp:GridView ID="grdReports" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
        <Columns>
            <asp:TemplateField HeaderText="S/s">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:BoundField DataField="Name" HeaderText="Xidmətin adı">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>

            <asp:BoundField DataField="Count" HeaderText="Say">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="350px" />
            </asp:BoundField>

            <asp:BoundField DataField="UsedMinute" DataFormatString="{0} dəq." HeaderText="İstifadə vaxtı">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="350px" />
            </asp:BoundField>

            <asp:BoundField DataField="VotesAverage" HeaderText="Ortalama səs">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="350px" />
            </asp:BoundField>


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

