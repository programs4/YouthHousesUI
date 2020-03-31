<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_CalendarManagment_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="pnlControls" runat="server" class="row">
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-12">
                            Sosial status:<br />
                            <asp:DropDownList ID="dListCalendarTypes" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12">
                            Başlıq:<br />
                            <asp:TextBox ID="txtTitle" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12">
                            Qeyd:<br />
                            <asp:TextBox ID="txtDescription" CssClass="form-control" Height="114px" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>

                        <div class="col-md-12">
                            Başlama tarixi:<br />
                            <asp:TextBox ID="txtStartDate" CssClass="form_datetime form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>

                        <div class="col-md-12">
                            Bitmə tarixi:<br />
                            <asp:TextBox ID="txtEndDate" CssClass="form_datetime form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>

                        <div class="col-md-12">
                            Status:<br />
                            <asp:DropDownList ID="dListStatus" CssClass="form-control" runat="server">
                                <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Aktiv" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Deaktiv" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12 text-right">
                            <asp:Button ID="btnSave" runat="server" Height="40px" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Height="40px" CssClass="op-btn op-btn-default" Text="İMTİNA" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
              

            </asp:Panel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div style="padding: 20px;">İstifadə edən şəxsin təşkilatı təyin edilməyib!</div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

