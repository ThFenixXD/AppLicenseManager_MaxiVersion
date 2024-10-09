using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLicenseManager.Util;

namespace AppLicenseManager
{
    public partial class Login : PageBase
    {
        #region Metodos

        protected void LimpaCampos()
        {
            txtEmail.Text =
            txtSenha.Text = "";
        }

        protected bool ValidaUsuario(string _email, string _senha)
        {
            try
            {
                string email = _email;
                string senha = CryptoEngine.Criptografar(_senha, true);

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    var oUsuario = (from objUsuario in _ctx.tb_usuarios
                                    where objUsuario.email == email && objUsuario.senha == senha && objUsuario.cdStatusUsuario == 1
                                    select objUsuario).FirstOrDefault();

                    if (oUsuario != null)
                    {
                        Session["Autenticado"] = 1;
                        Session["NomeUsuario"] = oUsuario.nomeUsuario.ToString().Trim();
                        Session["EmailUsuario"] = oUsuario.email.ToString();
                        Session["cdUsuario"] = oUsuario.cdUsuario.ToString();
                        return true;

                    }
                    else
                    {
                        Session["Autenticado"] = 0;
                        Session["NomeUsuario"] =
                        Session["EmailUsuario"] =
                        Session["cdUsuario"] = string.Empty;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error", "Erro ao realizar o Login: " + ex.ToString(), "error");
                return false;
            }
        }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region Click
        protected void btn_logon_Click(object sender, EventArgs e)
        {
            if (ValidaUsuario(txtEmail.Text, txtSenha.Text) == true)
            {
                Framework.EscondePaineis(pnlLogin);
                Framework.Alerta("Sucesso", "Usuário autenticado com sucesso!", "success");
                Framework.DelayRedirect("Default.aspx", 1200);
            }
            else
            {
                Framework.Alerta("Erro", "Erro ao autenticar usuário, verifique os dados e tente novamente", "error");
            }
        }
        #endregion
    }
}