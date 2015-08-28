using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using clsComision = MaxBll.clsComision;
using clsLegislaturaRegistro = MaxBll.clsLegislaturaRegistro;
using clsFuncion = MaxBll.clsFuncion;
using System.Globalization;
using System.Configuration;
using System.Data;

namespace MaxApp
{
    public partial class frm_actCom : PageBase
    {
        #region Properties

        private List<Control> controlsRequired;
        private string defaultDateFrom;
        private int ComActCodigoSis { get; set; }
        private DateTime mainFechaDesde;
        private DateTime mainFechaHasta;

        public DataTable DtMiembros
        {
            get
            {
                DataTable o = (DataTable)ViewState["gv_MiembrosViewState"];
                return (o == null) ? null : o;
            }

            set
            {
                ViewState["gv_MiembrosViewState"] = value;
            }
        }

        #endregion

        #region Initializers

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            this.PreRender += Frm_actCom_PreRender;

            if (!Page.IsPostBack)
            {
                if (mtValidPage("frm_actCom.aspx", lblModulo))
                {
                    MenuFlotante.InnerHtml = mtGetMenu();
                    mtHabilitar(false);
                    SetDefaultFilter();
                    LoadData("gv_ActividadComisiones");
                    LoadData("Comision","Iniciativas","Estado","TipoReunion","TipoActividad","Legislatura","LugarActividad","FuncionariosEncargados","Miembros","Funcion");
                }         
            }

            if (controlsRequired == null)
            {
                controlsRequired = new List<Control>()
                {
                    DropDownEstado,
                    DropDownComision,
                    DropDownListTipoActividad,
                    DropDownLegislatura,
                    DropDownLugarActividad,
                    DropDownFuncionarios,
                    DropDownTipoReunion,
                    DropDownMiembros,
                    DropDownFuncion,
                    txtNroActividad,
                    txtDiaSemana,
                    txtFechaActividad,
                    listBoxNroIniciativa,
                    txtDescripcionActividad,
                    txtResultadosActividad,
                    txtInvitados,
                    txtHoraConvocatoria,
                    txtHoraInicio,
                    txtHoraCierre,   
                };
            }
        }

        #endregion

        #region Methods [events]

        private void Frm_actCom_PreRender(object sender, EventArgs e)
        {
            ViewState.Add("gv_MiembrosViewState", (DataTable)gv_Miembros.DataSource);
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string fCodigo = hfId.Value;
            string condition = String.Format("AdmLegCodigo = {0} AND AdmFunCodigo = {1}", DropDownMiembros.SelectedValue, DropDownFuncion.SelectedValue); // Case: Agregar/Quitar miembro
            switch ((sender as Button).ID)
            {
                case "btnBuscar":
                    mainFechaDesde = Convert.ToDateTime(Request.Form[txtFechaDesde.UniqueID]);
                    mainFechaHasta = Convert.ToDateTime(Request.Form[txtFechaHasta.UniqueID]);
                    LoadData("gv_ActividadComisiones");
                    break;

                case "btnLimpiar":
                    SetDefaultFilter();
                    LoadData("gv_ActividadComisiones");
                    break;

                case "btnNuevo":
                    mtHabilitar(true);
                    break;

                case "btnGuardar":
                    var objComision = new clsComision();
                    if (mtValidar())
                    {
                        #region Registro principal

                        List<string> selectedIniciativas = new List<string>();
                        foreach (ListItem iniciativa in listBoxNroIniciativa.Items)
                        {
                            if (iniciativa.Selected)
                                selectedIniciativas.Add(iniciativa.Value);
                        }
                        objComision.mtPutmComAct
                        (
                            clsComision.TipoRegistro.Crear,
                            (String.IsNullOrEmpty(fCodigo)) ? "0" : fCodigo,
                            DropDownListTipoActividad.SelectedValue,
                            DropDownComision.SelectedValue,
                            Request.Form[txtFechaActividad.UniqueID],
                            DropDownLegislatura.SelectedValue,
                            string.Join(";",selectedIniciativas.ToArray()),
                            null,
                            DropDownTipoReunion.SelectedValue,
                            txtDescripcionActividad.Text,
                            txtResultadosActividad.Text,
                            txtInvitados.Text,
                            DropDownLugarActividad.SelectedValue,
                            Request.Form[txtHoraConvocatoria.UniqueID],
                            Request.Form[txtHoraInicio.UniqueID],
                            Request.Form[txtHoraCierre.UniqueID],
                            DropDownFuncionarios.SelectedValue,
                            DropDownEstado.SelectedValue,
                            Session["AdmusrCodigo"]
                        );

                        #endregion

                        #region Sub-registro miembros

                        if (DtMiembros != null)
                        {
                            // Eliminación de todos los registros
                            objComision.mtPutdComAct
                            (
                                clsComision.TipoRegistro.Eliminar,
                                fCodigo
                            );

                            // Ingreso de nuevos registros
                            for (int i = 0; i < DtMiembros.Rows.Count; i++)
                            {
                                objComision.mtPutdComAct
                                (
                                    clsComision.TipoRegistro.Crear,
                                    fCodigo,
                                    DtMiembros.Rows[i][2],
                                    DtMiembros.Rows[i][4],
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    Session["AdmusrCodigo"]
                                );
                            }
                        }

                        #endregion

                        mtvAddMessage(String.Format("Registro {0} satisfactoriamente.", (fCodigo.Equals("0") ? "Agregado" : "Actualizado")), MessageType.success);
                        SetDefaultFilter();
                        LoadData("gv_ActividadComisiones");
                        hfId.Value = "0";
                    }
                    else
                    {
                        txtFechaActividad.Text = Request.Form[txtFechaActividad.UniqueID];
                        txtHoraConvocatoria.Text = Request.Form[txtHoraConvocatoria.UniqueID];
                        txtHoraInicio.Text = Request.Form[txtHoraInicio.UniqueID];
                        txtHoraCierre.Text = Request.Form[txtHoraCierre.UniqueID];
                        btnGuardar.Enabled = true;
                        return;
                    }
                    mtHabilitar(false);
                    btnGuardar.Enabled = true;
                    break;

                case "btnCancel":
                    mtHabilitar(false);
                    SetDefaultFilter();
                    LoadData("gv_ActividadComisiones");
                    break;

                case "btnEdit":
                    mtHabilitar(true);
                    EditarRegistro((sender as Button).CommandArgument);
                    LoadData("gv_Miembros");
                    goto default;

                case "btnVisualizar":
                    EditarRegistro((sender as Button).CommandArgument);
                    mtHabilitar(true, true);
                    goto default;

                case "btnAgregar":
                    if(DtMiembros != null)
                    {
                        if (!DtMiembros.Select(condition).Any())
                        {
                            DtMiembros.Rows.Add
                            (
                                new object[]
                                {
                                    -1,
                                    fCodigo,
                                    DropDownMiembros.SelectedValue,
                                    DropDownMiembros.SelectedItem.Text,
                                    DropDownFuncion.SelectedValue,
                                    DropDownFuncion.SelectedItem.Text
                                }
                            );
                        }
                        else
                        {
                            mtvAddMessage("El miembro ya ha sido agregado.", MessageType.error);
                            mtClearGridView(gv_Miembros);
                            mtBindGridView(gv_Miembros, DtMiembros);
                            return;
                        }
                        mtClearGridView(gv_Miembros);
                        mtBindGridView(gv_Miembros, DtMiembros);
                    }
                    break;

                case "btnQuitar":
                    if (DtMiembros != null)
                    {
                        var AdmLegCodigo = (sender as Button).CommandArgument;
                        DataRow row = DtMiembros.Select(String.Format("ComActNumero = '{0}' AND AdmLegCodigo = '{1}'", fCodigo, AdmLegCodigo)).FirstOrDefault();
                        if(row != null)
                            DtMiembros.Rows.Remove(row);
                    }
                        mtClearGridView(gv_Miembros);
                        mtBindGridView(gv_Miembros, DtMiembros);
                    break;
                
                default:
                    break;
            }
        }

        #endregion

        #region Methods

        private void LoadData(params object[] tipoObjeto)
        {
            var objComision = new clsComision();
            var objLegislaturaRegistro = new clsLegislaturaRegistro(false);

            foreach (string objeto in tipoObjeto)
            {
                switch (objeto)
                {
                    case "gv_ActividadComisiones":
                        mtHabilitar(false);
                        mtClearGridView(gv_ActividadComisiones);
                        mtBindGridView(gv_ActividadComisiones, objComision.mtGetmComAct(mainFechaDesde, mainFechaHasta));
                        if(gv_ActividadComisiones.Rows.Count > 0)
                        {
                            gv_ActividadComisiones.UseAccessibleHeader = true;
                            gv_ActividadComisiones.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                        break;

                    case "gv_Miembros":
                        DtMiembros = objComision.mtGetmComAct(ComActCodigoSis);               
                        mtClearGridView(gv_Miembros);
                        mtBindGridView(gv_Miembros, DtMiembros);
                        break;

                    case "NroActividad": txtNroActividad.Text = objComision.mtGetNextComActNumero();
                        break;

                    case "Iniciativas": 
                        mtBindListBox(listBoxNroIniciativa, objLegislaturaRegistro.mIniIniGetDatos(Convert.ToDateTime("01/01/1800"), DateTime.Now, false, null, "30"), "IniIniNumero", "IniIniCodigoSis", "Seleccione iniciativa");
                        break;

                    case "Comision": mtBindDropDownList(DropDownComision, objComision.mtGetComision(), "ComComNombre", "ComCfmId", "Seleccione comisión");
                        break;

                    case "Estado": mtBindDropDownList(DropDownEstado, objComision.mtGetdAdmEst(5), "AdmEstDescripcion", "AdmEstCodigo", "Seleccione estado");
                        break;

                    case "TipoReunion": mtBindDropDownList(DropDownTipoReunion, objComision.mtGetmAdmPar("sesionreunion"), "AdmParString", "AdmParNumerico", "Seleccione tipo de reunión");
                        break;

                    case "TipoActividad": mtBindDropDownList(DropDownListTipoActividad, objComision.mtGetmAdmPar("tipoactividad"), "AdmParString", "AdmParNumerico", "Seleccione tipo de actividad");
                        break;

                    case "Legislatura": mtBindDropDownList(DropDownLegislatura, objLegislaturaRegistro.mtGetmAdmLet(), "AdmLetDescripcion", "AdmLetCodigo", "Seleccione legislatura");
                        break;

                    case "LugarActividad": mtBindDropDownList(DropDownLugarActividad, objComision.mtGetmAdmSal(), "AdmSalDescripcion", "AdmSalCodigo", "Seleccione lugar de actividad");
                        break;

                    case "FuncionariosEncargados": mtBindDropDownList(DropDownFuncionarios, objComision.mtGetrFunUsr(), "AdmUsrNombre", "FunUsrCodigo", "Seleccione funcionario encargado");
                        break;

                    case "Miembros": mtBindDropDownList(DropDownMiembros, objLegislaturaRegistro.mAdmLegGetDatos(), "FullName", "AdmLegCodigo", "Seleccionar miembro");
                        break;

                    case "Funcion":
                        var objFuncion = new clsFuncion();
                        mtBindDropDownList(DropDownFuncion, objFuncion.mtGetFuncion(3), "AdmFunDescripcion", "AdmFunCodigo", "Seleccione función");
                        break;
                }
            }

            objComision.mtDispose();
            objLegislaturaRegistro.mAdmParDispose();
        }

        private void EditarRegistro(string argument)
        {
            try
            {
                string fCodigo = argument;
                hfId.Value = fCodigo;

                foreach (GridViewRow row in gv_ActividadComisiones.Rows)
                {
                    if (fCodigo == gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActNumero"].ToString())
                    {
                        string DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Convert.ToDateTime(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActFecha"]).DayOfWeek);
                        listBoxNroIniciativa.ClearSelection(); // Borrar selección: 0
                        ComActCodigoSis =                           String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActCodigoSis"].ToString()) ? 0 : Convert.ToInt32(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActCodigoSis"]);
                        DropDownEstado.SelectedValue =              String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString();
                        DropDownComision.SelectedValue =            String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComCfmId"].ToString()) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComCfmId"].ToString();
                        DropDownListTipoActividad.SelectedValue =   String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActTipo"].ToString()) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActTipo"].ToString();
                        DropDownLegislatura.SelectedValue =         (DropDownLegislatura.Items.FindByValue(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["AdmLetCodigo"].ToString()) == null) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["AdmLetCodigo"].ToString();
                        DropDownLugarActividad.SelectedValue =      String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["AdmSalCodigo"].ToString()) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["AdmSalCodigo"].ToString();
                        DropDownFuncionarios.SelectedValue =        String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActFuncionarioCom"].ToString()) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActFuncionarioCom"].ToString();
                        DropDownTipoReunion.SelectedValue =         String.IsNullOrEmpty(gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActTipoReunion"].ToString()) ? "0" : gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActTipoReunion"].ToString();
                        txtNroActividad.Text =                      gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActNumero"].ToString();
                        txtFechaActividad.Text =                    gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActFecha"].ToString();
                        txtDiaSemana.Text =                         String.Concat(DayOfWeek.Substring(0, 1).ToUpper(), DayOfWeek.Substring(1, DayOfWeek.Length - 1));
                        foreach (var iniciativas in gv_ActividadComisiones.DataKeys[row.RowIndex].Values["IniIniCodigoSis"].ToString().Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if((listBoxNroIniciativa.Items.FindByValue(iniciativas)) != null)
                                listBoxNroIniciativa.Items.FindByValue(iniciativas).Selected = true;
                        }
                        txtDescripcionActividad.Text =              gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActDescripcion"].ToString();
                        txtResultadosActividad.Text =               gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActResultados"].ToString();
                        txtInvitados.Text =                         gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActInvitados"].ToString();
                        txtHoraConvocatoria.Text =                  gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActHoraConvoca"].ToString();
                        txtHoraInicio.Text =                        gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActHoraInicio"].ToString();
                        txtHoraCierre.Text =                        gv_ActividadComisiones.DataKeys[row.RowIndex].Values["ComActHoraCierre"].ToString();
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
                btnCancel.Visible = true;
                DropDownEstado.SelectedValue = "18"; // Estado: CONVOCADA
                LoadData("NroActividad");

                if (hfId.Value.Equals("0"))
                {
                    // PESTAÑAS DE EDICIÓN
                    //tabComision.Visible = false;
                    //tabDatosControl.Visible = false;
                    //tabMiembrosComision.Visible = false;
                }
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;
                //tabComision.Visible = true;
                //tabDatosControl.Visible = true;
                //tabMiembrosComision.Visible = true;
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
            mtClearGridView(gv_Miembros);
            hfId.Value = "0";
        }

        private void SetDefaultFilter()
        {
            DateTime outFechaDesde = DateTime.Today;
            if (DateTime.TryParse(mainFechaDesde.ToString(), out outFechaDesde))
                mainFechaDesde = DateTime.Now.AddDays(-Convert.ToDouble(GetValueAppSettings("LimitDays_actCom") ?? DateTime.Now.ToString()));

            if (DateTime.TryParse(mainFechaHasta.ToString(), out outFechaDesde))
                mainFechaHasta = DateTime.Now;

            defaultDateFrom = GetValueAppSettings("LimitDays_actCom") ?? DateTime.Now.ToString();
            txtFechaDesde.Text = mainFechaDesde.ToString("dd/MM/yyyy");
            txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        #endregion

        #region Methods [NonReturns]

        private Boolean mtValidar()
        {
            string fMensaje = default(string);

            if (DropDownComision.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar la comisión.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownComision.Focus();
            }
            else if (String.IsNullOrEmpty(txtFechaActividad.Text))
            {
                fMensaje = "El campo: Fecha de actividad, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtFechaActividad.Focus();
            }
            else if (DropDownEstado.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el estado.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownEstado.Focus();
            }
            else if (listBoxNroIniciativa.SelectedValue.Equals("0") || listBoxNroIniciativa.SelectedIndex == -1)
            {
                fMensaje = "Debe seleccionar las iniciativas.";
                mtvAddMessage(fMensaje, MessageType.warning);
                listBoxNroIniciativa.Focus();
            }
            else if (DropDownTipoReunion.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el tipo de reunión.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownTipoReunion.Focus();
            }
            else if (DropDownListTipoActividad.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el tipo de actividad.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownListTipoActividad.Focus();
            }
            else if (String.IsNullOrEmpty(txtDescripcionActividad.Text))
            {
                fMensaje = "El campo: Descripción de la actividad, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtDescripcionActividad.Focus();
            }
            else if (String.IsNullOrEmpty(txtResultadosActividad.Text))
            {
                fMensaje = "El campo: Resultados de la actividad, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtResultadosActividad.Focus();
            }
            else if (String.IsNullOrEmpty(txtInvitados.Text))
            {
                fMensaje = "El campo: Invitados, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtInvitados.Focus();
            }
            else if (!DropDownLegislatura.SelectedItem.Text.Substring(0, 4).Equals(DateTime.Now.Year.ToString()))
            {
                if(DropDownLegislatura.SelectedValue.Equals("0"))
                {
                    fMensaje = "Debe seleccionar la legislatura.";
                    mtvAddMessage(fMensaje, MessageType.warning);
                    DropDownLegislatura.Focus();
                }
                else
                {
                    fMensaje = "La legislatura debe ser del período actual.";
                    mtvAddMessage(fMensaje, MessageType.warning);
                    DropDownLegislatura.Focus();
                }
            }
            else if (DropDownLugarActividad.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el lugar de actividad.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownLugarActividad.Focus();
            }
            else if (String.IsNullOrEmpty(txtHoraInicio.Text))
            {
                fMensaje = "El campo: Hora de inicio, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtHoraInicio.Focus();
            }
            else if (String.IsNullOrEmpty(txtHoraConvocatoria.Text))
            {
                fMensaje = "El campo: Hora de convocatoria, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtHoraConvocatoria.Focus();
            }
            else if (String.IsNullOrEmpty(txtHoraCierre.Text))
            {
                fMensaje = "El campo: Hora de cierre, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtHoraCierre.Focus();
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        private String GetValueAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion
    }
}