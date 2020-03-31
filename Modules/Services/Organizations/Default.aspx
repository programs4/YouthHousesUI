<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Services_Organizations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div class="row">
        <div class="col-md-2">
            Xidmətlər:<br />
            <asp:DropDownList ID="dListServices" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
            <br />
            <br />
        </div>
        <div class="col-md-2">
            <br />
            <asp:Button ID="btnSave" runat="server" Height="40px" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSave_Click" />
        </div>
    </div>

    <asp:GridView ID="grdSrvices" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
        <Columns>
            <asp:TemplateField HeaderText="S/s">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:BoundField DataField="ServicesName" HeaderText="Adı">
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
                    <asp:LinkButton ID="lnkGenerateQRCode" CommandArgument='<%#Eval("Barcode")%>'  OnClick="lnkGenerateQRCode_Click" runat="server">QRCode</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="100px" Font-Size="11pt" />
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDeleted" ForeColor="Red" Font-Size="10pt" CommandArgument='<%#Eval("Id")%>' OnClick="lnkDeleted_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi');" runat="server">
                       <span class="glyphicon glyphicon-trash" ></span>Sil
                    </asp:LinkButton>
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

