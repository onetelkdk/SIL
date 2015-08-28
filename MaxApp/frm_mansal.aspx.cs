using MaxBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mansal : PageBase
    {
        clsSalones Salones;
        clsDoc Doc;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_mansal.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = true;

                        mtLoadData();
                        mtLoadCombos();
                    }

                }
                catch (Exception ex)
                {

                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }
        }

        private void mtLoadCombos()
        {
            Doc = new clsDoc();

            String opcion = "[Seleccione una Opción]";

            mtBindDropDownList(ddlTipo, Doc.mtGetCargaDocumentos(), "AdmCatNombre", "AdmCatCodigo", opcion);

            Doc.mtDispose();
        }

        private void mtLoadData()
        {
            Salones = new clsSalones();

            mtBindGridView(GridViewSalones, Salones.mtGetSalones());
            if (GridViewSalones.Rows.Count > 0)
            {
                GridViewSalones.UseAccessibleHeader = true;
                GridViewSalones.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Salones.mtDispose();
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            txtNombreSalon.Enabled = false;
            ddlEstado.Enabled = false;
            ddlTipo.Enabled = false;
            ddlEstado.CssClass = "select-240";
            ddlTipo.CssClass = "select-240";
        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                ViewState["hfId"] = fId.ToString();

                int id = int.Parse(ViewState["hfId"].ToString());

                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;


                foreach (GridViewRow row in GridViewSalones.Rows)
                {
                    if (fId == GridViewSalones.DataKeys[row.RowIndex].Values["AdmSalCodigo"].ToString())
                    {
                        txtNombreSalon.Text = GridViewSalones.DataKeys[row.RowIndex].Values["AdmSalDescripcion"].ToString();
                        ddlTipo.SelectedValue = String.IsNullOrEmpty(GridViewSalones.DataKeys[row.RowIndex].Values["AdmCatCodigo"].ToString()) ? "0" : GridViewSalones.DataKeys[row.RowIndex].Values["AdmCatCodigo"].ToString();
                        string codigo = String.IsNullOrEmpty(GridViewSalones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "True" : GridViewSalones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString();
                        ddlEstado.SelectedValue = codigo.Trim();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["hfId"] = "0";
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnEditar.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtNombreSalon.Text = "";
                ddlEstado.SelectedIndex = 0;
                ddlTipo.SelectedIndex = 0;
                txtNombreSalon.Focus();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["hfId"] = "0";
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnCancel.Visible = false;
                btnGuardar.Visible = false;
                txtNombreSalon.Enabled = true;
                ddlEstado.Enabled = true;
                ddlTipo.Enabled = true;
                mtLoadData();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreSalon.Text.Trim()))
            {
                mtvAddMessage("Captura el nombre del salon", MessageType.warning);
                return;
            }


            Salones = new clsSalones();
            Salones.mtPutSalones
                (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                , txtNombreSalon.Text.Trim()
                ,int.Parse(ddlTipo.SelectedValue)
                ,bool.Parse(ddlEstado.SelectedValue)
                , Session["AdmusrCodigo"].ToString()
                );

            Salones.mtDispose();
            ViewState["hfId"] = "0";
            
            mtvAddMessage("Registro Guardado satisfactoriamente", MessageType.success);

            PanelMantenimientos.Visible = false;
            btnGuardar.Visible = false;
            btnNuevo.Visible = true;
            btnCancel.Visible = false;
            panelLista.Visible = true;

            mtLoadData();
        }
    }
}