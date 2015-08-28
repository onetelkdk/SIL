using MaxBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mansini : PageBase
    {

        clsSubTipoIniciativa objSubTipoIniciativa;
        clsTipoIniciativa objTipoIniciativa;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_mansini.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        objTipoIniciativa = new clsTipoIniciativa();
                        mtBindDropDownList(cboTipoIniciativa, objTipoIniciativa.mtGetTiposIniciativas(), "IniTipDescripcion", "IniTipCodigo", "[Seleccione una Opción]");
                        objTipoIniciativa.mtDispose();
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
            objSubTipoIniciativa = new MaxBll.clsSubTipoIniciativa();
            mtBindGridView(gv_SubTiposIniciativas, objSubTipoIniciativa.mtGetSubTiposIniciativas());
            objSubTipoIniciativa.mtDispose();

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
                cboTipoIniciativa.SelectedValue = "0";
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

                foreach (GridViewRow row in gv_SubTiposIniciativas.Rows)
                {
                    if (fId == gv_SubTiposIniciativas.DataKeys[row.RowIndex].Values["IniStpCodigo"].ToString())
                    {
                        txtDescripcion.Text = gv_SubTiposIniciativas.DataKeys[row.RowIndex].Values["IniStpDescripcion"].ToString();
                        try
                        {
                            cboTipoIniciativa.SelectedValue = gv_SubTiposIniciativas.DataKeys[row.RowIndex].Values["IniTipCodigo"].ToString();
                        }
                        catch
                        { }
                        if (Boolean.Parse(gv_SubTiposIniciativas.DataKeys[row.RowIndex].Values["AdmstsCodigo"].ToString()))
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
            else if (cboTipoIniciativa.SelectedValue == "0")
            {
                fMensaje = "Seleccione un Tipo de Iniciativa";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboTipoIniciativa.Focus();
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

                objSubTipoIniciativa = new MaxBll.clsSubTipoIniciativa();
                objSubTipoIniciativa.mtPutSubTiposIniciativas
                    (
                    Int32.Parse(hfId.Value)
                   , txtDescripcion.Text.Trim()
                   , Int32.Parse(cboTipoIniciativa.SelectedValue)
                   , fEstado
                );

                objSubTipoIniciativa.mtDispose();
                mtLoadData();
                mtvAddMessage("SubTipo Iniciativa guardada satisfactoriamente", MessageType.success);
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