using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLicenseManager.Util;

namespace AppLicenseManager
{
    public partial class profile : PageBase
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

                ddlStatus.Items.Clear();
                PopulaDdlStatus();
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: LimpaCampos()", "error: " + ex.ToString(), "error");
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
            ddlStatus.DataSource = Framework.GetDataTable("SELECT cdStatus, status FROM tb_status");
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void AdicionarUsuario()
        {
            tb_usuarios usuario = new tb_usuarios();

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                if (!string.IsNullOrEmpty(hdfcdUsuario.Value))
                {
                    int cdUsuario = Convert.ToInt32(hdfcdUsuario.Value);

                    var queryUsuario = (from obj in _ctx.tb_usuarios
                                        where obj.cdUsuario == cdUsuario
                                        select obj);

                    if (queryUsuario != null)
                    {
                        usuario = queryUsuario.FirstOrDefault();
                    }
                }
                usuario.nomeUsuario = txtNomeUsuario.Text.Trim();
                usuario.email = txtEmail.Text.Trim();
                usuario.cdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                usuario.cdStatusUsuario = Convert.ToInt32(ddlStatus.SelectedValue);
                usuario.senha = !string.IsNullOrEmpty(txtSenha.Text) ? CryptoEngine.Criptografar(txtSenha.Text.Trim(), true) : usuario.senha;

                if (string.IsNullOrEmpty(hdfcdUsuario.Value))
                {
                    _ctx.tb_usuarios.Add(usuario);
                    Framework.Alerta("Sucesso", "Usuário adicionado com sucesso", "success");
                }

                _ctx.SaveChanges();
                Framework.Alerta("Sucesso", "Alterações efetuadas", "success");
                LimpaCampos();
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
                        ddlStatus.SelectedValue = queryUsuario.cdStatusUsuario.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
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
                Response.Redirect("login.aspx?permissao=err");
            }
            #endregion

            if (!IsPostBack)
            {
                PopulaDdlStatus();
                PopulaDdlTipoUsuario();
                PopulaCamposUsuario(cdUsuario);
            }
        }
        #endregion

        #region Click
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Default.aspx");
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionarUsuario();
        } 
        #endregion
    }
}