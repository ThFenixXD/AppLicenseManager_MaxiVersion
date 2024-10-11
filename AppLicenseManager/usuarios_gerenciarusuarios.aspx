<%@ Page Title="" Language="C#" MasterPageFile="~/AppLicenseManager.Master" AutoEventWireup="true" CodeBehind="usuarios_gerenciarusuarios.aspx.cs" Inherits="AppLicenseManager.usuarios_gerenciarusuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Header --%>
    <div class="header-page">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">ALM</a></li>
                <li class="breadcrumb-item"><a href="#">Usuários</a></li>
                <li class="breadcrumb-item active" aria-current="page">Gerênciamento de Usuários</li>
            </ol>
        </nav>
        <h1 class="title-page">Gerenciamento de Usuários</h1>
    </div>

    <%-- Content Page --%>


    <asp:Panel ID="pnlGridUsuarios" runat="server" Visible="true">

        <telerik:RadGrid ID="GridUsuarios" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            AllowSorting="true"
            AllowFilteringByColumn="false"
            OnNeedDataSource="GridUsuarios_NeedDataSource"
            OnItemCommand="GridUsuarios_ItemCommand"
            OnItemDataBound="GridUsuarios_ItemDataBound">

            <ClientSettings>
                <Scrolling AllowScroll="false" />
            </ClientSettings>

            <GroupingSettings CollapseTooltip="Recolher Grupo"
                ExpandTooltip="Expandir Grupo"
                GroupContinuedFormatString="... continua na página anterior. "
                GroupContinuesFormatString="Grupo continua na próxima página"
                GroupSplitDisplayFormat="Mostrando {0} de {1} itens."
                UnGroupButtonTooltip="Clique aqui para desagrupar"
                UnGroupTooltip="Arraste para fora para desagrupar" />

            <SortingSettings
                SortedAscToolTip="Ordenar Ascendente"
                SortedDescToolTip="Ordenar Descendente"
                SortToolTip="Clique aqui para ordenar" />

            <HierarchySettings
                CollapseTooltip="Recolher"
                ExpandTooltip="Expandir" />

            <ExportSettings>
                <Pdf
                    AllowPrinting="False"
                    PageWidth="">
                </Pdf>
            </ExportSettings>

            <MasterTableView
                DataKeyNames="cdUsuario"
                CommandItemDisplay="Top">

                <CommandItemTemplate>
                    <telerik:RadToolBar
                        ID="rtbUsuarios" runat="server"
                        Width="100%">
                        <Items>
                            <telerik:RadToolBarButton CssClass="ms-2 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAtualizarUsuario" runat="server" OnClick="lnkAtualizarUsuario_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-arrow-rotate-right fa-lg"></i></span>
                                        <span class="text-rtb">Atualizar Usuários</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>

                            <telerik:RadToolBarButton CssClass="ms-3 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdicionarUsuario" runat="server" OnClick="lnkAdicionarUsuario_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-user-plus fa-lg"></i></span>
                                        <span class="text-rtb">Novo Usuário</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>
                </CommandItemTemplate>


                <Columns>

                    <%-- Status --%>
                    <telerik:GridTemplateColumn
                        UniqueName="statusIcon" HeaderText="Status">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1em" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1em" />
                        <ItemTemplate>
                            <i id="status" class='<%# GetFontAwesomeIcon(Eval("cdStatus")) %>' aria-hidden="true"></i>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>



                    <%-- Botão Opções --%>
                    <telerik:GridTemplateColumn
                        UniqueName="Op" HeaderText="Op">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1em" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1em" />

                        <ItemTemplate>
                            <div class="dropdown show">
                                <button
                                    class="btn-opGrid btn btn-primary"
                                    type="button"
                                    data-bs-toggle="dropdown"
                                    aria-expanded="false">
                                    <i class="fa-solid fa-caret-down"></i>
                                </button>
                                <ul class="dropdown-opGrid dropdown-menu">
                                    <li>
                                        <asp:LinkButton ID="lnkEditar" CssClass="lnkOp" runat="server" CommandName="opEditar">
                                            <span class="icon-op"><i class="fa-solid fa-pen-to-square"></i></span>
                                            <span class="text-op">Editar</span>
                                        </asp:LinkButton>

                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkStatus" CssClass="lnkOp" runat="server" CommandName="opAlteraStatus">
                                            <span class="icon-op"><i class="fa-solid fa-ban"></i></span>
                                            <span class="text-op">Bloq./Desb.</span>
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkExcluir" CssClass="lnkOp" runat="server" CommandName="opDelete">
                                            <span class="icon-op"><i class="fa-solid fa-trash-can"></i></i></span>
                                            <span class="text-op">Excluir</span>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <%-- Data Columns --%>
                    <telerik:GridBoundColumn
                        DataField="cdUsuario"
                        HeaderText="Código Usuário"
                        UniqueName="cdUsuario"
                        SortExpression="cdUsuario">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="nomeUsuario"
                        HeaderText="Nome usuário"
                        UniqueName="nomeUsuario"
                        SortExpression="nomeUsuario">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="email"
                        HeaderText="E-mail"
                        UniqueName="email"
                        SortExpression="email">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="senha"
                        HeaderText="Senha"
                        UniqueName="senha"
                        SortExpression="senha">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="tipousuario"
                        HeaderText="Tipo usuário"
                        UniqueName="tipousuario"
                        SortExpression="tipousuario">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="dataCadastro"
                        HeaderText="Data de cadastro"
                        UniqueName="dataCadastro"
                        SortExpression="dataCadastro"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </telerik:GridBoundColumn>

                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>

    <asp:UpdatePanel ID="upPnlCadastroUsuarios" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Panel ID="pnlCadastroUsuarios" runat="server" Visible="false">

                <div class="card">
                    <div class="card-header">
                        <ul class="nav nav-tabs card-header-tabs">
                            <li class="nav-item">
                                <asp:LinkButton ID="lnkDadosUsuario" runat="server" CssClass="nav-link active" Text="Dados Usuário" OnClick="lnkDadosUsuario_Click"></asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton ID="lnkPermissoesUsuario" runat="server" CssClass="nav-link" Text="Permissões Usuário" OnClick="lnkPermissoesUsuario_Click"></asp:LinkButton>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">

                        <asp:Panel ID="pnlDadosUsuario" runat="server">

                            <div class="card p-3">

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
                                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control" DataTextField="tipoUsuario" DataValueField="cdTipoUsuario" PlaceHolder="Tipo usuario"></asp:DropDownList>
                                        <asp:Label ID="lblTipoUsuario" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlTipoUsuario" Text="Tipo usuário"></asp:Label>
                                    </div>

                                    <div class="col-4 col-md-4 col-sm-4 form-floating">
                                        <asp:DropDownList ID="ddlStatusUsuario" runat="server" CssClass="form-control" DataTextField="status" DataValueField="cdStatus" PlaceHolder="Status usuário"></asp:DropDownList>
                                        <asp:Label ID="lblStatusUsuario" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlStatusUsuario" Text="Status usuário"></asp:Label>
                                    </div>

                                </div>

                            </div>

                            <div class="py-3">
                                <asp:Button ID="btn_salvar" runat="server" Text="Salvar Alterações" CssClass="btn_op btn_salvar" OnClick="btn_salvar_Click" />
                                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar Alterações" CssClass="btn_op btn_cancelar" OnClick="btn_cancelar_Click" />
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlPermissoesUsuario" runat="server" Visible="false">

                            <div class="card p-3">

                                <telerik:RadTreeView
                                    ID="rtvPermissoes"
                                    runat="server"
                                    CheckBoxes="true"
                                    TriStateCheckBoxes="true"
                                    CheckChildNodes="true"
                                    DataFieldID="cdMenu"
                                    DataFieldParentID="cdMenuPai"
                                    DataValueField="cdMenu"
                                    DataTextField="descMenu"
                                    OnNodeDataBound="rtvPermissoes_NodeDataBound">
                                </telerik:RadTreeView>

                            </div>

                            <div class="py-3">
                                <asp:Button ID="btn_salvarPermissoes" runat="server" Text="Salvar Alterações" CssClass="btn_op btn_salvar" OnClick="btn_salvarPermissoes_Click" />
                                <asp:Button ID="btn_cancelarPermissoes" runat="server" Text="Cancelar Alterações" CssClass="btn_op btn_cancelar" OnClick="btn_cancelarPermissoes_Click" />
                            </div>


                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:HiddenField ID="hdfcdUsuario" runat="server" />
</asp:Content>
