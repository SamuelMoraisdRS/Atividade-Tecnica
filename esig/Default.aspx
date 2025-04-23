<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="esig.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<asp:GridView ID="empregadosGridView" runat="server"
    CssClass="table table-striped table-bordered center"
    AutoGenerateColumns="False"
    AutoGenerateEditButton="True"
    DataKeyNames="id"
    OnRowEditing="empregadosGridView_RowEditing"
    OnRowUpdating="empregadosGridView_RowUpdating"
    OnRowCancelingEdit="empregadosGridView_RowCancelingEdit"
    AllowPaging="True" PageSize="20"
    OnPageIndexChanging="empregadosGridView_PageIndexChanging">
    
    <Columns>
        <asp:BoundField DataField="Id" Visible="false" />
        <asp:BoundField DataField="nome_pessoa" HeaderText="Nome" ReadOnly="True" />
        <asp:BoundField DataField="nome_cargo" HeaderText="Cargo" ReadOnly="True" />
        <asp:BoundField DataField="salario" HeaderText="Salário" DataFormatString="{0:N2}" HtmlEncode="false" ReadOnly="False" />
    </Columns>

    <PagerSettings Mode="Numeric" Position="Bottom" />
    <PagerTemplate>
        <div class="pagination">
            <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="NavigatePage" CommandArgument="Prev" />
            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="NavigatePage" CommandArgument="Next" />
        </div>
    </PagerTemplate>

</asp:GridView>

</asp:Content>
