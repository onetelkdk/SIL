using MaxBll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public partial class frm_relDoc : PageBase
    {
        clsDoc Doc;
        clsClaseDocumentos ClaseDocumentos;
        clsTipoDocumentos TipoDocumentos;

        private string defaultDateFrom;
        private DateTime mainFechaDesde;
        private DateTime mainFechaHasta = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            mtvClearMessage();
            if (!Page.IsPostBack)
            {
                try
                {
                    if (mtValidPage("frm_relDoc.aspx", lblModulo))
                    {

                        MenuFlotante.InnerHtml = mtGetMenu();

                        //PanelMantenimientos.Visible = false;
                        btnGuardar.Visible = false;
                        //panelLista.Visible = true;
                        mtLoadCombos();
                        CargarViewState();
                        //mtLoadData();

                    }

                }
                catch (Exception ex)
                {

                    mtvAddMessage(ex.Message, MessageType.error);
                }
            }
            //else
            //{
            //    //defaultDateFrom = GetValueAppSettings("LimitDays_actCom") ?? DateTime.Now.ToString();
            //    //mainFechaDesde = DateTime.Now.AddDays(Convert.ToDouble(GetValueAppSettings("LimitDays") ?? DateTime.Now.ToString()));
            //    mainFechaDesde =  DateTime.Now;
            //    txtFechaDesde.Text = mainFechaDesde.ToString("dd/MM/yyyy");
            //    txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}
        }

        private void CargarViewState()
        {
            try
            {
                ViewState["dt_Documentos"] = null;
                DataTable dt_Documentos = new DataTable();
                dt_Documentos.Columns.Add("AdmDocNumero");
                dt_Documentos.Columns.Add("AdmDocSecuencia");
                dt_Documentos.Columns.Add("AdmDclCodigo");
                dt_Documentos.Columns.Add("AdmDtpCodigo");
                dt_Documentos.Columns.Add("AdmDocTitulo");
                dt_Documentos.Columns.Add("AdmDocRuta");
                dt_Documentos.Columns.Add("AdmDocNombre");
                dt_Documentos.Columns.Add("AdmDocExtension");
                dt_Documentos.Columns.Add("FileByte", System.Type.GetType("System.Byte[]"));
                dt_Documentos.Columns.Add("Nuevo");
                dt_Documentos.Columns.Add("Eliminado");
                ViewState["dt_Documentos"] = dt_Documentos;
            }
            catch { }
        }

        private void mtLoadCombos()
        {
            Doc = new clsDoc();
            ClaseDocumentos = new clsClaseDocumentos();
            TipoDocumentos = new clsTipoDocumentos();

            String opcion = "[Seleccione una Opción]";

            mtBindDropDownList(ddlDocumento, Doc.mtGetCargaDocumentos(), "AdmCatNombre", "AdmCatCodigo", opcion);
            mtBindDropDownList(ddlCDocumento, ClaseDocumentos.mtGetClaseDocumentos(), "AdmDclDescripcion", "AdmDclCodigo", opcion);
            mtBindDropDownList(ddlTDocumento, TipoDocumentos.mtGetTipoDocumentos(), "AdmDtpDescripcion", "AdmDtpCodigo", opcion);

            Doc.mtDispose();
            ClaseDocumentos.mtDispose();
            TipoDocumentos.mtDispose();
        }



        private void mtLoadData(int documento,DateTime fechaDesde, DateTime fechaHasta)
        {
            Doc = new clsDoc();

            mtBindGridView(GridViewDocumentos, Doc.mtGetDocumentos(documento, fechaDesde, fechaHasta));
           
            Doc.mtDispose();
        }

        private void mtLoadData2(int AdmDocMaster)
        {
            Doc = new clsDoc();

            DataTable dt_Documentos = (DataTable)ViewState["dt_Documentos"];
            dt_Documentos.Clear();
            var dt = Doc.mtGetDocumentosDetalle(AdmDocMaster, int.Parse(ddlDocumento.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {
                    DataRow row1 = dt_Documentos.NewRow();
                    row1["AdmDocNumero"] = row["AdmDocNumero"].ToString();
                    row1["AdmDocSecuencia"] = row["AdmDocSecuencia"].ToString();
                    row1["AdmDclCodigo"] = row["AdmDclCodigo"].ToString();
                    row1["AdmDtpCodigo"] = row["AdmDtpCodigo"].ToString();
                    row1["AdmDocTitulo"] = row["AdmDocTitulo"].ToString();
                    row1["AdmDocRuta"] = row["AdmDocRuta"].ToString();
                    row1["AdmDocNombre"] = row["AdmDocNombre"].ToString();
                    row1["AdmDocExtension"] = row["AdmDocExtension"].ToString();
                    //row1["FileByte"] = "";
                    row1["Nuevo"] = false;
                    row1["Eliminado"] = false;
                    dt_Documentos.Rows.Add(row1);

                    ViewState["dt_Documentos"] = dt_Documentos;
                }
            }

            mtBindGridView(GridViewDocumentosCargados, dt);
            if (GridViewDocumentosCargados.Rows.Count > 0)
            {
                GridViewDocumentosCargados.UseAccessibleHeader = true;
                GridViewDocumentosCargados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Doc.mtDispose();
        }

        private void mtLoadDataViewState()
        {
            DataTable dt_Documentos = (DataTable)ViewState["dt_Documentos"];

            EnumerableRowCollection<DataRow> query = from order in dt_Documentos.AsEnumerable()
                                                     where order.Field<string>("Eliminado") == "False"
                                                     select order;

            DataView view = query.AsDataView();

            mtBindGridView(GridViewDocumentosCargados, view);
            if (GridViewDocumentosCargados.Rows.Count > 0)
            {
                GridViewDocumentosCargados.UseAccessibleHeader = true;
                GridViewDocumentosCargados.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void Adjuntar(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String fId = imgButton.CommandArgument;
                ViewState["AdmDocMaster"] = fId.ToString();

                int AdmDocMaster = int.Parse(ViewState["AdmDocMaster"].ToString());
                int AdmCatCodigo = int.Parse(ddlDocumento.SelectedValue);

                gridresultados.Visible = false;
                formularioAdjuntar.Visible = true;
                btnGuardar.Visible = true;
                btnCancel.Visible = true;
                FBuscar.Visible = false;

                ddlCDocumento.SelectedIndex = 0;
                ddlTDocumento.SelectedIndex = 0;
                txtTDocumento.Text = "";
                foreach (GridViewRow row in GridViewDocumentos.Rows)
                {
                    if (fId == GridViewDocumentos.DataKeys[row.RowIndex].Values["Numero"].ToString())
                    {
                        lblDescripcion.Text = GridViewDocumentos.DataKeys[row.RowIndex].Values["Descripcion"].ToString();
                        break;
                    }
                }

                mtLoadData2(AdmDocMaster);

            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            gridresultados.Visible = true;
            formularioAdjuntar.Visible = false;
            btnGuardar.Visible = false;
            btnCancel.Visible = false;
            FBuscar.Visible = true;

            if (GridViewDocumentos.Rows.Count > 0)
            {
                GridViewDocumentos.UseAccessibleHeader = true;
                GridViewDocumentos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int AdmDocMaster = int.Parse(ViewState["AdmDocMaster"].ToString());
                int AdmCatCodigo = int.Parse(ddlDocumento.SelectedValue);

                Doc = new clsDoc();
                var id = Doc.mtPutDocumentos(0, AdmDocMaster, AdmCatCodigo, Session["AdmusrCodigo"].ToString());
                Borrar(id);
                DataTable dt_Documentos = (DataTable)ViewState["dt_Documentos"];
                foreach (DataRow item in dt_Documentos.Rows)
                {
                    if (item["Eliminado"].ToString() == "False")
                    {
                        Doc.mtPutDocumentosDetalle
                            (id,
                             0,
                             int.Parse(item["AdmDclCodigo"].ToString().Trim()),
                             int.Parse(item["AdmDtpCodigo"].ToString().Trim()),
                             item["AdmDocTitulo"].ToString().Trim(),
                             item["AdmDocRuta"].ToString().Trim(),
                             item["AdmDocNombre"].ToString().Trim(),
                             item["AdmDocExtension"].ToString().Trim(),
                             Session["AdmusrCodigo"].ToString().Trim(),
                             0
                             );

                        if (!string.IsNullOrEmpty(item["FileByte"].ToString()))
                        {
                            byte[] b = new byte[0];
                            b = (Byte[])(item["FileByte"]);

                            if (b != null)
                            {
                                using (MemoryStream memStream = new MemoryStream(b))
                                {
                                    using (FileStream fstream = new FileStream(item["AdmDocRuta"].ToString().Trim(), FileMode.Create))
                                    {
                                        memStream.WriteTo(fstream);
                                    }
                                }
                            }
                        }
                    }
                }

                mtvAddMessage("frm_ManMsj.aspx", "MensajeSatisfactorio", MessageType.success);

                gridresultados.Visible = true;
                formularioAdjuntar.Visible = false;
                btnGuardar.Visible = false;
                btnCancel.Visible = false;
                FBuscar.Visible = true;

                if (GridViewDocumentos.Rows.Count > 0)
                {
                    GridViewDocumentos.UseAccessibleHeader = true;
                    GridViewDocumentos.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void CargarArchivo(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTDocumento.Text.Trim()))
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc01", MessageType.warning);
                    return;
                }

                if (ddlCDocumento.SelectedIndex == 0)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc02", MessageType.warning);
                    return;
                }

                if (ddlTDocumento.SelectedIndex == 0)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc03", MessageType.warning);
                    return;
                }

                if (!FileUploadASP.HasFile)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc04", MessageType.warning);
                    return;
                }

                DataTable dt_Documentos = (DataTable)ViewState["dt_Documentos"];
                int existe = 0;
                foreach (DataRow item in dt_Documentos.Rows)
                {
                    if (item["AdmDocTitulo"].ToString() == txtTDocumento.Text.Trim() && item["Eliminado"].ToString() == "False")
                    {
                        existe = 1;
                        break;
                    }
                }

                if (existe == 1)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc05", MessageType.warning);
                    return;
                }

                HttpPostedFile mifichero = FileUploadASP.PostedFile;
                string nombreFile = FileUploadASP.FileName;
                string nombreFile2 = Path.GetFileNameWithoutExtension(FileUploadASP.FileName) + Path.GetExtension(FileUploadASP.FileName);
                string extension = Path.GetExtension(nombreFile);
                string nomb = Path.GetFileNameWithoutExtension(FileUploadASP.FileName);


                double Kb = mifichero.ContentLength / 1024;
                if (Kb > 4096)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc06", MessageType.warning);
                    return;
                }

                var fullPath = String.Concat(@System.Configuration.ConfigurationManager.AppSettings["Documentos"], nombreFile2);
                MemoryStream memStream = new MemoryStream();

                int fileLen;
                fileLen = FileUploadASP.PostedFile.ContentLength;
                byte[] input = new byte[fileLen - 1];
                input = FileUploadASP.FileBytes;

                DataRow row1 = dt_Documentos.NewRow();
                row1["AdmDocNumero"] = 0;
                row1["AdmDocSecuencia"] = 0;
                row1["AdmDclCodigo"] = Int32.Parse(ddlCDocumento.SelectedValue);
                row1["AdmDtpCodigo"] = Int32.Parse(ddlTDocumento.SelectedValue);
                row1["AdmDocTitulo"] = txtTDocumento.Text.Trim();
                row1["AdmDocRuta"] = fullPath;
                row1["AdmDocNombre"] = nombreFile2;
                row1["AdmDocExtension"] = extension.Replace(".","");
                row1["FileByte"] = input;
                row1["Nuevo"] = true;
                row1["Eliminado"] = false;
                dt_Documentos.Rows.Add(row1);



                ViewState["dt_Documentos"] = dt_Documentos;

                txtTDocumento.Text = "";
                ddlCDocumento.SelectedIndex = 0;
                ddlTDocumento.SelectedIndex = 0;
                mtvAddMessage("frm_relDoc.aspx", "relDoc10", MessageType.success);

                mtLoadDataViewState();
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
                if (ddlDocumento.SelectedIndex == 0)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc07", MessageType.warning);
                    return;
                }

                mainFechaDesde = Convert.ToDateTime(Request.Form[txtFechaDesde.UniqueID]);
                mainFechaHasta = Convert.ToDateTime(Request.Form[txtFechaHasta.UniqueID]);

                if (mainFechaDesde > mainFechaHasta)
                {
                    mtvAddMessage("frm_relDoc.aspx", "relDoc08", MessageType.warning);
                    return;
                }

                
                mtLoadData(int.Parse(ddlDocumento.SelectedValue), mainFechaDesde, mainFechaHasta);
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
        private String GetValueAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            mainFechaDesde = Convert.ToDateTime(defaultDateFrom);
            mainFechaHasta = DateTime.Now;
            ddlDocumento.SelectedIndex = 0;

        }

        protected void BorrarRegistroViewState(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String AdmDocTitulo = imgButton.CommandArgument;

                foreach (GridViewRow row in GridViewDocumentosCargados.Rows)
                {
                    if (AdmDocTitulo == GridViewDocumentosCargados.DataKeys[row.RowIndex].Values["AdmDocTitulo"].ToString())
                    {
                        DataTable dt_Documentos = (DataTable)ViewState["dt_Documentos"];
                        foreach (DataRow item in dt_Documentos.Rows)
                        {
                            if (item["AdmDocTitulo"].ToString() == AdmDocTitulo.Trim())
                            {
                                item["Eliminado"] = true;
                            }
                        }
                        ViewState["dt_Documentos"] = dt_Documentos;
                        mtvAddMessage("frm_relDoc.aspx", "relDoc09", MessageType.success);
                        break;
                    }
                }
                mtLoadDataViewState();
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void Borrar(int id)
        {
            try
            {
                Doc = new clsDoc();
                Doc.mtPutDocumentosDetalle
                       (id,
                        0,
                        0,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        1
                        );
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }

        protected void VisualizarDocumento(object sender, EventArgs e)
        {
            try
            {
                Button imgButton = (Button)sender;
                String AdmDocTitulo = imgButton.CommandArgument;

                string ruta = "";
                string extension = "";

                foreach (GridViewRow row in GridViewDocumentosCargados.Rows)
                {
                    if (AdmDocTitulo == GridViewDocumentosCargados.DataKeys[row.RowIndex].Values["AdmDocTitulo"].ToString())
                    {
                        ruta = GridViewDocumentosCargados.DataKeys[row.RowIndex].Values["AdmDocRuta"].ToString();
                        extension = GridViewDocumentosCargados.DataKeys[row.RowIndex].Values["AdmDocExtension"].ToString();
                        break;
                    }
                }
                
                if(!string.IsNullOrEmpty(ruta.Trim()) && !string.IsNullOrEmpty(extension.Trim()))
                {
                  if(extension.ToUpper() == "JPG" || extension.ToUpper() == "PNG")
                  {
                      Session["imagenURL"] = ruta;
                      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('frm_VisorImagenes.aspx','_blank')", true);
                  }
                  if(extension.ToUpper() == "PDF")
                  {
                      Session["PdfURL"] = ruta;
                      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('frm_VisorPDF.aspx','_blank')", true);
                  }
                  if (extension.ToUpper() == "DOC" || extension.ToUpper() == "XLS" || extension.ToUpper() == "PPT")
                  {
                      string patch = ruta;
                      System.IO.FileInfo toDownload = new System.IO.FileInfo(patch);

                      HttpContext.Current.Response.Clear();
                      HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                      HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                      HttpContext.Current.Response.ContentType = "application/octet-stream";
                      HttpContext.Current.Response.WriteFile(patch);
                      HttpContext.Current.Response.Flush();
                      HttpContext.Current.ApplicationInstance.CompleteRequest();
                  }
                    
                }
            }
            catch (Exception ex)
            {
                mtvAddMessage(ex.Message, MessageType.error);
            }
        }
    }
}