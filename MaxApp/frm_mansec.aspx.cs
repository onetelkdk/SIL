using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mansec : PageBase
    {
        MaxBll.clsMunicipios objMunicipios;
        MaxBll.clsSectores objSectores;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_mansec.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();

                        objMunicipios = new MaxBll.clsMunicipios();
                        mtBindDropDownList(cboMunicipio, objMunicipios.mtGetMunicipios(), "AdmMunDescripcion", "AdmMunCodigo", "[Seleccione una Opción]");
                        objMunicipios.mtDispose();
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
            objSectores = new MaxBll.clsSectores();
            mtBindGridView(gv_Sectores, objSectores.mtGetSectores());
            objSectores.mtDispose();

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
                txtSector.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;

                txtSector.Text = String.Empty;
                cboMunicipio.SelectedValue = "0";
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

                foreach (GridViewRow row in gv_Sectores.Rows)
                {
                    if (fId == gv_Sectores.DataKeys[row.RowIndex].Values["AdmSecCodigo"].ToString())
                    {
                        txtSector.Text = gv_Sectores.DataKeys[row.RowIndex].Values["AdmSecDescripcion"].ToString();
                        try
                        {
                            cboMunicipio.SelectedValue = gv_Sectores.DataKeys[row.RowIndex].Values["AdmMunCodigo"].ToString();
                        }
                        catch
                        { }
                        if (Boolean.Parse(gv_Sectores.DataKeys[row.RowIndex].Values["AdmStscodigo"].ToString()))
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


            if (cboMunicipio.SelectedValue == "0")
            {
                fMensaje = "Seleccione un Municipio";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboMunicipio.Focus();
            }
            else if (String.IsNullOrEmpty(txtSector.Text))
            {
                fMensaje = "El campo Nombre Sector se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtSector.Focus();
            }


            return String.IsNullOrEmpty(fMensaje);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtSector.Focus();
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

                objSectores = new MaxBll.clsSectores();
                objSectores.mtPutSectores
                    (
                    Int32.Parse(hfId.Value)
                   , txtSector.Text.Trim()
                   , Int32.Parse(cboMunicipio.SelectedValue)
                   , fEstado
                );
                objSectores.mtDispose();
                mtLoadData();
                mtvAddMessage("Sector guardado satisfactoriamente", MessageType.success);
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