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
    public partial class localizacao_setores : PageBase
    {
        #region Metodos
        protected void LimpaCampos()
        {
            txtSetor.Text =
            hdfcdSetor.Value = "";
        }

        protected void PopulaCampos(int _cdSetor)
        {
            try
            {
                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    int cdSetor = _cdSetor;
                    hdfcdSetor.Value = cdSetor.ToString();

                    var querySetor = (from objSetor in _ctx.tb_setores
                                      where objSetor.cdSetor == cdSetor
                                      select objSetor).FirstOrDefault();

                    if (querySetor != null)
                    {
                        txtSetor.Text = querySetor.nomeSetor;
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.Alerta("Error method: PopulaCampos", "Erro: " + ex.ToString(), "error");
            }
        }

        protected void AtualizaGridSetores()
        {
            GridSetores.DataSource = Framework.GetDataTable("SELECT cdSetor, nomeSetor FROM tb_setores ORDER BY nomeSetor");
            GridSetores.DataBind();
        }

        protected void AdicionarSetor()
        {
            try
            {
                tb_setores setor = new tb_setores();

                using (_ctx = new db_AppLicenseManager_Entities())
                {
                    if (!string.IsNullOrEmpty(hdfcdSetor.Value))
                    {
                        int cdSetor = Convert.ToInt32(hdfcdSetor.Value);

                        var querySetor = (from obj in _ctx.tb_setores
                                          where obj.cdSetor == cdSetor
                                          select obj);

                        if (querySetor != null)
                        {
                            setor = querySetor.FirstOrDefault();
                        }
                    }
                    var setorExistente = _ctx.tb_setores.FirstOrDefault(set => set.nomeSetor == txtSetor.Text.Trim());

                    if (setorExistente != null && setorExistente.deleted == 0)
                    {
                        if (setorExistente.nomeSetor == txtSetor.Text.Trim())
                        {
                            throw new Exception("Este setor já está registrado.");
                        }
                    }

                    setor.nomeSetor = txtSetor.Text.Trim();
                    setor.deleted = 0;

                    if (string.IsNullOrEmpty(hdfcdSetor.Value))
                    {
                        _ctx.tb_setores.Add(setor);
                        Framework.Alerta("Sucesso", "adicionado com sucesso", "success");
                    }

                    _ctx.SaveChanges();
                    Framework.Alerta("Sucesso", "Alterações efetuadas", "success");
                    LimpaCampos();
                    Framework.EscondePaineis(pnlCadastroSetores);
                    pnlGridSetores.Visible = true;
                    AtualizaGridSetores();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Este setor já está registrado."))
                {
                    Framework.Alerta("Erro", "Setor já registrado no sistema", "error");
                }
                else
                {
                    Framework.Alerta("Erro", "Ocorreu um erro na execução do método AdicionaFabricante(): " + ex.Message, "error");
                }
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
        }
        #endregion

        #region NeedDataSource
        protected void GridSetores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridSetores.DataSource = Framework.GetDataTable("SELECT cdSetor, nomeSetor FROM tb_setores ORDER BY nomeSetor");
        }
        #endregion

        #region ItemCommand
        protected void GridSetores_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                int cdSetor = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cdSetor"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        Framework.EscondePaineis(pnlGridSetores);
                        pnlCadastroSetores.Visible = true;
                        LimpaCampos();
                        PopulaCampos(cdSetor);
                        break;

                    case "opDelete":
                        using (_ctx = new db_AppLicenseManager_Entities())
                        {
                            var setor = (from objSetor in _ctx.tb_setores
                                           where objSetor.cdSetor == cdSetor
                                           select objSetor).FirstOrDefault();

                            setor.deleted = 1;
                            _ctx.SaveChanges();
                            AtualizaGridSetores();
                        }
                        break;
                }
            }
        }
        #endregion

        #region Click
        protected void lnkAtualizarSetores_Click(object sender, EventArgs e)
        {
            AtualizaGridSetores();
        }

        protected void lnkAdicionarSetores_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlGridSetores);
            LimpaCampos();
            pnlCadastroSetores.Visible = true;
        }

        protected void btn_salvar_Click(object sender, EventArgs e)
        {
            AdicionarSetor();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Framework.EscondePaineis(pnlCadastroSetores);
            LimpaCampos();
            pnlGridSetores.Visible = true;
            AtualizaGridSetores();
        }
        #endregion
    }
}