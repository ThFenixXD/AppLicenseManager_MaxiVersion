using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLicenseManager.Util;
using Telerik.Web.UI;

namespace AppLicenseManager
{
    public partial class usuarios_gerenciarusuarios : PageBase
    {
        #region Metodos
        protected void LimpaCampos()
        {
            try
            {
                txtNomeUsuario.Text =
                txtEmail.Text =
                txtSenha.Text =
                hdfcdUsuario.Value = "";

                ddlTipoUsuario.Items.Clear();
                PopulaDdlTipoUsuario();

                ddlStatusUsuario.Items.Clear();
                PopulaDdlStatus();
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: LimpaCampos()", "error: " + ex.ToString(), "error");
            }
        }

        protected void PopulaCamposUsuario(int _cdUsuario)
        {
            try
            {
                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    int cdUsuario = _cdUsuario;
                    hdfcdUsuario.Value = cdUsuario.ToString();

                    var queryUsuario = (from objUsuario in _ctx.tb_usuarios
                                        where objUsuario.cdUsuario == cdUsuario
                                        select objUsuario).FirstOrDefault();

                    if (queryUsuario != null)
                    {
                        txtNomeUsuario.Text = queryUsuario.nomeUsuario;
                        txtEmail.Text = queryUsuario.email;
                        ddlTipoUsuario.SelectedValue = queryUsuario.cdTipoUsuario.ToString();
                        ddlStatusUsuario.SelectedValue = queryUsuario.cdStatusUsuario.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
            }
        }

        protected void PopulaDdlTipoUsuario()
        {
            ddlTipoUsuario.DataSource = Framework.GetDataTable("SELECT cdTipousuario, tipoUsuario FROM tb_tipoUsuario WHERE deleted = 0");
            ddlTipoUsuario.DataBind();
            ddlTipoUsuario.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlStatus()
        {
            ddlStatusUsuario.DataSource = Framework.GetDataTable("SELECT cdStatus, status FROM tb_status");
            ddlStatusUsuario.DataBind();
            ddlStatusUsuario.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void AtualizaGridUsuarios()
        {
            GridUsuarios.DataSource = Framework.GetDataTable($"SELECT u.cdUsuario, u.nomeUsuario, u.email, u.senha, u.dataCadastro, ut.cdTipoUsuario, ut.tipoUsuario, us.cdStatus, us.status FROM tb_usuarios u INNER JOIN tb_tipoUsuario ut ON u.cdTipoUsuario = ut.cdTipoUsuario INNER JOIN tb_status us ON u.cdStatusUsuario = us.cdStatus WHERE u.deleted = 0 AND ut.deleted = 0 ORDER BY u.nomeUsuario");
            GridUsuarios.DataBind();
        }

        protected void AdicionarUsuario()
        {
            try
            {
                tb_usuarios usuario = new tb_usuarios();

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    if (!string.IsNullOrEmpty(hdfcdUsuario.Value))
                    {
                        int cdUsuario = Convert.ToInt32(hdfcdUsuario.Value);

                        usuario = (from obj in _ctx.tb_usuarios
                                   where obj.cdUsuario == cdUsuario
                                   select obj).FirstOrDefault();

                        if (usuario == null)
                        {
                            throw new Exception("Erro: Usuário não encontrado para edição.");
                        }
                    }
                    //Validação de Usuário
                    var usuarioExistente = _ctx.tb_usuarios.FirstOrDefault(user => user.email == txtEmail.Text.Trim() || user.nomeUsuario == txtNomeUsuario.Text.Trim());

                    //Verifica Registro E-mail e nome de usuário
                    if (usuarioExistente != null && usuarioExistente.cdUsuario != usuario.cdUsuario && usuarioExistente.deleted == 0)
                    {
                        if (usuarioExistente.email == txtEmail.Text.Trim())
                        {
                            throw new Exception("Erro: Este e-mail já está registrado.");
                        }

                        if (usuarioExistente.nomeUsuario == txtNomeUsuario.Text.Trim())
                        {
                            throw new Exception("Erro: Este usuário já está registrado.");
                        }
                    }

                    //Validação E-mail de usuário
                    if (txtEmail.Text == "")
                    {
                        throw new Exception("Erro: E-mail de usuário não informado.");
                    }
                    else
                    {
                        usuario.email = txtEmail.Text.Trim();
                    }

                    //Validação nome de usuário
                    if (txtNomeUsuario.Text == "")
                    {
                        throw new Exception("Erro: Nome de usuário não informado.");
                    }
                    else
                    {
                        usuario.nomeUsuario = txtNomeUsuario.Text.Trim();
                    }


                    usuario.senha = !string.IsNullOrEmpty(txtSenha.Text) ? CryptoEngine.Criptografar(txtSenha.Text.Trim(), true) : usuario.senha;
                    usuario.deleted = 0;
                    usuario.dataCadastro = DateTime.Now;


                    //Validação tipo de usuário
                    if (ddlTipoUsuario.SelectedValue == "0")
                    {
                        throw new Exception("Erro: Tipo de usuário não selecionado.");
                    }
                    else
                    {
                        usuario.cdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                    }

                    //Validação Status
                    if (ddlStatusUsuario.SelectedValue == "0")
                    {
                        throw new Exception("Erro: Status não selecionado.");
                    }
                    else
                    {
                        usuario.cdStatusUsuario = Convert.ToInt32(ddlStatusUsuario.SelectedValue);
                    }

                    if (string.IsNullOrEmpty(hdfcdUsuario.Value))
                    {
                        _ctx.tb_usuarios.Add(usuario);
                    }

                    _ctx.SaveChanges();
                    Framework.Alerta("Sucesso", string.IsNullOrEmpty(hdfcdUsuario.Value) ? "Usuário adicionado com sucesso" : "Alterações efetuadas", "success");
                    LimpaCampos();
                    Framework.EscondePaineis(pnlCadastroUsuarios);
                    pnlGridUsuarios.Visible = true;
                    AtualizaGridUsuarios();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("e-mail já está registrado."))
                {
                    Framework.Alerta("Erro", "E-mail já registrado no sistema.", "error");
                }
                else if (ex.Message.Contains("usuário já está registrado"))
                {
                    Framework.Alerta("Erro", "Usuário já registrado no sistema.", "error");
                }
                else
                {
                    Framework.Alerta("Erro", "Ocorreu um erro na execução do método AdicionarUsuario(): " + ex.Message, "error");
                }
            }
        }

        protected void AlteraStatus(int _cdUsuario)
        {
            tb_usuarios usuario = new tb_usuarios();

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                int cdUsuario = _cdUsuario;

                var queryUsuario = (from objUsuario in _ctx.tb_usuarios
                                    where objUsuario.cdUsuario == cdUsuario
                                    select objUsuario).FirstOrDefault();

                if (queryUsuario != null)
                {
                    usuario = queryUsuario;

                    usuario.cdStatusUsuario = usuario.cdStatusUsuario == 1 ? 2 : 1;
                }

                _ctx.SaveChanges();
                Framework.Alerta(usuario.cdStatusUsuario == 1 ? "Usuário desbloqueado" : "Usuário bloqueado", "Alteração realizada", "success");
                AtualizaGridUsuarios();
            }
        }

        public string GetFontAwesomeIcon(object cdStatusUsuario)
        {
            if (cdStatusUsuario != null)
            {
                string status = cdStatusUsuario.ToString();

                switch (status)
                {
                    case "1":
                        return "fa-solid fa-circle-check fa-2x text-success";
                    case "2":
                        return "fa-solid fa-circle-minus fa-2x text-danger";
                }
            }
            return "fas fa-question-circle text-secondary";
        }

        protected void PopulaPermissoes()
        {
            rtvPermissoes.DataSource = Framework.GetDataTable("SELECT cdMenu, cdMenuPai, DescMenu FROM tb_sitemap");
            rtvPermissoes.DataBind();
        }

        protected void ApagaPermissoes(int _cdUsuario)
        {
            using (_ctx = new db_AppLicenseManager_Entities())
            {
                try
                {
                    tb_permissoes permissao = new tb_permissoes();

                    int cdUsuario = _cdUsuario;

                    var Query = from objPermissoes in _ctx.tb_permissoes
                                where objPermissoes.cdUsuario == cdUsuario
                                select objPermissoes;

                    permissao = Query.FirstOrDefault();

                    _ctx.tb_permissoes.Remove(permissao);

                    _ctx.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        protected void InsereMarcados(RadTreeView _rtv, int _cdUsuario)
        {
            RadTreeView rtv = _rtv;
            int cdUsuario = _cdUsuario;
            IList<RadTreeNode> nodeCollection = rtv.CheckedNodes;

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                var permissoesList = new List<tb_permissoes>();
                foreach (RadTreeNode node in nodeCollection)
                {
                    tb_permissoes permissao = new tb_permissoes
                    {
                        cdSitemap = Convert.ToInt32(node.Value),
                        cdUsuario = cdUsuario
                    };
                    permissoesList.Add(permissao);
                }
                _ctx.tb_permissoes.AddRange(permissoesList);
                _ctx.SaveChanges();
            }
        }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AccessControl
            int cdUsuario = Convert.ToInt32(Session["cdUsuario"]);
            string PageName;
            PageName = Request.CurrentExecutionFilePath;
            PageName = PageName.Remove(0, PageName.IndexOf("/") + 1);
            if (VerificaPermissoes(cdUsuario, PageName) == false)
            {
                Response.Redirect("Default2.aspx?permissao=err");
            }
            #endregion

            if (!IsPostBack)
            {
                PopulaDdlTipoUsuario();
                PopulaDdlStatus();
            }
        }
        #endregion

        #region NeedDataSource
        protected void GridUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridUsuarios.DataSource = Framework.GetDataTable($"SELECT u.cdUsuario, u.nomeUsuario, u.email, u.senha, u.dataCadastro, ut.cdTipoUsuario, ut.tipoUsuario, us.cdStatus, us.status FROM tb_usuarios u INNER JOIN tb_tipoUsuario ut ON u.cdTipoUsuario = ut.cdTipoUsuario INNER JOIN tb_status us ON u.cdStatusUsuario = us.cdStatus WHERE u.deleted = 0 AND ut.deleted = 0 ORDER BY u.nomeUsuario");
        }
        #endregion

        #region ItemCommand
        protected void GridUsuarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int cdUsuario = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cdUsuario"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        Framework.EscondePaineis(pnlGridUsuarios);
                        pnlCadastroUsuarios.Visible = true;
                        LimpaCampos();
                        PopulaCamposUsuario(cdUsuario);
                        break;

                    case "opAlteraStatus":
                        AlteraStatus(cdUsuario);
                        break;

                    case "opDelete":
                        using (_ctx = new db_AppLicenseManager_Entities())
                        {
                            var usuario = (from objUsuario in _ctx.tb_usuarios
                                           where objUsuario.cdUsuario == cdUsuario
                                           select objUsuario).FirstOrDefault();

                            usuario.deleted = 1;
                            _ctx.SaveChanges();
                            AtualizaGridUsuarios();
                        }
                        break;
                }
            }
        }
        #endregion

        #region ItemDataBound
        protected void GridUsuarios_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        #endregion

        #region Click
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlCadastroUsuarios);
            LimpaCampos();
            pnlGridUsuarios.Visible = true;
            AtualizaGridUsuarios();
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionarUsuario();
        }

        protected void lnkAtualizarUsuario_Click(object sender, EventArgs e)
        {
            AtualizaGridUsuarios();
        }

        protected void lnkAdicionarUsuario_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlGridUsuarios);
            pnlCadastroUsuarios.Visible = true;
            LimpaCampos();
        }

        protected void lnkDadosUsuario_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlPermissoesUsuario);
            pnlDadosUsuario.Visible = true;
            lnkDadosUsuario.CssClass = "nav-link active";
            lnkPermissoesUsuario.CssClass = "nav-link";


        }

        protected void lnkPermissoesUsuario_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlDadosUsuario);
            pnlPermissoesUsuario.Visible = true;
            lnkPermissoesUsuario.CssClass = "nav-link active";
            lnkDadosUsuario.CssClass = "nav-link";
            PopulaPermissoes();
            InsereMarcados(rtvPermissoes, Convert.ToInt32(hdfcdUsuario.Value));
        }

        protected void btn_salvarPermissoes_Click(object sender, EventArgs e)
        {
            using (_ctx = new db_AppLicenseManager_Entities())
            {
                int _cdUsuario = Convert.ToInt32(hdfcdUsuario.Value);

                var Query = from objPermissoes in _ctx.tb_permissoes
                            where objPermissoes.cdUsuario == _cdUsuario
                            select objPermissoes;

                if (_ctx != null)
                {
                    foreach (var item in Query)
                    {
                        ApagaPermissoes(_cdUsuario);
                    }
                }
                InsereMarcados(rtvPermissoes, _cdUsuario);
            }
            Framework.Alerta("Sucesso", "Permissões Alteradas com sucesso", "success");
            LimpaCampos();
            Framework.EscondePaineis(pnlPermissoesUsuario);
            pnlDadosUsuario.Visible = true;
            Framework.EscondePaineis(pnlCadastroUsuarios);
            pnlGridUsuarios.Visible = true;
            AtualizaGridUsuarios();
        }

        protected void btn_cancelarPermissoes_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Framework.EscondePaineis(pnlPermissoesUsuario);
            pnlDadosUsuario.Visible = true;
            Framework.EscondePaineis(pnlCadastroUsuarios);
            pnlGridUsuarios.Visible = true;
        }

        #endregion

        #region NodeDataBound
        protected void rtvPermissoes_NodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            int cdSiteMap = Convert.ToInt32(e.Node.Value);
            int cdUsuario = Convert.ToInt32(hdfcdUsuario.Value);

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                tb_permissoes permissoes = new tb_permissoes();

                permissoes = (from objPermissoes in _ctx.tb_permissoes
                              where objPermissoes.cdUsuario == cdUsuario &&
                              objPermissoes.cdSitemap == cdSiteMap
                              select objPermissoes).FirstOrDefault();

                if (permissoes != null)
                {
                    if (permissoes.cdSitemap == Convert.ToInt32(e.Node.Value))
                    {
                        e.Node.Checked = true;
                    }
                }
                else
                {
                    e.Node.Checked = false;
                }
            }
        } 
        #endregion

        
    }
}