using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaxApp
{
    public enum MessageType
    {
        error,
        information,
        success,
        validation,
        warning
    }

    public partial class PageBase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void mtvClearMessage()
        {
            UpdatePanel fUpdate = (UpdatePanel)this.Master.FindControl("UpMensaje");
            Panel fPanel = (Panel)this.Master.FindControl("PanelMensajes");
            Label fLabel = (Label)this.Master.FindControl("lblStatus");
            Image fImagen = (Image)this.Master.FindControl("imgAlert");

            String fCss = String.Empty;
            fImagen.ImageUrl = "";
            fPanel.CssClass = fCss;
            fPanel.Visible = false;
            fLabel.Text = String.Empty;
            //fUpdate.Update();
            fPanel.Dispose();
        }

        public void mtvAddMessage(String vFormName, String vCode, MessageType Type)
        { 

            DataSet ds = new DataSet();
            ds.ReadXml(System.Configuration.ConfigurationManager.AppSettings["xmlMensajes"].ToString());
            DataTable dt = ds.Tables[0];

            var Query = from t in dt.AsEnumerable()
                        where t.Field<String>("Programa") == vFormName && t.Field<String>("Codigo") == vCode
                        select new
                        {
                            Mensaje = t.Field<String>("Descripcion")
                        };

            if (Query != null && Query.Count() > 0)
            {
                mtvAddMessage(Query.FirstOrDefault().Mensaje, Type);
            }
        }

        public void mtvAddMessage(String Description, MessageType Type)
        {
            UpdatePanel fUpdate = (UpdatePanel)this.Master.FindControl("UpMensaje");
            Panel fPanel = (Panel)this.Master.FindControl("PanelMensajes");
            Label fLabel = (Label)this.Master.FindControl("lblStatus");
            Image fImagen = (Image)this.Master.FindControl("imgAlert");
            String fCss = "";

            switch (Type)
            {
                case MessageType.error:
                    fCss = "alert alert-danger fade in msgLabel";
                    fImagen.ImageUrl = "/images/ic_warning.png";
                    break;
                case MessageType.information:
                    fCss = "alert alert-info fade in msgLabel";
                    fImagen.ImageUrl = "";
                    break;
                case MessageType.success:
                    fCss = "alert alert-success fade in msgLabel";
                    fImagen.ImageUrl = "";
                    break;
                case MessageType.warning:
                    fCss = "alert alert-warning fade in msgLabel";

                    break;
            }

            fPanel.CssClass = fCss;
            fPanel.Visible = true;
            fLabel.Text = Description;
            fUpdate.Update();
        }

        public void mtBindDropDownList(DropDownList pDropDownList, Object vSource, String vDataTextField, String vDataValueField, String vSelectDescripcion)
        {
            pDropDownList.DataTextField = vDataTextField;
            pDropDownList.DataValueField = vDataValueField;
            pDropDownList.DataSource = vSource;
            pDropDownList.DataBind();

            pDropDownList.Items.Insert(0, new ListItem(vSelectDescripcion, "0"));
            pDropDownList.SelectedValue = "0";
            pDropDownList.Enabled = true;

        }

        public void mtClearDropDownList(DropDownList pDropDownList, String vSelectDescripcion)
        {
            pDropDownList.DataSource = new System.Data.DataTable();
            pDropDownList.DataBind();

            pDropDownList.Items.Insert(0, new ListItem(vSelectDescripcion, "0"));
            pDropDownList.SelectedValue = "0";
            pDropDownList.Enabled = true;
        }

        public void mtBindGridView(GridView pGridView, Object vSource)
        {
            pGridView.DataSource = vSource;
            pGridView.DataBind();
            if (pGridView.Rows.Count > 0)
            {
                pGridView.UseAccessibleHeader = true;
                pGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        public void mtBindListBox(ListBox pListBox, Object vSource, String vDataTextField, String vDataValueField, String vSelectDescripcion)
        {
            pListBox.DataTextField = vDataTextField;
            pListBox.DataValueField = vDataValueField;
            pListBox.DataSource = vSource;
            pListBox.DataBind();

        }

        public void mtClearGridView(GridView pGridView)
        {
            pGridView.DataSource = new System.Data.DataTable();
            pGridView.DataBind();
        }

        public void mtClientFileDownload(string filePath)
        {
            try
            {
                FileInfo toDownload = new FileInfo(filePath);

                if (File.Exists(filePath))
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(filePath);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    mtvAddMessage("PageBase.aspx", "cieleg04", MessageType.information);
                }
            }
            catch (IOException)
            {
                throw;
            }
        }

        public String mtGetMenu()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                DataTable dtDataOpciones = (DataTable)Session["dtDataOpciones"];

                if (dtDataOpciones.Rows.Count > 0)
                {
                    sb.Append("<ul id=\"accordion\" class=\"accordion\">");

                    //Buscar los tipos para recorrerlos
                    //Cargando los tipos
                    var QueryTipo = from tipos in dtDataOpciones.AsEnumerable()
                                    where tipos.Field<String>("AdmmodCodigo") == Session["AdmmodCodigo"].ToString()
                                    orderby tipos.Field<Int32>("PrgtipOrden")
                                    group new
                                    {
                                        Codigo = tipos.Field<String>("PrgtipCodigo"),
                                        Nombre = tipos.Field<String>("PrgtipNombre"),
                                        Orden = tipos.Field<Int32>("PrgtipOrden"),
                                        Icono = tipos.Field<String>("PrgtipIcono")
                                    } by tipos.Field<String>("PrgtipCodigo") into newTipos
                                    select newTipos;


                    foreach (var Query in QueryTipo)
                    {

                        var item = from itemObject in Query
                                   orderby itemObject.Orden
                                   select itemObject;



                        //Busco las pantallas correspondientes al Tipo
                        var QueryProgramas = from programas in dtDataOpciones.AsEnumerable()
                                             where programas.Field<String>("PrgtipCodigo") == item.FirstOrDefault().Codigo
                                             && programas.Field<String>("AdmmodCodigo") == Session["AdmmodCodigo"].ToString()
                                             orderby programas.Field<Int16>("PrgprgOrden")
                                             select new
                                             {
                                                 Nombre = programas.Field<String>("PrgPrgNombre"),
                                                 Descripcion = programas.Field<String>("PrgprgDescripcion"),
                                                 ColorFondo = programas.Field<String>("PrgPrgColorFondo"),
                                                 Icono = programas.Field<String>("PrgprgIcono")
                                             };

                        if (QueryProgramas != null && QueryProgramas.Count() > 0)
                        {

                            sb.Append("<li><div class=\"link\">");
                            sb.Append(String.Format("<img src=\"images\\{0}\">{1}<i class=\"fa fa-chevron-down\"></i></div>", item.FirstOrDefault().Icono,item.FirstOrDefault().Nombre));
                            sb.Append("<ul class=\"submenu\">");

                            //Recorro las pantallas
                            foreach (var QueryP in QueryProgramas)
                            {
                                sb.Append(String.Format("<li><a href=\"{0}\">{1}</a></li>", QueryP.Nombre, QueryP.Descripcion));
                            }
                            
                            sb.Append("</ul>");

                        }
                    }


                    sb.Append("</ul>");
                    //Final del Inicio Acorrdion y General

                }
            }
            catch
            {
                throw;

            }

            return sb.ToString();
        }

        public Boolean mtValidPage(String vPage, Label vTituloModulo, Label vNombrePagina)
        {
            Boolean fReturn = false;

            if (Session["AdmusrCodigo"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (Session["dtDataOpciones"] == null)
            {
                Response.Redirect("Modulos.aspx");
            }
            else//Verifico Si la pagina está en el pool
            {
                DataTable dtDataOpciones = (DataTable)Session["dtDataOpciones"];
                if (dtDataOpciones.Rows.Count > 0)
                {
                    var QueryProgramas = from programas in dtDataOpciones.AsEnumerable()
                                         where programas.Field<String>("PrgPrgNombre") == vPage
                                         select new
                                         {
                                             Nombre = programas.Field<String>("AdmmodNombre"),
                                             Modulo = programas.Field<String>("AdmmodCodigo"),
                                             TituloPrograma= programas.Field<String>("PrgprgDescripcion")
                                         };

                    if (QueryProgramas.Count() > 0)
                    {
                        vTituloModulo.Text = QueryProgramas.FirstOrDefault().Nombre;
                        vNombrePagina.Text = QueryProgramas.FirstOrDefault().TituloPrograma;
                        Session["AdmmodCodigo"] = QueryProgramas.FirstOrDefault().Modulo;

                        fReturn = true;
                    }
                    else
                        Response.Redirect("accesodenegado.aspx");
                }
                else
                    Response.Redirect("Modulos.aspx");
            }

            return fReturn;
        }

        public Boolean mtValidPage(String vPage,Label vTituloModulo)
        {
            Boolean fReturn = false;

            if (Session["AdmusrCodigo"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (Session["dtDataOpciones"] == null)
            {
                Response.Redirect("Modulos.aspx");
            }
            else//Verifico Si la pagina está en el pool
            {
                DataTable dtDataOpciones = (DataTable)Session["dtDataOpciones"];
                if (dtDataOpciones.Rows.Count > 0)
                {
                    var QueryProgramas = from programas in dtDataOpciones.AsEnumerable()
                                         where programas.Field<String>("PrgPrgNombre") == vPage
                                         select new
                                         {
                                             Nombre = programas.Field<String>("AdmmodNombre"),
                                             Modulo = programas.Field<String>("AdmmodCodigo"),
                                         };

                    if (QueryProgramas.Count() > 0)
                    {
                        vTituloModulo.Text = QueryProgramas.FirstOrDefault().Nombre;
                           Session["AdmmodCodigo"] = QueryProgramas.FirstOrDefault().Modulo;

                        fReturn = true;
                    }
                    else
                        Response.Redirect("accesodenegado.aspx");
                }
                else
                    Response.Redirect("Modulos.aspx");
            }

            return fReturn;
        }
    }
}