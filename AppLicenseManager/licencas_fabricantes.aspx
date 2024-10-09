<%@ Page Title="" Language="C#" MasterPageFile="~/AppLicenseManager.Master" AutoEventWireup="true" CodeBehind="licencas_fabricantes.aspx.cs" Inherits="AppLicenseManager.licencas_fabricantes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Header --%>
    <div class="header-page">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">ALM</a></li>
                <li class="breadcrumb-item"><a href="#">Licenças</a></li>
                <li class="breadcrumb-item active" aria-current="page">Fabricantes</li>
            </ol>
        </nav>
        <h1 class="title-page">Fabricantes</h1>
    </div>

    <%-- Content Page --%>
    <asp:Panel ID="pnlGridFabricantes" runat="server" Visible="true">

        <telerik:RadGrid ID="GridFabricantes" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            AllowSorting="true"
            AllowFilteringByColumn="false"
            OnNeedDataSource="GridFabricantes_NeedDataSource"
            OnItemCommand="GridFabricantes_ItemCommand"
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
                DataKeyNames="cdFabricante"
                CommandItemDisplay="Top">

                <CommandItemTemplate>
                    <telerik:RadToolBar
                        ID="rtbFabricantes" runat="server"
                        Width="100%">
                        <Items>
                            <telerik:RadToolBarButton CssClass="ms-2 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAtualizarFabricantes" runat="server" OnClick="lnkAtualizarFabricantes_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-arrow-rotate-right fa-lg"></i></span>
                                        <span class="text-rtb">Atualizar Fabricantes</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>

                            <telerik:RadToolBarButton CssClass="ms-3 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdicionarFabricante" runat="server" OnClick="lnkAdicionarFabricante_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-industry"></i></span>
                                        <span class="text-rtb">Novo Fabricante</span>
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
                        DataField="cdFabricante"
                        HeaderText="Código Fabricante"
                        UniqueName="cdFabricante"
                        SortExpression="cdFabricante">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="nomeFabricante"
                        HeaderText="Nome fabricante"
                        UniqueName="nomeFabricante"
                        SortExpression="nomeFabricante">
                    </telerik:GridBoundColumn>

                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true"/>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </MasterTableView>
        </telerik:RadGrid>

    </asp:Panel>

    <asp:Panel ID="pnlCadastroFabricante" runat="server" Visible="false">

        <div class="card p-3">
            <h2 class="title-card">Dados Fabricante</h2>

            <div class="row my-2">
                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:TextBox ID="txtFabricante" runat="server" CssClass="form-control" PlaceHolder="Razão Social"></asp:TextBox>
                    <asp:Label ID="lblFabricante" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtFabricante" Text="Razão Social"></asp:Label>
                </div>

                <div class="col-4 col-md-4 col-sm-4 form-floating">
                    <asp:DropDownList ID="ddlStatusFabricante" runat="server" CssClass="form-control" PlaceHolder="Status" DataTextField="status" DataValueField="cdStatus"></asp:DropDownList>
                    <asp:Label ID="lblStatusFabricante" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlStatusFabricante" Text="Status"></asp:Label>
                </div>

            </div>

            <div>
                <asp:Button ID="btn_salvar" runat="server" Text="Salvar Alterações" OnClick="btn_salvar_Click"/>
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar Alterações" OnClick="btn_cancelar_Click"/>
            </div>
        </div>

    </asp:Panel>

    <asp:HiddenField ID="hdfcdFabricante" runat="server" />

</asp:Content>
