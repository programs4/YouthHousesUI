<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Reports_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Panel ID="pnlFilter" runat="server" CssClass="filter">
        <div class="row">

            <div class="col-md-2">
                Qurumlar:
                <asp:DropDownList ID="dListOrganizations" runat="server" DataValueField="ID" DataTextField="Name" CssClass="form-control">
                </asp:DropDownList>
            </div>

             <div class="col-md-2">
                Hesabat növü:
                <asp:DropDownList ID="dlistReportsType" runat="server"  CssClass="form-control">
                    <asp:ListItem Value="ReportUseServicesForAge" Text="Yaş aralığı üzrə xidmətlərdən istifadə"></asp:ListItem>
                   <%-- <asp:ListItem Value="ReportUseServicesForOrganizations" Text="Bölgələr üzrə xidmətlərdən istifadə"></asp:ListItem>--%>
                    <asp:ListItem Value="ReportUseServices" Text="Ən çox istifadə edilən xidmət sahələri"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                Tarixdən:<br />
                <asp:TextBox ID="TxtStartDt" CssClass="form_date form-control" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-2">
                Tarixədək:<br />
                <asp:TextBox ID="TxtEndDt" CssClass="form_date form-control" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-2">
                <asp:Button ID="btnFilter" runat="server" Width="65%" Height="40px" CssClass="op-btn op-btn-gray" Text="Axtar" OnClick="btnFilter_Click" />
                <asp:Button ID="btnClear" runat="server" Width="33%" Height="40px" CssClass="op-btn op-btn-default" Text="Təmizlə" OnClick="btnClear_Click" />
            </div>
        </div>
    </asp:Panel>
    <br />

    <div class="row">
        <div class="col-md-12">
             <asp:Panel ID="PnlContent" runat="server"></asp:Panel>
        </div>

        <div class="col-md-4">           
            <asp:GridView ID="grdReportsAges" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
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
        </div>

        <div class="col-md-4">
      
            <asp:GridView ID="grdReportsRegions" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
                <Columns>
                    <asp:TemplateField HeaderText="S/s">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Yaş">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Count" HeaderText="Say">
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
        </div>

        <div class="col-md-4">
         
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
                <Columns>
                    <asp:TemplateField HeaderText="S/s">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Yaş">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Count" HeaderText="Say">
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
        </div>
    </div>



</asp:Content>

