using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_ManExc : PageBase
    {
        MaxBll.clsExcusa objExcusa;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            try
            {
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_ManExc.aspx", lblModulo))
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
            objExcusa = new MaxBll.clsExcusa();
            mtBindGridView(gv_Excusas, objExcusa.mtGetExcusas());
            objExcusa.mtDispose();

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
                txtDescripcionIniciativa.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;

                txtDescripcionIniciativa.Value = String.Empty;
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

                foreach (GridViewRow row in gv_Excusas.Rows)
                {
                    if (fId == gv_Excusas.DataKeys[row.RowIndex].Values["AdmExcCodigo"].ToString())
                    {
                        txtDescripcionIniciativa.Value = gv_Excusas.DataKeys[row.RowIndex].Values["AdmExcDescripcion"].ToString();
                        if (Boolean.Parse(gv_Excusas.DataKeys[row.RowIndex].Values["AdmstsCodigo"].ToString()))
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


            if (String.IsNullOrEmpty(txtDescripcionIniciativa.Value))
            {
                fMensaje = "El campo Descripción se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtDescripcionIniciativa.Focus();
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtDescripcionIniciativa.Focus();
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

                objExcusa = new MaxBll.clsExcusa();
                objExcusa.mtPutExcusa
                    (
                    Int32.Parse(hfId.Value)
                   , txtDescripcionIniciativa.Value.Trim()
                   , fEstado
                );
                objExcusa.mtDispose();
                mtLoadData();
                mtvAddMessage("Excusa guardada satisfactoriamente", MessageType.success);
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