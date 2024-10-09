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
    public partial class licencas_fabricantes : PageBase
    {
        #region Metodos
        protected void LimpaCampos()
        {
            txtFabricante.Text =
            hdfcdFabricante.Value = "";

            ddlStatusFabricante.Items.Clear();
            PopulaDdlStatus();
        }

        protected void PopulaCampos(int _cdFabricante)
        {
            try
            {
                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    int cdFabricante = _cdFabricante;
                    hdfcdFabricante.Value = cdFabricante.ToString();

                    var queryFabricante = (from objFabricante in _ctx.tb_fabricantes
                                           where objFabricante.cdFabricante == cdFabricante
                                           select objFabricante).FirstOrDefault();

                    if (queryFabricante != null)
                    {
                        txtFabricante.Text = queryFabricante.nomeFabricante;
                        ddlStatusFabricante.SelectedValue = queryFabricante.cdStatus.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
            }
        }

        protected void PopulaDdlStatus()
        {
            ddlStatusFabricante.DataSource = Framework.GetDataTable("SELECT cdStatus, status FROM tb_status");
            ddlStatusFabricante.DataBind();
            ddlStatusFabricante.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void AdicionaFabricante()
        {
            try
            {
                tb_fabricantes fabricante = new tb_fabricantes();

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    if (!string.IsNullOrEmpty(hdfcdFabricante.Value))
                    {
                        int cdFabricante = Convert.ToInt32(hdfcdFabricante.Value);

                        var queryFabricante = (from obj in _ctx.tb_fabricantes
                                               where obj.cdFabricante == cdFabricante
                                               select obj);

                        if (queryFabricante != null)
                        {
                            fabricante = queryFabricante.FirstOrDefault();
                        }
                    }
                    var fabricanteExistente = _ctx.tb_fabricantes.FirstOrDefault(fab => fab.nomeFabricante == txtFabricante.Text.Trim());

                    if (fabricanteExistente != null && fabricanteExistente.deleted == 0)
                    {
                        if (fabricanteExistente.nomeFabricante == txtFabricante.Text.Trim())
                        {
                            throw new Exception("Este fabricante já está registrado.");
                        }
                    }

                    fabricante.nomeFabricante = txtFabricante.Text.Trim();
                    fabricante.data_cadastro = DateTime.Now;
                    fabricante.cdStatus = Convert.ToInt32(ddlStatusFabricante.SelectedValue);
                    fabricante.deleted = 0;

                    if (string.IsNullOrEmpty(hdfcdFabricante.Value))
                    {
                        _ctx.tb_fabricantes.Add(fabricante);
                        Framework.Alerta("Sucesso", "Adicionado com sucesso", "success");
                    }

                    _ctx.SaveChanges();
                    Framework.Alerta("Sucesso", "Alterações efetuadas", "success");
                    LimpaCampos();
                    Framework.EscondePaineis(pnlCadastroFabricante);
                    pnlGridFabricantes.Visible = true;
                    AtualizaGridFabricantes();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Este fabricante já está registrado."))
                {
                    Framework.Alerta("Erro", "Fabricante já registrado no sistema", "error");
                }
                else
                {
                    Framework.Alerta("Erro", "Ocorreu um erro na execução do método AdicionaFabricante(): " + ex.Message, "error");
                }
            }
        }

        protected void AlteraStatus(int _cdFabricante)
        {
            tb_fabricantes fabricante = new tb_fabricantes();

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                int cdFabricante = _cdFabricante;

                var queryUsuario = (from objFabricante in _ctx.tb_fabricantes
                                    where objFabricante.cdFabricante == cdFabricante
                                    select objFabricante).FirstOrDefault();

                if (queryUsuario != null)
                {
                    fabricante = queryUsuario;

                    fabricante.cdStatus = fabricante.cdStatus == 1 ? 2 : 1;
                }

                _ctx.SaveChanges();
                Framework.Alerta(fabricante.cdStatus == 1 ? "Usuário desbloqueado" : "Usuário bloqueado", "Alteração realizada", "success");
                AtualizaGridFabricantes();
            }
        }

        protected void AtualizaGridFabricantes()
        {
            GridFabricantes.DataSource = Framework.GetDataTable("SELECT f.cdFabricante, f.nomeFabricante, f.data_cadastro, s.cdStatus FROM tb_fabricantes f INNER JOIN tb_status s ON f.cdStatus = s.cdStatus WHERE f.deleted = 0 ORDER BY nomeFabricante");
            GridFabricantes.DataBind();
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
            }
        }
        #endregion

        #region NeedDataSource
        protected void GridFabricantes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridFabricantes.DataSource = Framework.GetDataTable("SELECT f.cdFabricante, f.nomeFabricante, f.data_cadastro, s.cdStatus FROM tb_fabricantes f INNER JOIN tb_status s ON f.cdStatus = s.cdStatus WHERE f.deleted = 0 ORDER BY nomeFabricante");
        }
        #endregion

        #region ItemCommand
        protected void GridFabricantes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int cdFabricante = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cdFabricante"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        Framework.EscondePaineis(pnlGridFabricantes);
                        pnlCadastroFabricante.Visible = true;
                        LimpaCampos();
                        PopulaCampos(cdFabricante);
                        break;

                    case "opAlteraStatus":
                        AlteraStatus(cdFabricante);
                        break;

                    case "opDelete":
                        using (_ctx = new db_AppLicenseManager_Entities())
                        {
                            var fabricante = (from objFabricante in _ctx.tb_fabricantes
                                              where objFabricante.cdFabricante == cdFabricante
                                              select objFabricante).FirstOrDefault();

                            fabricante.deleted = 1;
                            _ctx.SaveChanges();
                            AtualizaGridFabricantes();
                        }
                        break;
                }
            }
        }
        #endregion

        #region Click
        protected void lnkAtualizarFabricantes_Click(object sender, EventArgs e)
        {
            AtualizaGridFabricantes();
        }

        protected void lnkAdicionarFabricante_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlGridFabricantes);
            LimpaCampos();
            pnlCadastroFabricante.Visible = true;
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionaFabricante();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Framework.EscondePaineis(pnlCadastroFabricante);
            pnlGridFabricantes.Visible = true;
            AtualizaGridFabricantes();
        }
        #endregion
    }
}