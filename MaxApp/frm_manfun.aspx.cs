using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Custom references
using clsFuncion = MaxBll.clsFuncion;

namespace MaxApp
{
    public partial class frm_manfun : PageBase
    {
        #region Properties

        private List<Control> controlsRequired;
        private List<Estado> ListEstado;

        #endregion

        #region Initializers

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            if (!Page.IsPostBack)
            {
                if (mtValidPage("frm_manfun.aspx", lblModulo))
                {
                    MenuFlotante.InnerHtml = mtGetMenu();
                    mtHabilitar(false);
                    //    SetDefaultFilter();
                    LoadData("gv_Funciones");
                    LoadData("Catálogo", "Estado", "EstadoFilter");
                }
            }

            if (controlsRequired == null)
            {
                controlsRequired = new List<Control>()
                    {
                        DropDownCatalogo,
                        DropDownEstado,
                        txtDescripcionFuncion 
                    };
            }
        }

        #endregion

        #region Methods [Events]

        protected void btn_Click(object sender, EventArgs e)
        {
            string fCodigo = hfId.Value;
            switch ((sender as Button).ID)
            {
                case "btnNuevo":
                    mtHabilitar(true);
                    break;

                case "btnGuardar":
                    var objFuncion = new clsFuncion();
                    if (mtValidar())
                    {
                        objFuncion.mtPutFuncion
                        (
                            clsFuncion.TipoRegistro.Crear,
                            (String.IsNullOrEmpty(fCodigo)) ? "0" : fCodigo,
                            txtDescripcionFuncion.Text,
                            DropDownCatalogo.SelectedValue,
                            null,
                            DropDownEstado.SelectedValue,
                            Session["AdmusrCodigo"]
                        );

                        mtvAddMessage(String.Format("Registro {0} satisfactoriamente.", (fCodigo.Equals("0") ? "Agregado" : "Actualizado")), MessageType.success);
                        LoadData("gv_Funciones");
                        hfId.Value = "0";
                    }
                    else
                    {
                        btnGuardar.Enabled = true;
                        return;
                    }
                    mtHabilitar(false);
                    btnGuardar.Enabled = true;
                    break;

                case "btnCancelar":
                    mtHabilitar(false);
                    LoadData("gv_Funciones");
                    break;

                case "btnEdit":
                    mtHabilitar(true);
                    EditarRegistro((sender as Button).CommandArgument);
                    break;

                case "btnVisualizar":
                    EditarRegistro((sender as Button).CommandArgument);
                    mtHabilitar(true, true);
                    break;

                case "btnBuscar":
                    var objFuncion2 = new clsFuncion();
                    mtClearGridView(gv_Funciones);
                    mtBindGridView(gv_Funciones, objFuncion2.mtGetFuncion(-1, Convert.ToChar(DropDownSearchEstado.SelectedValue)));
                    break;
            }
        }

        #endregion

        #region Methods [Others]

        private void LoadData(params object[] tipoObjeto)
        {
            var objFuncion = new clsFuncion();

            foreach (string objeto in tipoObjeto)
            {
                switch (objeto)
                {
                    case "gv_Funciones":
                            mtHabilitar(false);
                            mtClearGridView(gv_Funciones);
                            mtBindGridView(gv_Funciones, objFuncion.mtGetFuncion(-1,'1'));
                        break;

                    case "CódigoSis": txtCodigoSis.Text = objFuncion.mtGetNextmAdmFunCodigo().ToString();
                        break;

                    case "Catálogo": mtBindDropDownList(DropDownCatalogo, objFuncion.mtGetmAdmCat(2), "AdmCatNombre", "AdmCatCodigo", "Seleccione catálogo"); // Param (2): ALL
                        break;

                    case "Estado":
                        mtBindDropDownList(DropDownEstado, GetEstados(), "Description", "IsActive", "Seleccione estado");
                        break;

                    case "EstadoFilter":
                        DropDownSearchEstado.Items.Insert(0, new ListItem("Inactivo", "0"));
                        DropDownSearchEstado.Items.Insert(1, new ListItem("Activo", "1"));
                        DropDownSearchEstado.Items.Insert(2, new ListItem("Todos", "2"));
                        DropDownSearchEstado.SelectedValue = "1";
                        DropDownSearchEstado.Enabled = true;
                        break;
                }
            }

            objFuncion.mtDispose();
        }

        private void EditarRegistro(string argument)
        {
            try
            {
                string fCodigo = argument;
                hfId.Value = fCodigo;

                foreach (GridViewRow row in gv_Funciones.Rows)
                {
                    if (fCodigo == gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString())
                    {
                        txtCodigoSis.Text = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString()) ? "-1" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString();
                        DropDownCatalogo.SelectedValue = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmCatCodigo"].ToString()) ? "0" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmCatCodigo"].ToString();
                        DropDownEstado.SelectedValue = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "0" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString();
                        txtDescripcionFuncion.Text = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunDescripcion"].ToString()) ? "0" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunDescripcion"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtHabilitar(bool vEsNewEdit, bool isReadOnly = false)
        {
            if (vEsNewEdit)
            {
                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = (isReadOnly) ? false : true;
                btnCancelar.Visible = true;
                LoadData("CódigoSis");

            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                LimpiarControles();
            }

            if (controlsRequired != null)
            {
                foreach (Control control in controlsRequired)
                {
                    switch (control.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.DropDownList":
                            (control as System.Web.UI.WebControls.DropDownList).Enabled = (isReadOnly) ? false : true;
                            (control as System.Web.UI.WebControls.DropDownList).CssClass = "select-240";
                            break;
                        case "System.Web.UI.WebControls.TextBox": (control as System.Web.UI.WebControls.TextBox).ReadOnly = (isReadOnly) ? true : false;
                            break;
                        case "System.Web.UI.WebControls.ListBox":
                            (control as System.Web.UI.WebControls.ListBox).Enabled = (isReadOnly) ? false : true;
                            (control as System.Web.UI.WebControls.ListBox).CssClass = "selectM-2row";
                            break;
                    }
                }
            }
        }

        private void LimpiarControles()
        {
            if (controlsRequired != null)
            {
                foreach (Control control in controlsRequired)
                {
                    switch (control.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.DropDownList":
                            (control as System.Web.UI.WebControls.DropDownList).ClearSelection();
                            break;
                        case "System.Web.UI.WebControls.TextBox": (control as System.Web.UI.WebControls.TextBox).Text = String.Empty;
                            break;
                        case "System.Web.UI.WebControls.ListBox": (control as System.Web.UI.WebControls.ListBox).ClearSelection();
                            break;
                    }
                }
            }
            //mtClearGridView(gv_Miembros);
            hfId.Value = "0";
        }

        private List<Estado> GetEstados()
        {
            ListEstado = new List<Estado>()
            {
                new Estado { IsActive = false, Description = "Activo" },
                new Estado { IsActive = true, Description = "Inactivo" },
            };
            return ListEstado;
        }

        #endregion

        #region Methods [NonReturns]

        private Boolean mtValidar()
        {
            string fMensaje = default(string);

            if (DropDownCatalogo.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el catálogo.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownCatalogo.Focus();
            }
            else if (DropDownEstado.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el estado.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownEstado.Focus();
            }
            else if (String.IsNullOrEmpty(txtDescripcionFuncion.Text))
            {
                fMensaje = "El campo: Descripción, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtDescripcionFuncion.Focus();
            }
            
            return String.IsNullOrEmpty(fMensaje);
        }

        #endregion

    }

    public class Estado
    {
        public bool IsActive { get; set; }
        public string Description { get; set; } 
    }
}