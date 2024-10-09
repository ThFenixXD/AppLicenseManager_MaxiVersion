using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppLicenseManager
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //#region AccessControl
            //int cdUsuario = Convert.ToInt32(Session["cdUsuario"]);
            //string PageName;
            //PageName = Request.CurrentExecutionFilePath;
            //PageName = PageName.Remove(0, PageName.IndexOf("/") + 1);
            //if (VerificaPermissoes(cdUsuario, PageName) == false)
            //{
            //    Response.Redirect("Default2.aspx?permissao=err");
            //}
            //#endregion
        }
    }
}