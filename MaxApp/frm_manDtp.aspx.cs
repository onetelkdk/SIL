using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manDtp : PageBase
    {
        MaxBll.clsClaseDocumentos objClaseDocumentos;
        MaxBll.clsTipoDocumentos objTipoDocumentos;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_manDtp.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        objClaseDocumentos = new MaxBll.clsClaseDocumentos();
                        mtBindDropDownList(cboClaseDocumentos, objClaseDocumentos.mtGetClaseDocumentos(), "AdmDclDescripcion", "AdmDclCodigo", "[Seleccione una Opción]");
                        objClaseDocumentos.mtDispose();
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
            objTipoDocumentos = new MaxBll.clsTipoDocumentos();
            mtBindGridView(gv_TipoDocumentos, objTipoDocumentos.mtGetTipoDocumentos());
            objTipoDocumentos.mtDispose();

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
                cboClaseDocumentos.SelectedValue = "0";
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

                foreach (GridViewRow row in gv_TipoDocumentos.Rows)
                {
                    if (fId == gv_TipoDocumentos.DataKeys[row.RowIndex].Values["AdmDtpCodigo"].ToString())
                    {
                        txtDescripcion.Text = gv_TipoDocumentos.DataKeys[row.RowIndex].Values["AdmDtpDescripcion"].ToString();
                        try
                        {
                            cboClaseDocumentos.SelectedValue = gv_TipoDocumentos.DataKeys[row.RowIndex].Values["AdmDclCodigo"].ToString();
                        }
                        catch
                        { }
                        if (Boolean.Parse(gv_TipoDocumentos.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()))
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
            else if (cboClaseDocumentos.SelectedValue == "0")
            {
                fMensaje = "Seleccione una Clase de Documentos";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboClaseDocumentos.Focus();
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

                objTipoDocumentos = new MaxBll.clsTipoDocumentos();
                objTipoDocumentos.mtPutTipoDocumentos
                    (
                    Int32.Parse(hfId.Value)
                   , txtDescripcion.Text.Trim()
                   , Int32.Parse(cboClaseDocumentos.SelectedValue)
                   , fEstado
                );
                objTipoDocumentos.mtDispose();
                mtLoadData();
                mtvAddMessage("Tipo Documento guardado satisfactoriamente", MessageType.success);
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