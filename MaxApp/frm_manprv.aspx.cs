using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manprv : PageBase
    {
        MaxBll.clsProvincias objProvincias;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            try
            {
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_manprv.aspx", lblModulo))
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
            objProvincias = new MaxBll.clsProvincias();
            mtBindGridView(gv_Provincias, objProvincias.mtGetProvincias());
            objProvincias.mtDispose();

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
                txtProvincia.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;

                txtProvincia.Text = String.Empty;
                cboEstado.SelectedValue = "1";
                hfId.Value = "0";

            }
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (String.IsNullOrEmpty(txtProvincia.Text))
            {
                fMensaje = "El campo Nombre Provincia se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtProvincia.Focus();
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                mtHabilitar(true);

                foreach (GridViewRow row in gv_Provincias.Rows)
                {
                    if (fId == gv_Provincias.DataKeys[row.RowIndex].Values["AdmPrvCodigo"].ToString())
                    {
                        txtProvincia.Text = gv_Provincias.DataKeys[row.RowIndex].Values["AdmPrvDescripcion"].ToString();
                        if (Boolean.Parse(gv_Provincias.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()))
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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try 
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtProvincia.Focus();
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

                objProvincias = new MaxBll.clsProvincias();
                objProvincias.mtPutProvincias
                    (
                    Int32.Parse(hfId.Value)
                   , txtProvincia.Text.Trim()
                   , fEstado
                );
                objProvincias.mtDispose();
                mtLoadData();
                mtvAddMessage("Provincia guardada satisfactoriamente", MessageType.success);
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