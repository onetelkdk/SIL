using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manprg : PageBase
    {
        MaxBll.clsProgramas objProgramas;
        MaxBll.clsModulos objModulos;
        MaxBll.clsTiposPrg objTipos;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_manprg.aspx", lblModulo))
                    {
                        objModulos = new MaxBll.clsModulos();
                        objTipos = new MaxBll.clsTiposPrg();

                        MenuFlotante.InnerHtml = mtGetMenu();

                        mtBindDropDownList(cboModulo, objModulos.mtGetModulos(String.Empty), "AdmmodNombre", "AdmmodCodigo", "Seleccione el Módulo");
                        mtBindDropDownList(cboOpciones, objTipos.mtGetTipos(), "PrgtipNombre", "PrgtipCodigo", "Seleccione la Opción/Tipo");
                        cboModulo.Dispose();
                        objTipos.mtDispose();

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
            objProgramas = new MaxBll.clsProgramas();
            mtBindGridView(gv_Programas, objProgramas.mtGetProgramas(String.Empty, String.Empty));
            gv_Programas.UseAccessibleHeader = true;
            gv_Programas.HeaderRow.TableSection = TableRowSection.TableHeader;
            objProgramas.mtDispose();

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
                txtNombre.Text = String.Empty;
                txtDescripcion.Text = String.Empty;
                cboModulo.SelectedValue = "0";
                cboOpciones.SelectedValue = "0";
                txtIcono.Text = String.Empty;
                txtColorFondo.Text = String.Empty;
                txtOrden.Text = String.Empty;
                chkEstado.Checked = false;
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

                foreach (GridViewRow row in gv_Programas.Rows)
                {
                    if (fId == gv_Programas.DataKeys[row.RowIndex].Values["Id"].ToString())
                    {

                        txtNombre.Text = gv_Programas.DataKeys[row.RowIndex].Values["PrgprgNombre"].ToString();
                        txtDescripcion.Text = gv_Programas.DataKeys[row.RowIndex].Values["PrgprgDescripcion"].ToString();
                        cboModulo.SelectedValue = gv_Programas.DataKeys[row.RowIndex].Values["AdmmodCodigo"].ToString();
                        cboOpciones.SelectedValue = gv_Programas.DataKeys[row.RowIndex].Values["PrgtipCodigo"].ToString();
                        txtIcono.Text = gv_Programas.DataKeys[row.RowIndex].Values["PrgprgIcono"].ToString();
                        txtColorFondo.Text = gv_Programas.DataKeys[row.RowIndex].Values["PrgPrgColorFondo"].ToString();
                        txtOrden.Text = gv_Programas.DataKeys[row.RowIndex].Values["PrgprgOrden"].ToString();
                        if (Boolean.Parse(gv_Programas.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()))
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
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                fMensaje = "El campo Nombre Programa se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtNombre.Focus();
            }
            else if (String.IsNullOrEmpty(txtDescripcion.Text))
            {
                fMensaje = "El campo Descripción se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtDescripcion.Focus();
            }

            else if (cboModulo.SelectedValue == "0")
            {
                fMensaje = "Tiene que seleccionar un Módulo";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboModulo.Focus();
            }
            else if (cboOpciones.SelectedValue == "0")
            {
                fMensaje = "Tiene que seleccionar una Opción";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboOpciones.Focus();
            }

            else if (String.IsNullOrEmpty(txtIcono.Text))
            {
                fMensaje = "El campo Icono se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtIcono.Focus();
            }
            else if (String.IsNullOrEmpty(txtColorFondo.Text))
            {
                fMensaje = "El campo Color Fondo se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtColorFondo.Focus();
            }



            return String.IsNullOrEmpty(fMensaje);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!mtValidar())
                    return;

                objProgramas = new MaxBll.clsProgramas();
                objProgramas.mtPutProgramas
                    (
                        Int32.Parse(hfId.Value)
                        ,txtNombre.Text
                        ,txtDescripcion.Text
                        ,cboModulo.SelectedValue
                        ,cboOpciones.SelectedValue
                        ,txtIcono.Text
                        ,txtColorFondo.Text
                        ,Int16.Parse(txtOrden.Text)
                        ,chkEstado.Checked
                    );
                objProgramas.mtDispose();
                mtLoadData();
                mtvAddMessage("Programa guardado satisfactoriamente", MessageType.success);

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