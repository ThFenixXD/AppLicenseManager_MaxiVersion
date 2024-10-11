<%@ Page Title="" Language="C#" MasterPageFile="~/AppLicenseManager.Master" AutoEventWireup="true" CodeBehind="localizacao_setores.aspx.cs" Inherits="AppLicenseManager.localizacao_setores" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Header --%>
    <div class="header-page">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">ALM</a></li>
                <li class="breadcrumb-item"><a href="#">Localização</a></li>
                <li class="breadcrumb-item active" aria-current="page">Setores</li>
            </ol>
        </nav>
        <h1 class="title-page">Setores</h1>
    </div>

    <%-- Content Page --%>
    <asp:Panel ID="pnlGridSetores" runat="server" Visible="true">

        <telerik:RadGrid ID="GridSetores" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            AllowSorting="true"
            AllowFilteringByColumn="false"
            OnNeedDataSource="GridSetores_NeedDataSource"
            OnItemCommand="GridSetores_ItemCommand"
            Width="100%">

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
                DataKeyNames="cdSetor"
                CommandItemDisplay="Top">

                <CommandItemTemplate>
                    <telerik:RadToolBar
                        ID="rtbSetores" runat="server"
                        Width="100%">
                        <Items>
                            <telerik:RadToolBarButton CssClass="ms-2 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAtualizarSetores" runat="server" OnClick="lnkAtualizarSetores_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-arrow-rotate-right fa-lg"></i></span>
                                        <span class="text-rtb">Atualizar Setores</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>

                            <telerik:RadToolBarButton CssClass="ms-3 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdicionarSetores" runat="server" OnClick="lnkAdicionarSetores_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-location-dot"></i></span>
                                        <span class="text-rtb">Novo Setor</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>
                </CommandItemTemplate>

                <Columns>

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
                                            <span class="icon-op"><i class="fa-solid fa-user-pen"></i></span>
                                            <span class="text-op">Editar</span>
                                        </asp:LinkButton>
                                        <li>
                                            <asp:LinkButton ID="lnkExcluir" CssClass="lnkOp" runat="server" CommandName="opDelete">
                                            <span class="icon-op"><i class="fa-solid fa-trash-can"></i></i></span>
                                            <span class="text-op">Excluir</span>
                                            </asp:LinkButton>
                                        </li>
                                    </li>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <%-- Data Columns --%>
                    <telerik:GridBoundColumn
                        DataField="cdSetor"
                        HeaderText="Código Setor"
                        UniqueName="cdSetor"
                        SortExpression="cdSetor">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="nomeSetor"
                        HeaderText="Nome Setor"
                        UniqueName="nomeSetor"
                        SortExpression="nomeSetor">
                    </telerik:GridBoundColumn>

                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </MasterTableView>
        </telerik:RadGrid>

    </asp:Panel>

    <asp:Panel ID="pnlCadastroSetores" runat="server" Visible="false">

        <div class="card p-3">
            <h2 class="title-card">Dados Setores</h2>

            <div class="row my-2">
                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:TextBox ID="txtSetor" runat="server" CssClass="form-control" PlaceHolder="Setor"></asp:TextBox>
                    <asp:Label ID="lblSetor" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtSetor" Text="Setor"></asp:Label>
                </div>
            </div>

            <div>
                <asp:Button ID="btn_salvar" runat="server" Text="Salvar Alterações" CssClass="btn_op btn_salvar" OnClick="btn_salvar_Click" />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar Alterações" CssClass="btn_op btn_cancelar" OnClick="btn_cancelar_Click" />
            </div>
        </div>

    </asp:Panel>

    <asp:HiddenField ID="hdfcdSetor" runat="server" />

</asp:Content>
