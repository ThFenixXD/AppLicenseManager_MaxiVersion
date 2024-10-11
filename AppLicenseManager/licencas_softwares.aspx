<%@ Page Title="" Language="C#" MasterPageFile="~/AppLicenseManager.Master" AutoEventWireup="true" CodeBehind="licencas_softwares.aspx.cs" Inherits="AppLicenseManager.licencas_produtos" %>

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
                <li class="breadcrumb-item active" aria-current="page">Softwares</li>
            </ol>
        </nav>
        <h1 class="title-page">Softwares</h1>
    </div>

    <%-- Content Page --%>


    <asp:Panel ID="pnlGridSoftwares" runat="server" Visible="true">

        <telerik:RadGrid ID="GridSoftwares" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            AllowSorting="true"
            AllowFilteringByColumn="false"
            OnNeedDataSource="GridSoftwares_NeedDataSource"
            OnItemCommand="GridSoftwares_ItemCommand">

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
                DataKeyNames="cdSoftware"
                CommandItemDisplay="Top">

                <CommandItemTemplate>
                    <telerik:RadToolBar
                        ID="rtbSoftware" runat="server"
                        Width="100%">
                        <Items>
                            <telerik:RadToolBarButton CssClass="ms-2 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAtualizarSoftwares" runat="server" OnClick="lnkAtualizarSoftwares_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-arrow-rotate-right fa-lg"></i></span>
                                        <span class="text-rtb">Atualizar Softwares</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>

                            <telerik:RadToolBarButton CssClass="ms-3 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdicionarSoftwares" runat="server" OnClick="lnkAdicionarSoftwares_Click">
                                        <span class="icon-rtb"><i class="fa-regular fa-window-restore"></i></span>
                                        <span class="text-rtb">Novo Software</span>
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
                        DataField="cdSoftware"
                        HeaderText="Código Software"
                        UniqueName="cdSoftware"
                        SortExpression="cdSoftware">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="nomeSoftware"
                        HeaderText="Nome Software"
                        UniqueName="nomeSoftware"
                        SortExpression="nomeSoftware">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="nomeFabricante"
                        HeaderText="Nome Fabricante"
                        UniqueName="nomeFabricante"
                        SortExpression="nomeFabricante">
                    </telerik:GridBoundColumn>

                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>

    <asp:Panel ID="pnlCadastroSoftware" runat="server" Visible="false">

        <div class="card p-3">
            <h2 id="title-cadastro">Dados Software</h2>

            <asp:UpdatePanel ID="upPnlDadosSoftware" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:TextBox ID="txtNomeSoftware" runat="server" CssClass="form-control" PlaceHolder="Nome software"></asp:TextBox>
                            <asp:Label ID="lblNomeSoftware" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtNomeSoftware" Text="Nome software"></asp:Label>
                        </div>

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlStatusSoftware" runat="server" CssClass="form-control" PlaceHolder="Status" DataTextField="status" DataValueField="cdStatus"></asp:DropDownList>
                            <asp:Label ID="lblStatusSoftware" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlStatusSoftware" Text="Status"></asp:Label>
                        </div>

                    </div>

                    <div class="row my-3">

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlFabricante" runat="server" CssClass="form-control" DataTextField="nomeFabricante" DataValueField="cdFabricante" PlaceHolder="Fabricante"></asp:DropDownList>
                            <asp:Label ID="lblFabricante" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlFabricante" Text="Fabricante"></asp:Label>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <div>
                <asp:Button ID="btn_salvar" runat="server" Text="Salvar Alterações" CssClass="btn_op btn_salvar" OnClick="btn_salvar_Click" />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar Alterações" CssClass="btn_op btn_cancelar" OnClick="btn_cancelar_Click" />
            </div>
        </div>

    </asp:Panel>

    <asp:HiddenField ID="hdfcdSoftware" runat="server" />

</asp:Content>
