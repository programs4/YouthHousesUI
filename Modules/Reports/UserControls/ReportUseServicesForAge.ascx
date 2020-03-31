<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUseServicesForAge.ascx.cs" Inherits="Modules_Reports_UserControl_ReportUseServicesForAge" %>

<asp:GridView ID="grdReports" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
    <Columns>
        <asp:TemplateField HeaderText="S/s">
            <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="50px" />
        </asp:TemplateField>

        <asp:BoundField DataField="Age" HeaderText="Yaş">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>

        <asp:BoundField DataField="Count" HeaderText="Say">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="350px" />
        </asp:BoundField>

        <asp:BoundField DataField="Interest" DataFormatString="{0} %" HeaderText="Faiz">
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
