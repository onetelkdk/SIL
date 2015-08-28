using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mantprg : PageBase
    {
        MaxBll.clsTiposPrg objTipos;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_mantprg.aspx", lblModulo))
                    {
                        mtLoadData();
                        MenuFlotante.InnerHtml = mtGetMenu();
                    }

                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtLoadData()
        {

            mtHabilitar(false);
            objTipos = new MaxBll.clsTiposPrg();
            mtBindGridView(gv_Tipos, objTipos.mtGetTipos());
            gv_Tipos.UseAccessibleHeader = true;
            gv_Tipos.HeaderRow.TableSection = TableRowSection.TableHeader;
            objTipos.mtDispose();

        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                txtCodRef.ReadOnly = true;

                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                foreach (GridViewRow row in gv_Tipos.Rows)
                {
                    if (fId == gv_Tipos.DataKeys[row.RowIndex].Values["Id"].ToString())
                    {
                        txtNombre.Text = gv_Tipos.DataKeys[row.RowIndex].Values["PrgtipNombre"].ToString();
                        txtOrden.Text = gv_Tipos.DataKeys[row.RowIndex].Values["PrgtipOrden"].ToString();
                        txtIcono.Text = gv_Tipos.DataKeys[row.RowIndex].Values["PrgtipIcono"].ToString();
                        txtCodRef.Text = gv_Tipos.DataKeys[row.RowIndex].Values["PrgtipCodigo"].ToString();
                        txtDescripcion.Value = gv_Tipos.DataKeys[row.RowIndex].Values["PrgTipDescripcion"].ToString();
                        if (Boolean.Parse(gv_Tipos.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()))
                            chkEstado.Checked = true;
                        else
                            chkEstado.Checked = false;
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
            mtHabilitar(true);
            hfId.Value = "0";
            txtCodRef.ReadOnly = false;
            txtNombre.Focus();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mtLoadData();
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                fMensaje = "El campo Nombre se encuentra en blanco, favor llenar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtNombre.Focus();
            }
            else if (String.IsNullOrEmpty(txtCodRef.Text))
            {
                fMensaje = "El campo Codigo / Referencia no puede estar vacío, favor llenar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtNombre.Focus();
            }
            else if (String.IsNullOrEmpty(txtIcono.Text))
            {
                fMensaje = "El campo icono no puede estar vacío, favor llenar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtIcono.Focus();
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        private void mtHabilitar(Boolean vEsNewEdit)
        {
            if (vEsNewEdit)
            {
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtNombre.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;
                txtNombre.Text = string.Empty;
                txtCodRef.Text = string.Empty;
                txtCodRef.ReadOnly = false;
                txtIcono.Text = string.Empty;
                txtOrden.Text = string.Empty;
                txtDescripcion.Value = String.Empty;
                chkEstado.Checked = false;
                hfId.Value = "0";

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!mtValidar())
                    return;

                objTipos = new MaxBll.clsTiposPrg();
                objTipos.mtPutTipoPrg(
                          Int32.Parse(hfId.Value)
                        , txtCodRef.Text
                        , txtNombre.Text
                        , txtDescripcion.Value
                        , txtOrden.Text
                        , txtIcono.Text
                        , chkEstado.Checked
                    );

                mtLoadData();
                mtvAddMessage("El registro fue guardado satisfactoriamente", MessageType.success);

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }


    }
}