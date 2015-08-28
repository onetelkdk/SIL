using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_ManDcl : PageBase
    {
        MaxBll.clsClaseDocumentos objClaseDocumentos;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            try
            {
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_ManDcl.aspx", lblModulo))
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
            objClaseDocumentos = new MaxBll.clsClaseDocumentos();
            mtBindGridView(gv_ClaseDocumentos, objClaseDocumentos.mtGetClaseDocumentos());
            objClaseDocumentos.mtDispose();

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

                txtDescripcion.Value = String.Empty;
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

                foreach (GridViewRow row in gv_ClaseDocumentos.Rows)
                {
                    if (fId == gv_ClaseDocumentos.DataKeys[row.RowIndex].Values["AdmDclCodigo"].ToString())
                    {
                        txtDescripcion.Value = gv_ClaseDocumentos.DataKeys[row.RowIndex].Values["AdmDclDescripcion"].ToString();
                        if (Boolean.Parse(gv_ClaseDocumentos.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()))
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


            if (String.IsNullOrEmpty(txtDescripcion.Value))
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

                objClaseDocumentos  = new MaxBll.clsClaseDocumentos();
                objClaseDocumentos.mtPutClaseDocumentos
                    (
                    Int32.Parse(hfId.Value)
                   , txtDescripcion.Value.Trim()
                   , fEstado
                );
                objClaseDocumentos.mtDispose();
                mtLoadData();
                mtvAddMessage("Clase de Documentos guardada satisfactoriamente", MessageType.success);
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