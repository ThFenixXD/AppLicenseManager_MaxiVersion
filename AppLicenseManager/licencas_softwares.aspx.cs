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
    public partial class licencas_produtos : PageBase
    {
        #region Metodos
        protected void LimpaCampos()
        {
            try
            {
                txtNomeSoftware.Text =
                hdfcdSoftware.Value = "";

                ddlStatusSoftware.Items.Clear();
                PopulaDdlStatusSoftware();

                ddlFabricante.Items.Clear();
                PopulaDdlFabricante();
            }

            catch (Exception ex)
            {
                Framework.Alerta("Error method: LimpaCampos()", "error: " + ex.ToString(), "error");
            }
        }

        protected void PopulaDdlStatusSoftware()
        {
            ddlStatusSoftware.DataSource = Framework.GetDataTable("SELECT cdStatus, status FROM tb_status");
            ddlStatusSoftware.DataBind();
            ddlStatusSoftware.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlFabricante()
        {
            ddlFabricante.DataSource = Framework.GetDataTable("SELECT cdFabricante, nomeFabricante FROM tb_fabricantes WHERE cdStatus = 1");
            ddlFabricante.DataBind();
            ddlFabricante.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaCampos(int _cdSoftware)
        {
            try
            {
                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    int cdSoftware = _cdSoftware;
                    hdfcdSoftware.Value = _cdSoftware.ToString();

                    var querySoftware = (from objSoftware in _ctx.tb_softwares
                                         where objSoftware.cdSoftware == cdSoftware
                                         select objSoftware).FirstOrDefault();

                    if (querySoftware != null)
                    {
                        txtNomeSoftware.Text = querySoftware.nomeSoftware;
                        ddlStatusSoftware.SelectedValue = querySoftware.cdStatus.ToString();
                        ddlFabricante.SelectedValue = querySoftware.cdFabricante.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
            }
        }

        protected void AtualizaGridSoftware()
        {
            GridSoftwares.DataSource = Framework.GetDataTable("SELECT s.cdSoftware, f.cdFabricante, f.nomefabricante, s.nomeSoftware, st.cdStatus, st.status FROM tb_softwares s LEFT JOIN tb_fabricantes f ON s.cdFabricante = f.cdFabricante INNER JOIN tb_status st ON s.cdStatus = st.cdStatus ORDER BY s.nomeSoftware");
            GridSoftwares.DataBind();
        }

        protected void AdicionarSoftware()
        {
            try
            {
                tb_softwares software = new tb_softwares();

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    if (!string.IsNullOrEmpty(hdfcdSoftware.Value))
                    {
                        int cdSoftware = Convert.ToInt32(hdfcdSoftware.Value);

                        var querySoftware = (from objSoftware in _ctx.tb_softwares
                                             where objSoftware.cdSoftware == cdSoftware
                                             select objSoftware);

                        if (querySoftware != null)
                        {
                            software = querySoftware.FirstOrDefault();
                        }
                    }
                    //Validação Software
                    var softwareExistente = _ctx.tb_softwares.FirstOrDefault(soft => soft.nomeSoftware == txtNomeSoftware.Text.Trim());

                    //Verifica Registro
                    if (softwareExistente != null && softwareExistente.cdSoftware != software.cdSoftware && softwareExistente.deleted == 0)
                    {
                        if (softwareExistente.nomeSoftware == txtNomeSoftware.Text.Trim())
                        {
                            throw new Exception("Erro: Este software já está registrado.");
                        }
                    }

                    //Validação Nome Software
                    if (txtNomeSoftware.Text == "")
                    {
                        throw new Exception("Erro: Nome do software não informado.");
                    }
                    else
                    {
                        software.nomeSoftware = txtNomeSoftware.Text.Trim();
                    }

                    //Validação Fabricante
                    if (ddlFabricante.SelectedValue == "0")
                    {
                        throw new Exception("Erro: Fabricante não selecionado.");
                    }
                    else
                    {
                        software.cdFabricante = Convert.ToInt32(ddlFabricante.SelectedValue);
                    }

                    //Validação Status
                    if (ddlStatusSoftware.SelectedValue == "0")
                    {
                        throw new Exception("Erro: Status não selecionado.");
                    }
                    else
                    {
                        software.cdStatus = Convert.ToInt32(ddlStatusSoftware.SelectedValue);
                    }

                    software.deleted = 0;

                    if (string.IsNullOrEmpty(hdfcdSoftware.Value))
                    {
                        _ctx.tb_softwares.Add(software);
                    }

                    _ctx.SaveChanges();
                    Framework.Alerta("Sucesso", string.IsNullOrEmpty(hdfcdSoftware.Value) ? "Software adicionado com sucesso" : "Alterações efetuadas", "success");
                    LimpaCampos();
                    Framework.EscondePaineis(pnlCadastroSoftware);
                    pnlGridSoftwares.Visible = true;
                    AtualizaGridSoftware();
                }
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Erro: Este software já está registrado.":
                        Framework.Alerta("Erro", "Software já registrado no sistema.", "error");
                        break;

                    case "Erro: Nome do software não informado.":
                        Framework.Alerta("Erro", "Nome do software não informado.", "error");
                        break;

                    case "Erro: Fabricante não selecionado.":
                        Framework.Alerta("Erro", "Fabricante não selecionado.", "error");
                        break;

                    case "Erro: Status não selecionado.":
                        Framework.Alerta("Erro", "Status não selecionado.", "error");
                        break;

                    default:
                        Framework.Alerta("Erro", $"Ocorreu um erro na execução do método AdicionarSoftware(): {ex.Message}", "error");
                        break;
                }
            }
        }

        protected void AlteraStatus(int _cdSoftware)
        {
            tb_softwares software = new tb_softwares();

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                int cdSoftware = _cdSoftware;

                var querySoftware = (from objSoftware in _ctx.tb_softwares
                                     where objSoftware.cdSoftware == cdSoftware
                                     select objSoftware).FirstOrDefault();

                if (querySoftware != null)
                {
                    software = querySoftware;

                    software.cdStatus = software.cdStatus == 1 ? 2 : 1;
                }

                _ctx.SaveChanges();
                Framework.Alerta(software.cdStatus == 1 ? "Desbloqueado" : "Bloqueado", "Alteração realizada", "success");
                AtualizaGridSoftware();
            }
        }

        public string GetFontAwesomeIcon(object cdStatus)
        {
            if (cdStatus != null)
            {
                string status = cdStatus.ToString();

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
                PopulaDdlFabricante();
                PopulaDdlStatusSoftware();
            }
        }
        #endregion

        #region NeedDataSource
        protected void GridSoftwares_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridSoftwares.DataSource = Framework.GetDataTable("SELECT s.cdSoftware, f.cdFabricante, f.nomefabricante, s.nomeSoftware, st.cdStatus, st.status FROM tb_softwares s LEFT JOIN tb_fabricantes f ON s.cdFabricante = f.cdFabricante INNER JOIN tb_status st ON s.cdStatus = st.cdStatus ORDER BY s.nomeSoftware");
        }
        #endregion

        #region ItemCommand
        protected void GridSoftwares_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int cdSoftware = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cdSoftware"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        Framework.EscondePaineis(pnlGridSoftwares);
                        pnlCadastroSoftware.Visible = true;
                        LimpaCampos();
                        PopulaCampos(cdSoftware);
                        break;

                    case "opAlteraStatus":
                        AlteraStatus(cdSoftware);
                        break;

                    case "opDelete":
                        using (_ctx = new db_AppLicenseManager_Entities())
                        {
                            var software = (from objSoftware in _ctx.tb_softwares
                                           where objSoftware.cdSoftware == cdSoftware
                                           select objSoftware).FirstOrDefault();

                            software.deleted = 1;
                            _ctx.SaveChanges();
                            AtualizaGridSoftware();
                        }
                        break;
                }
            }
        }
        #endregion

        #region Click
        protected void lnkAtualizarSoftwares_Click(object sender, EventArgs e)
        {
            AtualizaGridSoftware();
        }

        protected void lnkAdicionarSoftwares_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlGridSoftwares);
            LimpaCampos();
            pnlCadastroSoftware.Visible = true;

        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionarSoftware();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Framework.EscondePaineis(pnlCadastroSoftware);
            pnlGridSoftwares.Visible = true;
            AtualizaGridSoftware();
        }
        #endregion

    }
}