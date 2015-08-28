using MaxBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_mancnt : PageBase
    {
        clsContacto Contacto;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_mancnt.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = true;

                        mtLoadData();
                        mtLoadCombos();
                    }

                }
                catch (Exception ex)
                {

                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }
        }

        private void mtLoadData()
        {
            Contacto = new clsContacto();

            mtBindGridView(GridViewContacto, Contacto.mtGetContacto());
            if (GridViewContacto.Rows.Count > 0)
            {
                GridViewContacto.UseAccessibleHeader = true;
                GridViewContacto.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Contacto.mtDispose();
        }

        private void mtLoadCombos()
        {
            Contacto = new clsContacto();

            String opcion = "[Seleccione una Opción]";
            mtBindDropDownList(ddlTipo, Contacto.mtGetTipoContacto(), "AdmTcnNombre", "AdmTcnCodigo", opcion);
            Contacto.mtDispose();
        }

        protected void VisualizarRegistro(object sender, EventArgs e)
        {
            EditarRegistro(sender, e);
            btnGuardar.Visible = false;
            //txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            ddlTipo.Enabled = false;
            txtDireccion.Enabled = false;
            txtTMovil.Enabled = false;
            txtTCasa.Enabled = false;
            txtTOficina.Enabled = false;
            txtEmail.Enabled = false;
            txtEmpresa.Enabled = false;
            txtWebsite.Enabled = false;
        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                ViewState["hfId"] = fId.ToString();

                int id = int.Parse(ViewState["hfId"].ToString());

                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;


                foreach (GridViewRow row in GridViewContacto.Rows)
                {
                    if (fId == GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntCodigo"].ToString())
                    {
                        txtCodigo.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntCodigo"].ToString();
                        txtNombre.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntNombre"].ToString();
                        ddlTipo.SelectedValue = String.IsNullOrEmpty(GridViewContacto.DataKeys[row.RowIndex].Values["AdmTcnCodigo"].ToString()) ? "0" : GridViewContacto.DataKeys[row.RowIndex].Values["AdmTcnCodigo"].ToString();
                        txtDireccion.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntDireccion"].ToString();
                        txtTMovil.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntMovil"].ToString();
                        txtTCasa.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntTelCasa"].ToString();
                        txtTOficina.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntTelOficina"].ToString();
                        txtEmail.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntEmail"].ToString();
                        txtEmpresa.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntEmpresa"].ToString();
                        txtWebsite.Text = GridViewContacto.DataKeys[row.RowIndex].Values["AdmCntWebsite"].ToString();
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
                ViewState["hfId"] = "0";
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtDireccion.Text = "";
                ddlTipo.SelectedIndex = 0;
                txtTMovil.Text = "";
                txtTCasa.Text = "";
                txtTOficina.Text = "";
                txtEmail.Text = "";
                txtEmpresa.Text = "";
                txtWebsite.Text = "";
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
                ViewState["hfId"] = "0";
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnCancel.Visible = false;
                btnGuardar.Visible = false;

                //txtCodigo.Enabled = true;
                txtNombre.Enabled = true;
                ddlTipo.Enabled = true;
                txtDireccion.Enabled = true;
                txtTMovil.Enabled = true;
                txtTCasa.Enabled = true;
                txtTOficina.Enabled = true;
                txtEmail.Enabled = true;
                txtEmpresa.Enabled = true;
                txtWebsite.Enabled = true;

                if (GridViewContacto.Rows.Count > 0)
                {
                    GridViewContacto.UseAccessibleHeader = true;
                    GridViewContacto.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                mtvAddMessage("frm_mancnt.aspx", "mancnt01", MessageType.warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDireccion.Text.Trim()))
            {
                mtvAddMessage("frm_mancnt.aspx", "mancnt02", MessageType.warning);
                return;
            }

            Contacto = new clsContacto();
            Contacto.mtPutContacto
                (Int32.Parse(ViewState["hfId"].ToString())//Id del Registro
                , txtNombre.Text.Trim()
                , int.Parse(ddlTipo.SelectedValue)
                , txtEmail.Text.Trim()
                , txtEmpresa.Text.Trim()
                ,txtWebsite.Text.Trim()
                ,txtDireccion.Text.Trim()
                , txtTMovil.Text.Trim()
                , txtTOficina.Text.Trim()
                , txtTCasa.Text.Trim()
                , Session["AdmusrCodigo"].ToString()
                ,true);

            ViewState["hfId"] = "0";
            mtvAddMessage("frm_ManMsj.aspx", "MensajeSatisfactorio", MessageType.success);

            PanelMantenimientos.Visible = false;
            btnGuardar.Visible = false;
            btnNuevo.Visible = true;
            btnCancel.Visible = false;
            panelLista.Visible = true;

            mtLoadData();
        }
    }
}