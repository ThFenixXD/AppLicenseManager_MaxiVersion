<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AppLicenseManager.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <%-- Bootstrap CSS--%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">

    <%-- Bootstrap Icons CSS --%>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">

    <%-- Internal CSS --%>
    <link href="Util/style.css" rel="stylesheet" />

    <%-- Font Family --%>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Afacad+Flux:wght@100..1000&display=swap" rel="stylesheet">

    <%-- Bootstrap Js--%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

    <%-- Sweet Alert --%>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.12.4/dist/sweetalert2.all.min.js"></script>

    <%-- Alerta --%>
    <script src="Util/Alerta.js"></script>



    <title>ALM Login</title>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>

        <div id="login-page" class="container-fluid">
            <asp:Panel ID="pnlLogin" class="login-container container m-auto w-25" runat="server">
                <div class="row text-center">

                    <img id="logo" src="Images/Logo Maxi.png" alt="Alternate Text" />
                    <h1>Gerenciador de Licenças</h1>

                    <p>Entre com seus dados para autenticar</p>
                </div>

                <div class="content-login row">
                    <div class="form-floating">
                        <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="E-mail" CssClass="form-label"></asp:Label>
                    </div>
                    <span class="icon-login"></span>
                </div>

                <div class="content-login row my-2">
                    <div class="form-floating">
                        <asp:TextBox ID="txtSenha" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblSenha" runat="server" AssociatedControlID="txtSenha" Text="Senha" CssClass="form-label"></asp:Label>
                    </div>
                    <span class="icon-login"></span>
                </div>

                <div class="row">
                    <asp:Button ID="btn_logon" runat="server" Text="Entrar" OnClick="btn_logon_Click" />
                </div>
            </asp:Panel>
        </div>


    </form>
</body>
</html>
