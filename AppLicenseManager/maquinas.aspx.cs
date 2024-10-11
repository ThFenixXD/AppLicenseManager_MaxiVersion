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
    public partial class maquinas : PageBase
    {
        #region Metodos
        protected void LimpaCampos()
        {
            txtHostname.Text =
            hdfcdMaquina.Value = "";

            ddlStatus.Items.Clear();
            PopulaDdlStatus();

            ddlSetor.Items.Clear();
            PopulaDdlSetores();
        }

        protected void PopulaCampos(int _cdMaquina)
        {
            try
            {
                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    int cdMaquina = _cdMaquina;
                    hdfcdMaquina.Value = cdMaquina.ToString();

                    var queryMaquina = (from objMaquina in _ctx.tb_maquinas
                                        where objMaquina.cdMaquina == cdMaquina
                                        select objMaquina).FirstOrDefault();

                    if (queryMaquina != null)
                    {
                        txtHostname.Text = queryMaquina.hostname;
                        ddlSetor.SelectedValue = queryMaquina.cdSetor.ToString();
                        ddlStatus.SelectedValue = queryMaquina.cdStatus.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
            }
        }

        protected void PopulaDdlSetores()
        {
            ddlSetor.DataSource = Framework.GetDataTable("SELECT cdSetor, nomeSetor FROM tb_setores ORDER BY nomeSetor");
            ddlSetor.DataBind();
            ddlSetor.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlStatus()
        {
            ddlStatus.DataSource = Framework.GetDataTable("SELECT cdStatus, status FROM tb_status ORDER BY status");
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void AdicionaMaquina()
        {
            try
            {
                tb_maquinas maquina = new tb_maquinas();

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    if (!string.IsNullOrEmpty(hdfcdMaquina.Value))
                    {
                        int cdMaquina = Convert.ToInt32(hdfcdMaquina.Value);

                        var queryMaquina = (from obj in _ctx.tb_maquinas
                                            where obj.cdMaquina == cdMaquina
                                            select obj);

                        if (queryMaquina != null)
                        {
                            maquina = queryMaquina.FirstOrDefault();
                        }
                    }
                    //Validação de Máquina
                    var maquinaExistente = _ctx.tb_maquinas.FirstOrDefault(maq => maq.hostname == txtHostname.Text.Trim());

                    //Verifica Registro
                    if (maquinaExistente != null && maquinaExistente.cdMaquina != maquina.cdMaquina && maquinaExistente.deleted == 0)
                    {
                        throw new Exception("Erro: Esta máquina já está registrada.");
                    }

                    //Validação Hostname
                    if (txtHostname.Text == "")
                    {
                        throw new Exception("Erro: Hostname não informado.");
                    }
                    else
                    {
                        maquina.hostname = txtHostname.Text.ToUpper().Trim();
                    }

                    //Validação Setor
                    if (ddlSetor.SelectedValue == "0")
                    {
                        throw new Exception("Erro: Setor não informado.");
                    }
                    else
                    {
                        maquina.cdSetor = Convert.ToInt32(ddlSetor.SelectedValue);
                    }

                    //Validação Status
                    if (ddlStatus.SelectedValue == "0")
                    {
                        throw new Exception("Erro: Status não informado.");
                    }
                    else
                    {
                        maquina.cdStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    }

                    maquina.deleted = 0;

                    if (string.IsNullOrEmpty(hdfcdMaquina.Value))
                    {
                        _ctx.tb_maquinas.Add(maquina);
                    }

                    _ctx.SaveChanges();
                    Framework.Alerta("Sucesso", string.IsNullOrEmpty(hdfcdMaquina.Value) ? "Máquina adicionada com sucesso" : "Alterações efetuadas", "success");
                    LimpaCampos();
                    Framework.EscondePaineis(pnlCadastroMaquinas);
                    pnlGridMaquinas.Visible = true;
                    AtualizaGridMaquinas();
                }
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Erro: Esta máquina já está registrada.":
                        Framework.Alerta("Erro", "Máquina já registrada no sistema.", "error");
                        break;

                    case "Erro: Hostname não informado.":
                        Framework.Alerta("Erro", "Hostname não informado.", "error");
                        break;

                    case "Erro: Setor não informado.":
                        Framework.Alerta("Erro", "Setor não informado.", "error");
                        break;

                    case "Erro: Status não informado.":
                        Framework.Alerta("Erro", "Status não informado.", "error");
                        break;

                    default:
                        Framework.Alerta("Erro", $"Ocorreu um erro na execução do método AdicionaMaquina(): {ex.Message}", "error");
                        break;
                }
            }
        }

        protected void AlteraStatus(int _cdMaquina)
        {
            tb_maquinas maquina = new tb_maquinas();

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                int cdMaquina = _cdMaquina;

                var queryMaquina = (from objMaquina in _ctx.tb_maquinas
                                    where objMaquina.cdMaquina == cdMaquina
                                    select objMaquina).FirstOrDefault();

                if (queryMaquina != null)
                {
                    maquina = queryMaquina;

                    maquina.cdStatus = queryMaquina.cdStatus == 1 ? 2 : 1;
                }

                _ctx.SaveChanges();
                Framework.Alerta(maquina.cdStatus == 1 ? "Desbloqueado" : "Bloqueado", "Alteração realizada", "success");
                AtualizaGridMaquinas();
            }
        }

        protected void AtualizaGridMaquinas()
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT m.cdMaquina, m.hostname, st.cdSetor, st.nomeSetor, s.cdStatus, s.status FROM tb_maquinas m INNER JOIN tb_status s ON m.cdStatus = s.cdStatus INNER JOIN tb_setores st ON m.cdSetor = st.cdSetor WHERE m.deleted = 0 ORDER BY st.cdSetor");
            GridMaquinas.DataBind();
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
                PopulaDdlSetores();
                PopulaDdlStatus();
            }
        }
        #endregion

        #region Click
        protected void lnkAtualizarMaquinas_Click(object sender, EventArgs e)
        {
            AtualizaGridMaquinas();
        }

        protected void lnkAdicionarMaquinas_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlGridMaquinas);
            LimpaCampos();
            pnlCadastroMaquinas.Visible = true;
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionaMaquina();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Framework.EscondePaineis(pnlCadastroMaquinas);
            pnlGridMaquinas.Visible = true;
            AtualizaGridMaquinas();
        }
        #endregion

        #region NeedDataSource
        protected void GridMaquinas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT m.cdMaquina, m.hostname, st.cdSetor, st.nomeSetor, s.cdStatus, s.status FROM tb_maquinas m INNER JOIN tb_status s ON m.cdStatus = s.cdStatus INNER JOIN tb_setores st ON m.cdSetor = st.cdSetor WHERE m.deleted = 0 ORDER BY st.cdSetor");
        }
        #endregion

        #region ItemCommand
        protected void GridMaquinas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int cdMaquina = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cdMaquina"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        Framework.EscondePaineis(pnlGridMaquinas);
                        pnlCadastroMaquinas.Visible = true;
                        LimpaCampos();
                        PopulaCampos(cdMaquina);
                        break;

                    case "opAlteraStatus":
                        AlteraStatus(cdMaquina);
                        break;

                    case "opDelete":
                        using (_ctx = new db_AppLicenseManager_Entities())
                        {
                            var maquina = (from objMaquina in _ctx.tb_maquinas
                                           where objMaquina.cdMaquina == cdMaquina
                                           select objMaquina).FirstOrDefault();

                            maquina.deleted = 1;
                            AtualizaGridMaquinas();
                        }
                        break;
                }
            }
        }
        #endregion

        #region TextChanged
        protected void txtHostname_TextChanged(object sender, EventArgs e)
        {
            txtHostname.Text = txtHostname.Text.ToUpper();
        }
        #endregion
    }
}