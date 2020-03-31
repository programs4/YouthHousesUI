<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_CalendarOrganizations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

     <asp:Panel ID="pnlControls" runat="server" class="row">
                <div class="col-md-2">
                    Adı:<br />
                    <asp:TextBox ID="txtName" AutoCompleteType="Disabled" CssClass="form-control" runat="server" MaxLength="200"></asp:TextBox>
                    <br />
                    <br />
                </div>
                <div class="col-md-2">
                    Qısa adı:<br />
                    <asp:TextBox ID="txtShortName" AutoCompleteType="Disabled" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    <br />
                    <br />
                </div>
                <div class="col-md-2">
                    Status:<br />
                    <asp:DropDownList ID="dListStatus" CssClass="form-control" runat="server">
                        <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Aktiv" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Deaktiv" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                </div>

                <div class="col-md-2">
                    Nümayiş:<br />
                    <asp:DropDownList ID="DlistPublic" CssClass="form-control" runat="server">
                        <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Açıq" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Bağlı" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:Button ID="btnSave" runat="server" CommandArgument="0" Height="40px" CssClass="btn btn-default" Text="ƏLAVƏ ET" OnClick="btnSave_Click" />
                </div>
            </asp:Panel>

            <asp:GridView ID="grdCalendarOrganizations" runat="server" AutoGenerateColumns="False" BorderColor="#EBEDF3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
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

                    <asp:BoundField DataField="ShortName" HeaderText="Qısa adı">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <%#((bool)Eval("IsActive"))?"Aktiv":"Deaktiv"%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nümayiş">
                        <ItemTemplate>
                            <%#((bool)Eval("IsPublic"))?"Açıq":"Bağlı"%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tarix">
                        <ItemTemplate>
                            <%#((DateTime)Eval("CreatedDate")).ToString("dd.MM.yyyy")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Düzəliş">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnlkEdit" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="lnlkEdit_Click">                    
                        <span class="glyphicon glyphicon-edit"></span>
                        Düzəliş
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

