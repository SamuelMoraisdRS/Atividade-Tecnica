<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPessoa.aspx.cs" Inherits="esig.EditarPessoa" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main >
        <asp:Label ID="lblID" runat="server" Visible="false" />
        <asp:TextBox ID="editNome" runat="server" />
        <asp:TextBox ID="editEmail" runat="server" />
        <asp:TextBox ID="editUsuario" runat="server" />
        <asp:TextBox ID="editCargo" runat="server" />
        <asp:TextBox ID="editTelefone" runat="server" />
        <asp:TextBox ID="editCEP" runat="server" />
        <asp:TextBox ID="editCidade" runat="server" />
        <asp:TextBox ID="editEndereco" runat="server" />
        <asp:TextBox ID="editPais" runat="server" />
        <asp:TextBox ID="editDataNascimento" runat="server" />

        <asp:RequiredFieldValidator 
               ID="validaData" runat="server"
               ControlToValidate="editDataNascimento" 
               ForeColor="Red"
               Display="Dynamic" />
<asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />

    </main>
</asp:Content>
