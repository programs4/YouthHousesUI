<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Administrators_Operations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Panel ID="pnlControls" runat="server" class="row">

        <div class="col-md-4">

            <div class="col-md-12">
                Qurum:<br />
                <asp:DropDownList ID="dListOrganizations" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Təşkilat:<br />
                <asp:DropDownList ID="dlistCalendar" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Soyadı,adı və atasının adı:<br />
                <asp:TextBox ID="txtFullName" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Vəzifəsi:<br />
                <asp:TextBox ID="txtPosition" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                İstifadəçi adı:<br />
                <asp:TextBox ID="txtUsername" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Şifrə:<br />
                <asp:TextBox ID="txtPasswordUp" TextMode="Password" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Təkrar şifrə:<br />
                <asp:TextBox ID="txtPasswordUpRepeat" TextMode="Password" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>
            <div class="col-md-12">
                Telefon nömrəsi:<br />
                <asp:TextBox ID="txtPhone" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Elektron poçt:<br />
                <asp:TextBox ID="txtEmail" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>


            <div class="col-md-12">
                Status:<br />
                <asp:DropDownList ID="dListAdministratorsStatusId" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                <br />
                <br />
            </div>

            <div class="col-md-12 text-right">
                <asp:Button ID="btnSave" runat="server" Height="40px" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Height="40px" CssClass="op-btn op-btn-default" Text="İMTİNA" OnClick="btnCancel_Click" />
            </div>

        </div>
        <div class="col-md-4">
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdAdministratorsMenu" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="Id">
                        <Columns>
                            <asp:TemplateField HeaderText="S/s">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdministratorsMenu" data-id='<%#Eval("Id")%>' CssClass="Chekbx" AutoPostBack="true" OnCheckedChanged="chkAdministratorsMenu_CheckedChanged" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Name" HeaderText="Menu">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
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
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div style="display: none" class="col-md-4 text-left">
            <br />
            <asp:Image ID="imgUser" Width="250px" Height="300px" ImageUrl="/uploads/0.jpg" onerror="src='/uploads/0.jpg'" runat="server" />
            <br />
            <br />
            <asp:FileUpload ID="flUpImg" Width="250px" CssClass="form-control" runat="server" />
        </div>

    </asp:Panel>
</asp:Content>

