using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_manusr : PageBase
    {
        MaxBll.clsUsuarios objUsuarios;
        MaxBll.clsRoles objRoles;
        MaxBll.clsDepartamentos objDepartamentos;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            try
            {
                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_manusr.aspx", lblModulo))
                    {
                        MenuFlotante.InnerHtml = mtGetMenu();

                        mtLoadRoles();
                        mtLoadDepartamentos();

                        mtLoadData();
                    }
                   
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }


        private void mtLoadRoles()
        {
            objRoles = new MaxBll.clsRoles();
            mtBindDropDownList(cboRoles, objRoles.mtGetRoles(), "AdmrolDescripcion", "AdmrolCodigo", "Seleccione un Rol");
            objRoles.mtDispose();

        }

        private void mtLoadDepartamentos()
        {
            objDepartamentos = new MaxBll.clsDepartamentos();
            mtBindDropDownList(cboDepartamentos, objDepartamentos.mtGetDepartamentos(), "AdmDepNombre", "AdmDepCodigo", "Seleccione un Departamento");
            objDepartamentos.mtDispose();

        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                mtHabilitar(true);
                txtUsuario.ReadOnly = true;
                txtClave.ReadOnly = true;
                txtConfirmClave.ReadOnly = true;

                foreach (GridViewRow row in gv_Usuarios.Rows)
                {
                    if (fId == gv_Usuarios.DataKeys[row.RowIndex].Values["Id"].ToString())
                    {
                        txtUsuario.Text = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmusrCodigo"].ToString();
                        txtNombre.Text = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmusrNombre"].ToString();
                        txtClave.Text = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmusrPassword"].ToString();
                        txtConfirmClave.Text = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmusrPassword"].ToString();
                        txtCorreo.Text = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmusrCorreo"].ToString();
                        cboRoles.SelectedValue = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmrolCodigo"].ToString();
                        hfCambiarClave.Value = gv_Usuarios.DataKeys[row.RowIndex].Values["AdmusrCambiarClave"].ToString();
                        cboEstado.SelectedValue = Boolean.Parse(gv_Usuarios.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "1" : "0";
                        cboDepartamentos.SelectedValue = String.IsNullOrEmpty(gv_Usuarios.DataKeys[row.RowIndex].Values["AdmDepCodigo"].ToString().Trim()) ? "0" : gv_Usuarios.DataKeys[row.RowIndex].Values["AdmDepCodigo"].ToString();

                        break;
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
            objUsuarios = new MaxBll.clsUsuarios();
            mtBindGridView(gv_Usuarios, objUsuarios.mtGetUsuario(String.Empty));
            gv_Usuarios.UseAccessibleHeader = true;
            gv_Usuarios.HeaderRow.TableSection = TableRowSection.TableHeader;
            objUsuarios.mtDispose();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mtLoadData();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            mtHabilitar(true);
            hfCambiarClave.Value = "False";
            hfId.Value = "0";
            txtUsuario.Focus();
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (String.IsNullOrEmpty(txtUsuario.Text))
            {
                fMensaje = "El campo Usuario se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtUsuario.Focus();
            }
            else if (String.IsNullOrEmpty(txtNombre.Text))
            {
                fMensaje = "El campo Nombre Usuario se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtNombre.Focus();
            }

            else if (cboRoles.SelectedValue == "0")
            {
                fMensaje = "Tiene que seleccionar un Rol";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboRoles.Focus();
            }

            else if (String.IsNullOrEmpty(txtClave.Text) && hfId.Value == "0")
            {
                fMensaje = "El campo Contraseña se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtClave.Focus();
            }

            else if (String.IsNullOrEmpty(txtConfirmClave.Text) && hfId.Value == "0")
            {
                fMensaje = "El campo Confirmar Contraseña se encuentra en blanco, favor insertar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtConfirmClave.Focus();
            }
            else if (hfId.Value == "0" && txtClave.Text != txtConfirmClave.Text)
            {
                fMensaje = "El campo Contraseña no conincide con el de confirmación, deben ser iguales";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtConfirmClave.Focus();
            }

            else if (cboDepartamentos.SelectedValue == "0")
            {
                fMensaje = "Tiene que seleccionar un Departamento";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtConfirmClave.Focus();
            }



            return String.IsNullOrEmpty(fMensaje);
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

                txtClave.ReadOnly = false;
                txtConfirmClave.ReadOnly = false;

                txtUsuario.Text = String.Empty;
                txtNombre.Text = String.Empty;
                txtClave.Text = String.Empty;
                txtCorreo.Text = String.Empty;
                cboRoles.SelectedValue = "0";
                hfCambiarClave.Value = "False";
                hfId.Value = "0";
                
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!mtValidar())
                    return;

                Boolean fEstado = false;

                if (cboEstado.SelectedValue == "1")
                    fEstado = true;

                objUsuarios = new MaxBll.clsUsuarios();
                objUsuarios.mtPutUsuarios(
                        Int32.Parse(hfId.Value)
                        , txtUsuario.Text
                        , txtNombre.Text
                        , MaxBll.clsUtils.mtCreateMD5(txtClave.Text.Trim())
                        , txtCorreo.Text
                        , cboRoles.SelectedValue
                        , fEstado
                        ,Int32.Parse(cboDepartamentos.SelectedValue)
                        ,Boolean.Parse(hfCambiarClave.Value)
                    );

                objUsuarios.mtDispose();
                mtLoadData();
                mtvAddMessage("Usuario guardado satisfactoriamente", MessageType.success);

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}