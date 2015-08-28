using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manmod : PageBase
    {
        MaxBll.clsModulos objModulos;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_manmod.aspx", lblModulo))
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
            objModulos = new MaxBll.clsModulos();

            mtBindGridView(gv_Modulos, objModulos.mtGetModulos(String.Empty));
            gv_Modulos.UseAccessibleHeader = true;
            gv_Modulos.HeaderRow.TableSection = TableRowSection.TableHeader;
            objModulos.mtDispose();

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
                txtCodigo.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;
                txtCodigo.Text = String.Empty;
                txtNombre.Text = String.Empty;
                txtIcono.Text = String.Empty;
                txtColorFondo.Text = String.Empty;
                txtTamano.Text = String.Empty;
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

                foreach (GridViewRow row in gv_Modulos.Rows)
                {
                    if (fId == gv_Modulos.DataKeys[row.RowIndex].Values["Id"].ToString())
                    {

                        txtCodigo.Text = gv_Modulos.DataKeys[row.RowIndex].Values["AdmmodCodigo"].ToString();
                        txtNombre.Text = gv_Modulos.DataKeys[row.RowIndex].Values["AdmmodNombre"].ToString();
                        txtIcono.Text = gv_Modulos.DataKeys[row.RowIndex].Values["AdmmodIcono"].ToString();
                        txtColorFondo.Text = gv_Modulos.DataKeys[row.RowIndex].Values["AdmModColorFondo"].ToString();
                        txtTamano.Text = gv_Modulos.DataKeys[row.RowIndex].Values["AdmModTamano"].ToString();
                        txtOrden.Text = gv_Modulos.DataKeys[row.RowIndex].Values["AdmmodOrden"].ToString();
                        if (Boolean.Parse(gv_Modulos.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()))
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
                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                fMensaje = "El campo Código Módulo se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtCodigo.Focus();
            }
            else if (String.IsNullOrEmpty(txtNombre.Text))
            {
                fMensaje = "El campo Nombre se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtNombre.Focus();
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
            else if (String.IsNullOrEmpty(txtTamano.Text))
            {
                fMensaje = "El campo Tamaño se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtColorFondo.Focus();
            }
            else if (String.IsNullOrEmpty(txtOrden.Text))
            {
                fMensaje = "El campo Orden se encuentra en blanco, favor insertar";
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

                objModulos = new MaxBll.clsModulos();
                objModulos.mtPutModulos
                (
                    Int32.Parse(hfId.Value)
                    , txtCodigo.Text
                    , txtNombre.Text
                    , Int16.Parse(txtOrden.Text)
                    , txtIcono.Text
                    , txtColorFondo.Text
                    , txtTamano.Text
                    , chkEstado.Checked
                     );
                objModulos.mtDispose();
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