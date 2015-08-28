using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

//Custom references
using clsLegislaturaRegistro = MaxBll.clsLegislaturaRegistro;

namespace MaxApp
{
    public partial class frm_manleg : PageBase
    {
        #region Properties

        private List<Control> controlsRequired;

        private string fileNamePerfil;
        public string FileNamePerfil
        {
            get { return fileNamePerfil; }
            set { fileNamePerfil = value; }
        }

        private bool isValidImage = false;
        private const string imagePerfilDefault = @"images/silueta.png";
        
        #endregion

        #region Initializers

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                if (mtValidPage("frm_manleg.aspx", lblModulo))
                {
                    MenuFlotante.InnerHtml = mtGetMenu();
                    mtHabilitar(false);
                    LoadData("gv_Legisladores");
                    LoadData("TipoId", "Sexo", "Posiciones", "Partido", "Periodos", "Estado", "Sector", "Provincia", "Municipio", "Objetivo", "Iniciativas", "EstadoIniciativa");
                }             
            }


            if (controlsRequired == null)
            {
                controlsRequired = new List<Control>()
                {
                    FileUploadASP,
                    DropDownAdmLegTipoId,
                    txtAdmLegCedula,
                    txtAdmlegNombres,
                    txtAdmlegApellido1,
                    txtAdmlegApellido2,
                    DropDownAdmLegSexo,
                    txtAdmlegFechaNac,
                    DropDownAdmPdoCodigo,
                    DropDownAdmFunCodigo,
                    DropDownAdmLegPeriodoElecto,
                    DropDownAdmPdoCodigo,
                    DropDownAdmEstCodigo,
                    txtAdmLegDireccion,
                    DropDownListAdmPrvCodigo,
                    DropDownAdmMunCodigo,
                    DropDownAdmSecCodigo,
                    txtAdmLegCelular,
                    txtAdmLegTelefonoSenado,
                    txtAdmLegFax,
                    txtAdmLegCorreo,
                    txtAdmLegTelefonoProvincial,
                    txtAdmLegApartadoPostal,
                    txtAdmLegTwitter,
                    txtAdmLegSitioWeb,
                    txtAdmLegLinkedin
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
                    var objLegislaturaRegistro = new clsLegislaturaRegistro();
                    if (mtValidar())
                    {
                        SubirImagen();
                        objLegislaturaRegistro.mtPutLegisladores
                        (
                            clsLegislaturaRegistro.TipoRegistro.Crear,
                            (String.IsNullOrEmpty(fCodigo)) ? "0" : fCodigo,
                            DropDownAdmLegTipoId.SelectedValue,
                            txtAdmLegCedula.Text,
                            txtAdmlegNombres.Text,
                            txtAdmlegApellido1.Text,
                            txtAdmlegApellido2.Text,
                            DropDownAdmLegSexo.SelectedValue,
                            DropDownAdmFunCodigo.SelectedValue,
                            DropDownListAdmPrvCodigo.SelectedValue,
                            FileNamePerfil,
                            null,
                            DropDownAdmPdoCodigo.SelectedValue,
                            DropDownAdmLegPeriodoElecto.SelectedValue,
                            null,
                            Request.Form[txtAdmlegFechaNac.UniqueID], //For state: [ReadOnly] instead of txtAdmlegFechaNac.Text
                            txtAdmLegDireccion.Text,
                            null,
                            DropDownAdmMunCodigo.SelectedValue,
                            DropDownAdmSecCodigo.SelectedValue,
                            txtAdmLegCelular.Text,
                            txtAdmLegTelefonoSenado.Text,
                            txtAdmLegTelefonoProvincial.Text,
                            txtAdmLegCorreo.Text,
                            txtAdmLegApartadoPostal.Text,
                            txtAdmLegFax.Text,
                            txtAdmLegSitioWeb.Text,
                            txtAdmLegTwitter.Text,
                            txtAdmLegLinkedin.Text,
                            null,
                            null,
                            null,
                            null,
                            DropDownAdmEstCodigo.SelectedValue,
                            null,
                            null
                        );

                        mtvAddMessage(String.Format("Registro {0} satisfactoriamente.",(fCodigo.Equals("0") ? "Agregado" : "Actualizado")), MessageType.success);
                        LoadData("gv_Legisladores");
                        hfId.Value = "0";
                    }
                    else
                    {
                        txtAdmlegFechaNac.Text = Request.Form[txtAdmlegFechaNac.UniqueID];
                        btnGuardar.Enabled = true;
                        return;
                    }
                    mtHabilitar(false);
                    btnGuardar.Enabled = true;
                    break;

                case "btnCancel": 
                    mtHabilitar(false);
                    LoadData("gv_Legisladores");
                    break;

                case "btnEdit":
                    mtHabilitar(true);
                    EditarRegistro((sender as Button).CommandArgument);
                    goto default;

                case "btnVisualizar":
                    EditarRegistro((sender as Button).CommandArgument);
                    mtHabilitar(true, true);
                    goto default;

                default:
                    LoadData("gv_Comisiones", (sender as Button).CommandArgument);
                    LoadData("gv_Excusas", (sender as Button).CommandArgument);
                    LoadData("gv_Iniciativas", (sender as Button).CommandArgument);
                    break;
            }
        }

        #endregion

        #region Methods [Others]

        private void LoadData(params object[] tipoObjeto)
        {
            var objLegislaturaRegistro = new clsLegislaturaRegistro();
            string fCodigo = default(string); // Argument: ID Legislador
            string legisladorValue = default(string); // Var: Nombres + Apellido1 + Apellido2

            foreach (string objeto in tipoObjeto)
            {
                switch (objeto)
                {
                    case "gv_Legisladores":
                        mtHabilitar(false);
                        mtBindGridView(gv_Legisladores, objLegislaturaRegistro.mAdmLegGetDatos());
                        gv_Legisladores.UseAccessibleHeader = true;
                        gv_Legisladores.HeaderRow.TableSection = TableRowSection.TableHeader;
                        break;

                    case "gv_Comisiones":
                        fCodigo = tipoObjeto[1] as string;
                        foreach (GridViewRow row in gv_Legisladores.Rows)
                        {
                            if (fCodigo == gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCodigo"].ToString())
                            {
                                legisladorValue = String.Concat
                                (
                                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegNombres"].ToString()," ",
                                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegApellido1"].ToString(), " ",
                                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegApellido2"].ToString()
                                ).TrimStart().TrimEnd();
                                break;
                            }
                        }
                        mtBindGridView(gv_Comisiones, objLegislaturaRegistro.dComMbrGetDatos(legisladorValue)); // ParamValue: Legislador/Proponente seleccionado
                        gv_Comisiones.UseAccessibleHeader = true;
                        gv_Comisiones.HeaderRow.TableSection = TableRowSection.TableHeader;
                        break;

                    case "gv_Excusas":
                        fCodigo = tipoObjeto[1] as string;
                        foreach (GridViewRow row in gv_Legisladores.Rows)
                        {
                            if (fCodigo == gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCodigo"].ToString())
                            {
                                legisladorValue =gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCedula"].ToString().TrimStart().TrimEnd();
                                break;
                            }
                        }
                        mtBindGridView(gv_Excusas, objLegislaturaRegistro.dComAleGetDatos(legisladorValue)); // ParamValue: Legislador/Proponente seleccionado [Cédula]
                        gv_Excusas.UseAccessibleHeader = true;
                        gv_Excusas.HeaderRow.TableSection = TableRowSection.TableHeader;
                        break;

                    case "gv_Iniciativas":
                        fCodigo = tipoObjeto[1] as string;
                        foreach (GridViewRow row in gv_Legisladores.Rows)
                        {
                            if (fCodigo == gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCodigo"].ToString())
                            {
                                legisladorValue = String.Concat
                                (
                                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegNombres"].ToString(), " ",
                                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegApellido1"].ToString(), " ",
                                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegApellido2"].ToString()
                                ).TrimStart().TrimEnd();
                                break;
                            }
                        }
                        mtBindGridView(gv_Iniciativas, objLegislaturaRegistro.mIniIniGetDatos(Convert.ToDateTime("01/01/1800"), DateTime.Now, legisladorValue)); // ParamValue: Legislador/Proponente seleccionado
                        gv_Iniciativas.UseAccessibleHeader = true;
                        gv_Iniciativas.HeaderRow.TableSection = TableRowSection.TableHeader;
                        break;

                    case "TipoId": mtBindDropDownList(DropDownAdmLegTipoId, objLegislaturaRegistro.mAdmParGetDatos(clsLegislaturaRegistro.TipoDatos.TipoId), "AdmParString", "AdmParNumerico", "Seleccione identificación");
                        break;

                    case "Sexo": mtBindDropDownList(DropDownAdmLegSexo, objLegislaturaRegistro.mAdmParGetDatos(clsLegislaturaRegistro.TipoDatos.Sexo), "AdmParString", "AdmParNumerico", "Seleccione sexo");
                        break;

                    case "Posiciones": mtBindDropDownList(DropDownAdmFunCodigo, objLegislaturaRegistro.mAdmFunGetDatos(9), "AdmFunDescripcion", "AdmFunCodigo", "Seleccione posición");
                        break;

                    case "Partido": mtBindDropDownList(DropDownAdmPdoCodigo, objLegislaturaRegistro.mAdmPodGetDatos(-1), "AdmPdoDescripcion", "AdmPdoCodigo", "Seleccione partido");
                        break;

                    case "Periodos": mtBindDropDownList(DropDownAdmLegPeriodoElecto, objLegislaturaRegistro.mAdmPcoGetDatos(), "AdmpcoDescripcion", "AdmpcoCodigo", "Seleccione período electo");
                        break;

                    case "Estado": mtBindDropDownList(DropDownAdmEstCodigo, objLegislaturaRegistro.dAdmEstGetDatos(), "AdmEstDescripcion", "AdmEstCodigo", "Seleccionar estado");
                        break;

                    case "Sector": mtBindDropDownList(DropDownAdmSecCodigo, objLegislaturaRegistro.mAdmSecGetDatos(), "AdmSecDescripcion", "AdmSecCodigo", "Seleccione sector");
                        break;

                    case "Provincia": mtBindDropDownList(DropDownListAdmPrvCodigo, objLegislaturaRegistro.mAdmPrvGetDatos(), "AdmPrvDescripcion", "AdmPrvCodigo", "Seleccione providencia");
                        break;

                    case "Municipio": mtBindDropDownList(DropDownAdmMunCodigo, objLegislaturaRegistro.mAdmMunGetDatos(), "AdmMunDescripcion", "AdmMunCodigo", "Seleccione municipio");
                        break;
                }
            }

            objLegislaturaRegistro.mAdmParDispose();
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
                DropDownAdmEstCodigo.SelectedValue = "13"; // Estado: ACTIVO
                FileUploadASP.Enabled = (isReadOnly) ? false : true;

                if(hfId.Value.Equals("0"))
                {
                    tabComisiones.Visible = false;
                    tabExcusas.Visible = false;
                    tabIniciativas.Visible = false;
                }
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;
                FileUploadASP.Enabled = (isReadOnly) ? false : true;
                tabComisiones.Visible = true;
                tabExcusas.Visible = true;
                tabIniciativas.Visible = true;
                LimpiarControles();
            }

            if (controlsRequired != null)
            {
                foreach (Control control in controlsRequired)
                {
                    switch(control.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.DropDownList": 
                            (control as System.Web.UI.WebControls.DropDownList).Enabled = (isReadOnly) ? false : true;
                            (control as System.Web.UI.WebControls.DropDownList).CssClass = "select-240";
                            break;
                        case "System.Web.UI.WebControls.TextBox": (control as System.Web.UI.WebControls.TextBox).ReadOnly = (isReadOnly) ? true : false;
                            break;
                        case "System.Web.UI.WebControls.FileUpload": (control as System.Web.UI.WebControls.FileUpload).Enabled = (isReadOnly) ? false : true;
                            break;
                    }
                }
            } 
        }

        private void EditarRegistro(string argument)
        {
            try
            {
                string fCodigo = argument;
                hfId.Value = fCodigo;
                //hfId.Value = fId.ToString();

                foreach (GridViewRow row in gv_Legisladores.Rows)
                {
                    if (fCodigo == gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCodigo"].ToString())
                    {
                        var imgUrl = String.Concat(@System.Configuration.ConfigurationManager.AppSettings["PicsFolderDefault"], gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegFoto"].ToString()).Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
                        DropDownAdmLegTipoId.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegTipoId"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegTipoId"].ToString();
                        txtAdmLegCedula.Text =                      gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCedula"].ToString();
                        txtAdmlegNombres.Text =                     gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegNombres"].ToString();
                        txtAdmlegApellido1.Text =                   gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegApellido1"].ToString();
                        txtAdmlegApellido2.Text =                   gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegApellido2"].ToString();
                        DropDownAdmLegSexo.SelectedValue =          String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegSexo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmlegSexo"].ToString();
                        txtAdmlegFechaNac.Text =                    Convert.ToDateTime(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegFechaNac"]).ToShortDateString();
                        DropDownAdmPdoCodigo.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmPdoCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmPdoCodigo"].ToString();
                        DropDownAdmFunCodigo.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString();
                        DropDownAdmLegPeriodoElecto.SelectedValue = String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmPcoCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmPcoCodigo"].ToString();
                        DropDownAdmPdoCodigo.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmPdoCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmPdoCodigo"].ToString();
                        DropDownAdmEstCodigo.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmEstCodigo"].ToString();
                        txtAdmLegDireccion.Text =                   gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegDireccion"].ToString();
                        DropDownListAdmPrvCodigo.SelectedValue =    String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmProProvincia"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmProProvincia"].ToString();
                        ImagenPerfil.ImageUrl =                     String.IsNullOrEmpty(Path.GetExtension(imgUrl)) ? imagePerfilDefault : imgUrl;
                        DropDownAdmMunCodigo.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmMunCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmMunCodigo"].ToString();
                        DropDownAdmSecCodigo.SelectedValue =        String.IsNullOrEmpty(gv_Legisladores.DataKeys[row.RowIndex].Values["AdmSecCodigo"].ToString()) ? "0" : gv_Legisladores.DataKeys[row.RowIndex].Values["AdmSecCodigo"].ToString();
                        txtAdmLegCelular.Text =                     gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCelular"].ToString();
                        txtAdmLegTelefonoSenado.Text =              gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegTelefonoSenado"].ToString();
                        txtAdmLegFax.Text =                         gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegFax"].ToString();
                        txtAdmLegCorreo.Text =                      gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegCorreo"].ToString();
                        txtAdmLegTelefonoProvincial.Text =          gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegTelefonoProvincial"].ToString();
                        txtAdmLegApartadoPostal.Text =              gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegApartadoPostal"].ToString();
                        txtAdmLegTwitter.Text =                     gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegTwitter"].ToString();
                        txtAdmLegSitioWeb.Text =                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegSitioWeb"].ToString();
                        txtAdmLegLinkedin.Text =                    gv_Legisladores.DataKeys[row.RowIndex].Values["AdmLegLinkedlin"].ToString();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void LimpiarControles()
        {
            DropDownAdmLegTipoId.ClearSelection();    
            txtAdmLegCedula.Text = String.Empty;                  
            txtAdmlegNombres.Text = String.Empty;                   
            txtAdmlegApellido1.Text = String.Empty;                  
            txtAdmlegApellido2.Text = String.Empty;                
            DropDownAdmLegSexo.ClearSelection();         
            txtAdmlegFechaNac.Text = String.Empty;                  
            DropDownAdmPdoCodigo.ClearSelection();      
            DropDownAdmFunCodigo.ClearSelection();      
            DropDownAdmLegPeriodoElecto.ClearSelection(); 
            DropDownAdmPdoCodigo.ClearSelection();       
            DropDownAdmEstCodigo.ClearSelection();       
            txtAdmLegDireccion.Text = String.Empty;                 
            DropDownListAdmPrvCodigo.ClearSelection(); 
            DropDownAdmMunCodigo.ClearSelection(); 
            DropDownAdmSecCodigo.ClearSelection(); 
            txtAdmLegCelular.Text = String.Empty;                   
            txtAdmLegTelefonoSenado.Text = String.Empty;            
            txtAdmLegFax.Text = String.Empty;                       
            txtAdmLegCorreo.Text = String.Empty;                    
            txtAdmLegTelefonoProvincial.Text = String.Empty;       
            txtAdmLegApartadoPostal.Text = String.Empty;            
            txtAdmLegTwitter.Text = String.Empty;                   
            txtAdmLegSitioWeb.Text = String.Empty;
            txtAdmLegLinkedin.Text = String.Empty;
            FileNamePerfil = String.Empty;
            isValidImage = false;
            ImagenPerfil.ImageUrl = imagePerfilDefault;
            hfId.Value = "0";   
        }

        private Boolean mtValidar()
        {
            string fMensaje = default(string);

            if (DropDownAdmLegTipoId.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el tipo de identificación.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmLegTipoId.Focus();
            }
            else if (String.IsNullOrEmpty(txtAdmLegCedula.Text))
            {
                fMensaje = "El campo: Número de identificación, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtAdmLegCedula.Focus();
            }
            else if (String.IsNullOrEmpty(txtAdmlegNombres.Text))
            {
                fMensaje = "El campo: Nombres, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtAdmlegNombres.Focus();
            }
            else if (String.IsNullOrEmpty(txtAdmlegApellido1.Text))
            {
                fMensaje = "El campo: Primer apellido, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtAdmlegApellido1.Focus();
            }
            else if (String.IsNullOrEmpty(txtAdmlegApellido2.Text))
            {
                fMensaje = "El campo: Segundo apellido, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtAdmlegApellido2.Focus();
            }
            else if (DropDownAdmLegSexo.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el tipo de sexo.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmLegSexo.Focus();
            }
            else if (String.IsNullOrEmpty(Request.Form[txtAdmlegFechaNac.UniqueID]))
            {
                fMensaje = "El campo: Fecha de nacimiento, no debe ir en blanco.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmLegSexo.Focus();
            }
            else if (DropDownAdmPdoCodigo.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el partido político.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmPdoCodigo.Focus();
            }
            else if (DropDownAdmFunCodigo.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar la posición o bloque partidario.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmFunCodigo.Focus();
            }
            else if (DropDownAdmPdoCodigo.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar el período.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmPdoCodigo.Focus();
            }
            else if (DropDownAdmEstCodigo.SelectedValue.Equals("0"))
            {
                fMensaje = "Debe seleccionar un estado.";
                mtvAddMessage(fMensaje, MessageType.warning);
                DropDownAdmEstCodigo.Focus();
            }
            else if(String.Compare(ImagenPerfil.ImageUrl,imagePerfilDefault) == 0 && !FileUploadASP.HasFile)
            {
                fMensaje = "Debe subir una foto de perfil.";
                mtvAddMessage(fMensaje, MessageType.warning);
            }

            #region Datos - Contactos [No requeridos]
            //else if (String.IsNullOrEmpty(txtAdmLegDireccion.Text))
            //{
            //    fMensaje = "El campo: Dirección, no debe ir en blanco.";
            //    mtvAddMessage(fMensaje, MessageType.warning);
            //    txtAdmLegDireccion.Focus();
            //}
            //else if (DropDownListAdmPrvCodigo.SelectedValue.Equals("0"))
            //{
            //    fMensaje = "Debe seleccionar la provincia.";
            //    mtvAddMessage(fMensaje, MessageType.warning);
            //    DropDownListAdmPrvCodigo.Focus();
            //}
            //else if (DropDownAdmMunCodigo.SelectedValue.Equals("0"))
            //{
            //    fMensaje = "Debe seleccionar el municipio.";
            //    mtvAddMessage(fMensaje, MessageType.warning);
            //    DropDownAdmMunCodigo.Focus();
            //}
            //else if (DropDownAdmSecCodigo.SelectedValue.Equals("0"))
            //{
            //    fMensaje = "Debe seleccionar un sector.";
            //    mtvAddMessage(fMensaje, MessageType.warning);
            //    DropDownAdmSecCodigo.Focus();
            //}
            #endregion

            SubirImagen(true);

            return isValidImage ? String.IsNullOrEmpty(fMensaje) : false;
        }

        private void SubirImagen(bool onlyValidate = false)
        {
            if(onlyValidate)
            { 
                if (String.Compare(ImagenPerfil.ImageUrl, imagePerfilDefault) == -1 && !FileUploadASP.HasFile)
                {
                    isValidImage = true;
                    return;
                }
            }

            HttpPostedFile mifichero = FileUploadASP.PostedFile;
            var fileNameUpload = FileUploadASP.FileName;
            string nombreFile = String.IsNullOrEmpty(FileUploadASP.FileName) ? ImagenPerfil.ImageUrl : FileUploadASP.FileName;
            string nombreFile2 = !String.IsNullOrEmpty(fileNameUpload) ?
                Path.GetFileNameWithoutExtension(fileNameUpload) + "_" + DateTime.Now.ToString("ddMMyyyymmss") + Path.GetExtension(fileNameUpload) :
                Path.GetFileNameWithoutExtension(ImagenPerfil.ImageUrl) + Path.GetExtension(ImagenPerfil.ImageUrl);
            //string nombreFile2 = Path.GetFileNameWithoutExtension(fileNameUpload) + "_" + DateTime.Now.ToString("ddMMyyyymmss") + Path.GetExtension(String.IsNullOrEmpty(fileNameUpload) ? ImagenPerfil.ImageUrl : FileUploadASP.FileName);
            string extension = Path.GetExtension(nombreFile);
            string nomb = Path.GetFileNameWithoutExtension(String.IsNullOrEmpty(FileUploadASP.FileName) ? ImagenPerfil.ImageUrl : FileUploadASP.FileName);

            if(onlyValidate)
            {
                double Kb = mifichero.ContentLength / 1024;
                if (Kb > 2048)
                {
                    mtvAddMessage("No se puede subir el documento, excede el tamaño permitido de 4 mb.", MessageType.warning);
                    return;
                }

                if (!extension.ToUpper().Equals(".JPG") && !extension.ToUpper().Equals(".PNG"))
                {
                    mtvAddMessage("La imagen de perfil tiene un formato incorrecto. Favor, volver intentar.", MessageType.error);
                    return;
                }
            }
            else
            {
                try
                {
                    // Condiciona ruta no mayor a 200 -tamaño de campo en DB-
                    var fullPath = String.Concat(@System.Configuration.ConfigurationManager.AppSettings["PicsFolderDefault"], nombreFile2);
                    var fixedPath = (fullPath.Length > 200) ? fullPath.Substring(0, 195) + extension : fullPath.Substring(0, fullPath.Length);
                    FileNamePerfil = Path.GetFileName(fixedPath);

                    // Verifica que exista para eliminarlo antes
                    if (!File.Exists(fixedPath))
                        FileUploadASP.SaveAs(@fixedPath);
                        //File.Delete(fixedPath);

                    // Guarda el archivo subido en la carpeta del AppSettings
                    
                }
                catch
                {
                    throw;
                }
            }
            isValidImage = true;
        }

        #endregion
    }
}