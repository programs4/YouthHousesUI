<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Calendar_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" href="/css/New-style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <asp:Panel ID="PnlModalDialog" runat="server" CssClass="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="myModalLabel">
                                <asp:Literal ID="LtrModalTitle" runat="server" Text="Ətraflı"></asp:Literal></h4>
                        </div>
                        <div class="modal-body">

                            <table class="EventDetail table table-bordered table-striped" style="width: 98%">

                                <tr>
                                    <td>Təşkilatın adı:
                                    </td>
                                    <td>
                                        <asp:Label ID="LblOrgname" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Başlıq:
                                    </td>
                                    <td>
                                        <asp:Label ID="LblTitle" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Tədbirin başlanma tarixi:
                                    </td>
                                    <td>
                                        <asp:Label ID="LblStartDate" runat="server"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>Tədbirin bitmə tarixi:
                                    </td>
                                    <td>
                                        <asp:Label ID="LblEndDate" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Əlavə edilmə tarixi:
                                    </td>
                                    <td>
                                        <asp:Label ID="LblCreatedDate" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Ətraflı:
                                    </td>
                                    <td>
                                        <asp:Label ID="LblDescription" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="text-right">
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">
                                    <span class="glyphicon glyphicon-pencil" ></span>
                                    Düzəliş et</asp:LinkButton>
                                    </td>
                                </tr>

                            </table>

                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>

            <asp:HiddenField ID="HiddenDataId" runat="server" ClientIDMode="Static" />
            <asp:Button ID="BtnDetail" runat="server" OnClick="BtnDetail_Click" Style="visibility: hidden" ClientIDMode="Static" />

            <div class="calendar-container">
                <div class="calendar-control">

                    <asp:LinkButton ID="LnkBtnPrev" runat="server" CssClass="prev" OnClick="LnkBtnPrev_Click">
                Sen. 2019
                    </asp:LinkButton>

                    <p class="current-month">
                        <asp:DropDownList ID="DlistMonth" runat="server" CssClass="form-control" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="DlistMonth_SelectedIndexChanged">
                        </asp:DropDownList>

                        <asp:DropDownList ID="DlistYear" runat="server" CssClass="form-control" Width="70px" AutoPostBack="True" OnSelectedIndexChanged="DlistYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </p>

                    <asp:LinkButton ID="LnkBtnNext" runat="server" CssClass="next" OnClick="LnkBtnNext_Click">
                Noy. 2019
                    </asp:LinkButton>
                    &nbsp;
            |&nbsp;&nbsp;
            <asp:LinkButton ID="LnkToday" runat="server" CssClass="next" OnClick="LnkToday_Click">
              Bügün
            </asp:LinkButton>

                </div>

                <div class="searchControl">

                    <asp:DropDownList ID="DlistOrganizations" runat="server" CssClass="form-control" Width="300px" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="DlistOrganizations_SelectedIndexChanged">
                    </asp:DropDownList>

                    &nbsp;&nbsp;

                    <asp:DropDownList ID="DlistTypes" runat="server" CssClass="form-control" Width="200px" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="DlistTypes_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <table class="calendar-self">
                    <thead>
                        <tr>
                            <th>Bazar ertəsi</th>
                            <th>Çərşənbə axşamı</th>
                            <th>Çərşənbə</th>
                            <th>Cümə axşamı</th>
                            <th>Cümə</th>
                            <th class="weekend">Şənbə</th>
                            <th class="weekend">Bazar</th>
                        </tr>
                    </thead>

                    <tbody>

                        <asp:Literal ID="LtrCalenderContent" runat="server"></asp:Literal>

                    </tbody>
                </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

