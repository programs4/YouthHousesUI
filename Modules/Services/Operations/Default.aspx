<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Services_Operations_Default" %>

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

                <div class="col-md-12">
                    Ən az istifadə müddəti (dəq):<br />
                    <asp:TextBox ID="txtMinMinute" AutoCompleteType="Disabled" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

            </div>
        </div>

        <div class="col-md-4">

            <div class="col-md-12">
                Gün ərzində istifadə sayı:<br />
                <asp:TextBox ID="txtDailyUse" AutoCompleteType="Disabled" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Saat aralığında istifadə sayı:<br />
                <asp:TextBox ID="txtUseBetweenHour" AutoCompleteType="Disabled" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

            <div class="col-md-12">
                Bitmə tarixi:<br />
                <asp:TextBox ID="txtExpireDate" AutoCompleteType="Disabled" CssClass="form_date form-control" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>

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

    </asp:Panel>
</asp:Content>

