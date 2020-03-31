<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Registration_Operations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Panel ID="pnlControls" runat="server" class="row">

        <div class="col-md-4">
            <div class="row">
                <div class="col-md-12">
                    Sosial status:<br />
                    <asp:DropDownList ID="dListSocialStatus" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Sənəd nömrəsi:<br />
                    <asp:TextBox ID="txtDocumentNumber" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Adı:<br />
                    <asp:TextBox ID="txtName" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Soyadı:<br />
                    <asp:TextBox ID="txtSurname" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>


                <div class="col-md-12">
                    Ata adı:<br />
                    <asp:TextBox ID="txtPatronymic" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    İstifadəçi adı:<br />
                    <asp:TextBox ID="txtUsername" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>
                <asp:Panel ID="pnlPassword" runat="server">

                    <div class="col-md-12">
                        Şifrə:<br />
                        <asp:TextBox ID="txtPassword" TextMode="Password" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                        <br />
                        <br />
                    </div>

                    <div class="col-md-12">
                        Təkrar şifrə:<br />
                        <asp:TextBox ID="txtPasswordRepeat" TextMode="Password" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                </asp:Panel>
                <div class="col-md-12">
                    Status:<br />
                    <asp:DropDownList ID="dListStatus" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                    <br />
                    <br />
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="row">
                <div class="col-md-12">
                    Doğum tarixi:<br />
                    <asp:TextBox ID="txtBirthDate" CssClass="form_date form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Cinsi:<br />
                    <asp:DropDownList ID="dListGender" CssClass="form-control" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Ünvan:<br />
                    <asp:TextBox ID="txtAddress" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Email:<br />
                    <asp:TextBox ID="txtEmail" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Əlaqə nömrəsi:<br />
                    <asp:TextBox ID="txtContact" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>

                <div class="col-md-12">
                    Qeyd:<br />
                    <asp:TextBox ID="txtDescription" CssClass="form-control" Height="114px" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </div>
                <div class="col-md-12 text-right">
                    <asp:Button ID="btnSave" runat="server" Height="40px" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Height="40px" CssClass="op-btn op-btn-default" Text="İMTİNA" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <div class="col-md-4 text-left">
            <br />
            <asp:Image ID="imgUser" Width="250px" Height="300px" ImageUrl="/uploads/0.jpg" onerror="src='/uploads/0.jpg'" runat="server" />
            <br />
            <br />
            <asp:FileUpload ID="flUpImg" Width="250px" CssClass="form-control" runat="server" />
        </div>

    </asp:Panel>
</asp:Content>

