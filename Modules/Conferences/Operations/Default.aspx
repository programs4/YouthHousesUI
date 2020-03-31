<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Conferences_Operations_Default" %>
<%@ Import Namespace="YouthHousesLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:Panel ID="pnlControls" runat="server" CssClass="row">

        <div class="col-md-4">
            <div class="row">

                <div class="col-md-12">
                    Ümumi başlıq:<br />
                    <asp:TextBox ID="txtTitle" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Adı:<br />
                    <asp:TextBox ID="txtNameLangs" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Təsviri:<br />
                    <asp:TextBox ID="txtDescriptionLangs" AutoCompleteType="Disabled" TextMode="MultiLine" Height="110px" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <asp:Panel ID="pnlServicesVotesTypes" Visible="false" runat="server" class="col-md-12">
                    Səs vermə növü:<br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdServicesVotesTypes" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="Id">
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
                                            <asp:CheckBox ID="chkServicesVotesTypes" data-id='<%#Eval("Id")%>' data-servicesvotestypesrelationsid='<%#Eval("servicesvotestypesrelationsid")%>' data-isactive='<%#Eval("IsActive")%>' Checked='<%#(Eval("IsActive")!=DBNull.Value && Eval("IsActive")._ToInt16()==1)%>'  CssClass="Chekbx" AutoPostBack="true" OnCheckedChanged="chkServicesVotesTypes_CheckedChanged" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Name" HeaderText="Növü">
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
                    <br />
                </asp:Panel>               

                <div class="col-md-12">
                    Statusu:<br />
                    <asp:DropDownList ID="dListStatus" CssClass="form-control" runat="server">
                        <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Aktiv"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Deaktiv"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                </div>

                <div class="col-md-12 text-right">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Height="40px" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Height="40px" CssClass="op-btn op-btn-default" Text="İMTİNA" OnClick="btnCancel_Click" />
                </div>

            </div>
        </div>

        <div class="col-md-4">
        </div>

    </asp:Panel>
</asp:Content>

