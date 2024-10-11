<%@ Page Title="" Language="C#" MasterPageFile="~/AppLicenseManager.Master" AutoEventWireup="true" CodeBehind="licencas_chaveativacao.aspx.cs" Inherits="AppLicenseManager.licencas_chaveativacao" %>

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
                <li class="breadcrumb-item active" aria-current="page">Chaves de Ativação</li>
            </ol>
        </nav>
        <h1 class="title-page">Chaves de Ativação</h1>
    </div>

    <%-- Content Page --%>
    <asp:Panel ID="pnlGridChaves" runat="server" Visible="true">

        <telerik:RadGrid ID="GridChaves" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            AllowSorting="true"
            AllowFilteringByColumn="false"
            OnNeedDataSource="GridChaves_NeedDataSource"
            OnItemCommand="GridChaves_ItemCommand">

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
                DataKeyNames="cdChaveAtivacao"
                CommandItemDisplay="Top">

                <CommandItemTemplate>
                    <telerik:RadToolBar
                        ID="rtbChave" runat="server"
                        Width="100%">
                        <Items>
                            <telerik:RadToolBarButton CssClass="ms-2 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAtualizarChaves" runat="server" OnClick="lnkAtualizarChaves_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-arrow-rotate-right fa-lg"></i></span>
                                        <span class="text-rtb">Atualizar Chaves</span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>

                            <telerik:RadToolBarButton CssClass="ms-3 rtb-menu">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdicionarChave" runat="server" OnClick="lnkAdicionarChave_Click">
                                        <span class="icon-rtb"><i class="fa-solid fa-key"></i></span>
                                        <span class="text-rtb">Nova Chave</span>
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
                        DataField="cdChaveAtivacao"
                        HeaderText="Código Chave de Ativação"
                        UniqueName="cdChaveAtivacao"
                        SortExpression="cdChaveAtivacao">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="chaveAtivacao"
                        HeaderText="Chave de Ativação"
                        UniqueName="chaveAtivacao"
                        SortExpression="chaveAtivacao">
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

                    <telerik:GridBoundColumn
                        DataField="tipolicenca"
                        HeaderText="Tipo de Licença"
                        UniqueName="tipolicenca"
                        SortExpression="tipolicenca">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="data_aquisicao"
                        HeaderText="Data de Aquisição"
                        UniqueName="data_aquisicao"
                        SortExpression="data_aquisicao"
                        DataFormatString="{0:dd/MM/yyy}">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="data_validade"
                        HeaderText="Data de Validade"
                        UniqueName="data_validade"
                        SortExpression="data_validade"
                        DataFormatString="{0:dd/MM/yyy}">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn
                        DataField="hostname"
                        HeaderText="Máquina"
                        UniqueName="hostname"
                        SortExpression="hostname">
                    </telerik:GridBoundColumn>

                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>

    <asp:Panel ID="pnlCadastroChaves" runat="server" Visible="false">

        <div class="card p-3">
            <h2 id="title-cadastro">Dados Chaves de Ativação</h2>

            <asp:UpdatePanel ID="upPnlDados" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlFabricante" runat="server" CssClass="form-control" PlaceHolder="Fabricante" DataTextField="nomeFabricante" DataValueField="cdFabricante" OnSelectedIndexChanged="ddlFabricante_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:Label ID="lblFabricante" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlFabricante" Text="Fabricante"></asp:Label>
                        </div>

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlSoftware" runat="server" CssClass="form-control" PlaceHolder="Software" DataTextField="nomeSoftware" DataValueField="cdSoftware"></asp:DropDownList>
                            <asp:Label ID="lblSoftware" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlSoftware" Text="Software"></asp:Label>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlTipoLicenca" runat="server" CssClass="form-control" PlaceHolder="Tipo de Licença" DataTextField="tipoLicenca" DataValueField="cdTipoLicenca" OnSelectedIndexChanged="ddlTipoLicenca_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:Label ID="lblTipolicenca" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlTipoLicenca" Text="Tipo de Licença"></asp:Label>
                        </div>

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:TextBox ID="txtDataAquisicao" runat="server" CssClass="form-control" PlaceHolder="Data de Aquisição (dd/mm/aaaa)"></asp:TextBox>
                            <asp:Label ID="lblDataAquisição" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtDataAquisicao" Text="Data de Aquisição (dd/mm/aaaa)"></asp:Label>
                        </div>

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:TextBox ID="txtDataValidade" runat="server" CssClass="form-control" PlaceHolder="Data de Validade (dd/mm/aaaa)"></asp:TextBox>
                            <asp:Label ID="lblDataValidade" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtDataValidade" Text="Data de Validade (dd/mm/aaaa)"></asp:Label>
                        </div>
                    </div>

                    <div class="row mt-3">
                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlMaquina" runat="server" CssClass="form-control" PlaceHolder="Máquina" DataTextField="hostname" DataValueField="cdMaquina" ></asp:DropDownList>
                            <asp:Label ID="lblMaquina" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlMaquina" Text="Máquina"></asp:Label>
                        </div>

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlSetor" runat="server" CssClass="form-control" PlaceHolder="Setor" DataTextField="nomeSetor" DataValueField="cdSetor"></asp:DropDownList>
                            <asp:Label ID="lblSetor" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlSetor" Text="Setor"></asp:Label>
                        </div>
                    </div>

                    <div class="row my-3">
                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:TextBox ID="txtChaveAtivacao" runat="server" CssClass="form-control" PlaceHolder="Chave de Ativação"></asp:TextBox>
                            <asp:Label ID="lblChaveAtivacao" runat="server" CssClass="form-label ms-2" AssociatedControlID="txtChaveAtivacao" Text="Chave de Ativação"></asp:Label>
                        </div>

                        <div class="col-4 col-md-4 col-sm-4 form-floating">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" PlaceHolder="Status" DataTextField="status" DataValueField="cdStatus"></asp:DropDownList>
                            <asp:Label ID="lblStatus" runat="server" CssClass="form-label ms-2" AssociatedControlID="ddlStatus" Text="Status"></asp:Label>
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

    <asp:HiddenField ID="hdfcdChaveAtivacao" runat="server" />

</asp:Content>
