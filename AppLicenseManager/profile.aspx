<%@ Page Title="" Language="C#" MasterPageFile="~/AppLicenseManager.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="AppLicenseManager.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Header --%>
    <div class="header-page">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">ALM</a></li>
                <li class="breadcrumb-item"><a href="#">Usuários</a></li>
                <li class="breadcrumb-item active" aria-current="page">Ver Perfil</li>
            </ol>
        </nav>
        <h1 class="title-page">Ver Perfil</h1>
    </div>

    <%-- Content Page --%>

    <asp:Panel ID="pnlPerfilUsuario" runat="server" Visible="true">

        <div class="card p-3">
            <h2 id="title-cadastroUsuarios">Dados Usuário</h2>

            <div class="row">
                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:TextBox ID="txtNomeUsuario" runat="server" CssClass="form-control" PlaceHolder="Nome usuário"></asp:TextBox>
                    <asp:Label ID="lblNomeUsuario" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtNomeUsuario" Text="Nome usuário"></asp:Label>
                </div>

                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" PlaceHolder="E-mail"></asp:TextBox>
                    <asp:Label ID="lblEmail" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtEmail" Text="E-mail"></asp:Label>
                </div>

                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" TextMode="Password" PlaceHolder="Nome usuário"></asp:TextBox>
                    <asp:Label ID="lblSenha" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtSenha" Text="Senha"></asp:Label>
                </div>

            </div>

            <div class="row my-3">

                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control" DataTextField="tipoUsuario" DataValueField="cdTipoUsuario" PlaceHolder="Tipo usuario" Enabled="false"></asp:DropDownList>
                    <asp:Label ID="lblTipoUsuario" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlTipoUsuario" Text="Tipo usuário"></asp:Label>
                </div>

                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataTextField="status" DataValueField="cdStatus" PlaceHolder="Status usuário" Enabled="false"></asp:DropDownList>
                    <asp:Label ID="lblStatusUsuario" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlStatus" Text="Status usuário"></asp:Label>
                </div>

            </div>

            <div>
                <asp:Button ID="btn_salvar" runat="server" Text="Salvar Alterações" OnClick="btn_salvar_Click" />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar Alterações" OnClick="btn_cancelar_Click" />
            </div>
        </div>

    </asp:Panel>

    <asp:HiddenField ID="hdfcdUsuario" runat="server" />

</asp:Content>
