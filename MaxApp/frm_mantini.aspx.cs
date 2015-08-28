using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mantini : PageBase
    {
        MaxBll.clsTipoIniciativa objTipoIniciativa;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            try
            {
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_mantini.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();
                        mtLoadData();
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
            objTipoIniciativa = new MaxBll.clsTipoIniciativa();
            mtBindGridView(gv_TiposIniciativas, objTipoIniciativa.mtGetTiposIniciativas());
            objTipoIniciativa.mtDispose();

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
                txtDescripcion.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;

                txtDescripcion.Text = String.Empty;
                cboEstado.SelectedValue = "1";
                hfId.Value = "0";

            }
        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                mtHabilitar(true);

                foreach (GridViewRow row in gv_TiposIniciativas.Rows)
                {
                    if (fId == gv_TiposIniciativas.DataKeys[row.RowIndex].Values["IniTipCodigo"].ToString())
                    {
                        txtDescripcion.Text = gv_TiposIniciativas.DataKeys[row.RowIndex].Values["IniTipDescripcion"].ToString();
                        if (Boolean.Parse(gv_TiposIniciativas.DataKeys[row.RowIndex].Values["AdmstsCodigo"].ToString()))
                            cboEstado.SelectedValue = "1";
                        else
                            cboEstado.SelectedValue = "0";
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (String.IsNullOrEmpty(txtDescripcion.Text))
            {
                fMensaje = "El campo Descripción se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtDescripcion.Focus();
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtDescripcion.Focus();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean fEstado = false;

                if (!mtValidar())
                    return;

                if (cboEstado.SelectedValue == "1")
                    fEstado = true;

                objTipoIniciativa = new MaxBll.clsTipoIniciativa();
                objTipoIniciativa.mtPutTiposIniciativas
                    (
                    Int32.Parse(hfId.Value)
                   , txtDescripcion.Text.Trim()
                   , fEstado
                );
                objTipoIniciativa.mtDispose();
                mtLoadData();
                mtvAddMessage("Tipo Iniciativa guardada satisfactoriamente", MessageType.success);
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
                mtLoadData();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}