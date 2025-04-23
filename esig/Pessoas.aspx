<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pessoas.aspx.cs" Inherits="esig.Pessoas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<asp:GridView ID="pessoasGridView" runat="server"
    CssClass="table table-striped table-bordered center"
    AutoGenerateColumns="False"
    AutoGenerateEditButton="False"
    DataKeyNames="id"   
    AllowPaging="True" PageSize="20"
    OnPageIndexChanging="pessoasGridView_PageIndexChanging"
    OnRowCommand="pessoasGridView_RowCommand">
    
    <Columns>
        <asp:BoundField DataField="id" Visible="false" />
        <asp:BoundField DataField="nome" HeaderText="Nome" ReadOnly="True" />
        <asp:BoundField DataField="usuario" HeaderText="Usuario" ReadOnly="True" />
        <asp:BoundField DataField="email" HeaderText="Email" DataFormatString="{0:N2}" HtmlEncode="false" ReadOnly="False" />
       <asp:TemplateField>
            <ItemTemplate>
            <asp:Button ID="btnEditar" runat="server"
                CommandName="Editar"
                CommandArgument='<%# Container.DataItemIndex %>'
                Text="Editar" />
        </ItemTemplate>
</asp:TemplateField>
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
