using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppLicenseManager
{
    public partial class AppLicenseManager : System.Web.UI.MasterPage
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserSession.Text = Session["NomeUsuario"].ToString();
            }
        }
        #endregion

        #region Click
        protected void lnkVerPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/profile.aspx");
        }

        protected void lnkSairApp_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/login.aspx");
        } 
        #endregion
    }
}