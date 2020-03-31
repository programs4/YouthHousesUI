<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Modules_Surveys_Operations_Default" %>

<%@ Import Namespace="YouthHousesLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:Panel ID="pnlSurveys" runat="server" class="panel panel-default">
        <div class="panel-heading">Sorğu</div>
        <div class="panel-body">

            <asp:Panel ID="pnlControls" runat="server" class="row">
                <div class="col-md-4">
                    <div class="row">

                        <div class="col-md-12">
                            Ümumi başlıq:<br />
                            <asp:TextBox ID="txtTitle" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>

                        <div class="col-md-12">
                            Sual:<br />
                            <asp:TextBox ID="txtQuestions" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>

                        <div class="col-md-12">
                            Sualın statusu:<br />
                            <asp:DropDownList ID="dListStatus" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control"></asp:DropDownList>
                            <br />
                            <br />
                        </div>

                        <div class="col-md-12 text-right">
                            <br />
                            <asp:Button ID="btnSave" runat="server" Height="40px" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Height="40px" CssClass="op-btn op-btn-default" Text="İMTİNA" OnClick="btnCancel_Click" />
                            <br />
                            <br />
                        </div>

                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>


    <asp:Panel ID="pnlAnswers" runat="server" class="panel panel-default">
        <div class="panel-heading">Cavablar</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-6">
                            Cavab:<br />
                            <asp:TextBox ID="txtAnswer" AutoCompleteType="Disabled" CssClass="form-control" runat="server"></asp:TextBox>
                            <br />
                            <br />
                        </div>
                        <div class="col-md-6">
                            <br />
                            <asp:Button ID="btnSaveAnswers" runat="server" Height="40px" CommandArgument="0" CssClass="op-btn op-btn-gray" Text="YADDA SAXLA" OnClick="btnSaveAnswers_Click" />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="grdSurveysAnswers" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
                        <Columns>
                            <asp:TemplateField HeaderText="S/s">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Answer" HeaderText="Cavab">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="VotesCount" HeaderText="Səslərin sayı">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Tarix">
                                <ItemTemplate>
                                    <%#((DateTime)Eval("CreatedDate")).ToString("dd.MM.yyyy")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditAnswer" CommandArgument='<%#Eval("Id")%>' CommandName='<%#Eval("Answer")%>' Font-Size="11pt" ForeColor="#000" OnClick="lnkEditAnswer_Click" runat="server">
                                       <span class="glyphicon glyphicon-edit"></span> Düzəliş et</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDeletedAnswer" Font-Size="11pt" ForeColor="#cc0000" CommandArgument='<%#Eval("Id")%>' OnClick="lnkDeletedAnswer_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');return false;" runat="server">
                                       <span class="glyphicon glyphicon-trash"></span> Sil</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
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
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlSubscriptions" runat="server" class="panel panel-default">
        <div class="panel-heading">Abunəliklər</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdSurveysSubscriptions" runat="server" AutoGenerateColumns="False" BorderColor="#ebedf3" BorderWidth="1px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="Id">
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
                                            <asp:CheckBox ID="chkSubscriptions" data-id='<%#Eval("Id")%>' data-surveyssubscriptionsid='<%#Eval("surveyssubscriptionsid")%>' data-isactive='<%#Eval("IsActive")%>' Checked='<%#(Eval("IsActive")!=DBNull.Value && Eval("IsActive")._ToInt16()==1)%>' CssClass="Chekbx" AutoPostBack="true" OnCheckedChanged="chkSubscriptions_CheckedChanged" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Name" HeaderText="Gənclər evi">
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
            </div>
        </div>

    </asp:Panel>

</asp:Content>

