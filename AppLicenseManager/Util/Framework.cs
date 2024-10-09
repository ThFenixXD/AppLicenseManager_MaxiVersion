using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppLicenseManager.Util
{
    public class Framework
    {
        #region GetDataTable
        public static DataTable GetDataTable(string query)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["db_AppLicenseManager_MAXIConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable myDataTable = new DataTable();
            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }
            return myDataTable;
        }
        #endregion

        #region Metodos
        public static void EscondePaineis(Control container)
        {
            if (container is Panel)
                container.Visible = false;

            foreach (Control ctrl in container.Controls)
                EscondePaineis(ctrl);
        }

        public static string FormatWithMask(string input, string mask)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var output = string.Empty;
            var index = 0;
            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < input.Length)
                    {
                        output += input[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }

        public int SubtrairData(DateTime data1, DateTime data2)
        {
            return (data1 - data2).Days;
        }
        #endregion

        #region Alertas
        public static void Alerta(string titulo, string mensagem, string tipo) // tipo = "success" ou "error"
        {
            Page p = HttpContext.Current.CurrentHandler as Page;

            if (p != null && ScriptManager.GetCurrent(p) != null)
            {
                titulo = HttpUtility.JavaScriptStringEncode(titulo, true);
                mensagem = HttpUtility.JavaScriptStringEncode(mensagem, true);
                tipo = HttpUtility.JavaScriptStringEncode(tipo, true);

                string script = $"alertaClick({titulo}, {mensagem}, {tipo});";

                ScriptManager.RegisterStartupScript(p, p.GetType(), "alertaClick", script, true);
            }
        }

        public static void DelayRedirect(string link, int delayTime)
        {
            Page p = HttpContext.Current.CurrentHandler as Page;

            if (p != null && ScriptManager.GetCurrent(p) != null)
            {
                link = HttpUtility.JavaScriptStringEncode(link, true);

                string script = $"delayRedirect({link}, {delayTime});";

                ScriptManager.RegisterStartupScript(p, p.GetType(), "delayRedirect", script, true);
            }
        }
        
        #endregion
    }
}