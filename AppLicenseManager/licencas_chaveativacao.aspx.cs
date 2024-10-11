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
    public partial class licencas_chaveativacao : PageBase
    {
        #region Metodos
        protected void LimpaCampos()
        {
            try
            {
                txtDataAquisicao.Text =
                txtDataValidade.Text =
                txtChaveAtivacao.Text =
                hdfcdChaveAtivacao.Value = "";

                ddlFabricante.Items.Clear();
                PopulaDdlFabricante();

                ddlSoftware.Items.Clear();
                PopulaDdlSoftware();

                ddlMaquina.Items.Clear();
                PopulaDdlMaquina();

                ddlSetor.Items.Clear();
                PopulaDdlSetor();

                ddlTipoLicenca.Items.Clear();
                PopuladdlTipoLicenca();

                ddlStatus.Items.Clear();
                PopuladdlStatus();

            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: LimpaCampos()", "error: " + ex.ToString(), "error");
            }
        }

        protected void PopulaCampos(int _cdChave)
        {
            try
            {
                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    int cdChave = _cdChave;
                    hdfcdChaveAtivacao.Value = _cdChave.ToString();

                    var queryChave = (from objChave in _ctx.tb_chaveativacao
                                      join objSoftware in _ctx.tb_softwares on objChave.cdSoftware equals objSoftware.cdSoftware
                                      join objFabricante in _ctx.tb_fabricantes on objSoftware.cdFabricante equals objFabricante.cdFabricante
                                      join objMaquina in _ctx.tb_maquinas on objChave.cdMaquina equals objMaquina.cdMaquina
                                      join objSetor in _ctx.tb_setores on objMaquina.cdSetor equals objSetor.cdSetor
                                      join objStatus in _ctx.tb_status on objChave.cdStatus equals objStatus.cdStatus
                                      join objTipoLicenca in _ctx.tb_tipolicenca on objChave.cdTipoLicenca equals objTipoLicenca.cdTipoLicenca
                                      where
                                        objChave.cdStatus == 1 &&
                                        objSoftware.cdStatus == 1 &&
                                        objFabricante.cdStatus == 1 &&
                                        objMaquina.cdStatus == 1
                                      select new
                                      {
                                          objChave,
                                          objSoftware,
                                          objFabricante,
                                          objMaquina,
                                          objSetor,
                                          objStatus,
                                          objTipoLicenca
                                      }).FirstOrDefault();

                    if (queryChave != null)
                    {
                        txtDataAquisicao.Text = queryChave.objChave.data_aquisicao.HasValue ? queryChave.objChave.data_aquisicao.Value.ToString("dd/MM/yyyy") : string.Empty;
                        txtDataValidade.Text = queryChave.objChave.data_validade.HasValue ? queryChave.objChave.data_validade.Value.ToString("dd/MM/yyyy") : string.Empty;
                        ddlFabricante.SelectedValue = queryChave.objFabricante.cdFabricante.ToString();
                        ddlSoftware.SelectedValue = queryChave.objSoftware.cdSoftware.ToString();
                        ddlMaquina.SelectedValue = queryChave.objMaquina.cdMaquina.ToString();
                        ddlSetor.SelectedValue = queryChave.objSetor.cdSetor.ToString();
                        ddlStatus.SelectedValue = queryChave.objStatus.cdStatus.ToString();
                        ddlTipoLicenca.SelectedValue = queryChave.objTipoLicenca.cdTipoLicenca.ToString();
                        txtDataValidade.Enabled = ddlTipoLicenca.SelectedValue == "2" ? false : true;
                        txtChaveAtivacao.Text = queryChave.objChave.chaveAtivacao;
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
            }
        }

        protected void PopulaDdlFabricante()
        {
            ddlFabricante.DataSource = Framework.GetDataTable("SELECT cdFabricante, nomeFabricante, cdStatus FROM tb_fabricantes WHERE cdStatus = 1 ORDER BY nomeFabricante");
            ddlFabricante.DataBind();
            ddlFabricante.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlSoftware()
        {
            ddlSoftware.DataSource = Framework.GetDataTable("SELECT cdSoftware, nomeSoftware, cdStatus FROM tb_softwares WHERE cdStatus = 1 ORDER BY nomeSoftware");
            ddlSoftware.DataBind();
            ddlSoftware.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlSoftware(int _cdFabricante)
        {
            int cdFabricante = _cdFabricante;
            ddlSoftware.DataSource = Framework.GetDataTable($"SELECT cdSoftware, nomeSoftware, cdFabricante, cdStatus FROM tb_softwares WHERE cdStatus = 1 AND cdFabricante = {cdFabricante} ORDER BY nomeSoftware");
            ddlSoftware.DataBind();
            ddlSoftware.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlMaquina()
        {
            ddlMaquina.DataSource = Framework.GetDataTable("SELECT cdMaquina, hostname, cdStatus FROM tb_maquinas WHERE cdStatus = 1 ORDER BY hostname");
            ddlMaquina.DataBind();
            ddlMaquina.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopulaDdlSetor()
        {
            ddlSetor.DataSource = Framework.GetDataTable("SELECT cdSetor, nomeSetor FROM tb_setores ORDER BY nomeSetor");
            ddlSetor.DataBind();
            ddlSetor.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopuladdlTipoLicenca()
        {
            ddlTipoLicenca.DataSource = Framework.GetDataTable("SELECT cdTipoLicenca, tipoLicenca FROM tb_tipolicenca ORDER BY tipoLicenca DESC");
            ddlTipoLicenca.DataBind();
            ddlTipoLicenca.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void PopuladdlStatus()
        {
            ddlStatus.DataSource = Framework.GetDataTable("SELECT cdStatus, status FROM tb_status ORDER BY status");
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void AtualizaGridChaves()
        {
            GridChaves.DataSource = Framework.GetDataTable("SELECT ch.cdChaveAtivacao, chaveAtivacao, fab.cdfabricante, fab.nomeFabricante, sft.cdSoftware, sft.nomeSoftware, tl.cdTipoLicenca, tl.tipoLicenca, ch.data_aquisicao, ch.data_validade, maq.cdMaquina, maq.hostname, st.cdSetor, st.nomeSetor, sts.cdStatus, sts.status FROM tb_chaveativacao ch LEFT JOIN tb_softwares sft ON ch.cdSoftware = sft.cdSoftware LEFT JOIN tb_fabricantes fab ON sft.cdFabricante = fab.cdFabricante LEFT JOIN tb_maquinas maq ON ch.cdMaquina = maq.cdMaquina LEFT JOIN tb_setores st ON maq.cdSetor = st.cdSetor LEFT JOIN tb_status sts ON ch.cdStatus = sts.cdStatus LEFT JOIN tb_tipolicenca tl ON ch.cdTipoLicenca = tl.cdTipoLicenca");
            GridChaves.DataBind();
        }

        protected void AdicionarChave()
        {
            try
            {
                tb_chaveativacao chave = new tb_chaveativacao();

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    if (!string.IsNullOrEmpty(hdfcdChaveAtivacao.Value))
                    {
                        int cdChave = Convert.ToInt32(hdfcdChaveAtivacao.Value);

                        var queryChave = (from objChave in _ctx.tb_chaveativacao
                                          where objChave.cdSoftware == cdChave
                                          select objChave);

                        if (queryChave != null)
                        {
                            chave = queryChave.FirstOrDefault();
                        }
                    }
                    //Valida chave de ativação
                    var chaveExistente = _ctx.tb_chaveativacao.FirstOrDefault(key => key.chaveAtivacao == txtChaveAtivacao.Text.Trim());

                    if (chaveExistente != null && chaveExistente.cdChaveAtivacao != chave.cdChaveAtivacao && chaveExistente.deleted == 0)
                    {
                        if (chaveExistente.chaveAtivacao == txtChaveAtivacao.Text.Trim())
                        {
                            throw new Exception("Esta chave já está registrada.");
                        }
                    }

                    //Valida data de aquisição
                    if (Convert.ToDateTime(txtDataAquisicao.Text) > DateTime.Now.Date)
                    {
                        throw new Exception("Data de aquisição maior que a data atual.");
                    }
                    else
                    {
                        chave.data_aquisicao = !string.IsNullOrEmpty(txtDataAquisicao.Text) ? Convert.ToDateTime(txtDataAquisicao.Text) : chave.data_aquisicao;
                    }

                    //Valida data de validade
                    if (Convert.ToInt32(ddlTipoLicenca.SelectedValue) == 1 && (string.IsNullOrEmpty(txtDataValidade.Text)))
                    {
                        throw new Exception("Data de validade obrigatória quando selecionado 'Licença Temporária'.");
                    }
                    else if (chave.cdTipoLicenca == 1 && Convert.ToDateTime(txtDataValidade.Text) < Convert.ToDateTime(txtDataAquisicao.Text))
                    {
                        throw new Exception("Data de validade menor que a data aquisição.");
                    }
                    else
                    {
                        chave.data_validade = !string.IsNullOrEmpty(txtDataValidade.Text) ? Convert.ToDateTime(txtDataValidade.Text) : chave.data_validade;
                    }

                    chave.cdSoftware = Convert.ToInt32(ddlSoftware.SelectedValue);
                    chave.cdMaquina = Convert.ToInt32(ddlMaquina.SelectedValue);
                    chave.cdStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    chave.cdTipoLicenca = Convert.ToInt32(ddlTipoLicenca.SelectedValue);
                    chave.chaveAtivacao = txtChaveAtivacao.Text.Trim();
                    chave.deleted = 0;

                    if (string.IsNullOrEmpty(hdfcdChaveAtivacao.Value))
                    {
                        try
                        {
                            _ctx.tb_chaveativacao.Add(chave);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao adicionar chave de ativação, verifique os parametros inseridos e tente novamente. Caso necessário entre em contato com o Administrador.");
                        }
                    }

                    _ctx.SaveChanges();
                    Framework.Alerta("Sucesso", "Alterações efetuadas", "success");
                    LimpaCampos();
                    txtDataValidade.Enabled = true;
                    Framework.EscondePaineis(pnlCadastroChaves);
                    pnlGridChaves.Visible = true;
                    AtualizaGridChaves();
                }
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Esta chave já está registrada.":
                        Framework.Alerta("Erro", "Chave já registrada no sistema.", "error");
                        break;

                    case "Data de aquisição maior que a data atual.":
                        Framework.Alerta("Erro", "Data de aquisição maior que a data atual.", "error");
                        break;

                    case "Data de validade menor que a data aquisição.":
                        Framework.Alerta("Erro", "Data de validade menor que a data aquisição.", "error");
                        break;

                    case "Erro ao adicionar chave de ativação, verifique os parametros inseridos e tente novamente. Caso necessário entre em contato com o Administrador.":
                        Framework.Alerta("Erro", "Erro ao adicionar chave de ativação, verifique os parametros inseridos e tente novamente. Caso necessário entre em contato com o Administrador.", "error");
                        break;

                    case "Data de validade obrigatória quando selecionado 'Licença Temporária'.":
                        Framework.Alerta("Erro", "Data de validade obrigatória quando selecionado 'Licença Temporária'.", "error");
                        break;

                    default:
                        Framework.Alerta("Erro", "Ocorreu um erro na execução do método AdicionarChave(): " + ex.Message, "error");
                        break;
                }
            }
        }

        protected void AlteraStatus(int _cdChave)
        {
            tb_chaveativacao chave = new tb_chaveativacao();

            using (_ctx = new db_AppLicenseManager_Entities())
            {
                int cdChave = _cdChave;

                var queryChave = (from objChave in _ctx.tb_chaveativacao
                                  where objChave.cdChaveAtivacao == cdChave
                                  select objChave).FirstOrDefault();

                if (queryChave != null)
                {
                    chave = queryChave;
                    chave.cdStatus = chave.cdStatus == 1 ? 2 : 1;
                }

                _ctx.SaveChanges();
                Framework.Alerta(chave.cdStatus == 1 ? "Desbloqueado" : "Bloqueado", "Alteração realizada", "success");
                AtualizaGridChaves();
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
                PopulaDdlMaquina();
                PopulaDdlSetor();
                PopuladdlTipoLicenca();
                PopuladdlStatus();
            }
        }
        #endregion

        #region NeedDataSource
        protected void GridChaves_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GridChaves.DataSource = Framework.GetDataTable("SELECT ch.cdChaveAtivacao, chaveAtivacao, fab.cdfabricante, fab.nomeFabricante, sft.cdSoftware, sft.nomeSoftware, tl.cdTipoLicenca, tl.tipoLicenca, ch.data_aquisicao, ch.data_validade, maq.cdMaquina, maq.hostname, st.cdSetor, st.nomeSetor, sts.cdStatus, sts.status FROM tb_chaveativacao ch LEFT JOIN tb_softwares sft ON ch.cdSoftware = sft.cdSoftware LEFT JOIN tb_fabricantes fab ON sft.cdFabricante = fab.cdFabricante LEFT JOIN tb_maquinas maq ON ch.cdMaquina = maq.cdMaquina LEFT JOIN tb_setores st ON maq.cdSetor = st.cdSetor LEFT JOIN tb_status sts ON ch.cdStatus = sts.cdStatus LEFT JOIN tb_tipolicenca tl ON ch.cdTipoLicenca = tl.cdTipoLicenca");
        }
        #endregion

        #region ItemCommand
        protected void GridChaves_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int cdChave = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cdChave"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        Framework.EscondePaineis(pnlGridChaves);
                        pnlCadastroChaves.Visible = true;
                        LimpaCampos();
                        PopulaCampos(cdChave);
                        break;

                    case "opAlteraStatus":
                        AlteraStatus(cdChave);
                        break;

                    case "opDelete":
                        using (_ctx = new db_AppLicenseManager_Entities())
                        {
                            var chave = (from objChave in _ctx.tb_chaveativacao
                                         where objChave.cdChaveAtivacao == cdChave
                                         select objChave).FirstOrDefault();

                            chave.deleted = 1;
                            _ctx.SaveChanges();
                            AtualizaGridChaves();
                        }
                        break;
                }
            }
        }
        #endregion

        #region Click
        protected void lnkAtualizarChaves_Click(object sender, EventArgs e)
        {
            AtualizaGridChaves();
        }

        protected void lnkAdicionarChave_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlGridChaves);
            LimpaCampos();
            pnlCadastroChaves.Visible = true;
            ddlSoftware.Enabled = false;
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionarChave();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            Framework.EscondePaineis(pnlCadastroChaves);
            pnlGridChaves.Visible = true;
            AtualizaGridChaves();
        }
        #endregion

        #region SelectedIndexChanged
        protected void ddlFabricante_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cdFabricante = Convert.ToInt32(ddlFabricante.SelectedValue);

            if (cdFabricante != 0)
            {
                PopulaDdlSoftware(cdFabricante);
                ddlSoftware.Enabled = true;
            }
            else
            {
                ddlSoftware.Items.Clear();
                ddlSoftware.Enabled = false;

            }
        }

        protected void ddlTipoLicenca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cdTipolicenca = Convert.ToInt32(ddlTipoLicenca.SelectedValue);
            txtDataValidade.Enabled = cdTipolicenca == 1 ? true : false;
        }

        #endregion

    }
}