using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_actdtsts : PageBase
    {
        MaxBll.clsIniciativas objIniciativas;
        MaxBll.clsEstado objEstados;
        MaxBll.clsIniHis objclsIniHis;
        MaxBll.clsrFunUser iniUsers;
        MaxBll.clsCondicionActual objCondicionActual;
        Dictionary<int, string> objListIDs;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mtvClearMessage();

                if (!Page.IsPostBack)
                {
                    if (mtValidPage("frm_actdtsts.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        mtClearGridView(gv_Iniciativas);

                        mtLoadEstados();

                        iniUsers = new MaxBll.clsrFunUser();
                        objCondicionActual = new MaxBll.clsCondicionActual();

                        String opcion = "[Seleccione una Opción]";

                        //Seccion para lleñar sólo los combos que el usuario usará
                        switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
                        {
                            case 60://Secretaria General Legislativa (SGL)

                                mtBindDropDownList(cboAnalisisRealizadoPor, iniUsers.mtGetFunUser(23, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboRevisadoPor, iniUsers.mtGetFunUser(5, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboDespachadoPor, iniUsers.mtGetFunUser(13, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboOficioEnviadoComisionPor, iniUsers.mtGetFunUser(4, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboEnviadoComisionPor, iniUsers.mtGetFunUser(4, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboCodicionActual, objCondicionActual.mtGetCondicionActual(), "IniCacDescripcion", "IniCacCodigo", opcion);
                                break;
                            case 9://Departamento Técnico de Revisión Legislativa (DETEREL)

                                break;
                            case 7://Coordinación de Comisiones
                                mtBindDropDownList(cboInformeElaboradoPor, iniUsers.mtGetFunUser(1, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboCorreccionEstilo, iniUsers.mtGetFunUser(2, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboCorrecionTecnica, iniUsers.mtGetFunUser(18, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                break;
                            case 23://Auditoria Legislativa
                                mtBindDropDownList(cboDespachadoTranPor, iniUsers.mtGetFunUser(16, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                break;
                            case 63://Transcripción Legislativa
                                mtBindDropDownList(cboRecibidoTranscripcionPor, iniUsers.mtGetFunUser(7, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboTranscritoPor, iniUsers.mtGetFunUser(5, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                mtBindDropDownList(cboCorregidoTranPor, iniUsers.mtGetFunUser(19, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                break;
                            case 20://Documentación Archivo y Correspondencia
                                mtBindDropDownList(cboDespachadaHacia, iniUsers.mtGetFunUser(28, 1), "AdmUsrNombre", "FunUsrCodigo", opcion);
                                break;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtLoadEstados()
        {
            objEstados = new MaxBll.clsEstado();
            mtBindDropDownList(cboEstados, objEstados.mtGetEstado(1), "AdmEstDescripcion", "AdmEstCodigo", "[Seleccione una Opción]");
            objEstados.mtDispose();
        }

        private void mtLoadEstadoSiguiente()
        {

            objEstados = new MaxBll.clsEstado();
            mtBindDropDownList(cboEstadoSiguiente, objEstados.mtGetEstadoSiguientes(Int32.Parse(hfIdEstado.Value)), "AdmEstDescripcion", "AdmEstCodigo", "[Seleccione una Opción]");
            objEstados.mtDispose();
        }

        private void mtLoadData()
        {

            panelLista.Visible = true;
            PanelMantenimientos.Visible = false;

            PanelCampos.Visible = false;
            Panel60.Visible = false;
            Panel09.Visible = false;
            Panel07.Visible = false;
            Panel23.Visible = false;
            Panel63.Visible = false;
            Panel20.Visible = false;

            PanelEstados.Visible = false;
            txtEstadoActual.Text = String.Empty;
            cboEstadoSiguiente.SelectedValue = "0";
            txtFechaInicia.Text = String.Empty;
            txtFechaVence.Text = String.Empty;
            cboPreaviso.SelectedValue = "0";
            txtNotaEstado.Value = String.Empty;
            hfIdEstado.Value = "0";
            hfCondicionActual.Value = "0";
            btnActualizar.Visible = true;
            cboOpcion.Visible = true;

            btnGuardar.Visible = false;
            btnCancelar.Visible = false;

            DateTime fFechaDesde = DateTime.Parse(txtFechaDesde.Text);
            DateTime fFechaHasta = DateTime.Parse(txtFechaHasta.Text);
            objIniciativas = new MaxBll.clsIniciativas();
            mtBindGridView(gv_Iniciativas, objIniciativas.mtGetIniciativas(fFechaDesde, fFechaHasta, Int32.Parse(cboEstados.SelectedValue)));
            objIniciativas.mtDispose();
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkAll = (CheckBox)sender;

                foreach (GridViewRow dr in gv_Iniciativas.Rows)
                {
                    CheckBox chk = (CheckBox)dr.Cells[0].FindControl("chkRow");
                    chk.Checked = chkAll.Checked;
                }

                if (gv_Iniciativas.Rows.Count > 0)
                {
                    gv_Iniciativas.UseAccessibleHeader = true;
                    gv_Iniciativas.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private void mtLimpiar()
        {
            panelLista.Visible = true;
            PanelMantenimientos.Visible = false;
            PanelCampos.Visible = false;
            PanelEstados.Visible = false;

            btnActualizar.Visible = true;
            cboOpcion.Visible = true;

            ListIniciativas.Items.Clear();

            btnGuardar.Visible = false;
            btnCancelar.Visible = false;

            mtClearGridView(gv_Iniciativas);
            cboEstados.SelectedValue = "0";
            cboOpcion.SelectedValue = "0";
            txtFechaDesde.Text = String.Empty;
            txtFechaHasta.Text = String.Empty;
            txtFechaDesde.Focus();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFechaDesde.Text) && !String.IsNullOrEmpty(txtFechaHasta.Text))
                {
                    if (cboEstados.SelectedValue != "0")
                        mtLoadData();
                    else
                        mtvAddMessage("Seleccione un estado para la busqueda", MessageType.warning);
                }
                else
                    mtvAddMessage("Espeficique la Fecha Desde y Fecha Hasta para la busqueda", MessageType.warning);
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
                mtLimpiar();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        public void mtTotalRowsSelected()
        {
            Int32 fReturn = 0;
            Int32 fId = 0;

            foreach (GridViewRow row in gv_Iniciativas.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        fId = Int32.Parse((row.Cells[1].FindControl("fID") as Label).Text);

                        fReturn++;
                    }
                }
            }

            if (fReturn == 1)
            {
                foreach (GridViewRow row in gv_Iniciativas.Rows)
                {
                    if (fId.ToString() == gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCodigoSis"].ToString())
                    {
                        switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
                        {
                            case 60://Secretaria General Legislativa (SGL)
                                txtVecesDev.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniVecesDev"].ToString();
                                txtArchivoConNro.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniNumArchivo"].ToString();
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniPriorizada"].ToString()))
                                {
                                    cboIniciativaPriorizada.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniPriorizada"].ToString()) ? "1" : "0";
                                    if (cboIniciativaPriorizada.SelectedValue == "1")
                                        txtFechaIniciativaPriorizada.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPriorizada"].ToString();
                                }
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniPerimida"].ToString()))
                                {
                                    cboPerimida.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniPerimida"].ToString()) ? "1" : "0";
                                    if (cboPerimida.SelectedValue == "1")
                                        txtFechaPerimida.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPerimida"].ToString();
                                }
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniConteoLeg"].ToString()))
                                {
                                    cboConteoLegislaturaInicio.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniConteoLeg"].ToString()) ? "1" : "0";
                                    if (cboConteoLegislaturaInicio.SelectedValue == "1")
                                        txtFechaConteoLegInicio.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaConteoLeg"].ToString();
                                }
                                txtNroLegislaturaVigente.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniNumLegislaturaVigente"].ToString();
                                txtNroExpCamDiputados.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniNumExpDiputados"].ToString();
                                cboEnviadoComisionPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString();
                                cboAnalisisRealizadoPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniAnalizadoPor"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniAnalizadoPor"].ToString();
                                cboRevisadoPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString();
                                cboDespachadoPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadopor"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadopor"].ToString();
                                txtNroPromulgacion.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniNumProm"].ToString();
                                txtAprobacionPresididaPor.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniAprobPresidida"].ToString();
                                cboOficioEnviadoComisionPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniEnviadoComPor"].ToString();
                                txtMiembroComisionEspecial.Value = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniMbrComisionesEsp"].ToString();
                                txtSecretariosAprobacion.Value = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniSecretarios"].ToString();
                                break;
                            case 9://Departamento Técnico de Revisión Legislativa (DETEREL)
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeAses"].ToString()))
                                {
                                    cboInformeAsesores.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeAses"].ToString()) ? "1" : "0";
                                    if (cboInformeAsesores.SelectedValue == "1")
                                        txtFechaInfoAccesores.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeAses"].ToString();
                                }

                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOpa"].ToString()))
                                {
                                    cboInformeOPA.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOpa"].ToString()) ? "1" : "0";
                                    if (cboInformeOPA.SelectedValue == "1")
                                        txtFechaInfoOPA.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeOpa"].ToString();
                                }

                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeDtrl"].ToString()))
                                {
                                    cboInformeDTRL.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeDtrl"].ToString()) ? "1" : "0";
                                    if (cboInformeDTRL.SelectedValue == "1")
                                        txtFechaInfoDTRL.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeDtrl"].ToString();
                                }

                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOtros"].ToString()))
                                {
                                    cboOtrosInformes.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeOtros"].ToString()) ? "1" : "0";
                                    if (cboOtrosInformes.SelectedValue == "1")
                                        txtFechaOtrosInformes.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeOtros"].ToString();
                                }
                                break;
                            case 7://Coordinación de Comisiones
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeComisiones"].ToString()))
                                {
                                    cboInformeComisiones.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeComisiones"].ToString()) ? "1" : "0";
                                    if (cboInformeComisiones.SelectedValue == "1")
                                        txtFechaInfoComisiones.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaInformeComisiones"].ToString();
                                }
                                cboInformeElaboradoPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeElaborado"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniInformeElaborado"].ToString();
                                cboCorreccionEstilo.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionEst"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionEst"].ToString();
                                cboCorrecionTecnica.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionTec"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCorreccionTec"].ToString();
                                break;
                            case 23://Auditoria Legislativa
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachada"].ToString()))
                                {
                                    cboDespachada.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachada"].ToString()) ? "1" : "0";
                                    if (cboDespachada.SelectedValue == "1")
                                        txtFechaDespachada.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaDespachada"].ToString();
                                }
                                cboDespachadoTranPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadoTrans"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadoTrans"].ToString();
                                break;
                            case 63://Transcripción Legislativa
                                cboRecibidoTranscripcionPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniRecibidoTrans"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniRecibidoTrans"].ToString();
                                cboTranscritoPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniTranscritoPor"].ToString();
                                cboCorregidoTranPor.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCorregidoTrans"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniCorregidoTrans"].ToString();
                                break;
                            case 20://Documentación Archivo y Correspondencia
                                cboDespachadaHacia.SelectedValue = String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadaHacia"].ToString()) ? "0" : gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniDespachadaHacia"].ToString();
                                txtNroOficioDespacho.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniNumOficioDesp"].ToString();
                                if (!String.IsNullOrEmpty(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniPromulgada"].ToString()))
                                {
                                    cboPromulgada.SelectedValue = Boolean.Parse(gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniPromulgada"].ToString()) ? "1" : "0";
                                    if (cboPromulgada.SelectedValue == "1")
                                        txtFechaPromulgada.Text = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniFechaPromulgada"].ToString();
                                }
                                txtNotaDespacho.Value = gv_Iniciativas.DataKeys[row.RowIndex].Values["IniIniNotasDespacho"].ToString();
                                break;
                        }

                        break;
                    }
                }
            }
            else//Limpio los campos
            {
                switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
                {
                    case 60://Secretaria General Legislativa (SGL)
                        txtVecesDev.Text = String.Empty;
                        txtArchivoConNro.Text = String.Empty;
                        cboIniciativaPriorizada.SelectedValue = "0";
                        txtFechaIniciativaPriorizada.Text = String.Empty;
                        cboPerimida.SelectedValue = "0";
                        txtFechaPerimida.Text = String.Empty;
                        cboConteoLegislaturaInicio.SelectedValue = "0";
                        txtFechaConteoLegInicio.Text = String.Empty;
                        txtNroLegislaturaVigente.Text = String.Empty;
                        txtNroExpCamDiputados.Text = String.Empty;
                        cboEnviadoComisionPor.SelectedValue = "0";
                        cboAnalisisRealizadoPor.SelectedValue = "0";
                        cboRevisadoPor.SelectedValue = "0";
                        cboDespachadoPor.SelectedValue = "0";
                        txtNroPromulgacion.Text = String.Empty;
                        txtAprobacionPresididaPor.Text = String.Empty;
                        cboOficioEnviadoComisionPor.SelectedValue = "0";
                        txtMiembroComisionEspecial.Value = String.Empty;
                        txtSecretariosAprobacion.Value = String.Empty;
                        break;
                    case 9://Departamento Técnico de Revisión Legislativa (DETEREL)
                        cboInformeAsesores.SelectedValue = "0";
                        txtFechaInfoAccesores.Text = String.Empty;
                        cboInformeOPA.SelectedValue = "0";
                        txtFechaInfoOPA.Text = String.Empty;
                        cboInformeDTRL.SelectedValue = "0";
                        txtFechaInfoDTRL.Text = String.Empty;
                        cboOtrosInformes.SelectedValue = "0";
                        txtFechaOtrosInformes.Text = String.Empty;
                        break;
                    case 7://Coordinación de Comisiones
                        cboInformeComisiones.SelectedValue = "0";
                        txtFechaInfoComisiones.Text = String.Empty;
                        cboInformeElaboradoPor.SelectedValue = "0";
                        cboCorreccionEstilo.SelectedValue = "0";
                        cboCorrecionTecnica.SelectedValue = "0";
                        break;
                    case 23://Auditoria Legislativa
                        cboDespachada.SelectedValue = "0";
                        txtFechaDespachada.Text = String.Empty;
                        cboDespachadoTranPor.SelectedValue = "0";
                        break;
                    case 63://Transcripción Legislativa
                        cboRecibidoTranscripcionPor.SelectedValue = "0";
                        cboTranscritoPor.SelectedValue = "0";
                        cboCorregidoTranPor.SelectedValue = "0";
                        break;
                    case 20://Documentación Archivo y Correspondencia
                        cboDespachadaHacia.SelectedValue = "0";
                        txtNroOficioDespacho.Text = String.Empty;
                        cboPromulgada.SelectedValue = "0";
                        txtFechaPromulgada.Text = String.Empty;
                        txtNotaDespacho.Value = String.Empty;
                        break;
                }
            }
        }

        private Boolean mtSelectedRecord()
        {
            Boolean fReturn = false;

            objListIDs = new Dictionary<int, string>();

            foreach (GridViewRow row in gv_Iniciativas.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        Int32 fId = Int32.Parse((row.Cells[1].FindControl("fID") as Label).Text);
                        hfIdEstado.Value = (row.Cells[2].FindControl("fCodigoEstado") as Label).Text;
                        string fIniNro = (row.Cells[3].FindControl("fIniIniNumero") as Label).Text;
                        txtEstadoActual.Text = (row.Cells[4].FindControl("fDescEstado") as Label).Text;
                        hfCondicionActual.Value = (row.Cells[5].FindControl("fCondActual") as Label).Text;
                        objListIDs.Add(fId, fIniNro);

                        fReturn = true;
                    }
                }
            }

            Session["objListIDs"] = objListIDs;

            return fReturn;
        }

        private void mtLimpiarCamposPorDepartamentos()
        {
            switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
            {
                case 60://Secretaria General Legislativa (SGL)
                    txtVecesDev.Text = String.Empty;
                    txtArchivoConNro.Text = String.Empty;
                    cboIniciativaPriorizada.SelectedValue = "0";
                    txtFechaIniciativaPriorizada.Text = String.Empty;
                    cboPerimida.SelectedValue = "0";
                    txtFechaPerimida.Text = String.Empty;
                    cboConteoLegislaturaInicio.SelectedValue = "0";
                    txtFechaConteoLegInicio.Text = String.Empty;
                    txtNroLegislaturaVigente.Text = String.Empty;
                    txtNroExpCamDiputados.Text = String.Empty;
                    cboEnviadoComisionPor.SelectedValue = "0";
                    cboAnalisisRealizadoPor.SelectedValue = "0";
                    cboRevisadoPor.SelectedValue = "0";
                    cboDespachadoPor.SelectedValue = "0";
                    txtNroPromulgacion.Text = String.Empty;
                    txtAprobacionPresididaPor.Text = String.Empty;
                    cboOficioEnviadoComisionPor.SelectedValue = "0";
                    txtMiembroComisionEspecial.Value = String.Empty;
                    txtSecretariosAprobacion.Value = String.Empty;
                    break;
                case 9://Departamento Técnico de Revisión Legislativa (DETEREL)
                    cboInformeAsesores.SelectedValue = "0";
                    txtFechaInfoAccesores.Text = String.Empty;
                    cboInformeOPA.SelectedValue = "0";
                    txtFechaInfoOPA.Text = String.Empty;
                    cboInformeDTRL.SelectedValue = "0";
                    txtFechaInfoDTRL.Text = String.Empty;
                    cboOtrosInformes.SelectedValue = "0";
                    txtFechaOtrosInformes.Text = String.Empty;
                    break;
                case 7://Coordinación de Comisiones
                    cboInformeComisiones.SelectedValue = "0";
                    txtFechaInfoComisiones.Text = String.Empty;
                    cboInformeElaboradoPor.SelectedValue = "0";
                    cboCorreccionEstilo.SelectedValue = "0";
                    cboCorrecionTecnica.SelectedValue = "0";
                    break;
                case 23://Auditoria Legislativa
                    cboDespachada.SelectedValue = "0";
                    txtFechaDespachada.Text = String.Empty;
                    cboDespachadoTranPor.SelectedValue = "0";
                    break;
                case 63://Transcripción Legislativa
                    cboRecibidoTranscripcionPor.SelectedValue = "0";
                    cboTranscritoPor.SelectedValue = "0";
                    cboCorregidoTranPor.SelectedValue = "0";
                    break;
                case 20://Documentación Archivo y Correspondencia
                    cboDespachadaHacia.SelectedValue = "0";
                    txtNroOficioDespacho.Text = String.Empty;
                    cboPromulgada.SelectedValue = "0";
                    txtFechaPromulgada.Text = String.Empty;
                    txtNotaDespacho.Value = String.Empty;
                    break;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboOpcion.SelectedValue != "0")
                {
                    if (mtSelectedRecord() == false)
                    {
                        mtvAddMessage("No hay iniciativas seleccionadas para " + cboOpcion.SelectedItem.Text, MessageType.warning);
                        return;
                    }

                    cboOpcion.Visible = false;
                    btnActualizar.Visible = false;

                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;

                    panelLista.Visible = false;
                    PanelMantenimientos.Visible = true;

                    if (cboOpcion.SelectedValue == "1")//Campos
                    {
                        PanelCampos.Visible = true;
                        PanelEstados.Visible = false;

                        Panel60.Visible = false;
                        Panel09.Visible = false;
                        Panel07.Visible = false;
                        Panel23.Visible = false;
                        Panel63.Visible = false;
                        Panel20.Visible = false;

                        //Si hay solo un registro seleccionado poblo los campos correspondientes
                        mtTotalRowsSelected();

                        switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
                        {
                            case 60://Secretaria General Legislativa (SGL)
                                cboCodicionActual.SelectedValue = hfCondicionActual.Value;
                                cboCodicionActual.Enabled = false;
                                cboCodicionActual.CssClass = "select-240";
                                Panel60.Visible = true;
                                break;
                            case 9://Departamento Técnico de Revisión Legislativa (DETEREL)
                                Panel09.Visible = true;
                                break;
                            case 7://Coordinación de Comisiones
                                Panel07.Visible = true;
                                break;
                            case 23://Auditoria Legislativa
                                Panel23.Visible = true;
                                break;
                            case 63://Transcripción Legislativa
                                Panel63.Visible = true;
                                break;
                            case 20://Documentación Archivo y Correspondencia
                                Panel20.Visible = true;
                                break;
                        }

                    }
                    else//2=Estados
                    {
                        PanelEstados.Visible = true;
                        PanelCampos.Visible = false;
                        ListIniciativas.Items.Clear();
                        objListIDs = (Dictionary<int, string>)Session["objListIDs"];
                        foreach (var item in objListIDs)
                        {
                            ListIniciativas.Items.Add(new ListItem(item.Value, item.Key.ToString()));
                        };

                        mtLoadEstadoSiguiente();
                    }
                }
                else
                {
                    mtvAddMessage("Seleccione la Opción a trabajar [Estado,Campos]", MessageType.warning);
                }

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        private Boolean mtValidarCamposEstados()
        {
            String fMensaje = String.Empty;


            if (cboEstadoSiguiente.SelectedValue == "0")
            {
                fMensaje = "Seleccione el Estado";
                mtvAddMessage(fMensaje, MessageType.warning);
                cboEstadoSiguiente.Focus();
            }
            else if (String.IsNullOrEmpty(txtFechaInicia.Text))
            {
                fMensaje = "Inserte la Fecha Iniciar";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtFechaInicia.Focus();
            }
            else if (String.IsNullOrEmpty(txtFechaVence.Text))
            {
                fMensaje = "Inserte la Fecha de Vencimiento";
                mtvAddMessage(fMensaje, MessageType.warning);
                txtFechaVence.Focus();
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        private Boolean mtValidarCampos()
        {
            String fMensaje = String.Empty;

            switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
            {
                case 60://Secretaria General Legislativa (SGL)
                    if (cboIniciativaPriorizada.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaIniciativaPriorizada.Text))
                    {
                        fMensaje = "Si la Iniciativa es Priorizada se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaIniciativaPriorizada.Focus();
                    }
                    else if (cboPerimida.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaPerimida.Text))
                    {
                        fMensaje = "Si está Perimida se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaPerimida.Focus();
                    }
                    else if (cboConteoLegislaturaInicio.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaConteoLegInicio.Text))
                    {
                        fMensaje = "Tiene que especificar la Fecha del Conteo de la Legislatura Inicio";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaConteoLegInicio.Focus();
                    }
                    break;
                case 9://Departamento Técnico de Revisión Legislativa (DETEREL)
                    if (cboInformeAsesores.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaInfoAccesores.Text))
                    {
                        fMensaje = "Si hay Informe de Asesores se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaInfoAccesores.Focus();
                    }
                    else if (cboInformeOPA.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaInfoOPA.Text))
                    {
                        fMensaje = "Si hay Informe OPA se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaInfoOPA.Focus();
                    }
                    else if (cboInformeDTRL.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaInfoDTRL.Text))
                    {
                        fMensaje = "Si hay Informe DTRL se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaInfoDTRL.Focus();
                    }
                    else if (cboOtrosInformes.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaOtrosInformes.Text))
                    {
                        fMensaje = "Si hay Otros Informes se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaOtrosInformes.Focus();
                    }
                    break;
                case 7://Coordinación de Comisiones
                    if (cboInformeComisiones.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaInfoComisiones.Text))
                    {
                        fMensaje = "Si hay Informe comisiones se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaInfoComisiones.Focus();
                    }
                    break;
                case 23://Auditoria Legislativa
                    if (cboDespachada.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaDespachada.Text))
                    {
                        fMensaje = "Si es Despachada se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaDespachada.Focus();
                    }
                    break;
                case 63://Transcripción Legislativa

                    break;
                case 20://Documentación Archivo y Correspondencia
                    if (cboPromulgada.SelectedValue == "1" && String.IsNullOrEmpty(txtFechaPromulgada.Text))
                    {
                        fMensaje = "Si es Promulgada se tiene que especificar la Fecha";
                        mtvAddMessage(fMensaje, MessageType.warning);
                        txtFechaPromulgada.Focus();
                    }
                    break;
            }

            return String.IsNullOrEmpty(fMensaje);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboOpcion.SelectedValue == "1")//Guardar Campos
                {
                    if (!mtValidarCampos())
                        return;

                    objIniciativas = new MaxBll.clsIniciativas();
                    objListIDs = (Dictionary<int, string>)Session["objListIDs"];


                    switch (Int32.Parse(Session["AdmDepCodigo"].ToString()))
                    {
                        case 60://Secretaria General Legislativa (SGL)
                            foreach (var item in objListIDs)
                            {

                                Boolean fIniciativaPriorizada = false;
                                Boolean fvIniIniPerimida = false;
                                Boolean fConteoLegislaturaIniciado = false;

                                DateTime fIniIniFechaPriorizada = DateTime.Now;
                                DateTime fIniIniFechaPerimida = DateTime.Now;
                                DateTime fIniIniFechaConteoLeg = DateTime.Now;

                                if (cboIniciativaPriorizada.SelectedValue == "1")
                                {
                                    fIniciativaPriorizada = true;
                                    fIniIniFechaPriorizada = DateTime.Parse(txtFechaIniciativaPriorizada.Text);
                                }

                                if (cboPerimida.SelectedValue == "1")
                                {
                                    fvIniIniPerimida = true;
                                    fIniIniFechaPerimida = DateTime.Parse(txtFechaPerimida.Text);
                                }

                                if (cboConteoLegislaturaInicio.SelectedValue == "1")
                                {
                                    fConteoLegislaturaIniciado = true;
                                    fIniIniFechaConteoLeg = DateTime.Parse(txtFechaConteoLegInicio.Text);
                                }

                                objIniciativas.mtSetIniciativaSGL(
                                    item.Key
                                    , String.IsNullOrEmpty(txtVecesDev.Text) ? 0 : Int32.Parse(txtVecesDev.Text)
                                    , txtArchivoConNro.Text
                                    , fIniciativaPriorizada
                                    , fIniIniFechaPriorizada
                                    , fvIniIniPerimida
                                    , fIniIniFechaPerimida
                                    , fConteoLegislaturaIniciado
                                    , fIniIniFechaConteoLeg
                                    , String.IsNullOrEmpty(txtNroLegislaturaVigente.Text) ? 0 : Int32.Parse(txtNroLegislaturaVigente.Text)
                                    , txtNroExpCamDiputados.Text
                                    , Int32.Parse(cboEnviadoComisionPor.SelectedValue)
                                    , Int32.Parse(cboAnalisisRealizadoPor.SelectedValue)
                                    , Int32.Parse(cboRevisadoPor.SelectedValue)
                                    , Int32.Parse(cboDespachadoPor.SelectedValue)
                                    , txtNroPromulgacion.Text
                                    , txtAprobacionPresididaPor.Text
                                    , Int32.Parse(cboOficioEnviadoComisionPor.SelectedValue)
                                    , txtMiembroComisionEspecial.Value
                                    , txtSecretariosAprobacion.Value
                                    );
                            }
                            break;
                        case 9://Departamento Técnico de Revisión Legislativa (DETEREL)
                            foreach (var item in objListIDs)
                            {
                                Boolean fIniIniInformeAses = false;
                                Boolean fIniIniInformeOpa = false;
                                Boolean fIniIniInformeDtrl = false;
                                Boolean fIniIniInformeOtros = false;

                                DateTime fIniIniFechaInformeAses = DateTime.Now;
                                DateTime fIniIniFechaInformeOpa = DateTime.Now;
                                DateTime fIniIniFechaInformeDtrl = DateTime.Now;
                                DateTime fIniIniFechaInformeOtros = DateTime.Now;


                                if (cboInformeAsesores.SelectedValue == "1")
                                {
                                    fIniIniInformeAses = true;
                                    fIniIniFechaInformeAses = DateTime.Parse(txtFechaInfoAccesores.Text);
                                }

                                if (cboInformeOPA.SelectedValue == "1")
                                {
                                    fIniIniInformeOpa = true;
                                    fIniIniFechaInformeOpa = DateTime.Parse(txtFechaInfoOPA.Text);
                                }

                                if (cboInformeDTRL.SelectedValue == "1")
                                {
                                    fIniIniInformeDtrl = true;
                                    fIniIniFechaInformeDtrl = DateTime.Parse(txtFechaInfoDTRL.Text);
                                }

                                if (cboOtrosInformes.SelectedValue == "1")
                                {
                                    fIniIniInformeOtros = true;
                                    fIniIniFechaInformeOtros = DateTime.Parse(txtFechaOtrosInformes.Text);
                                }

                                objIniciativas.mtSetIniciativaDTRL(
                                    item.Key
                                    , fIniIniInformeAses
                                    , fIniIniFechaInformeAses
                                    , fIniIniInformeOpa
                                    , fIniIniFechaInformeOpa
                                    , fIniIniInformeDtrl
                                    , fIniIniFechaInformeDtrl
                                    , fIniIniInformeOtros
                                    , fIniIniFechaInformeOtros
                                    );
                            }
                            break;
                        case 7://Coordinación de Comisiones
                            foreach (var item in objListIDs)//Verificar Error
                            {
                                Boolean fIniIniInformeComisiones = false;
                                DateTime fIniIniFechaInformeComisiones = DateTime.Now;

                                if (cboInformeComisiones.SelectedValue == "1")
                                {
                                    fIniIniInformeComisiones = true;
                                    fIniIniFechaInformeComisiones = DateTime.Parse(txtFechaInfoComisiones.Text);
                                }

                                objIniciativas.mtSetIniciativaCORDCOM
                                    (
                                    item.Key
                                    , fIniIniInformeComisiones
                                    , fIniIniFechaInformeComisiones
                                    , Int32.Parse(cboInformeElaboradoPor.SelectedValue)
                                    , Int32.Parse(cboCorreccionEstilo.SelectedValue)
                                    , Int32.Parse(cboCorrecionTecnica.SelectedValue)
                                    );
                            }
                            break;
                        case 23://Auditoria Legislativa
                            foreach (var item in objListIDs)
                            {

                                Boolean fIniIniDespachada = false;
                                DateTime fIniIniFechaDespachada = DateTime.Now;


                                if (cboDespachada.SelectedValue == "1")
                                {
                                    fIniIniDespachada = true;
                                    fIniIniFechaDespachada = DateTime.Parse(txtFechaDespachada.Text);
                                }

                                objIniciativas.mtSetIniciativaAUDLEG
                                    (
                                    item.Key
                                    ,fIniIniDespachada
                                    ,fIniIniFechaDespachada
                                    ,Int32.Parse(cboDespachadoTranPor.SelectedValue)
                                    );
                            }
                            break;
                        case 63://Transcripción Legislativa
                            foreach (var item in objListIDs)
                            {
                                objIniciativas.mtSetIniciativaTRANLEG(
                                    item.Key
                                    , Int32.Parse(cboRecibidoTranscripcionPor.SelectedValue)
                                    , Int32.Parse(cboTranscritoPor.SelectedValue)
                                    , Int32.Parse(cboCorregidoTranPor.SelectedValue)
                                    );
                            }
                            break;
                        case 20://Documentación Archivo y Correspondencia
                            foreach (var item in objListIDs)
                            {
                                Boolean fIniIniPromulgada = false;
                                DateTime fIniIniFechaPromulgada = DateTime.Now;


                                if (cboPromulgada.SelectedValue == "1")
                                {
                                    fIniIniPromulgada = true;
                                    fIniIniFechaPromulgada = DateTime.Parse(txtFechaPromulgada.Text);
                                }

                                objIniciativas.mtSetIniciativaDOCUM(
                                    item.Key
                                    , Int32.Parse(cboDespachadaHacia.SelectedValue)
                                    , txtNroOficioDespacho.Text
                                    , fIniIniPromulgada
                                    , fIniIniFechaPromulgada
                                    , txtNotaDespacho.Value
                                    );
                            }
                            break;
                    }

                    mtLoadData();
                    mtLimpiarCamposPorDepartamentos();
                    mtvAddMessage("Campos de las iniciativas modificados satisfactoriamente", MessageType.success);
                }
                else //Guardar Estados
                {
                    if (!mtValidarCamposEstados())
                        return;

                    objIniciativas = new MaxBll.clsIniciativas();
                    objclsIniHis = new MaxBll.clsIniHis();
                    objListIDs = (Dictionary<int, string>)Session["objListIDs"];

                    Boolean fPreAviso = false;

                    if (cboPreaviso.SelectedValue == "1")
                        fPreAviso = true;

                    foreach (var item in objListIDs)
                    {
                        objIniciativas.mtSetIniciativas(item.Key, Int32.Parse(cboEstadoSiguiente.SelectedValue));

                        objclsIniHis.mtPutIniHis(0, item.Key, Int32.Parse(cboEstadoSiguiente.SelectedValue), DateTime.Parse(txtFechaInicia.Text), DateTime.Parse(txtFechaVence.Text), fPreAviso, txtNotaEstado.Value);
                    }

                    mtLoadData();

                    mtvAddMessage("Estados de las iniciativas modificados satisfactoriamente", MessageType.success);
                }
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
                mtLimpiarCamposPorDepartamentos();

                panelLista.Visible = true;
                PanelMantenimientos.Visible = false;
                PanelCampos.Visible = false;
                PanelEstados.Visible = false;

                cboOpcion.Visible = true;
                btnActualizar.Visible = true;

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;

                if (gv_Iniciativas.Rows.Count > 0)
                {
                    gv_Iniciativas.UseAccessibleHeader = true;
                    gv_Iniciativas.HeaderRow.TableSection = TableRowSection.TableHeader;
                }


            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}