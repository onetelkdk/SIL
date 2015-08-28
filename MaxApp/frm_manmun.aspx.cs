using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Custom references
using clsMunicipios = MaxBll.clsMunicipios;

namespace MaxApp
{
    public partial class frm_manmun : PageBase
    {
        MaxBll.clsProvincias objProvincias;
        MaxBll.clsMunicipios objMunicipios;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if(!Page.IsPostBack)
                {
                    if (mtValidPage("frm_manmun.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();

                        objProvincias = new MaxBll.clsProvincias();
                        mtBindDropDownList(cboProvincia, objProvincias.mtGetProvincias(), "AdmPrvDescripcion", "AdmPrvCodigo", "[Seleccione una Opción]");
                        objProvincias.mtDispose();
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
            objMunicipios = new MaxBll.clsMunicipios();
            mtBindGridView(gv_Municipios, objMunicipios.mtGetMunicipios());
            objMunicipios.mtDispose();

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
                txtMunicipio.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;

                txtMunicipio.Text = String.Empty;
                cboProvincia.SelectedValue = "0";
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

                foreach (GridViewRow row in gv_Municipios.Rows)
                {
                    if (fId == gv_Municipios.DataKeys[row.RowIndex].Values["AdmMunCodigo"].ToString())
                    {
                        txtMunicipio.Text = gv_Municipios.DataKeys[row.RowIndex].Values["AdmMunDescripcion"].ToString();
                        try
                        {
                            cboProvincia.SelectedValue = gv_Municipios.DataKeys[row.RowIndex].Values["AdmPrvCodigo"].ToString();
                        }
                        catch
                        { }
                        if (Boolean.Parse(gv_Municipios.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()))
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


            if (cboProvincia.SelectedValue == "0")
            {
                fMensaje = "Seleccione una Provincia";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboProvincia.Focus();
            }
            else if (String.IsNullOrEmpty(txtMunicipio.Text))
            {
                fMensaje = "El campo Nombre Municipio se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtMunicipio.Focus();
            }
             

            return String.IsNullOrEmpty(fMensaje);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtMunicipio.Focus();
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

                objMunicipios = new MaxBll.clsMunicipios();
                objMunicipios.mtPutMunicipios
                    (
                    Int32.Parse(hfId.Value)
                   ,txtMunicipio.Text.Trim()
                   , Int32.Parse(cboProvincia.SelectedValue)
                   , fEstado
                );
                objMunicipios.mtDispose();
                mtLoadData();
                mtvAddMessage("Municipio guardado satisfactoriamente", MessageType.success);
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