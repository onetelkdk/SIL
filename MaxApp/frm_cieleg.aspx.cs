using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Custom References
using clsLegislaturaRegistro = MaxBll.clsLegislaturaRegistro;
using clsProgramas = MaxBll.clsProgramas;

namespace MaxApp
{
    public partial class frm_cieleg : PageBase
    {
        #region Properties

        private List<Control> controlsRequired;
        protected static Dictionary<String, string> OpenAdmLetLast { get; set; }

        private clsLegislaturaRegistro objLegislaturaRegistro;
        protected clsLegislaturaRegistro ObjLegislaturaRegistro 
        { 
            get { return objLegislaturaRegistro ?? new clsLegislaturaRegistro(false); } 
            set { objLegislaturaRegistro = value; } 
        }

        private clsProgramas objProgramas;
        protected clsProgramas ObjProgramas
        {
            get { return objProgramas ?? new clsProgramas(); }
            set { objProgramas = value; }
        }

        private string uriSSRSConfig;
        protected string UriSSRSConfig
        {
            get { return uriSSRSConfig ?? ConfigurationManager.AppSettings["UriSSRS"]; }
            set { uriSSRSConfig = value; } 
        }

        private string renderReportPath;
        protected string RenderReportPath
        {
            get { return renderReportPath ?? ConfigurationManager.AppSettings["RenderReportsPath"]; }
            set { renderReportPath = value; }
        }

        #endregion

        #region Initializers

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            OpenAdmLetLast = ObjLegislaturaRegistro.mtGetmAdmOpenLetLast();
            ObjLegislaturaRegistro.mAdmParDispose();
            mtHabilitar(false);

            if (!Page.IsPostBack)
            {
                if (mtValidPage("frm_cieleg.aspx", lblModulo))
                {
                    MenuFlotante.InnerHtml = mtGetMenu();
                    //    SetDefaultFilter();
                    mtClearGridView(gv_Iniciativas);
                    LoadData("Legislatura");
                }
            }

            if (controlsRequired == null)
            {
                controlsRequired = new List<Control>()
                {
                    txtFechaDesde,
                    txtFechaHasta
                };
            }
        }

        #endregion

        #region Methods [events]

        protected void btn_Click(object sender, EventArgs e)
        {
            string fCodigo = hfId.Value;
            switch ((sender as Button).ID)
            {
                case "btnBuscar":
                    if(mtValidar())
                    {
                        LoadData("gv_Iniciativas");
                        if (Int32.Parse(OpenAdmLetLast["AdmEstCodigo"]) == 176)
                        {
                            mtHabilitar(true);
                        }
                        else
                        {
                            mtHabilitar(false);
                        }
                    }
                    SetDefaultFilter();
                    btnPreCerrar.Enabled = true;
                    break;

                //case "btnLimpiar":
                //    SetDefaultFilter();
                //    LoadData("gv_ActividadComisiones");
                //    break;

                //    case "btnLimpiar":
                //        mtHabilitar(true);
                //        break;

                case "btnPreCerrar":
                    if (mtValidar())
                    {
                        try
                        {
                            #region PreCierre

                            ObjLegislaturaRegistro.mtPutmIniIniCierre
                            (
                                clsLegislaturaRegistro.TipoRegistro.Actualizar,
                                true,
                                (OpenAdmLetLast["AdmLetCodigo"] != null) ? Int16.Parse(OpenAdmLetLast["AdmLetCodigo"]) : (short)-1,
                                Convert.ToDateTime(Request.Form[txtFechaDesde.UniqueID]),
                                Convert.ToDateTime(Request.Form[txtFechaHasta.UniqueID])
                                //Session["AdmusrCodigo"]
                            );

                            #endregion

                            mtvAddMessage("frm_cieleg.aspx", "cieleg01", MessageType.success);
                            btnPreCerrar.Visible = false;
                            //SetDefaultFilter();
                            //LoadData("gv_Iniciativas");
                            hfId.Value = "0";
                        }
                        catch 
                        {
                            throw;
                        }
                    }
                    else
                    {
                        btnPreCerrar.Enabled = true;
                        return;
                    }
                    OpenAdmLetLast = ObjLegislaturaRegistro.mtGetmAdmOpenLetLast();
                    ObjLegislaturaRegistro.mAdmParDispose();
                    mtHabilitar(false, true);
                    SetDefaultFilter();

                    ObjLegislaturaRegistro.mAdmParDispose();
                    break;

                case "btnCerrar":
                    if (mtValidar())
                    {
                        try
                        {
                            ObjLegislaturaRegistro.mtPutmIniIniCierre
                            (
                                clsLegislaturaRegistro.TipoRegistro.Crear,
                                false,
                                (OpenAdmLetLast["AdmLetCodigo"] != null) ? Int16.Parse(OpenAdmLetLast["AdmLetCodigo"]) : (short)-1,
                                Convert.ToDateTime(Request.Form[txtFechaDesde.UniqueID]),
                                Convert.ToDateTime(Request.Form[txtFechaHasta.UniqueID])
                                //Session["AdmusrCodigo"]
                            );

                            mtvAddMessage("frm_cieleg.aspx", "cieleg05", MessageType.success);
                            btnCerrar.Visible = false;
                            mtClearGridView(gv_Iniciativas);
                            hfId.Value = "0";
                        }
                        catch
                        {
                            throw;
                        }    
                    }
                    else
                    {
                        btnPreCerrar.Enabled = true;
                        return;
                    }
                    mtHabilitar(false);
                    SetDefaultFilter();
                    ObjLegislaturaRegistro.mAdmParDispose();
                    break;

                case "btnImprimir": 
                    DownloadReportCierre("Cierre de legislatura");
                    mtvAddMessage("frm_cieleg.aspx", "cieleg06", MessageType.success);
                    break;

                //    case "btnCancel":
                //        mtHabilitar(false);
                //        SetDefaultFilter();
                //        LoadData("gv_ActividadComisiones");
                //        break;

                //    case "btnEdit":
                //        mtHabilitar(true);
                //        EditarRegistro((sender as Button).CommandArgument);
                //        LoadData("gv_Miembros");
                //        goto default;

                //    case "btnVisualizar":
                //        EditarRegistro((sender as Button).CommandArgument);
                //        mtHabilitar(true, true);
                //        goto default;

                //    case "btnAgregar":
                //        if (DtMiembros != null)
                //        {
                //            if (!DtMiembros.Select(condition).Any())
                //            {
                //                DtMiembros.Rows.Add
                //                (
                //                    new object[]
                //                    {
                //                        -1,
                //                        fCodigo,
                //                        DropDownMiembros.SelectedValue,
                //                        DropDownMiembros.SelectedItem.Text,
                //                        DropDownFuncion.SelectedValue,
                //                        DropDownFuncion.SelectedItem.Text
                //                    }
                //                );
                //            }
                //            else
                //            {
                //                mtvAddMessage("El miembro ya ha sido agregado.", MessageType.error);
                //                mtClearGridView(gv_Miembros);
                //                mtBindGridView(gv_Miembros, DtMiembros);
                //                return;
                //            }
                //            mtClearGridView(gv_Miembros);
                //            mtBindGridView(gv_Miembros, DtMiembros);
                //        }
                //        break;

                //    case "btnQuitar":
                //        if (DtMiembros != null)
                //        {
                //            var AdmLegCodigo = (sender as Button).CommandArgument;
                //            DataRow row = DtMiembros.Select(String.Format("ComActNumero = '{0}' AND AdmLegCodigo = '{1}'", fCodigo, AdmLegCodigo)).FirstOrDefault();
                //            if (row != null)
                //                DtMiembros.Rows.Remove(row);
                //        }
                //        mtClearGridView(gv_Miembros);
                //        mtBindGridView(gv_Miembros, DtMiembros);
                //        break;

                //    default:
                //        break;
                //}
            }
            
        }

        #endregion

        #region Methods [Others]

        private void LoadData(params object[] tipoObjeto)
        {
            foreach (string objeto in tipoObjeto)
            {
                switch (objeto)
                {
                    case "gv_Iniciativas":
                        mtHabilitar(false);
                        mtClearGridView(gv_Iniciativas);
                        mtBindGridView(gv_Iniciativas, ObjLegislaturaRegistro.mIniIniGetDatos(Convert.ToDateTime(Request.Form[txtFechaDesde.UniqueID]), Convert.ToDateTime(Request.Form[txtFechaHasta.UniqueID]), false, true, null, null));
                        break;

                    case "Legislatura": 
                        if(OpenAdmLetLast != null) // IS NULL: Cerrada
                        {
                            //176: Abierta. 177: Pre-Cierre. 18: Cerrada.
                            if(Int32.Parse(OpenAdmLetLast["AdmEstCodigo"]) == 176)
                            {
                                mtHabilitar(true);
                            }
                            else
                            {
                                mtHabilitar(false);
                            }
                            btnBuscar.Visible = true;
                        }
                        else
                        {
                            labelLegislatura.Text = "No existen legislaturas abiertas.";
                            btnImprimir.Visible = false;
                            btnCerrar.Visible = false;
                            btnPreCerrar.Visible = false;
                            btnBuscar.Visible = false;
                            break;
                        }

                        labelLegislatura.Text = OpenAdmLetLast["AdmLetDescripcion"];
                        txtFechaDesde.Text = OpenAdmLetLast["AdmLetFechaDesde"];
                        txtFechaHasta.Text = OpenAdmLetLast["AdmLetFechaHasta"];
                        break;

                    //case "Catálogo": mtBindDropDownList(DropDownCatalogo, objFuncion.mtGetmAdmCat(2), "AdmCatNombre", "AdmCatCodigo", "Seleccione catálogo"); // Param (2): ALL
                    //    break;

                    //case "Estado":
                    //    mtBindDropDownList(DropDownEstado, GetEstados(), "Description", "IsActive", "Seleccione estado");
                    //    break;

                    //case "EstadoFilter":
                    //    DropDownSearchEstado.Items.Insert(0, new ListItem("Inactivo", "0"));
                    //    DropDownSearchEstado.Items.Insert(1, new ListItem("Activo", "1"));
                    //    DropDownSearchEstado.Items.Insert(2, new ListItem("Todos", "2"));
                    //    DropDownSearchEstado.SelectedValue = "1";
                    //    DropDownSearchEstado.Enabled = true;
                    //    break;
                }
            }
        }

        private void EditarRegistro(string argument)
        {
            //try
            //{
            //    string fCodigo = argument;
            //    hfId.Value = fCodigo;

            //    foreach (GridViewRow row in gv_Funciones.Rows)
            //    {
            //        if (fCodigo == gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString())
            //        {
            //            txtCodigoSis.Text = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString()) ? "-1" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunCodigo"].ToString();
            //            DropDownCatalogo.SelectedValue = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmCatCodigo"].ToString()) ? "0" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmCatCodigo"].ToString();
            //            DropDownEstado.SelectedValue = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString()) ? "0" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmStsCodigo"].ToString();
            //            txtDescripcionFuncion.Text = String.IsNullOrEmpty(gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunDescripcion"].ToString()) ? "0" : gv_Funciones.DataKeys[row.RowIndex].Values["AdmFunDescripcion"].ToString();
            //            break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    mtvAddMessage(ex.Message, MessageType.error);
            //}
        }

        private void mtHabilitar(bool vEsNewEdit, bool IsPreCierre = false, bool isReadOnly = false)
        {
            if (vEsNewEdit)
            {
                btnPreCerrar.Visible = true;
                btnCerrar.Visible = false;
                btnImprimir.Visible = false;
                //PanelMantenimientos.Visible = true;
                //panelLista.Visible = false;
                //btnNuevo.Visible = false;
                //btnGuardar.Visible = (isReadOnly) ? false : true;
                //btnCancelar.Visible = true;
                //LoadData("CódigoSis");

            }
            else
            {
                btnPreCerrar.Visible = false;
                btnPreCerrar.Enabled = false;
                btnImprimir.Visible = true;
                //PanelMantenimientos.Visible = false;
                //panelLista.Visible = true;
                //btnNuevo.Visible = true;
                //btnGuardar.Visible = false;
                //btnCancelar.Visible = false;
                LimpiarControles();
            }

            if(IsPreCierre)
            {
                btnCerrar.Visible = (Int32.Parse(OpenAdmLetLast["AdmEstCodigo"]) == 177);
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

        private void SetDefaultFilter()
        {
            txtFechaDesde.Text = Request.Form[txtFechaDesde.UniqueID];
            txtFechaHasta.Text = Request.Form[txtFechaHasta.UniqueID];
        }

        private void DownloadReportCierre(string reportName)
        {
            string directory = String.Concat(RenderReportPath,@"\Legislatura");
            string datePatternSave = String.Concat("_", OpenAdmLetLast["AdmLetCodigo"], "-", OpenAdmLetLast["AdmLetDescripcion"]);
            string fileName = String.Concat(reportName, datePatternSave, ".docx");
            string fullPath = Path.Combine(directory, fileName);

            if(!File.Exists(fullPath))
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                var report = new ServerReport();
                var urlReportServer = @UriSSRSConfig;
                report.ReportServerUrl = new Uri(urlReportServer);
                report.ReportPath = ObjProgramas.mtGetAdmParReport(reportName);

                ArrayList reportParam = new ArrayList();
                reportParam = ReportDefaultParam();

                ReportParameter[] param = new ReportParameter[reportParam.Count];
                for (int k = 0; k < reportParam.Count; k++)
                {
                    param[k] = (ReportParameter)reportParam[k];
                }

                report.SetParameters(param);

                var renders = report.ListRenderingExtensions();
                byte[] bytes = report.Render("WORDOPENXML", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                using (FileStream fs = File.Create(fullPath))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            mtClientFileDownload(fullPath);
            ObjProgramas.mtDispose();
        }


        private ArrayList ReportDefaultParam()
        {
            ArrayList arrLstDefaultParam = new ArrayList();
            arrLstDefaultParam.Add(CreateReportParam("BeginDate", OpenAdmLetLast["AdmLetFechaDesde"]));
            arrLstDefaultParam.Add(CreateReportParam("EndDate", OpenAdmLetLast["AdmLetFechaHasta"]));
            return arrLstDefaultParam;
        }

        private ReportParameter CreateReportParam(string paramName, string pramValue)
        {
            var aParam = new ReportParameter(paramName, pramValue);
            return aParam;
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            ICredentials IReportServerCredentials.NetworkCredentials
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
            {
                user = _UserName;
                password = _PassWord;
                authority = _DomainName;
                authCookie = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");
                return true;
                //authCookie = null;
                //user = password = authority = null;
                //return false;
            }
        }

        #endregion

        #region Methods [NonReturns]

        private Boolean mtValidar()
        {
            string fMensaje = default(string);

            if (String.IsNullOrEmpty(Request.Form[txtFechaDesde.UniqueID]) || String.IsNullOrEmpty(Request.Form[txtFechaHasta.UniqueID]))
            {
                fMensaje = "Hubo un error";
                mtvAddMessage("frm_cieleg.aspx", "cieleg03", MessageType.warning);
            }

            //else if (!btnPreCerrar.Visible)
            //{
            //    fMensaje = "Hubo un error";
            //    mtvAddMessage("frm_cieleg.aspx", "cieleg02", MessageType.warning);
            //    btnBuscar.Focus();
            //}

            return String.IsNullOrEmpty(fMensaje);
        }

        #endregion
    }

    public class Empleado
    {
        public string Name { get; set; }
        public string EmployeeID { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
    }
}