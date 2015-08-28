using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MaxBll;

namespace MaxApp
{
    public partial class frm_manini : PageBase
    {
        clsIniciativas objIniciativa;
        clsTipoIniciativa tipoIniciativa;
        clsSubTipoIniciativa subTipoIniciativa;
        clsLegislaturaInicio legislaturaInicio;
        clsMateria materia;
        clsPeriodoConstitucional periodoConstitucional;
        clsComision comision;
        clsrFunUser iniUsers;
        clsIniciativaPriorizada iniciativaPriorizada;
        clsAdmPar objclsPar;
        clsCondicionActual objCondicionActual;
        clsIniHis objIniHis;
        clsDoc objDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();

            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_manini.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        panelLista.Visible = true;

                        DateTime fFechaDesde = DateTime.Now.AddDays(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["LimitDays"].ToString()));
                        DateTime fFechaHasta = DateTime.Now;

                        mtLoadCombos();
                        mtLoadData(fFechaDesde, fFechaHasta);

                        mtDesabilitarCampos();

                    }

                }
                catch (Exception ex)
                {

                    mtvAddMessage(ex.Message, MessageType.error);
                }

            }
        }

        private void mtLoadCombos()
        {
            tipoIniciativa = new clsTipoIniciativa();
            subTipoIniciativa = new clsSubTipoIniciativa();
            legislaturaInicio = new clsLegislaturaInicio();
            materia = new clsMateria();
            periodoConstitucional = new clsPeriodoConstitucional();
            comision = new clsComision();
            iniUsers = new clsrFunUser();
            iniciativaPriorizada = new clsIniciativaPriorizada();
            objclsPar = new clsAdmPar();
            objCondicionActual = new clsCondicionActual();

            String opcion = "[Seleccione una Opción]";

            mtBindDropDownList(ddlLegislaturaInicio, legislaturaInicio.mtGetclsLegislaturaInicio(), "AdmLetDescripcion", "AdmLetCodigo", opcion);
            mtBindDropDownList(ddlPeriodoConstitucional, periodoConstitucional.mtGetPeriodoConstitucional(), "AdmpcoDescripcion", "AdmpcoCodigo", opcion);
            mtBindDropDownList(ddlTipoIniciativa, tipoIniciativa.mtGetTiposIniciativas(), "IniTipDescripcion", "IniTipCodigo", opcion);
            mtBindDropDownList(ddlSubTipoIniciativa, subTipoIniciativa.mtGetSubTiposIniciativas(), "IniStpDescripcion", "IniStpCodigo", opcion);
            mtBindDropDownList(ddlMateria, materia.mtGetMaterias(), "ComComNombre", "ComCfmId", opcion);
            listComision.DataTextField = "ComComNombre";
            listComision.DataValueField = "ComCfmId";
            listComision.DataSource = comision.mtGetComision();
            listComision.DataBind();
            mtBindDropDownList(ddlPoderDeOrigen, objclsPar.mtGetAdmPar("Poderes"), "AdmParString", "AdmParNumerico", opcion);
            mtBindDropDownList(ddlInformeElaboradoPor, iniUsers.mtGetFunUser(1, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(3, 0)
            mtBindDropDownList(ddlCorreccionEstilo, iniUsers.mtGetFunUser(2, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(3, 0)
            mtBindDropDownList(ddlDigitadoPor, iniUsers.mtGetFunUser(3, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(3, 1)
            mtBindDropDownList(ddlEnviadoComisionPor, iniUsers.mtGetFunUser(4, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(4, 0)
            mtBindDropDownList(ddlRevisadoPor, iniUsers.mtGetFunUser(5, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(4, 0)
            mtBindDropDownList(ddlOficioEnviadoComisionPor, iniUsers.mtGetFunUser(6, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(4, 0)
            mtBindDropDownList(ddlRecibidoTranscripcionPor, iniUsers.mtGetFunUser(7, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(7, 0)
            mtBindDropDownList(ddlDespachadoPor, iniUsers.mtGetFunUser(13, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(13, 0)
            mtBindDropDownList(ddlDespachadaHacia, iniUsers.mtGetFunUser(28, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(24, 0)
            mtBindDropDownList(ddlCondicionActual, objCondicionActual.mtGetCondicionActual(), "IniCacDescripcion", "IniCacCodigo", opcion);
            mtBindDropDownList(ddlCorregidoTranscripcionPor, iniUsers.mtGetFunUser(19, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(14, 0)
            mtBindDropDownList(ddlRevisadoTranscripcionPor, iniUsers.mtGetFunUser(15, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(15, 0)
            mtBindDropDownList(ddlDespachadoTranscripcionPor, iniUsers.mtGetFunUser(16, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(16, 0)
            mtBindDropDownList(ddlCreadoPor, iniUsers.mtGetFunUser(17, 1), "AdmUsrNombre", "FunUsrCodigo", opcion); //(3, 1)
            mtBindDropDownList(ddlCorrecionTecnica, iniUsers.mtGetFunUser(18, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(18, 0)
            mtBindDropDownList(ddlAnalisisRealizadoPor, iniUsers.mtGetFunUser(23, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);//(23, 0)
            mtBindDropDownList(ddlCamaraInicial, objclsPar.mtGetAdmPar("Camaras"), "AdmParString", "AdmParNumerico", opcion);

            tipoIniciativa.mtDispose();
            subTipoIniciativa.mtDispose();
            legislaturaInicio.mtDispose();
            materia.mtDispose();
            periodoConstitucional.mtDispose();
            comision.mtDispose();
            iniUsers.mtDispose();
            iniciativaPriorizada.mtDispose();
            objclsPar.mtDispose();
            objCondicionActual.mtDispose();

        }

        private void mtLoadData(DateTime fFechaDesde, DateTime fFechaHasta)
        {
            mtHabilitar(false);

            objIniciativa = new clsIniciativas();

            mtBindGridView(GridViewIniciativas, objIniciativa.mtGetIniciativas(fFechaDesde, fFechaHasta));
            objIniciativa.mtDispose();

            mtDesabilitarCampos();
        }

        private Boolean mtValidar()
        {
            String fMensaje = String.Empty;


            if (ddlLegislaturaInicio.SelectedValue == "0")
            {
                fMensaje = "Seleccione el campo Legislatura de Inicio";
                mtvAddMessage(fMensaje, MessageType.warning);
                ddlLegislaturaInicio.Focus();
            }
            else if (String.IsNullOrEmpty(txtINiIniFecha.Text))
            {
                fMensaje = "Inserte el Campo Fecha Recibido por el Senado";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtINiIniFecha.Focus();
            }
            else if (String.IsNullOrEmpty(txtDescripcionIniciativa.Value))
            {
                fMensaje = "Inserte el Campo Descripción del Proyecto";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtDescripcionIniciativa.Focus();
            }
            else if (ddlCamaraInicial.SelectedValue == "1" && ddlConteoLegislaturaInicio.SelectedValue == "0")
            {
                fMensaje = "El Conteo de Legislatura Iniciado es obligatorio cuando la Cámara Inicial es la de Diputados";
                mtvAddMessage(fMensaje, MessageType.warning);
                ddlConteoLegislaturaInicio.Focus();
            }
            else if (ddlIniciativaPriorizada.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaIniciativaPriorizada.Text))
            {
                fMensaje = "Tiene que especificar la fecha de la Iniciativa Priorizada";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtFechaIniciativaPriorizada.Focus();
            }
            else if (ddlConteoLegislaturaInicio.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaConteoLegInicio.Text))
            {
                fMensaje = "Tiene que especificar la fecha del Conteo legislaturas Inicio";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtFechaConteoLegInicio.Focus();
            }
            else if (ddlPerimida.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaPerimida.Text))
            {
                fMensaje = "Tiene que especificar la fecha si es Perimida";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtFechaPerimida.Focus();
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
                btnCancelar.Visible = true;
                txtDescripcionIniciativa.Focus();
            }
            else
            {
                PanelMantenimientos.Visible = false;
                panelLista.Visible = true;
                btnNuevo.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;

                hfId.Value = "0";

                txtNoIniciativa.Text = String.Empty;
                ddlMateria.SelectedValue = "0";
                txtDescripcionIniciativa.Value = String.Empty;
                txtProponentes.Value = String.Empty;
                txtObservaciones.Value = String.Empty;
                ddlIniciativaPriorizada.SelectedValue = "0";
                ddlPeriodoConstitucional.SelectedValue = "0";
                ddlTipoIniciativa.SelectedValue = "0";
                ddlLegislaturaInicio.SelectedValue = "0";
                ddlSubTipoIniciativa.SelectedValue = "0";
                txtINiIniFecha.Text = String.Empty;

                foreach (ListItem items in listComision.Items)
                {
                    items.Selected = false;
                }

                txtAnioLegislativo.Text = String.Empty;
                txtMiembroComisionEspecial.Value = String.Empty;
                ddlCamaraInicial.SelectedValue = "0";
                txtIniIniVecesDev.Text = String.Empty;
                ddlConteoLegislaturaInicio.SelectedValue = "0";
                ddlPoderDeOrigen.SelectedValue = "0";
                ddlCreadoPor.SelectedValue = "0";
                txtIniIniNumOficioOrig.Text = String.Empty;
                ddlDigitadoPor.SelectedValue = "0";

                ddlAnalisisLegislativo.SelectedValue = "0";
                ddlAnalisisRealizadoPor.SelectedValue = "0";
                txtAprobacionPresidida.Text = String.Empty;
                ddlPromulgada.SelectedValue = "0";
                txtSecretariosAprobacion.Value = String.Empty;
                txtNroExpCamaraDiputados.Text = String.Empty;
                ddlInformeComisiones.SelectedValue = "0";
                txtArchivadoConNro.Text = String.Empty;
                txtNroPromulgacion.Text = String.Empty;
                ddlCorreccionEstilo.SelectedValue = "0";
                ddlInformeElaboradoPor.SelectedValue = "0";
                ddlCorrecionTecnica.SelectedValue = "0";
                ddlCorregidoTranscripcionPor.SelectedValue = "0";
                ddlInformeAsesores.SelectedValue = "0";
                ddlInformeOPA.SelectedValue = "0";
                ddlInformeDTRL.SelectedValue = "0";
                ddlOtrosInformes.SelectedValue = "0";

                ddlEnviadoComisionPor.SelectedValue = "0";
                ddlDespachada.SelectedValue = "0";
                ddlDespachadoPor.SelectedValue = "0";
                ddlRevisadoPor.SelectedValue = "0";
                ddlDespachadaHacia.SelectedValue = "0";
                ddlRevisadoTranscripcionPor.SelectedValue = "0";
                ddlOficioEnviadoComisionPor.SelectedValue = "0";
                txtNroOficioDespacho.Text = String.Empty;
                ddlDespachadoTranscripcionPor.SelectedValue = "0";
                ddlRecibidoTranscripcionPor.SelectedValue = "0";
                txtNroLegislaturaVigente.Text = String.Empty;
                txtNotaDespacho.Value = "";

                ddlCondicionActual.SelectedValue = "0";
                mtClearGridView(gv_Estados);
                mtClearGridView(gv_Documentos);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {



                if (!mtValidar())
                    return;

                String fComisiones = String.Empty;
                String fNombreUsuario = String.Empty;

                Boolean fConteoLegislaturaIniciado = false;
                Boolean fIniciativaPriorizada = false;
                Boolean fvIniIniPerimida = false;

                DateTime fIniIniFechaPriorizada = DateTime.Now;
                DateTime fIniIniFechaConteoLeg = DateTime.Now;
                DateTime fIniIniFechaPerimida = DateTime.Now;

                Int32 fAdmAnoCodigo = 0;
                Int32 fvIniIniVecesDev = 0;

                if (ddlConteoLegislaturaInicio.SelectedValue == "1")
                {
                    fConteoLegislaturaIniciado = true;
                    fIniIniFechaConteoLeg = DateTime.Parse(txtFechaConteoLegInicio.Text);
                }

                if (ddlIniciativaPriorizada.SelectedValue == "1")
                {
                    fIniciativaPriorizada = true;
                    fIniIniFechaPriorizada = DateTime.Parse(txtFechaIniciativaPriorizada.Text);
                }

                if (ddlPerimida.SelectedValue == "1")
                {
                    fvIniIniPerimida = true;
                    fIniIniFechaPerimida = DateTime.Parse(txtFechaPerimida.Text);
                }


                if (!String.IsNullOrEmpty(txtAnioLegislativo.Text))
                    fAdmAnoCodigo = Int32.Parse(txtAnioLegislativo.Text);
                if (!String.IsNullOrEmpty(txtIniIniVecesDev.Text))
                    fvIniIniVecesDev = Int32.Parse(txtIniIniVecesDev.Text);

                fNombreUsuario = Session["AdmUsrNombre"].ToString();

                foreach (ListItem items in listComision.Items)
                {
                    if (items.Selected)
                        fComisiones += items.Value + "\t";
                }

                fComisiones = fComisiones.Trim();
                fComisiones = fComisiones.Replace('\t', ';');


                objIniciativa = new clsIniciativas();

                objIniciativa.mtPutIniciativa
                    (
                     Int32.Parse(hfId.Value) //Id del Registro
                     , ddlLegislaturaInicio.SelectedItem.Text //Legislatura Inicio
                     , Int32.Parse(ddlMateria.SelectedValue)//Materia
                     , Int32.Parse(ddlPeriodoConstitucional.SelectedValue)//Período Constitucional
                     , fIniciativaPriorizada //Iniciativa Priorizada
                     , fIniIniFechaPriorizada //Fecha Iniciativa Priorizada
                     , Int32.Parse(ddlTipoIniciativa.SelectedValue)//Tipo de Iniciativa
                     , Int32.Parse(ddlSubTipoIniciativa.SelectedValue)//Sub-Tipo Iniciativa
                     , DateTime.Parse(txtINiIniFecha.Text)//Fecha Recibido por el Senado
                     , txtDescripcionIniciativa.Value //Descripcion del Proyecto
                     , txtProponentes.Value // Proponentes
                     , txtObservaciones.Value //Anotaciones especiales
                     , fComisiones //Comisiones
                     , fAdmAnoCodigo//Año Legislativo 
                     , fvIniIniVecesDev//Veces Devuelto De la Cámara Diputados
                     , Int32.Parse(ddlPoderDeOrigen.SelectedValue)//Poder de Origen
                     , fConteoLegislaturaIniciado //Conteo Legislatura Inicio
                     , fIniIniFechaConteoLeg
                     , fvIniIniPerimida //Es Perimida
                     , fIniIniFechaPerimida
                     , Int32.Parse(ddlDigitadoPor.SelectedValue)//Digitado Por
                     , Int32.Parse(ddlCamaraInicial.SelectedValue) //Cámara inicial
                     , Int32.Parse(ddlCreadoPor.SelectedValue)//Creado Por
                     , txtIniIniNumOficioOrig.Text //Número Oficio Original
                     , fNombreUsuario
                    );

                #region "Borrar"
                /*

                    , Int16.Parse(ddlLegislaturaInicio.SelectedValue) //Legislatura de Inicio *
                    , *
                    , txtDescripcionIniciativa.Value//Descripcion del Proyecto *
                    ,  *
                    ,  *
                    ,  *
                    , Int32.Parse(ddlCamaraInicial.SelectedValue) //Cámara inicial *
                    , Int32.Parse(ddlPoderDeOrigen.SelectedValue)//Poder de Origen *
                    , txtIniIniNumOficioOrig.Text //Número Oficio Original *
                    , txtIniIniVecesDev.Text//Veces Devuelto De la Cámara Diputados *
                    , Int32.Parse(ddlCreadoPor.SelectedValue)//Creado Por *
                    , Int32.Parse(ddlDigitadoPor.SelectedValue)//Digitado Por *
                    , txtAnioLegislativo.Text //Año Legislativo *
                    , fConteoLegislaturaIniciado //Conteo legislaturas iniciado *
                    , txtMiembroComisionEspecial.Value //Miembros comisión especial *
                    , fNombreUsuario // *
                    , false//fAnalisisLegislativo // Análisis Legislativo
                    , 0//Int32.Parse(ddlCorreccionEstilo.SelectedValue)//Corrección de Estilo
                    , 0//Int32.Parse(ddlAnalisisRealizadoPor.SelectedValue) //Análisis realizado por 
                    , 0//Int32.Parse(ddlCorrecionTecnica.SelectedValue)//Corrección Técnica
                    , Int32.Parse(ddlInformeElaboradoPor.SelectedValue)//Informe Elaborado Por 
                    , 0//0//vIniIniCorregidoTrans
                    , false//fInformeDTRL
                    , false//fInformeOPA
                    , false//fInformeComisiones//Informe comisiones
                    , false//fInformeOtros
                    , false//fInformeAsesores//Informes Asesores 
                    , String.Empty//txtNroExpCamaraDiputados.Text//# Exp. Cámara de Diputados
                    , String.Empty//txtAprobacionPresidida.Text//Aprobación Presidida por
                    , String.Empty//txtSecretariosAprobacion.Value
                    , String.Empty//txtNroPromulgacion.Text //Numero Promulgacion
                    , false //fPromulgada
                    , String.Empty// txtArchivadoConNro.Text//Archivado con el Num
                    , 0//Int32.Parse(ddlEnviadoComisionPor.SelectedValue)//Enviado a Comisión por
                    , false//fDespachada//Despachada
                    , 0//Int32.Parse(ddlDespachadoPor.SelectedValue)//Despachado Por
                    , 0//Int32.Parse(ddlDespachadaHacia.SelectedValue)//Despachada hacia
                    , 0//Int32.Parse(ddlRecibidoTranscripcionPor.SelectedValue)//Recibido en Transcripción Por
                    , 0//Int32.Parse(ddlRevisadoTranscripcionPor.SelectedValue)//Revisado en Transcripción Por
                    , 0//Int32.Parse(ddlCorregidoTranscripcionPor.SelectedValue) //Corregido transcripción por
                    , 0//Int32.Parse(ddlDespachadoTranscripcionPor.SelectedValue)//Despachado a Transcripción Por 
                    , 0//Int32.Parse(ddlOficioEnviadoComisionPor.SelectedValue)//Oficio Enviado a Comisión Por
                    , String.Empty//txtNroOficioDespacho.Text//No. Oficio Despacho
                    , String.Empty //txtNroLegislaturaVigente.Text//Numero de Legislatura Vigente
                    , String.Empty//txtNotaDespacho.Value//Nota de Despacho 
                    , 0//vIniCacCodigo
                    );
                     */

                #endregion

                objIniciativa.mtDispose();

                DateTime fFechaDesde = fFechaDesde = DateTime.Now.AddDays(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["LimitDays"].ToString()));
                DateTime fFechaHasta = DateTime.Now;


                if (!String.IsNullOrEmpty(txtFechaDesde.Text) && !String.IsNullOrEmpty(txtFechaHasta.Text))
                {
                    fFechaDesde = DateTime.Parse(txtFechaDesde.Text);
                    fFechaHasta = DateTime.Parse(txtFechaHasta.Text);
                }

                mtLoadData(fFechaDesde, fFechaHasta);

                mtvAddMessage("Registro Guardado satisfactoriamente", MessageType.success);

            }
            catch (Exception ex)
            {

                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        protected void EditarRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;



                foreach (GridViewRow row in GridViewIniciativas.Rows)
                {
                    if (fId == GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCodigoSis"].ToString())
                    {
                        //Quinto Tab Estado
                        ddlCondicionActual.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniCacCodigo"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniCacCodigo"].ToString();
                        objIniHis = new clsIniHis();
                        mtBindGridView(gv_Estados, objIniHis.mtGetIniHis(Int32.Parse(hfId.Value)));

                        //Sexto Tab Documentos Anexos
                        objDoc = new clsDoc();
                        mtBindGridView(gv_Documentos, objDoc.mtGetDoc(Int32.Parse(hfId.Value)));

                        //Primer Tab De la Iniciativa
                        txtNoIniciativa.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumero"].ToString();
                        ddlLegislaturaInicio.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmLetCodigoInicio"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmLetCodigoInicio"].ToString();
                        ddlMateria.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniMateria"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniMateria"].ToString();
                        ddlPeriodoConstitucional.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmpcoCodigo"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmpcoCodigo"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPriorizada"].ToString()))
                        {
                            ddlIniciativaPriorizada.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPriorizada"].ToString()) ? "1" : "0";
                            if (ddlIniciativaPriorizada.SelectedValue == "1")
                                txtFechaIniciativaPriorizada.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPriorizada"].ToString();
                        }
                        ddlTipoIniciativa.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniTipCodigo"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniTipCodigo"].ToString();
                        ddlSubTipoIniciativa.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniStpCodigo"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniStpCodigo"].ToString();
                        txtINiIniFecha.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFecha"].ToString();
                        txtDescripcionIniciativa.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDescripcion"].ToString();
                        txtProponentes.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniProponentes"].ToString();
                        txtObservaciones.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniObservaciones"].ToString();


                        //Segundo Tab Datos de Control
                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["ComCfmId"].ToString()))
                        {
                            // listComision
                            String[] fComisiones = GridViewIniciativas.DataKeys[row.RowIndex].Values["ComCfmId"].ToString().Split(';');

                            foreach (ListItem items in listComision.Items)
                            {
                                if (fComisiones.Contains(items.Value))
                                    items.Selected = true;
                            }
                        }

                        txtAnioLegislativo.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmAnoCodigo"].ToString();
                        txtIniIniVecesDev.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniVecesDev"].ToString();
                        ddlPoderDeOrigen.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPoderOrigen"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPoderOrigen"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniConteoLeg"].ToString()))
                        {
                            ddlConteoLegislaturaInicio.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniConteoLeg"].ToString()) ? "1" : "0";
                            if (ddlConteoLegislaturaInicio.SelectedValue == "1")
                                txtFechaConteoLegInicio.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaConteoLeg"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPerimida"].ToString()))
                        {
                            ddlPerimida.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPerimida"].ToString()) ? "1" : "0";
                            if (ddlPerimida.SelectedValue == "1")
                                txtFechaPerimida.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPerimida"].ToString();
                        }

                        ddlDigitadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDigitadoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDigitadoPor"].ToString();
                        ddlCamaraInicial.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmCamCodigo"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmCamCodigo"].ToString();
                        ddlCreadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCreadoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCreadoPor"].ToString();
                        txtIniIniNumOficioOrig.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumOficioOrig"].ToString();
                        txtMiembroComisionEspecial.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniMbrComisionesEsp"].ToString();


                        //TercerTab Control, Revision y Auditoria
                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalisisLeg"].ToString()))
                        {
                            ddlAnalisisLegislativo.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalisisLeg"].ToString()) ? "1" : "0";
                            if (ddlAnalisisLegislativo.SelectedValue == "1")
                                txtFechaAnalisisLeg.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechAnalisisLeg"].ToString();
                        }

                        txtNroPromulgacion.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumProm"].ToString();
                        ddlCorregidoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorregidoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorregidoTrans"].ToString();
                        ddlAnalisisRealizadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalizadoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalizadoPor"].ToString();
                        txtNroExpCamaraDiputados.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumExpDiputados"].ToString();
                        ddlCorreccionEstilo.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionEst"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionEst"].ToString();
                        txtAprobacionPresidida.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAprobPresidida"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeComisiones"].ToString()))
                        {
                            ddlInformeComisiones.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeComisiones"].ToString()) ? "1" : "0";
                            if (ddlInformeComisiones.SelectedValue == "1")
                                txtFechaInfoComisiones.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeComisiones"].ToString();
                        }

                        ddlInformeElaboradoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeElaborado"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeElaborado"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPromulgada"].ToString()))
                        {
                            ddlPromulgada.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPromulgada"].ToString()) ? "1" : "0";
                            if (ddlPromulgada.SelectedValue == "1")
                                txtFechaPromulgada.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPromulgada"].ToString();
                        }
                        txtArchivadoConNro.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumArchivo"].ToString();
                        ddlCorrecionTecnica.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionTec"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionTec"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeAses"].ToString()))
                        {
                            ddlInformeAsesores.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeAses"].ToString()) ? "1" : "0";
                            if (ddlInformeAsesores.SelectedValue == "1")
                                txtFechaInfoAccesores.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeAses"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOpa"].ToString()))
                        {
                            ddlInformeOPA.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOpa"].ToString()) ? "1" : "0";
                            if (ddlInformeOPA.SelectedValue == "1")
                                txtFechaInfoOPA.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeOpa"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeDtrl"].ToString()))
                        {
                            ddlInformeDTRL.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeDtrl"].ToString()) ? "1" : "0";
                            if (ddlInformeDTRL.SelectedValue == "1")
                                txtFechaInfoDTRL.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeDtrl"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOtros"].ToString()))
                        {
                            ddlOtrosInformes.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOtros"].ToString()) ? "1" : "0";
                            if (ddlOtrosInformes.SelectedValue == "1")
                                txtFechaOtrosInformes.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeOtros"].ToString();
                        }
                        txtSecretariosAprobacion.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniSecretarios"].ToString();


                        //Cuarto Tab Flujo, Envio y Destino
                        ddlEnviadoComisionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString();
                        ddlDespachadaHacia.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadaHacia"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadaHacia"].ToString();
                        ddlDespachadoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadoTrans"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachada"].ToString()))
                        {
                            ddlDespachada.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachada"].ToString()) ? "1" : "0";
                            if (ddlDespachada.SelectedValue == "1")
                                txtFechaDespachada.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaDespachada"].ToString();
                        }
                        ddlRevisadoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRevisadoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRevisadoTrans"].ToString();
                        ddlRecibidoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRecibidoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRecibidoTrans"].ToString();
                        ddlDespachadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadopor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadopor"].ToString();
                        try
                        {
                            ddlOficioEnviadoComisionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniOficioEnvComis"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniOficioEnvComis"].ToString();
                        }
                        catch
                        { }

                        txtNroLegislaturaVigente.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumLegislaturaVigente"].ToString();
                        try
                        {
                            ddlRevisadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString();
                        }
                        catch
                        { }
                        txtNroOficioDespacho.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumOficioDesp"].ToString();
                        txtNotaDespacho.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNotasDespacho"].ToString();
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        protected void VerRegistro(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                hfId.Value = fId.ToString();

                PanelMantenimientos.Visible = true;
                panelLista.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = false;
                btnCancelar.Visible = true;

                foreach (GridViewRow row in GridViewIniciativas.Rows)
                {
                    if (fId == GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCodigoSis"].ToString())
                    {
                        //Quinto Tab Estado
                        ddlCondicionActual.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniCacCodigo"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniCacCodigo"].ToString();
                        objIniHis = new clsIniHis();
                        mtBindGridView(gv_Estados, objIniHis.mtGetIniHis(Int32.Parse(hfId.Value)));

                        //Sexto Tab Documentos Anexos
                        objDoc = new clsDoc();
                        mtBindGridView(gv_Documentos, objDoc.mtGetDoc(Int32.Parse(hfId.Value)));

                        //Primer Tab De la Iniciativa
                        txtNoIniciativa.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumero"].ToString();
                        ddlLegislaturaInicio.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmLetCodigoInicio"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmLetCodigoInicio"].ToString();
                        ddlMateria.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniMateria"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniMateria"].ToString();
                        ddlPeriodoConstitucional.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmpcoCodigo"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmpcoCodigo"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPriorizada"].ToString()))
                        {
                            ddlIniciativaPriorizada.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPriorizada"].ToString()) ? "1" : "0";
                            if (ddlIniciativaPriorizada.SelectedValue == "1")
                                txtFechaIniciativaPriorizada.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPriorizada"].ToString();
                        }
                        ddlTipoIniciativa.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniTipCodigo"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniTipCodigo"].ToString();
                        ddlSubTipoIniciativa.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniStpCodigo"].ToString().Trim()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniStpCodigo"].ToString();
                        txtINiIniFecha.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFecha"].ToString();
                        txtDescripcionIniciativa.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDescripcion"].ToString();
                        txtProponentes.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniProponentes"].ToString();
                        txtObservaciones.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniObservaciones"].ToString();


                        //Segundo Tab Datos de Control
                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["ComCfmId"].ToString()))
                        {
                            // listComision
                            String[] fComisiones = GridViewIniciativas.DataKeys[row.RowIndex].Values["ComCfmId"].ToString().Split(';');

                            foreach (ListItem items in listComision.Items)
                            {
                                if (fComisiones.Contains(items.Value))
                                    items.Selected = true;
                            }
                        }

                        txtAnioLegislativo.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmAnoCodigo"].ToString();
                        txtIniIniVecesDev.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniVecesDev"].ToString();
                        ddlPoderDeOrigen.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPoderOrigen"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPoderOrigen"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniConteoLeg"].ToString()))
                        {
                            ddlConteoLegislaturaInicio.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniConteoLeg"].ToString()) ? "1" : "0";
                            if (ddlConteoLegislaturaInicio.SelectedValue == "1")
                                txtFechaConteoLegInicio.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaConteoLeg"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPerimida"].ToString()))
                        {
                            ddlPerimida.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPerimida"].ToString()) ? "1" : "0";
                            if (ddlPerimida.SelectedValue == "1")
                                txtFechaPerimida.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPerimida"].ToString();
                        }

                        ddlDigitadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDigitadoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDigitadoPor"].ToString();
                        ddlCamaraInicial.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmCamCodigo"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["AdmCamCodigo"].ToString();
                        ddlCreadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCreadoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCreadoPor"].ToString();
                        txtIniIniNumOficioOrig.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumOficioOrig"].ToString();
                        txtMiembroComisionEspecial.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniMbrComisionesEsp"].ToString();


                        //TercerTab Control, Revision y Auditoria
                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalisisLeg"].ToString()))
                        {
                            ddlAnalisisLegislativo.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalisisLeg"].ToString()) ? "1" : "0";
                            if (ddlAnalisisLegislativo.SelectedValue == "1")
                                txtFechaAnalisisLeg.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechAnalisisLeg"].ToString();
                        }

                        txtNroPromulgacion.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumProm"].ToString();
                        ddlCorregidoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorregidoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorregidoTrans"].ToString();
                        ddlAnalisisRealizadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalizadoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAnalizadoPor"].ToString();
                        txtNroExpCamaraDiputados.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumExpDiputados"].ToString();
                        ddlCorreccionEstilo.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionEst"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionEst"].ToString();
                        txtAprobacionPresidida.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniAprobPresidida"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeComisiones"].ToString()))
                        {
                            ddlInformeComisiones.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeComisiones"].ToString()) ? "1" : "0";
                            if (ddlInformeComisiones.SelectedValue == "1")
                                txtFechaInfoComisiones.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeComisiones"].ToString();
                        }

                        ddlInformeElaboradoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeElaborado"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeElaborado"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPromulgada"].ToString()))
                        {
                            ddlPromulgada.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniPromulgada"].ToString()) ? "1" : "0";
                            if (ddlPromulgada.SelectedValue == "1")
                                txtFechaPromulgada.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPromulgada"].ToString();
                        }
                        txtArchivadoConNro.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumArchivo"].ToString();
                        ddlCorrecionTecnica.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionTec"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionTec"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeAses"].ToString()))
                        {
                            ddlInformeAsesores.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeAses"].ToString()) ? "1" : "0";
                            if (ddlInformeAsesores.SelectedValue == "1")
                                txtFechaInfoAccesores.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeAses"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOpa"].ToString()))
                        {
                            ddlInformeOPA.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOpa"].ToString()) ? "1" : "0";
                            if (ddlInformeOPA.SelectedValue == "1")
                                txtFechaInfoOPA.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeOpa"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeDtrl"].ToString()))
                        {
                            ddlInformeDTRL.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeDtrl"].ToString()) ? "1" : "0";
                            if (ddlInformeDTRL.SelectedValue == "1")
                                txtFechaInfoDTRL.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeDtrl"].ToString();
                        }

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOtros"].ToString()))
                        {
                            ddlOtrosInformes.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOtros"].ToString()) ? "1" : "0";
                            if (ddlOtrosInformes.SelectedValue == "1")
                                txtFechaOtrosInformes.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeOtros"].ToString();
                        }
                        txtSecretariosAprobacion.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniSecretarios"].ToString();


                        //Cuarto Tab Flujo, Envio y Destino
                        ddlEnviadoComisionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString();
                        ddlDespachadaHacia.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadaHacia"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadaHacia"].ToString();
                        ddlDespachadoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadoTrans"].ToString();

                        if (!String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachada"].ToString()))
                        {
                            ddlDespachada.SelectedValue = Boolean.Parse(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachada"].ToString()) ? "1" : "0";
                            if (ddlDespachada.SelectedValue == "1")
                                txtFechaDespachada.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniFechaDespachada"].ToString();
                        }
                        ddlRevisadoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRevisadoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRevisadoTrans"].ToString();
                        ddlRecibidoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRecibidoTrans"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniRecibidoTrans"].ToString();
                        ddlDespachadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadopor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadopor"].ToString();
                        try
                        {
                            ddlOficioEnviadoComisionPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniOficioEnvComis"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniOficioEnvComis"].ToString();
                        }
                        catch
                        { }

                        txtNroLegislaturaVigente.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumLegislaturaVigente"].ToString();
                        try
                        {
                            ddlRevisadoPor.SelectedValue = String.IsNullOrEmpty(GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString()) ? "0" : GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString();
                        }
                        catch
                        { }
                        txtNroOficioDespacho.Text = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNumOficioDesp"].ToString();
                        txtNotaDespacho.Value = GridViewIniciativas.DataKeys[row.RowIndex].Values["IniIniNotasDespacho"].ToString();
                        break;
                    }
                }

                mtVer(false);


            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        private void mtDesabilitarCampos()
        {
            //Tercera Pestaña
            ddlAnalisisLegislativo.Enabled = false;
            ddlAnalisisLegislativo.CssClass = "select-240";
            txtFechaAnalisisLeg.Enabled = false;
            txtFechaAnalisisLeg.CssClass = "text-240";
            ddlAnalisisRealizadoPor.Enabled = false;
            ddlAnalisisRealizadoPor.CssClass = "select-240";
            txtAprobacionPresidida.Enabled = false;
            txtAprobacionPresidida.CssClass = "text-240";
            ddlPromulgada.Enabled = false;
            ddlPromulgada.CssClass = "select-240";
            txtFechaPromulgada.Enabled = false;
            txtFechaPromulgada.CssClass = "text-240";
            txtNroPromulgacion.Enabled = false;
            txtNroPromulgacion.CssClass = "text-240";
            txtNroExpCamaraDiputados.Enabled = false;
            txtNroExpCamaraDiputados.CssClass = "text-240";
            ddlInformeComisiones.Enabled = false;
            ddlInformeComisiones.CssClass = "select-240";
            txtFechaInfoComisiones.Enabled = false;
            txtFechaInfoComisiones.CssClass = "text-240";
            txtArchivadoConNro.Enabled = false;
            txtArchivadoConNro.CssClass = "text-240";
            ddlCorregidoTranscripcionPor.Enabled = false;
            ddlCorregidoTranscripcionPor.CssClass = "select-240";
            ddlCorreccionEstilo.Enabled = false;
            ddlCorreccionEstilo.CssClass = "select-240";
            ddlInformeElaboradoPor.Enabled = false;
            ddlInformeElaboradoPor.CssClass = "select-240";
            ddlCorrecionTecnica.Enabled = false;
            ddlCorrecionTecnica.CssClass = "select-240";
            ddlInformeAsesores.Enabled = false;
            ddlInformeAsesores.CssClass = "select-240";
            txtFechaInfoAccesores.Enabled = false;
            txtFechaInfoAccesores.CssClass = "text-240";
            ddlInformeOPA.Enabled = false;
            ddlInformeOPA.CssClass = "select-240";
            txtFechaInfoOPA.Enabled = false;
            txtFechaInfoOPA.CssClass = "text-240";
            ddlInformeDTRL.Enabled = false;
            ddlInformeDTRL.CssClass = "select-240";
            txtFechaInfoDTRL.Enabled = false;
            txtFechaInfoDTRL.CssClass = "text-240";
            ddlOtrosInformes.Enabled = false;
            ddlOtrosInformes.CssClass = "select-240";
            txtFechaOtrosInformes.Enabled = false;
            txtFechaOtrosInformes.CssClass = "text-240";
            txtSecretariosAprobacion.Disabled = true;
            //txtSecretariosAprobacion.CssClass = "";

            //Cuarta  Pestaña
            ddlEnviadoComisionPor.Enabled = false;
            ddlEnviadoComisionPor.CssClass = "select-240";
            ddlDespachada.Enabled = false;
            ddlDespachada.CssClass = "select-240";
            txtFechaDespachada.Enabled = false;
            txtFechaDespachada.CssClass = "text-240";
            ddlDespachadoPor.Enabled = false;
            ddlDespachadoPor.CssClass = "select-240";
            ddlRevisadoPor.Enabled = false;
            ddlRevisadoPor.CssClass = "select-240";
            ddlDespachadaHacia.Enabled = false;
            ddlDespachadaHacia.CssClass = "select-240";
            ddlRevisadoTranscripcionPor.Enabled = false;
            ddlRevisadoTranscripcionPor.CssClass = "select-240";
            ddlOficioEnviadoComisionPor.Enabled = false;
            ddlOficioEnviadoComisionPor.CssClass = "select-240";
            txtNroOficioDespacho.Enabled = false;
            txtNroOficioDespacho.CssClass = "text-240";
            ddlDespachadoTranscripcionPor.Enabled = false;
            ddlDespachadoTranscripcionPor.CssClass = "select-240";
            ddlRecibidoTranscripcionPor.Enabled = false;
            ddlRecibidoTranscripcionPor.CssClass = "select-240";
            txtNroLegislaturaVigente.Enabled = false;
            txtNroLegislaturaVigente.CssClass = "text-240";
            txtNotaDespacho.Disabled = true;
            //txtNotaDespacho.CssClass = "";

            //Quinta  Pestaña
            ddlCondicionActual.Enabled = false;
            ddlCondicionActual.CssClass = "select-240";


        }

        private void mtVer(Boolean vDesabilitar)
        {

            //Primer Tab De la Iniciativa
            txtNoIniciativa.Enabled = vDesabilitar;
            txtNoIniciativa.CssClass = "text-240 disabled";
            ddlMateria.Enabled = vDesabilitar;
            ddlMateria.CssClass = "select-240";
            ddlIniciativaPriorizada.Enabled = vDesabilitar;
            ddlIniciativaPriorizada.CssClass = "select-240";
            txtFechaIniciativaPriorizada.Enabled = vDesabilitar;
            txtFechaIniciativaPriorizada.CssClass = "date fecha-240";
            ddlTipoIniciativa.Enabled = vDesabilitar;
            ddlTipoIniciativa.CssClass = "select-240";
            ddlLegislaturaInicio.Enabled = vDesabilitar;
            ddlLegislaturaInicio.CssClass = "select-240";
            ddlPeriodoConstitucional.Enabled = vDesabilitar;
            ddlPeriodoConstitucional.CssClass = "select-240";
            ddlSubTipoIniciativa.Enabled = vDesabilitar;
            ddlSubTipoIniciativa.CssClass = "select-240";
            txtINiIniFecha.Enabled = vDesabilitar;
            txtINiIniFecha.CssClass = "date fecha-240";
            txtDescripcionIniciativa.Disabled = !vDesabilitar;
            txtProponentes.Disabled = !vDesabilitar;
            txtObservaciones.Disabled = !vDesabilitar;

            //2do Tab de la Iniciativa
            listComision.Enabled = vDesabilitar;
            listComision.CssClass = "multipleSts";
            txtAnioLegislativo.Enabled = vDesabilitar;
            txtAnioLegislativo.CssClass = "text-240";
            ddlConteoLegislaturaInicio.Enabled = vDesabilitar;
            ddlConteoLegislaturaInicio.CssClass = "select-240";
            txtFechaConteoLegInicio.Enabled = vDesabilitar;
            txtFechaConteoLegInicio.CssClass = "date fecha-240";
            ddlCamaraInicial.Enabled = vDesabilitar;
            ddlCamaraInicial.CssClass = "select-240";
            txtIniIniVecesDev.Enabled = vDesabilitar;
            txtIniIniVecesDev.CssClass = "text-240";
            ddlPerimida.Enabled = vDesabilitar;
            ddlPerimida.CssClass = "select-240";
            txtFechaPerimida.Enabled = vDesabilitar;
            txtFechaPerimida.CssClass = "date fecha-240";
            ddlCreadoPor.Enabled = vDesabilitar;
            ddlCreadoPor.CssClass = "select-240";
            ddlPoderDeOrigen.Enabled = vDesabilitar;
            ddlPoderDeOrigen.CssClass = "select-240";
            ddlDigitadoPor.Enabled = vDesabilitar;
            ddlDigitadoPor.CssClass = "select-240";
            txtIniIniNumOficioOrig.Enabled = vDesabilitar;
            txtIniIniNumOficioOrig.CssClass = "text-240";
            txtMiembroComisionEspecial.Disabled = !vDesabilitar;

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                mtHabilitar(true);
                hfId.Value = "0";
                txtDescripcionIniciativa.Focus();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                mtVer(true);
                mtHabilitar(false);
                mtDesabilitarCampos();

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                mtLoadData(DateTime.Parse(txtFechaDesde.Text), DateTime.Parse(txtFechaHasta.Text));
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtFechaDesde.Text = String.Empty;
                txtFechaHasta.Text = String.Empty;
                mtClearGridView(GridViewIniciativas);
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }


    }
}